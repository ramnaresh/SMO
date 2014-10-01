'============================================================================
'  File:    DependencyForm.vb 
'
'  Summary: Implements dependency tree and property display window in VB.NET.
'
'  Date:    January 25, 2005
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

Imports Microsoft.SqlServer.Management.Sdk.Sfc


Public Class DependencyForm

#Region "Form Init"
    Private server As Server

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.PropertiesListView.View = Windows.Forms.View.Details
        'Me.PropertiesListView.Columns.Add("Name", 120, HorizontalAlignment.Left)
        'Me.PropertiesListView.Columns.Add("Value", 120, HorizontalAlignment.Left)
        'Me.PropertiesListView.GridLines = True
        'Me.PropertiesListView.FullRowSelect = True
        'Me.PropertiesListView.Items.Clear()
    End Sub
#End Region

    Public Sub ShowDependencies(ByVal sqlServer As Server, ByVal dependTree As DependencyTreeNode)
        Dim rootNode As DependencyTreeNode
        Dim treeNode As TreeNode

        If (dependTree Is Nothing) Then
            Throw New ArgumentNullException("dependTree")
        End If

        ' Set server for later use
        server = sqlServer

        ' Get the first child in tree
        rootNode = dependTree.FirstChild

        ' Iterate children
        While rootNode IsNot Nothing
            ' Add treeview node
            treeNode = New TreeNode(rootNode.Urn.GetAttribute("Name") & _
                    " (" & rootNode.Urn.Type & ")")

            ' Set Urn for later use
            treeNode.Tag = rootNode.Urn

            ' Set context menu for node
            treeNode.ContextMenu = Me.NodeContextMenu

            ' Add Node to treeview
            Me.DependenciesTreeView.Nodes.Add(treeNode)

            ' Add child nodes to tree (this will recurse)
            AddChildren(treeNode, rootNode)

            ' Skip to next child node from root
            rootNode = rootNode.NextSibling
        End While

        Me.DependenciesTreeView.ExpandAll()
    End Sub

    Private Sub AddChildren(ByVal parentNode As TreeNode, ByVal dependencyTreeNode As DependencyTreeNode)
        Dim child As DependencyTreeNode
        Dim treeNode As TreeNode

        ' Get first child of this node
        child = dependencyTreeNode.FirstChild

        While child IsNot Nothing
            ' Add treeview node
            treeNode = New TreeNode(child.Urn.GetAttribute("Name") & " (" & child.Urn.Type & ")")

            ' Set Urn for later use
            treeNode.Tag = child.Urn

            ' Set context menu for node
            treeNode.ContextMenu = Me.NodeContextMenu

            ' Add node to treeview
            parentNode.Nodes.Add(treeNode)

            ' Recursively add the other nodes
            AddChildren(treeNode, child)

            ' Skip to next child node at this level
            child = child.NextSibling
        End While
    End Sub

    Private Sub DependenciesTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles DependenciesTreeView.AfterSelect
        Dim smoObject As SqlSmoObject
        Dim urn As Urn
        Dim item As ListViewItem
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Get the Urn from the selected node
            urn = CType(e.Node.Tag, Urn)

            ' Clear the list
            Me.PropertiesListView.Items.Clear()

            ' No Urn : we cannot display
            If urn Is Nothing Then
                Exit Sub
            End If

            ' Instantiate the SMO object using the Urn
            smoObject = server.GetSmoObject(urn)

            ' Initialize its properties
            smoObject.Initialize(True)

            ' Now print each retrieved property in the ListView
            For Each prop As [Property] In smoObject.Properties
                If prop.Retrieved = True Then
                    item = New ListViewItem(prop.Name)
                    If Not prop.Value Is Nothing Then
                        item.SubItems.Add(prop.Value.ToString())
                    End If

                    Me.PropertiesListView.Items.Add(item)
                End If
            Next

            ' Size it
            Me.PropertiesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            ' Restore the original cursor
            Me.Cursor = csr
        End Try
    End Sub

    Private Sub WhereUsedMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhereUsedMenuItem.Click
        Dim node As TreeNode
        Dim newNode As New TreeNode
        Dim urns(0) As Urn
        Dim scripter As Scripter

        ' Get selected node
        node = Me.DependenciesTreeView.SelectedNode

        ' Only do this once
        If node.Nodes.ContainsKey(My.Resources.UsedBy) = True Or node.Name = My.Resources.UsedBy Then
            Exit Sub
        End If

        ' Get the urn from the node
        urns(0) = CType(node.Tag, Urn)

        ' Add a "where used" node
        newNode = New TreeNode(My.Resources.UsedBy)
        newNode.Name = My.Resources.UsedBy
        node.Nodes.Add(newNode)

        ' And add the tree to the current node
        scripter = New Scripter(server)
        AddChildren(newNode, scripter.DiscoverDependencies(urns, DependencyType.Children).FirstChild)
        node.Expand()
        newNode.Expand()
    End Sub

    Private Sub DependenciesMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DependenciesMenuItem.Click
        Dim urns(0) As Urn
        Dim scripter As Scripter
        Dim frm As DependencyForm

        ' Get the urn from the node
        urns(0) = CType(Me.DependenciesTreeView.SelectedNode.Tag, Urn)

        ' Instantiate scripter
        scripter = New Scripter(server)

        ' Get a new form
        frm = New DependencyForm()

        ' Set the tree
        frm.ShowDependencies(server, scripter.DiscoverDependencies(urns, DependencyType.Parents))

        ' Show the form
        frm.Show()
    End Sub
End Class
