'============================================================================
'  File:    ScriptingForm.vb
'
'  Summary: Implements a sample SMO scripting form in VB.NET.
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

Partial Public Class ScriptingForm 'The Partial modifier is only required on one class definition per project.
    Inherits Form
    Private sqlServerSelection As Server
    Private dependTree As DependencyTree
    Private dependList As DependencyCollection
    Private smoScripter As Scripter

    Public Sub New(ByVal srvr As Server, ByVal smoObject As SqlSmoObject)
        InitializeComponent()

        Me.sqlServerSelection = srvr

        Me.smoScripter = New Scripter(srvr)

        ' Generate the dependency tree
        Me.dependTree = Me.smoScripter.DiscoverDependencies(New SqlSmoObject() {smoObject}, True)

        UpdateTree()
        UpdateListAndScript()

        If Phase1TreeView.Nodes.Count > 0 Then
            Phase1TreeView.Nodes(0).ExpandAll()
        End If
    End Sub

    Private Sub UpdateTree()
        Dim root As DependencyTreeNode = Me.dependTree.FirstChild

        Phase1TreeView.Nodes.Clear()

        While Not (root Is Nothing)
            Dim node As TreeNode

            node = New TreeNode(root.Urn.GetAttribute("Name") + " (" + root.Urn.Type + ")")
            node.Tag = root
            Phase1TreeView.Nodes.Add(node)
            AddChildren(node, root)
            root = root.NextSibling
        End While
    End Sub

    Private Sub UpdateListAndScript()
        Me.Phase2ListBox.Items.Clear()
        Me.Phase3TextBox.Clear()
        Me.Phase3TextBox.AppendText(My.Resources.GeneratingScript)

        ' Generate the script list
        Me.dependList = Me.smoScripter.WalkDependencies(Me.dependTree)

        Me.Phase2ListBox.DisplayMember = "ToString()"
        Dim dependencyNode As DependencyCollectionNode
        For Each dependencyNode In Me.dependList
            Me.Phase2ListBox.Items.Add(Me.sqlServerSelection.GetSmoObject(dependencyNode.Urn))
        Next dependencyNode

        Dim sb As New StringBuilder()
        Dim sc As New StringCollection()

        'this.smoScripter.Options.Indexes = true;
        'this.smoScripter.Options.DriAll = true;
        'this.smoScripter.Options.ExtendedProperties = true;
        'this.smoScripter.Options.IncludeIfNotExists = true;
        'this.smoScripter.Options.Permissions = true;
        Me.smoScripter.Options.SchemaQualify = True

        ' Generate the script
        sc = Me.smoScripter.ScriptWithList(Me.dependList)
        Dim s As String
        For Each s In sc
            sb.Append(s)
            sb.Append(Environment.NewLine + "GO" + Environment.NewLine)
        Next s
        Me.Phase3TextBox.Text = sb.ToString()
    End Sub

    Private Sub AddChildren(ByVal parent As TreeNode, ByVal d As DependencyTreeNode)
        Dim child As DependencyTreeNode = d.FirstChild

        Dim node As TreeNode

        While Not (child Is Nothing)
            node = New TreeNode(child.Urn.GetAttribute("Name") + " (" + child.Urn.Type + ")")
            node.Tag = child
            parent.Nodes.Add(node)
            AddChildren(node, child)
            child = child.NextSibling
        End While
    End Sub
End Class