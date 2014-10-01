/*============================================================================
  File:    MainForm.cs

  Summary: Implements a sample SMO dependency and scripting utility in C#.

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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.MessageBox;

namespace Microsoft.Samples.SqlServer
{
    /// <summary>
    /// Summary description for form.
    /// </summary>
    public partial class MainForm : System.Windows.Forms.Form
    {
        // Use the Server object to connect to a specific server
        private Server sqlServerSelection;
        private TreeNode root;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ConnectToServer(string server)
        {
            try
            {
                Server srvr = new Server(server);
                srvr.ConnectionContext.ConnectTimeout = 5;
                srvr.ConnectionContext.Connect();
                this.sqlServerSelection = srvr;
            }
            catch (ConnectionFailureException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);

                Cursor = Cursors.Default;
                this.sqlServerSelection = null;
            }

            this.serverVersionToolStripStatusLabel.Text
                = this.sqlServerSelection.Information.Version.ToString()
                + " " + this.sqlServerSelection.Information.Edition;
            this.SqlServerTreeView.Nodes.Clear();

            // Add Server node
            TreeNode node = new TreeNode(this.sqlServerSelection.ConnectionContext.TrueName);
            node.Name = Properties.Resources.Server;
            root = node;
            node.Tag = this.sqlServerSelection;
            this.SqlServerTreeView.Nodes.Add(node);

            // Add Databases node
            node = new TreeNode(Properties.Resources.Databases);
            node.Name = Properties.Resources.Databases;
            node.Tag = this.sqlServerSelection.Databases;
            root.Nodes.Add(node);
            AddDummyNode(node);

            this.SqlServerTreeView.SelectedNode = root;

            // Optimizing code
            this.sqlServerSelection.SetDefaultInitFields(typeof(Database),
                "CreateDate", "IsSystemObject", "CompatibilityLevel");
            this.sqlServerSelection.SetDefaultInitFields(typeof(Table),
                "CreateDate", "IsSystemObject");
            this.sqlServerSelection.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.View),
                "CreateDate", "IsSystemObject");
            this.sqlServerSelection.SetDefaultInitFields(typeof(StoredProcedure),
                "CreateDate", "IsSystemObject");
            this.sqlServerSelection.SetDefaultInitFields(typeof(Column), true);
        }

        private void SqlServerTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DateTime start = DateTime.Now;
            ShowDetails(this.SqlServerTreeView.SelectedNode);
            TimeSpan diff = DateTime.Now - start;
            this.speedToolStripStatusLabel.Text
                = String.Format(System.Globalization.CultureInfo.InvariantCulture,
                "{0:####}", diff.TotalMilliseconds);
            Cursor = Cursors.Default;
        }

        private void SqlServerTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            LoadTreeViewItems(e.Node);
            Cursor = Cursors.Default;
        }

        // Loads treeview items and updates listview as well.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private void ShowDetails(TreeNode node)
        {
            SqlSmoObject smoObject = null;
            SmoCollectionBase smoCollection = null;

            this.ListView.Items.Clear();

            if (node == null)
            {
                return;
            }

            switch (node.Tag.GetType().Name)
            {
                case "DatabaseCollection":
                case "TableCollection":
                case "ViewCollection":
                case "StoredProcedureCollection":
                case "ColumnCollection":
                case "SqlAssemblyCollection":
                    // Load the items of a collection, if not already loaded
                    LoadTreeViewItems(node);

                    // Update the TreeView
                    smoCollection = (SmoCollectionBase)node.Tag;
                    UpdateListViewWithCollection(smoCollection);

                    break;

                case "Server":
                    smoObject = ((Server)node.Tag).Information;
                    UpdateListView(smoObject, true, "Information");

                    smoObject = ((Server)node.Tag).Settings;
                    UpdateListView(smoObject, false, "Settings");

                    break;

                case "Database":
                case "Table":
                case "View":
                case "StoredProcedure":
                case "Column":
                case "SqlAssembly":
                    smoObject = (SqlSmoObject)node.Tag;
                    UpdateListView(smoObject, true, null);

                    break;

                default:
                    throw new Exception(Properties.Resources.UnrecognizedType 
                        + node.Tag.GetType().ToString());
            }
        }

        // Update the list view for a property list
        private void UpdateListView(SqlSmoObject smoObject, bool clear, string group)
        {
            smoObject.Initialize(true);
            if (clear == true)
            {
                this.ListView.Columns.Clear();
                this.ListView.Groups.Clear();

                ColumnHeader colHeader = new ColumnHeader();
                colHeader.Text = Properties.Resources.PropertyName;
                this.ListView.Columns.Add(colHeader);

                colHeader = new ColumnHeader();
                colHeader.Text = Properties.Resources.Value;
                this.ListView.Columns.Add(colHeader);
            }

            ListViewGroup lvGroup = null;
            if (group != null)
            {
                lvGroup = new ListViewGroup(group);
                this.ListView.Groups.Add(lvGroup);
            }

            ListViewItem lvi = new ListViewItem();
            lvi.Text = Properties.Resources.Urn;
            lvi.Name = Properties.Resources.Urn;
            lvi.Group = lvGroup;
            lvi.SubItems.Add(smoObject.Urn);
            this.ListView.Items.Add(lvi);

            foreach (Property prop in smoObject.Properties)
            {
                if (prop.Value != null)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = prop.Name;
                    lvItem.Name = prop.Name;
                    lvItem.Group = lvGroup;

                    lvItem.SubItems.Add(prop.Value.ToString());
                    this.ListView.Items.Add(lvItem);
                }
            }

            this.ListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListView.Sort();
            this.ListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.ListView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        // Lists a collection in the listview if the container node is selected
        private void UpdateListViewWithCollection(SmoCollectionBase smoCollection)
        {
            this.ListView.Columns.Clear();
            this.ListView.Groups.Clear();

            ColumnHeader colHeader = new ColumnHeader();
            colHeader.Text = Properties.Resources.ObjectName;
            this.ListView.Columns.Add(colHeader);

            colHeader = new ColumnHeader();
            if (smoCollection is ColumnCollection)
            {
                colHeader.Text = Properties.Resources.DataType;
            }
            else
            {
                colHeader.Text = Properties.Resources.DateCreated;
            }

            this.ListView.Columns.Add(colHeader);

            foreach (SqlSmoObject smoObject in smoCollection)
            {
                if (smoObject.Properties.Contains("IsSystemObject") == true
                    && (bool)smoObject.Properties["IsSystemObject"].Value == true)
                {
                    continue;
                }

                ListViewItem lvi = new ListViewItem();
                lvi.Text = smoObject.ToString();
                lvi.Name = smoObject.ToString();

                if (smoObject is Column)
                {
                    lvi.SubItems.Add(smoObject.Properties["DataType"].Value.ToString());
                }
                else
                {
                    lvi.SubItems.Add(smoObject.Properties["CreateDate"].Value.ToString());
                }

                lvi.Tag = smoObject;
                this.ListView.Items.Add(lvi);
            }

            this.ListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListView.Sort();
            this.ListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.ListView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        /// <summary>
        /// Loads the collection items. This has to be deferred else starting is 
        /// too expensive.
        /// </summary>
        /// <param name="node"></param>
        private void LoadTreeViewItems(TreeNode node)
        {
            if (node.Nodes.Count == 1
                && node.Nodes[Properties.Resources.DummyNode] != null)
            {
                node.Nodes[Properties.Resources.DummyNode].Remove();
            }
            else
            {
                return;
            }

            switch (node.Name)
            {
                case "Databases":
                    LoadTreeViewDatabases(node);

                    break;

                case "Stored Procedures":
                    LoadTreeViewStoredProcedures(node);

                    break;

                case "Assemblies":
                    LoadTreeViewAssemblies(node);

                    break;

                case "Tables":
                    LoadTreeViewTables(node);

                    break;

                case "Views":
                    LoadTreeViewViews(node);

                    break;

                case "Columns":
                    LoadTreeViewColumns(node);

                    break;
            }
        }

        private void LoadTreeViewDatabases(TreeNode node)
        {
            foreach (Database db in (DatabaseCollection)node.Tag)
            {
                if (db.IsSystemObject == false)
                {
                    TreeNode dbNode = new TreeNode(db.Name);
                    dbNode.Name = db.Name;
                    dbNode.Tag = db;
                    root.Nodes["Databases"].Nodes.Add(dbNode);
                    AddDummyNode(node);

                    TreeNode tcNode = new TreeNode(Properties.Resources.Tables);
                    tcNode.Name = Properties.Resources.Tables;
                    tcNode.Tag = db.Tables;
                    dbNode.Nodes.Add(tcNode);
                    AddDummyNode(tcNode);

                    TreeNode vcNode = new TreeNode(Properties.Resources.Views);
                    vcNode.Name = Properties.Resources.Views;
                    vcNode.Tag = db.Views;
                    dbNode.Nodes.Add(vcNode);
                    AddDummyNode(vcNode);

                    TreeNode spNode = new TreeNode(Properties.Resources.StoredProcedures);
                    spNode.Name = Properties.Resources.StoredProcedures;
                    spNode.Tag = db.StoredProcedures;
                    dbNode.Nodes.Add(spNode);
                    AddDummyNode(spNode);

                    if (sqlServerSelection.Information.Version.Major >= 9)
                    {
                        TreeNode assyNode = new TreeNode(Properties.Resources.Assemblies);
                        assyNode.Name = Properties.Resources.Assemblies;
                        assyNode.Tag = db.Assemblies;
                        dbNode.Nodes.Add(assyNode);
                        AddDummyNode(assyNode);
                    }
                }
            }
        }

        private static void LoadTreeViewTables(TreeNode node)
        {
            foreach (Table tbl in (TableCollection)node.Tag)
            {
                if (tbl.IsSystemObject == false)
                {
                    TreeNode tNode = new TreeNode(tbl.ToString());
                    tNode.Name = tbl.ToString();
                    tNode.Tag = tbl;
                    node.Nodes.Add(tNode);

                    // Add Columns
                    TreeNode tcNode = new TreeNode(Properties.Resources.Columns);
                    tcNode.Name = Properties.Resources.Columns;
                    tcNode.Tag = tbl.Columns;
                    tNode.Nodes.Add(tcNode);
                    AddDummyNode(tcNode);
                }
            }
        }

        private static void LoadTreeViewViews(TreeNode node)
        {
            foreach (Microsoft.SqlServer.Management.Smo.View v in (ViewCollection)node.Tag)
            {
                if (v.IsSystemObject == false)
                {
                    TreeNode tNode = new TreeNode(v.ToString());
                    tNode.Name = v.ToString();
                    tNode.Tag = v;
                    node.Nodes.Add(tNode);

                    // Add Columns
                    TreeNode tcNode = new TreeNode(Properties.Resources.Columns);
                    tcNode.Name = Properties.Resources.Columns;
                    tcNode.Tag = v.Columns;
                    tNode.Nodes.Add(tcNode);
                    AddDummyNode(tcNode);
                }
            }
        }

        private static void LoadTreeViewColumns(TreeNode node)
        {
            foreach (Column col in (ColumnCollection)node.Tag)
            {
                TreeNode tNode = new TreeNode(col.ToString());
                tNode.Name = col.ToString();
                tNode.Tag = col;
                node.Nodes.Add(tNode);
            }
        }

        private static void LoadTreeViewStoredProcedures(TreeNode node)
        {
            foreach (StoredProcedure sp in (StoredProcedureCollection)node.Tag)
            {
                if (sp.IsSystemObject == false)
                {
                    TreeNode tNode = new TreeNode(sp.ToString());
                    tNode.Name = sp.ToString();
                    tNode.Tag = sp;
                    node.Nodes.Add(tNode);
                }
            }
        }

        private static void LoadTreeViewAssemblies(TreeNode node)
        {
            foreach (SqlAssembly assy in (SqlAssemblyCollection)node.Tag)
            {
                TreeNode tNode = new TreeNode(assy.ToString());
                tNode.Name = assy.ToString();
                tNode.Tag = assy;
                node.Nodes.Add(tNode);
            }
        }

        // Adds a dummy node, so we can expand a node without querying
        // whether there are any items
        private static void AddDummyNode(TreeNode node)
        {
            if (node.Nodes.Count != 0)
            {
                return;
            }

            node.Nodes.Add(Properties.Resources.DummyNode);
            node.Nodes[0].Name = Properties.Resources.DummyNode;
        }

        // Double click on a listview item causes the tree to be synced.
        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count == 0)
            {
                return;
            }

            object obj = ListView.SelectedItems[0].Tag;
            if ((obj is SqlSmoObject) == false)
            {
                return;
            }

            TreeNode[] found = root.Nodes.Find(obj.ToString(), true);

            // If new objects are added the names may clash
            if (found.Length > 1)
            {
                return;
            }

            SqlServerTreeView.SelectedNode = found[0];
            SqlServerTreeView.Focus();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void MainForm_Load(object sender, EventArgs e)
        {
            ServerConnection ServerConn = new ServerConnection();
            ServerConnect scForm;
            DialogResult dr;

            try
            {
                // Display the main window first
                this.Show();
                Application.DoEvents();

                ServerConn = new ServerConnection();
                scForm = new ServerConnect(ServerConn);
                dr = scForm.ShowDialog(this);
                if ((dr == DialogResult.OK)
                    && (ServerConn.SqlConnectionObject.State == ConnectionState.Open))
                {
                    this.sqlServerSelection = new Server(ServerConn);
                    if (this.sqlServerSelection != null)
                    {
                        this.Text = Properties.Resources.AppTitle
                            + this.sqlServerSelection.Name;

                        // Refresh database list
                        ConnectToServer(ServerConn.ServerInstance);
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ListView.SelectedItems.Count == 0)
            {
                return;
            }

            object selectedObject = this.ListView.SelectedItems[0].Tag;
            IScriptable scriptableObject = selectedObject as IScriptable;
            if (scriptableObject != null)
            {
                StringCollection strings = scriptableObject.Script();
                TextOutputForm frm = new TextOutputForm(strings);
                frm.ShowDialog(this);
            }
        }

        private void scriptwithDependenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ListView.SelectedItems.Count == 0)
            {
                return;
            }

            object selectedObject = this.ListView.SelectedItems[0].Tag;
            IScriptable scriptableObject = selectedObject as IScriptable;
            if (scriptableObject != null)
            {
                ScriptingForm frm = new ScriptingForm(this.sqlServerSelection,
                    (SqlSmoObject)selectedObject);
                frm.ShowDialog(this);
            }
        }

        private void dependenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ListView.SelectedItems.Count == 0)
            {
                return;
            }

            object selectedObject = this.ListView.SelectedItems[0].Tag;
            IScriptable scriptableObject = selectedObject as IScriptable;
            if (scriptableObject != null
                && !(selectedObject is Database))
            {
                SqlSmoObject[] smoObjects = new SqlSmoObject[1];
                smoObjects[0] = (SqlSmoObject)selectedObject;

                Scripter scripter = new Scripter(this.sqlServerSelection);
                DependenciesForm frm = new DependenciesForm(this.sqlServerSelection,
                    scripter.DiscoverDependencies(smoObjects, DependencyType.Parents));

                frm.ShowDialog(this);
            }
        }

        private void ListViewContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripMenuItem mnuItem in this.ListViewContextMenuStrip.Items)
            {
                mnuItem.Enabled = false;
            }

            if (this.ListView.SelectedItems.Count == 0)
            {
                return;
            }

            SqlSmoObject smoObject = (SqlSmoObject)this.ListView.SelectedItems[0].Tag;
            IScriptable scriptableObject = smoObject as IScriptable;
            if (scriptableObject != null)
            {
                this.scriptToolStripMenuItem.Enabled = true;
            }

            if (scriptableObject != null
                && !(smoObject is Database))
            {
                this.scriptwithDependenciesToolStripMenuItem.Enabled = true;
                this.dependenciesToolStripMenuItem.Enabled = true;
            }
        }
    }

}