'============================================================================
'  File:    SmoBrowser.vb 
'
'  Summary: Implements a sample SMO browser utility in VB.NET.
'
'  Date:    August 16, 2005
'------------------------------------------------------------------------------
'  This file is part of the Microsoft SQL Server Code Samples.
'
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
'============================================================================

#Region "Using directives"
Imports System
Imports System.Collections
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Globalization

Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Smo.Wmi
Imports Microsoft.SqlServer.MessageBox
#End Region

Partial Class SmoBrowser 'The Partial modifier is only required on one class definition per project.
    Inherits Form
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SmoBrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ServerConn As New ServerConnection()
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ServerConn = New ServerConnection()
        scForm = New ServerConnect(ServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK AndAlso ServerConn.SqlConnectionObject.State = ConnectionState.Open Then
            SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle + SqlServerSelection.Name
            End If
        Else
            Me.Close()
        End If

        If Not (SqlServerSelection Is Nothing) Then
            ' By default create Server and Managed Computer nodes
            objectTreeView.Nodes.Add(CreateNode(String.Format(CultureInfo.CurrentUICulture, My.Resources.SQLServerNodeName, SqlServerSelection.Name), SqlServerSelection))
            objectTreeView.Nodes.Add(CreateNode(String.Format(CultureInfo.CurrentUICulture, My.Resources.ManagedComputerNodeName, SqlServerSelection.Name), New ManagedComputer(SqlServerSelection.Name)))
        End If
    End Sub

    Private Sub exitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitToolStripMenuItem.Click
        Close()
        Application.Exit()
    End Sub

    Private Sub OnBeforeExpand(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles objectTreeView.BeforeExpand
        Try
            If e.Node.Nodes.Count = 1 AndAlso e.Node.Nodes(0).Text = My.Resources.NodePlaceHolder Then
                e.Node.Nodes.RemoveAt(0)
                If TypeOf e.Node.Tag Is ICollection Then
                    PopulateCollectionItems(e.Node)
                Else
                    PopulateExpandableProperties(e.Node)
                End If
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub

    Private Sub AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles objectTreeView.AfterSelect
        Dim oldCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            Me.propertyGrid1.SelectedObject = e.Node.Tag
            If TypeOf e.Node.Tag Is IScriptable Then
                Me.textBox1.Text = GetScriptText(CType(e.Node.Tag, IScriptable))
            Else
                Me.textBox1.Text = String.Empty
            End If

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = oldCursor
        End Try
    End Sub

    Private Shared Function GetScriptText(ByVal scriptable As IScriptable) As String
        If scriptable Is Nothing Then
            Return String.Empty
        End If

        Dim stringBuilder As New StringBuilder()
        Dim batches As StringCollection
        Try
            batches = scriptable.Script()
        Catch ex As Exception
            Return String.Empty
        End Try
        For Each batch As String In batches
            stringBuilder.AppendFormat("{0}" & vbCrLf & "GO" & vbCrLf, batch)
        Next

        Return stringBuilder.ToString()
    End Function

    Private Overloads Shared Function CreateNode(ByVal item As Object) As TreeNode
        Dim name As String = Nothing
        Dim [property] As PropertyDescriptor

        For Each propertyName As String In New String() {"Name", "Urn"}
            [property] = TypeDescriptor.GetProperties(item)(propertyName)
            If Not ([property] Is Nothing) Then
                name = [property].GetValue(item).ToString()
                Exit For
            End If
        Next

        If name Is Nothing Then
            name = item.ToString()
        End If

        Return CreateNode(name, item)
    End Function

    Private Overloads Shared Function CreateNode(ByVal name As String, ByVal item As Object) As TreeNode
        Dim node As New TreeNode(name)

        node.Tag = item
        If TypeOf item Is ICollection OrElse HasExpandableProperties(item) Then
            node.Nodes.Add(My.Resources.NodePlaceHolder)
        End If

        Return node
    End Function

    Private Shared Function HasExpandableProperties(ByVal item As Object) As Boolean
        For Each [property] As PropertyDescriptor In TypeDescriptor.GetProperties(item)
            If IsExpandableProperty([property]) = True Then
                Return True
            End If
        Next

        Return False
    End Function

    Private Shared Function IsCollection(ByVal [property] As PropertyDescriptor) As Boolean
        For Each typ As Type In [property].PropertyType.GetInterfaces()
            If TypeOf typ Is ICollection Then
                If [property].PropertyType.IsArray Then
                    Exit For
                End If

                If [property].Name = "Properties" Then
                    Exit For
                End If

                Return True
            End If
        Next

        Return False
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> Private Sub PopulateExpandableProperties(ByVal node As TreeNode)
        Dim value As Object
        Dim oldCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            For Each [property] As PropertyDescriptor In TypeDescriptor.GetProperties(node.Tag)
                If IsExpandableProperty([property]) = False Then
                    Continue For
                End If

                Try
                    value = [property].GetValue(node.Tag)
                Catch ex As Exception
                    value = String.Format(CultureInfo.CurrentUICulture, _
                        My.Resources.ExceptionString, ex.GetType(), ex.Message)
                End Try

                node.Nodes.Add(CreateNode([property].Name, value))
            Next
        Finally
            Me.Cursor = oldCursor
        End Try
    End Sub

    Private Shared Function IsExpandableProperty(ByVal [property] As PropertyDescriptor) As Boolean
        If Not IsExpandablePropertyType([property].PropertyType) Then
            Return False
        End If

        If [property].PropertyType.IsArray Then
            If Not IsExpandablePropertyType([property].PropertyType.GetElementType()) Then
                Return False
            End If
        End If

        If [property].Name = "Urn" _
            OrElse [property].Name = "UserData" _
            OrElse [property].Name = "Properties" _
            OrElse [property].Name = "ExtendedProperties" _
            OrElse [property].Name = "Parent" _
            OrElse [property].Name = "Events" Then
            Return False
        End If

        Return True
    End Function

    Private Shared Function IsExpandablePropertyType(ByVal typ As Type) As Boolean
        If Type.GetTypeCode(typ) <> TypeCode.Object Then
            Return False
        End If

        If typ Is GetType(Guid) OrElse typ Is GetType(DateTime) Then
            Return False
        End If

        Return True
    End Function

    Private Sub PopulateCollectionItems(ByVal node As TreeNode)
        Dim oldCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            For Each item As Object In CType(node.Tag, ICollection)
                node.Nodes.Add(CreateNode(item))
            Next
        Finally
            Me.Cursor = oldCursor
        End Try
    End Sub
End Class
