'============================================================================
'  File:    DependenciesForm.vb
'
'  Summary: Implements a sample SMO dependencies form in VB.NET.
'
'  Date:    September 30, 2005
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

Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Sdk.Sfc


Partial Public Class DependenciesForm 'The Partial modifier is only required on one class definition per project.
    Inherits Form
    Private sqlServerSelection As Server

    Public Sub New(ByVal srvr As Server, ByVal dependTree As DependencyTree)
        InitializeComponent()

        If Not (dependTree Is Nothing) Then
            Me.sqlServerSelection = srvr

            Dim root As DependencyTreeNode
            Dim node As TreeNode

            root = dependTree.FirstChild

            While Not (root Is Nothing)
                node = New TreeNode(root.Urn.GetAttribute("Name") + " (" + root.Urn.Type + ")")
                node.Tag = root.Urn
                DependenciesTreeView.Nodes.Add(node)
                AddChildren(node, root)
                root = root.NextSibling
            End While

            If DependenciesTreeView.Nodes.Count > 0 Then
                DependenciesTreeView.Nodes(0).Expand()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds the dependency tree to the treenode (recursive)
    ''' </summary>
    ''' <param name="parent"></param>
    ''' <param name="d"></param>
    Private Sub AddChildren(ByVal parent As TreeNode, ByVal d As DependencyTreeNode)
        Dim child As DependencyTreeNode = d.FirstChild

        Dim node As TreeNode

        While Not (child Is Nothing)
            node = New TreeNode(child.Urn.GetAttribute("Name") _
                & " (" & child.Urn.Type & ")")
            node.Tag = child.Urn
            parent.Nodes.Add(node)
            AddChildren(node, child)
            child = child.NextSibling
        End While
    End Sub

    ''' <summary>
    ''' Event handler for where used menu item.
    ''' Adds a "USED BY" node and performs a dependency discovery after which
    ''' all dependents are added to the "USED BY" node.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub WhereUsedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhereUsedToolStripMenuItem.Click
        Dim node As TreeNode
        Dim newNode As TreeNode

        node = DependenciesTreeView.SelectedNode

        If node.Nodes.ContainsKey(My.Resources.UsedBy) OrElse node.Name = My.Resources.UsedBy Then
            Return
        End If

        newNode = New TreeNode(My.Resources.UsedBy)
        newNode.Name = My.Resources.UsedBy
        node.Nodes.Add(newNode)

        ' Advanced Scripting
        Dim scripter As New Scripter(Me.sqlServerSelection)
        Dim urns(0) As Urn
        urns(0) = CType(node.Tag, Urn)

        ' Discover dependents
        Dim tree As DependencyTree = scripter.DiscoverDependencies(urns, DependencyType.Children)

        ' Add to tree (recursive)
        AddChildren(newNode, tree.FirstChild)
        node.Expand()
        newNode.Expand()
    End Sub

    ' Scripts an item in right window if it is scriptable
    Private Sub DependenciesTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles DependenciesTreeView.AfterSelect
        Me.ScriptTextBox.Clear()
        If e.Node.Tag Is Nothing Then
            Return
        End If

        ' Script an object
        Dim smoObject As SqlSmoObject = Me.sqlServerSelection.GetSmoObject(CType(e.Node.Tag, Urn))

        Dim so As New ScriptingOptions()
        so.DriAll = True
        so.Indexes = True
        so.IncludeHeaders = True
        so.Permissions = True
        so.PrimaryObject = True
        so.SchemaQualify = True
        so.Triggers = True

        Dim scriptableObject As IScriptable = TryCast(smoObject, IScriptable)
        If Not (scriptableObject Is Nothing) Then
            Dim sb As New StringBuilder()
            Dim s As String
            For Each s In scriptableObject.Script(so)
                sb.Append(s)
                sb.Append(Environment.NewLine)
            Next

            Me.ScriptTextBox.Text = sb.ToString()
        End If
    End Sub
End Class
