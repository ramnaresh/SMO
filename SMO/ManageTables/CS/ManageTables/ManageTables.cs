/*============================================================================
  File:    ManageTables.cs 

  Summary: Implements a sample SMO Manage Tables utility in C#.

  Date:    April 14, 2005
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

#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

// SMO namespaces
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class ManageTables : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public ManageTables()
        {
            InitializeComponent();
        }

        private void ManageTables_Load(object sender, System.EventArgs e)
        {
            ServerConnection ServerConn;
            ServerConnect scForm;
            DialogResult dr;

            // Display the main window first
            this.Show();
            Application.DoEvents();

            ServerConn = new ServerConnection();
            scForm = new ServerConnect(ServerConn);
            dr = scForm.ShowDialog(this);
            if ((dr == DialogResult.OK) &&
                (ServerConn.SqlConnectionObject.State == ConnectionState.Open))
            {
                SqlServerSelection = new Server(ServerConn);
                if (SqlServerSelection != null)
                {
                    this.Text = Properties.Resources.AppTitle + SqlServerSelection.Name;

                    // Refresh database list
                    ShowDatabases(true);
                }
            }
            else
            {
                this.Close();
            }
        }

        private void ManageTables_FormClosed(object sender, System.EventArgs e)
        {
            if (SqlServerSelection != null)
            {
                if (SqlServerSelection.ConnectionContext.IsOpen == true)
                {
                    SqlServerSelection.ConnectionContext.Disconnect();
                }
            }
        }

        private void Databases_SelectedIndexChangedComboBox(object sender,
            System.EventArgs e)
        {
            ShowTables();
        }

        private void Tables_SelectedIndexChangedComboBox(object sender,
            System.EventArgs e)
        {
            ShowColumns();
            UpdateButtons();
        }

        private void Columns_SelectedIndexChangedListView(System.Object sender,
            System.EventArgs e)
        {
            UpdateButtons();
        }

        private void AddTableButton_Click(object sender, System.EventArgs e)
        {
            Database db;
            Table tbl;
            Column col;
            Index idx;
            Default dflt;
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Show the current tables for the selected database
                db = (Database)DatabasesComboBox.SelectedItem;
                if (db.Tables.Contains(TableNameTextBox.Text) == false)
                {
                    // Create an empty string default
                    dflt = db.Defaults["dfltEmptyString"];
                    if (dflt == null)
                    {
                        dflt = new Default(db, "dfltEmptyString");
                        dflt.TextHeader = "CREATE DEFAULT [dbo].[dfltEmptyString] AS ";
                        dflt.TextBody = @"'';";
                        dflt.Create();
                    }

                    // Create a new table object
                    tbl = new Table(db,
                        TableNameTextBox.Text, db.DefaultSchema);

                    // Add the first column
                    col = new Column(tbl, @"Column1", DataType.Int);
                    tbl.Columns.Add(col);
                    col.Nullable = false;
                    col.Identity = true;
                    col.IdentitySeed = 1;
                    col.IdentityIncrement = 1;

                    // Add the primary key index
                    idx = new Index(tbl, @"PK_" + TableNameTextBox.Text);
                    tbl.Indexes.Add(idx);
                    idx.IndexedColumns.Add(new IndexedColumn(idx, col.Name));
                    idx.IsClustered = true;
                    idx.IsUnique = true;
                    idx.IndexKeyType = IndexKeyType.DriPrimaryKey;

                    // Add the second column
                    col = new Column(tbl, @"Column2", DataType.NVarChar(1024));
                    tbl.Columns.Add(col);
                    col.DataType.MaximumLength = 1024;
                    col.AddDefaultConstraint(null); // Use SQL Server default naming
                    col.DefaultConstraint.Text = Properties.Resources.DefaultConstraintText;
                    col.Nullable = false;

                    // Add the third column
                    col = new Column(tbl, @"Column3", DataType.DateTime);
                    tbl.Columns.Add(col);
                    col.Nullable = false;

                    // Create the table
                    tbl.Create();

                    // Refresh list and select the one we just created
                    ShowTables();

                    // Clear selected items
                    TablesComboBox.SelectedIndex = -1;

                    // Find the table just created
                    TablesComboBox.SelectedIndex = TablesComboBox.FindStringExact(tbl.ToString());
                }
                else
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox();
                    emb.Text = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.TableExists, TableNameTextBox.Text);
                    emb.Show(this);
                }
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        private void DeleteTableButton_Click(object sender, System.EventArgs e)
        {
            Database db;
            Table tbl;
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Show the current tables for the selected database
                db = (Database)DatabasesComboBox.SelectedItem;
                if (TablesComboBox.Items.Count > 0)
                {
                    tbl = db.Tables[((Table)TablesComboBox.SelectedItem).Name,
                        ((Table)TablesComboBox.SelectedItem).Schema];
                    if (tbl != null)
                    {
                        // Are you sure?  Default to No to avoid accidents
                        if (System.Windows.Forms.MessageBox.Show(this, string.Format(
                            System.Globalization.CultureInfo.InvariantCulture,
                            Properties.Resources.ReallyDrop, tbl.ToString()), this.Text,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2, 0) == DialogResult.No)
                        {
                            return;
                        }

                        tbl.Drop();
                    }

                    // Refresh list and select first entry
                    ShowTables();
                    if (TablesComboBox.Items.Count > 0)
                    {
                        TablesComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        private void ShowDatabases(bool selectDefault)
        {
            // Show the current databases on the server
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear control
                DatabasesComboBox.Items.Clear();

                // Limit the database properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Database),
                    new String[] { "Name", "IsSystemObject", "IsAccessible" });

                // Limit the table properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Table),
                    new String[] { "Name", "CreateDate", "IsSystemObject" });

                // Limit the column properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Column), new
                    String[] {"Name", "DataType", "SystemType", "Length", 
                        "NumericPrecision", "NumericScale", 
                        "XmlSchemaNamespace", "XmlSchemaNamespaceSchema", 
                        "DataTypeSchema", "Nullable", "InPrimaryKey"
                });

                // Add database objects to combobox; the default ToString will display the database name
                foreach (Database db in SqlServerSelection.Databases)
                {
                    if (db.IsSystemObject == false && db.IsAccessible == true)
                    {
                        DatabasesComboBox.Items.Add(db);
                    }
                }

                if ((selectDefault == true) &&
                    (DatabasesComboBox.Items.Count > 0))
                {
                    DatabasesComboBox.SelectedIndex = 0;
                }
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        private void ShowTables()
        {
            Database db;
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear the tables list
                TablesComboBox.Items.Clear();

                // Show the current tables for the selected database
                if (DatabasesComboBox.SelectedIndex >= 0)
                {
                    db = (Database)DatabasesComboBox.SelectedItem;

                    foreach (Table tbl in db.Tables)
                    {
                        if (tbl.IsSystemObject == false)
                        {
                            TablesComboBox.Items.Add(tbl);
                        }
                    }

                    // Select the first item in the list
                    if (TablesComboBox.Items.Count > 0)
                    {
                        TablesComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        // Clear the table detail list
                        ColumnsListView.Items.Clear();
                    }
                }

                UpdateButtons();
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        private void ShowColumns()
        {
            Cursor csr = null;
            ListViewItem ColumnListViewItem;
            Table tbl;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Delay rendering until after list filled
                ColumnsListView.BeginUpdate();

                // Clear the columns list
                ColumnsListView.Items.Clear();

                // Show the current columns for the selected table
                if (TablesComboBox.SelectedIndex >= 0)
                {
                    // Get the selected table object
                    tbl = (Table)TablesComboBox.SelectedItem;

                    // Iterate through all the columns to fill list
                    foreach (Column col in tbl.Columns)
                    {
                        ColumnListViewItem =
                            ColumnsListView.Items.Add(col.Name);
                        ColumnListViewItem.SubItems.Add(col.DataType.Name);
                        ColumnListViewItem.SubItems.Add(col.DataType.
                            MaximumLength.ToString(
                            System.Globalization.CultureInfo.InvariantCulture));
                        ColumnListViewItem.SubItems.Add(
                            col.Nullable.ToString());
                        ColumnListViewItem.SubItems.Add(
                            col.InPrimaryKey.ToString());
                    }
                }

                // Now we render the listview
                ColumnsListView.EndUpdate();
                UpdateButtons();
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        private void UpdateButtons()
        {
            AddTableButton.Enabled = (DatabasesComboBox.SelectedIndex >= 0);
            DeleteTableButton.Enabled = (TablesComboBox.SelectedIndex >= 0);
        }

        //private void Label1_Click(object sender, EventArgs e)
        //{

        //}
    }
}