//============================================================================
//  File:    DependencyExplorer.cs
//
//  Summary: Implements a sample SMO dependency explorer utility in C#.
//
//  Date:    June 06, 2005
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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Globalization;
using Microsoft.SqlServer.MessageBox;

namespace Microsoft.Samples.SqlServer
{
    public partial class DependencyExplorer : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public delegate void RefreshDatabaseTables();
        private RefreshDatabaseTables refreshDbTables;
        private Database databaseLast;

        public DependencyExplorer()
        {
            InitializeComponent();
        }

        private void DependencyExplorer_Load(object sender, EventArgs e)
        {
            this.DatabasesComboBox.Enabled = false;
            this.DatabasesComboBox.DisplayMember = Properties.Resources.Name;
            this.DatabasesComboBox.Text = Properties.Resources.SelectDatabase;

            this.TableListView.View = System.Windows.Forms.View.Details;
            this.TableListView.Columns.Add(Properties.Resources.Name, 120, HorizontalAlignment.Left);
            this.TableListView.Columns.Add(Properties.Resources.Schema, 120, HorizontalAlignment.Left);
            this.TableListView.Columns.Add(Properties.Resources.CreateDate, 120, HorizontalAlignment.Left);
            this.TableListView.Columns.Add(Properties.Resources.RowCount, 120, HorizontalAlignment.Right);
            this.TableListView.Columns.Add(Properties.Resources.DataSize, 120, HorizontalAlignment.Right);
            this.TableListView.Columns.Add(Properties.Resources.Urn, 120, HorizontalAlignment.Left);
            this.TableListView.GridLines = true;
            this.TableListView.FullRowSelect = true;
            this.TableListView.Items.Clear();

            this.StatusBar.Text = Properties.Resources.NotConnected;

            this.ConnectToServer();

            refreshDbTables = new RefreshDatabaseTables(RefreshTables);
        }

        private void DependencyExplorer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database database;

            // Get selected database from DatabaseComboBox
            database = (Database)(this.DatabasesComboBox.SelectedItem);

            // Stop receiving events
            if (database != null)
            {
                database.Events.StopEvents();

                // Unsubscribe to table events
                database.Events.UnsubscribeAllEvents();
            }

