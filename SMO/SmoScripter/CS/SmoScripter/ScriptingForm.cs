/*============================================================================
  File:    ScriptingForm.cs

  Summary: Implements a sample SMO scripting form in C#.

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

namespace Microsoft.Samples.SqlServer
{
    public partial class ScriptingForm : Form
    {
        private Server sqlServerSelection;
        private DependencyTree dependTree;
        private DependencyCollection dependList;
        private Scripter smoScripter;

        public ScriptingForm(Server srvr, SqlSmoObject smoObject)
        {
            InitializeComponent();

            this.sqlServerSelection = srvr;

            this.smoScripter = new Scripter(srvr);

            // Generate the dependency tree
            this.dependTree = this.smoScripter.DiscoverDependencies(new SqlSmoObject[] { smoObject }, true);

            UpdateTree();
            UpdateListAndScript();

            if (Phase1TreeView.Nodes.Count > 0)
            {
                Phase1TreeView.Nodes[0].ExpandAll();
            }
        }

        private void UpdateTree()
        {
            DependencyTreeNode root = this.dependTree.FirstChild;

            Phase1TreeView.Nodes.Clear();

            while (root != null)
            {
                TreeNode node;

                node = new TreeNode(root.Urn.GetAttribute("Name") +
                    " (" + root.Urn.Type + ")");
                node.Tag = root;
                Phase1TreeView.Nodes.Add(node);
                AddChildren(node, root);
                root = root.NextSibling;
            }
        }

        private void UpdateListAndScript()
        {
            this.Phase2ListBox.Items.Clear();
            this.Phase3TextBox.Clear();
            this.Phase3TextBox.AppendText(Properties.Resources.GeneratingScript);

            // Generate the script list
            this.dependList = this.smoScripter.WalkDependencies(this.dependTree);

            this.Phase2ListBox.DisplayMember = "ToString()";
            foreach (DependencyCollectionNode dependencyNode in this.dependList)
            {
                this.Phase2ListBox.Items.Add(this.sqlServerSelection.GetSmoObject(dependencyNode.Urn));
            }

            StringBuilder sb = new StringBuilder();
            StringCollection sc = new StringCollection();

            //this.smoScripter.Options.Indexes = true;
            //this.smoScripter.Options.DriAll = true;
            //this.smoScripter.Options.ExtendedProperties = true;
            //this.smoScripter.Options.IncludeIfNotExists = true;
            //this.smoScripter.Options.Permissions = true;
            this.smoScripter.Options.SchemaQualify = true;

            // Generate the script
            sc = this.smoScripter.ScriptWithList(this.dependList);
            foreach (string s in sc)
            {
                sb.Append(s);
                sb.Append(Environment.NewLine + "GO" + Environment.NewLine);
            }
            this.Phase3TextBox.Text = sb.ToString();
        }

        private void AddChildren(TreeNode parent, DependencyTreeNode d)
        {
            DependencyTreeNode child = d.FirstChild;

            TreeNode node;

            while (child != null)
            {
                node = new TreeNode(child.Urn.GetAttribute("Name") +
                    " (" + child.Urn.Type + ")");
                node.Tag = child;
                parent.Nodes.Add(node);
                AddChildren(node, child);
                child = child.NextSibling;
            }
        }
    }
}