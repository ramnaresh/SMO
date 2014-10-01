//============================================================================
//  File:    DependencyForm.cs
//
//  Summary: Implements dependency tree and property display window in C#.
//
//  Date:    May 20, 2005
//------------------------------------------------------------------------------
//  This file is part of the Microsoft SQL Server Code Samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.MessageBox;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace Microsoft.Samples.SqlServer
{
    public partial class DependencyForm : Form
    {
        private Server server;

        public DependencyForm()
        {
            InitializeComponent();
        }

        private void WhereUsedMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node;
            TreeNode newNode = new TreeNode();
            Urn[] urns = new Urn[1];
            Scripter scripter;

            // Get selected node
            node = this.DependenciesTreeView.SelectedNode;

            // Only do this once
            if (node.Nodes.ContainsKey(Properties.Resources.UsedBy) == true
                | node.Name == Properties.Resources.UsedBy)
            {
                return;
            }

            // Get the urn from the node
            urns[0] = (Urn)(node.Tag);

            // Add a "where used" node
            newNode = new TreeNode(Properties.Resources.UsedBy);
            newNode.Name = Properties.Resources.UsedBy;
            node.Nodes.Add(newNode);

            // And add the tree to the current node
            scripter = new Scripter(server);
            AddChildren(newNode, scripter.DiscoverDependencies(urns, DependencyType.Children).FirstChild);
            node.Expand();
            newNode.Expand();
        }

        private void DependenciesMenuItem_Click(object sender, EventArgs e)
        {
            Urn[] urns = new Urn[1];
            Scripter scripter;
            DependencyForm frm;
            DependencyTree deps;

            // Get the urn from the node
            urns[0] = (Urn)(this.DependenciesTreeView.SelectedNode.Tag);

            // Instantiate scripter
            scripter = new Scripter(server);

            // Get a new form
            frm = new DependencyForm();

            // Discover dependencies
            deps = scripter.DiscoverDependencies(urns, DependencyType.Parents);

            // Set the tree
            frm.ShowDependencies(server, deps);

            // Show the form
            frm.Show();
        }

        public void ShowDependencies(Server sqlServer, DependencyTreeNode dependTree)
        {
            DependencyTreeNode rootNode;
            TreeNode treeNode;

            if (dependTree == null)
            {
                throw new ArgumentNullException("dependTree");
            }

            // Set server for later use
            server = sqlServer;

            // Get the first child in tree
            rootNode = dependTree.FirstChild;

            // Iterate children
            while (rootNode != null)
            {
                // Add treeview node
                treeNode = new TreeNode(rootNode.Urn.GetAttribute("Name")
                    + " (" + rootNode.Urn.Type + ")");

                // Set Urn for later use
                treeNode.Tag = rootNode.Urn;

                // Set context menu for node
                treeNode.ContextMenu = this.NodeContextMenu;

                // Add Node to treeview
                this.DependenciesTreeView.Nodes.Add(treeNode);

                // Add child nodes to tree (this will recurse)
                AddChildren(treeNode, rootNode);

                // Skip to next child node from root
                rootNode = rootNode.NextSibling;
            }

            this.DependenciesTreeView.ExpandAll();
        }

        private void AddChildren(TreeNode parentNode, DependencyTreeNode dependencyTreeNode)
        {
            DependencyTreeNode child;
            TreeNode treeNode;

            // Get first child of this node
            child = dependencyTreeNode.FirstChild;

            while (child != null)
            {
                // Add treeview node
                treeNode = new TreeNode(child.Urn.GetAttribute("Name") + " (" + child.Urn.Type + ")");

                // Set Urn for later use
                treeNode.Tag = child.Urn;

                // Set context menu for node
                treeNode.ContextMenu = this.NodeContextMenu;

                // Add node to treeview
                parentNode.Nodes.Add(treeNode);

                // Recursively add the other nodes
                AddChildren(treeNode, child);

                // Skip to next child node at this level
                child = child.NextSibling;
            }
        }

        private void DependenciesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SqlSmoObject smoObject;
            Urn urn;
            ListViewItem item;
            Cursor csr = null;

            try
            {
                csr = this.Cursor; // Save the old cursor
                this.Cursor = Cursors.WaitCursor; // Display the waiting cursor

                // Get the Urn from the selected node
                urn = (Urn)(e.Node.Tag);

                // Clear the list
                this.PropertiesListView.Items.Clear();

                // No Urn : we cannot display
                if (urn == null)
                {
                    return;
                }

                // Instantiate the SMO object using the Urn
                smoObject = server.GetSmoObject(urn);

                // Initialize its properties
                smoObject.Initialize(true);

                // Now print each retrieved property in the ListView
                foreach (Property prop in smoObject.Properties)
                {
                    if (prop.Retrieved == true)
                    {
                        item = new ListViewItem(prop.Name);
                        if (prop.Value != null)
                        {
                            item.SubItems.Add(prop.Value.ToString());
                        }

                        this.PropertiesListView.Items.Add(item);
                    }
                }

                // Size it
                this.PropertiesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                // Restore the original cursor
                this.Cursor = csr;
            }
        }


    }
}