            SqlServerSelection = null;
            refreshDbTables = null;
        }

        private void MenuItem5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConnectToServer()
        {
            ServerConnection ServerConn;
            ServerConnect scForm;
            DialogResult dr;

            // Display the main window first
            this.Show();
            Application.DoEvents();

            // Load and display the server selection dialog
            ServerConn = new ServerConnection();

            // Set Application name
            ServerConn.ApplicationName = Application.ProductName;

            scForm = new ServerConnect(ServerConn);
            dr = scForm.ShowDialog(this);
            if (dr == DialogResult.OK &
                ServerConn.SqlConnectionObject.State == ConnectionState.Open)
            {
                this.SqlServerSelection = new Server(ServerConn);
                if (SqlServerSelection != null)
                {
                    this.Text = Properties.Resources.AppTitle + SqlServerSelection.Name;

                    // Refresh database list
                    ShowDatabases(true);

                    // Show server information on StatusBar
                    this.StatusBar.Text = SqlServerSelection.Name + " "
                        + SqlServerSelection.Information.VersionString + " "
                        + SqlServerSelection.Information.ProductLevel;
                }
            }

            scForm = null;
        }

        private void ShowDatabases(Boolean selectDefault)
        {
            // Show the current databases on the server
            Cursor csr = null;

            try
            {
                csr = this.Cursor; // Save the old cursor
                this.Cursor = Cursors.WaitCursor; // Display the waiting cursor

                DatabasesComboBox.Enabled = false;

                // Clear control
                DatabasesComboBox.Items.Clear();

                // Limit the properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Database),
                    new String[] { "Name", "IsSystemObject", "IsAccessible" });

                // Add database objects to combobox
                // The default ToString() will display the database name in the list
                foreach (Database db in SqlServerSelection.Databases)
                {
                    if (db.IsSystemObject == false && db.IsAccessible == true)
                    {
                        DatabasesComboBox.Items.Add(db);
                    }
                }

                if (selectDefault == true && DatabasesComboBox.Items.Count > 0)
                {
                    DatabasesComboBox.SelectedIndex = 0;
                }

                DatabasesComboBox.Enabled = true;
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = csr; // Restore the original cursor
            }
        }


        #region "Connect"
        private void ConnectMenuItem_Click(object sender, EventArgs e)
        {
            // Clear controls and apply defaults
            this.DatabasesComboBox.Items.Clear();
            this.TableListView.Items.Clear();
            this.DatabasesComboBox.Enabled = false;
            this.DatabasesComboBox.Text = Properties.Resources.SelectDatabase;
            this.StatusBar.Text = Properties.Resources.NotConnected;

            this.ConnectToServer();

            // Enable combo box
            this.DatabasesComboBox.Enabled = true;
        }
        #endregion

        #region "Select Database"
        private void DatabasesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database database;

            // Get selected database from DatabaseComboBox
            database = (Database)(this.DatabasesComboBox.SelectedItem);

            // Refresh the table list normally
            this.RefreshTables();

            if (database != null)
            {
                this.SetDatabaseEventHandler(database);
            }
        }

        private void AddTableItem(Table table)
        {
            ListViewItem item = new ListViewItem(table.Name);

            item.SubItems.Add(table.Schema);
            item.SubItems.Add(table.CreateDate.ToString(DateTimeFormatInfo.CurrentInfo));
            item.SubItems.Add(table.RowCount.ToString(DateTimeFormatInfo.InvariantInfo));
            item.SubItems.Add(table.DataSpaceUsed.ToString(DateTimeFormatInfo.InvariantInfo));
            item.SubItems.Add(table.Urn.ToString());

            // Add a reference to the table for convenience
            item.Tag = table;

            // Add the item to the list
            this.TableListView.Items.Add(item);
        }
        #endregion

        #region "Script Without Dependencies"
        private void WithoutDependenciesMenuItem_Click(object sender, EventArgs e)
        {
            Table table;
            TextForm form;

            // Just make sure something is selected
            if (this.TableListView.SelectedIndices.Count == 0)
            {
                return;
            }

            // It's the first one as we only allow one selection
            table = (Table)(this.TableListView.SelectedItems[0].Tag);

            form = new TextForm();
            form.DisplayText(table.Script());
            form.Show();
        }
        #endregion

        #region "Script With Dependencies"
        private void WithDependenciesMenuItem_Click(object sender, EventArgs e)
        {
            Table table;

            // Just make sure something is selected
            if (this.TableListView.SelectedIndices.Count == 0)
            {
                return;
            }

            // It's the first one as we only allow one selection
            table = (Table)(this.TableListView.SelectedItems[0].Tag);

            // Show script with ShowText sub
            ShowText(table.Script(ScriptOption.WithDependencies + ScriptOption.Default));
        }
        #endregion

        #region "Capture Mode"
        private void ShowDropDdlmenuItem_Click(object sender, EventArgs e)
        {
            Table table;

            // Just make sure something is selected
            if (this.TableListView.SelectedIndices.Count == 0)
            {
                return;
            }

            // It's the first one as we only allow one selection
            table = (Table)(this.TableListView.SelectedItems[0].Tag);

            // Set execution mode to capture
            table.Parent.Parent.ConnectionContext.SqlExecutionModes
                = SqlExecutionModes.CaptureSql;

            // Drop the table
            table.RebuildIndexes(10);
            table.RecalculateSpaceUsage();
            table.DisableAllIndexes();

            // Reset execution mode
            table.Parent.Parent.ConnectionContext.SqlExecutionModes
                = SqlExecutionModes.ExecuteSql;

            // Display captured text with ShowText sub
            ShowText(table.Parent.Parent.ConnectionContext.CapturedSql.Text);

            // Clear capture buffer
            table.Parent.Parent.ConnectionContext.CapturedSql.Text.Clear();
        }
        #endregion

        #region "Helper Code"
        private static void ShowText(StringCollection sc)
        {
            TextForm form = new TextForm();

            form.DisplayText(sc);
            form.Show();
        }

        private void SetDatabaseEventHandler(Database database)
        {
            DatabaseEventSet databaseEventSet = new DatabaseEventSet();

            if (databaseLast != null)
            {
                // Stop receiving events
                databaseLast.Events.StopEvents();

                // Unsubscribe to table events
                databaseLast.Events.UnsubscribeAllEvents();
            }

            databaseLast = database;

            // Subscribe to table events
            databaseEventSet.CreateTable = true;
            databaseEventSet.DropTable = true;
            databaseEventSet.AlterTable = true;

            database.Events.SubscribeToEvents(databaseEventSet,
                new ServerEventHandler(this.MyEventHandler));
            database.Events.StartEvents();
        }

        private void MyEventHandler(object sender, ServerEventArgs e)
        {
            this.Invoke(refreshDbTables);
        }

        private void RefreshTables()
        {
            Cursor csr = null;
            Database database;

            try
            {
                csr = this.Cursor; // Save the old cursor
                this.Cursor = Cursors.WaitCursor; // Display the waiting cursor

                this.TableListView.BeginUpdate();

                // Clear the list
                this.TableListView.Items.Clear();

                // Get selected database from DatabaseComboBox
                database = (Database)(this.DatabasesComboBox.SelectedItem);

                // Set default initial fields for table
                database.Parent.SetDefaultInitFields(typeof(Table),
                    "Name", "CreateDate", "RowCount", "DataSpaceUsed",
                    "IsSystemObject");

                // Get the updated list of tables
                database.Tables.Refresh();

                // Iterate user Tables and add to ListView Using AddTableItem
                foreach (Table tbl in database.Tables)
                {
                    if (tbl.IsSystemObject == false)
                    {
                        this.AddTableItem(tbl);
                    }
                }

                // Resize columns
                this.TableListView.AutoResizeColumns(
                    ColumnHeaderAutoResizeStyle.ColumnContent);

                // Select the first item
                if (this.TableListView.Items.Count > 0)
                {
                    this.TableListView.Items[0].Selected = true;
                }
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.TableListView.EndUpdate();

                // Restore the original cursor
                this.Cursor = csr;
            }
        }
        #endregion

        #region "Show Dependencies"
        private void ShowDependenciesmenuItem_Click(object sender, EventArgs e)
        {
            Table table;
            DependencyTree deps;
            Scripter scripter;
            SqlSmoObject[] smoObjects = new SqlSmoObject[1];
            DependencyForm form = new DependencyForm();

            // Just make sure something is selected
            if (this.TableListView.SelectedIndices.Count == 0)
            {
                return;
            }

            // It's the first one as we only allow one selection
            table = (Table)(this.TableListView.SelectedItems[0].Tag);

            // Declare and instantiate new Scripter object
            scripter = new Scripter(table.Parent.Parent);

            // Declare array of SqlSmoObjects
            smoObjects[0] = table;

            // Discover dependencies
            deps = scripter.DiscoverDependencies(smoObjects, true);

            // Show dependencies
            form.ShowDependencies(table.Parent.Parent, deps);
            form.Show();
        }
        #endregion

    }
}