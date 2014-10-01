/*============================================================================
  File:    ScriptTable.cs

  Summary: Implements a sample SMO table scripting utility in C#.

  Date:    January 25, 2005
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
using System.Collections.Specialized;
using System.Text;

// SMO namespaces
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class ScriptTable : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public ScriptTable()
        {
            InitializeComponent();
        }

        private void ScriptTable_Load(object sender, System.EventArgs e)
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

        private void ScriptTable_FormClosed(object sender, System.EventArgs e)
        {
            if (SqlServerSelection != null)
            {
                if (SqlServerSelection.ConnectionContext.SqlConnectionObject
                    .State == ConnectionState.Open)
                {
                    SqlServerSelection.ConnectionContext.Disconnect();
                }
            }
        }

        private void ScriptTableButton_Click(System.Object sender,
            System.EventArgs e)
        {
            Cursor csr = null;
            Database db;
            StringCollection strColl;
            Scripter scrptr;
            SqlSmoObject[] smoObjects;
            Table tbl;
            Int32 count;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Use the selected database
                db = (Database)DatabasesComboBox.SelectedItem;
                if (db.Name.Length == 0)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox();
                    emb.Text = Properties.Resources.NoDatabaseSelected;
                    emb.Show(this);

                    return;
                }

                // Create scripter object
                scrptr = new Scripter(SqlServerSelection);
                if (ScriptDropCheckBox.CheckState == CheckState.Checked)
                {
                    scrptr.Options.ScriptDrops = true;
                }
                else
                {
                    scrptr.Options.ScriptDrops = false;
                }

                scrptr.DiscoveryProgress +=
                    new ProgressReportEventHandler(
                    this.ScriptTable_DiscoveryProgressReport);
                scrptr.ScriptingProgress +=
                    new ProgressReportEventHandler(
                    this.ScriptTable_ScriptingProgressReport);
                if (TablesComboBox.SelectedIndex >= 0)
                {
                    // Get selected table
                    smoObjects = new SqlSmoObject[1];
                    tbl = (Table)TablesComboBox.SelectedItem;
                    if (tbl.IsSystemObject == false)
                    {
                        smoObjects[0] = tbl;
                    }

                    if (DependenciesCheckBox.CheckState == CheckState.Checked)
                    {
                        scrptr.Options.WithDependencies = true;
                    }
                    else
                    {
                        scrptr.Options.WithDependencies = false;
                    }

                    strColl = scrptr.Script(smoObjects);

                    // Clear control
                    ScriptTextBox.Clear();
                    count = 0;
                    foreach (String str in strColl)
                    {
                        count++;
                        sbrStatus.Text = string.Format(
                            System.Globalization.CultureInfo.InvariantCulture,
                            Properties.Resources.AppendingScript, count,
                            strColl.Count);
                        ScriptTextBox.AppendText(str);
                        ScriptTextBox.AppendText(Properties.Resources.Go);
                    }
                }
                else
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox();
                    emb.Text = Properties.Resources.ChooseTable;
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
                // Clean up
                sbrStatus.Text = Properties.Resources.Done;

                // Restore the original cursor
                this.Cursor = csr;
            }
        }

        private void ShowDatabases(bool selectDefault)
        {
            // Show the current databases on the server
            try
            {
                // Clear control
                DatabasesComboBox.Items.Clear();

                // Limit the properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Database),
                    new String[] { "Name", "IsSystemObject", "IsAccessible" });

                // Add database objects to combobox; the default ToString 
                // will display the database name
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

                // Limit the properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Table),
                    new String[] { "Name", "CreateDate", "IsSystemObject" });

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

        private void Databases_SelectedIndexChangedComboBox(System.Object
            sender, System.EventArgs e)
        {
            ShowTables();
            if (DatabasesComboBox.SelectedIndex >= 0)
            {
                ScriptTableButton.Enabled = true;
            }
            else
            {
                ScriptTableButton.Enabled = false;
            }
        }

        private void ScriptTable_DiscoveryProgressReport(System.Object sender,
            ProgressReportEventArgs e)
        {
            sbrStatus.Text = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                Properties.Resources.Discovering, e.TotalCount, e.Total);
            sbrStatus.Refresh();
        }

        private void ScriptTable_ScriptingProgressReport(System.Object sender,
            ProgressReportEventArgs e)
        {
            sbrStatus.Text = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                Properties.Resources.Scripting,
                e.TotalCount, e.Total);
            sbrStatus.Refresh();
        }
    }
}