'=====================================================================
'
'  File:    SqlObjectBrowser.vb
'  Summary: Object browser window to allow object selection
'  Date:    08-20-2004
'
'---------------------------------------------------------------------
'  This file is part of the Microsoft SQL Server Code Samples.
'  Copyright (C) Microsoft Corporation.  All rights reserved.
'
'  This source code is intended only as a supplement to Microsoft
'  Development Tools and/or on-line documentation.  See these other
'  materials for detailed information regarding Microsoft code samples.
'
'  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
'  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'  PARTICULAR PURPOSE.
'=======================================================================

#Region "Using directives"

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Reflection

Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Sdk.Sfc
Imports Microsoft.SqlServer
Imports Microsoft.SqlServer.MessageBox


#End Region

Partial Class SqlObjectBrowser 'The Partial modifier is only required on one class definition per project.
    Inherits Form
    Private objUrn As Urn
    Private server As Server
    Private serverConnected As Boolean

    Public ReadOnly Property Connected() As Boolean
        Get
            Return Me.serverConnected
        End Get
    End Property

    Public Sub New(ByVal serverConn As ServerConnection)
        InitializeComponent()

        ' Connect to SQL Server
        server = New Server(serverConn)
        Try
            server.ConnectionContext.Connect()

            ' In case connection succeeded we add the sql server node as root in object explorer (treeview)
            Dim tn As New TreeNode()
            tn.Text = server.Name
            tn.Tag = server.Urn
            Me.objUrn = server.Urn
            objectBrowserTreeView.Nodes.Add(tn)

            AddDummyNode(tn)

            serverConnected = True
        Catch ex As ConnectionException
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.ConnectionFailed
            emb.Show(Me)
        Catch ex As ApplicationException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub

    Public ReadOnly Property Urn() As Urn
        Get
            Return Me.objUrn
        End Get
    End Property

    Private Sub selectButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles selectButton.Click
        If Not (objectBrowserTreeView.SelectedNode.Tag Is Nothing) Then
            Me.objUrn = CType(objectBrowserTreeView.SelectedNode.Tag, Urn)
        End If

        Me.Visible = False
    End Sub

    Private Sub cancelCommandButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelCommandButton.Click
        Me.objUrn = Nothing
        Me.Visible = False
    End Sub

    Private Sub objectBrowserTreeView_AfterCollapse(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles objectBrowserTreeView.AfterCollapse
        e.Node.Nodes.Clear()

        AddDummyNode(e.Node)
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Private Sub objectBrowserTreeView_AfterExpand(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles objectBrowserTreeView.AfterExpand
        ' Remove dummy node
        e.Node.Nodes.RemoveAt(0)
        Dim smoObj As SqlSmoObject = Nothing
        Dim urnNode As Urn = CType(e.Node.Tag, Urn)

        Try
            smoObj = server.GetSmoObject(urnNode)
            AddCollections(e, smoObj)
        Catch ex As UnsupportedVersionException
            ' Right now don't do anything... but you might change the 
            ' Icon so it will emphasize the version issue
        Catch ex As SmoException
            Try
                AddItemInCollection(e, server)
            Catch except As Exception
                Dim emb As New ExceptionMessageBox(except)
                emb.Show(Me)
            End Try
        Catch ex As ApplicationException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub

    Private Shared Sub AddItemInCollection(ByVal node As System.Windows.Forms.TreeViewEventArgs, ByVal sqlServer As Server)
        Dim urnNode As Urn = CType(node.Node.Tag, Urn)
        Dim smoObj As SqlSmoObject = sqlServer.GetSmoObject(urnNode.Parent)
        Dim tn As TreeNode
        Dim namedObj As NamedSmoObject

        Dim p As PropertyInfo = smoObj.GetType().GetProperty(node.Node.Text)
        If Not (p Is Nothing) Then
            Dim iColl As ICollection = TryCast(p.GetValue(smoObj, Nothing), ICollection)
            If Not (iColl Is Nothing) Then
                Dim enum1 As IEnumerator = iColl.GetEnumerator()
                While enum1.MoveNext()
                    tn = New TreeNode()
                    namedObj = TryCast(enum1.Current, NamedSmoObject)
                    If Not (namedObj Is Nothing) Then
                        tn.Text = namedObj.Name
                    Else
                        tn.Text = CType(enum1.Current, SqlSmoObject).Urn
                    End If

                    tn.Tag = CType(enum1.Current, SqlSmoObject).Urn
                    node.Node.Nodes.Add(tn)

                    AddDummyNode(tn)
                End While
            Else
                Console.WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.PropertyNotICollection, p.Name))
            End If
        Else
            Console.WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.PropertyNotFound, node.Node.Text))
        End If
    End Sub

    ''' <summary>
    ''' Add the dummy node.
    ''' </summary>
    ''' <param name="tn"></param>
    Private Shared Sub AddDummyNode(ByVal tn As TreeNode)
        Dim tnD As TreeNode

        tnD = New TreeNode()
        tnD.Tag = "dummy"
        tn.Nodes.Add(tnD)
    End Sub

    ''' <summary>
    ''' Adds children to the current node. basically all collection of a SqlSmoObject object
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="smoObj"></param>
    Private Shared Sub AddCollections(ByVal node As System.Windows.Forms.TreeViewEventArgs, ByVal smoObj As SqlSmoObject)
        Dim tn As TreeNode
        Dim t As Type = smoObj.GetType()
        For Each p As System.Reflection.PropertyInfo In t.GetProperties()
            If (p.Name <> "SystemMessages" _
                AndAlso p.Name <> "UserDefinedMessages") Then
                ' If it is collection we create a node for it
                If p.PropertyType.IsSubclassOf(GetType(AbstractCollectionBase)) Then
                    tn = New TreeNode()
                    tn.Text = p.Name
                    tn.Tag = New Urn(smoObj.Urn.ToString() & "/" & p.Name)
                    node.Node.Nodes.Add(tn)

                    AddDummyNode(tn)
                End If
            End If
        Next

        ' Verify if we are at the server level and add the JobServer too 
        If TypeOf smoObj Is Server Then
            tn = New TreeNode()
            tn.Text = My.Resources.JobServer
            tn.Tag = New Urn(smoObj.Urn.ToString() & "/" & My.Resources.JobServer)
            node.Node.Nodes.Add(tn)

            AddDummyNode(tn)
        End If

        smoObj = Nothing
    End Sub

    Private Sub SqlObjectBrowser_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Display the first level of nodes
        If objectBrowserTreeView.Nodes.Count > 0 Then
            objectBrowserTreeView.Nodes(0).Expand()
        End If
    End Sub
End Class
