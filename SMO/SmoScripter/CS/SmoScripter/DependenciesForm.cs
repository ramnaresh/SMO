/*============================================================================
  File:    DependenciesForm.cs

  Summary: Implements a sample SMO dependencies form in C#.

  Date:    September 30, 2005
------------------------------------------------------------------------------
  This file is part of the Microsoft SQL Server Code Samples.

  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
============================================================================*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace Microsoft.Samples.SqlServer
{
    public partial class DependenciesForm : Form
    {
        private Server sqlServerSelection;

        public DependenciesForm(Server srvr, DependencyTree dependTree)
        {
            InitializeComponent();

            if (dependTree != null)
            {
                this.sqlServerSelection = srvr;

                DependencyTreeNode root;
                TreeNode node;

                root = dependTree.FirstChild;

                while (root != null)
                {
                    node = new TreeNode(root.Urn.GetAttribute("Name") +
                        " (" + root.Urn.Type + ")");
                    node.Tag = root.Urn;
                    DependenciesTreeView.Nodes.Add(node);
                    AddChildren(node, root);
                    root = root.NextSibling;
                }

                if (DependenciesTreeView.Nodes.Count > 0)
                {
                    DependenciesTreeView.Nodes[0].Expand();
                }
            }
        }

        /// <summary>
        /// Adds the dependency tree to the treenode (recursive)
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="d"></param>
        private void AddChildren(TreeNode parent, DependencyTreeNode d)
        {
            DependencyTreeNode child = d.FirstChild;

            TreeNode node;

            while (child != null)
            {
                node = new TreeNode(child.Urn.GetAttribute("Name") +
                    " (" + child.Urn.Type + ")");
                node.Tag = child.Urn;
                parent.Nodes.Add(node);
                AddChildren(node, child);
                child = child.NextSibling;
            }
        }

        /// <summary>
        /// Event handler for where used menu item.
        /// Adds a "USED BY" node and performs a dependency discovery after which
        /// all dependents are added to the "USED BY" node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhereUsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node, newNode;

            node = DependenciesTreeView.SelectedNode;

            if (node.Nodes.ContainsKey(Properties.Resources.UsedBy)
                || node.Name == Properties.Resources.UsedBy)
            {
                return;
            }

            newNode = new TreeNode(Properties.Resources.UsedBy);
            newNode.Name = Properties.Resources.UsedBy;
            node.Nodes.Add(newNode);

            // Advanced Scripting
            Scripter scripter = new Scripter(this.sqlServerSelection);
            Urn[] urns = new Urn[1];
            urns[0] = (Urn)node.Tag;

            // Discover dependents
            DependencyTree tree = scripter.DiscoverDependencies(urns, DependencyType.Children);

            // Add to tree (recursive)
            AddChildren(newNode, tree.FirstChild);
            node.Expand();
            newNode.Expand();
        }

        // Scripts an item in right window if it is scriptable
        private void DependenciesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ScriptTextBox.Clear();
            if (e.Node.Tag == null)
            {
                return;
            }

            // Script an object
            SqlSmoObject o = this.sqlServerSelection.GetSmoObject((Urn)e.Node.Tag);

            ScriptingOptions so = new ScriptingOptions();
            so.DriAll = true;
            so.Indexes = true;
            so.IncludeHeaders = true;
            so.Permissions = true;
            so.PrimaryObject = true;
            so.SchemaQualify = true;
            so.Triggers = true;

            IScriptable scriptableObject = o as IScriptable;
            if (scriptableObject != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in scriptableObject.Script(so))
                {
                    sb.Append(s);
                    sb.Append(Environment.NewLine);
                }

                this.ScriptTextBox.Text = sb.ToString();
            }
        }
    }
}