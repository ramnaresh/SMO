/*============================================================================
  File:    ManageDatabases.cs 

  Summary: Implements a sample SMO Manage Databases utility in C#.

  Date:    June 06, 2005
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
using System.Data.SqlClient;

// SMO namespaces
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class ManageDatabases : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public ManageDatabases()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load the server connect dialog and display the databases 
        /// after connecting to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageDatabases_Load(object sender, System.EventArgs e)
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
                (ServerConn.IsOpen == true))
            {
                SqlServerSelection = new Server(ServerConn);
                if (SqlServerSelection != null)
                {
                    this.Text = Properties.Resources.AppTitle + SqlServerSelection.Name;

                    // Refresh database list
                    ShowDatabases(false);
                }
            }
            else
            {
                this.Close();
            }

            UpdateControls();
        }

        /// <summary>
        /// Close the server connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageDatabases_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SqlServerSelection != null)
            {
                if (SqlServerSelection.ConnectionContext.IsOpen == true)
                {
                    SqlServerSelection.ConnectionContext.Disconnect();
                }
            }
        }

        /// <summary>
        /// Create a new database based on user input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateButton_Click(object sender, System.EventArgs e)
        {
            // Create the database
            Cursor csr = null;
            String sDatabaseName;
            Database db;
            FileGroup fg;
            DataFile df;
            LogFile lf;
            ListViewItem DatabaseListViewItem;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Get the name of the new database
                sDatabaseName = NewDatabaseTextBox.Text;

                // Check for new non-zero length name
                if (sDatabaseName.Length == 0)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox();
                    emb.Text = Properties.Resources.NoDatabaseName;
                    emb.Show(this);

                    return;
                }

                // Ensure we have the current list of databases to check.
                SqlServerSelection.Databases.Refresh();

                // Refresh database list
                ShowDatabases(false);

                // Is database name new and unique?
                if (SqlServerSelection.Databases.Contains(sDatabaseName))
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox();
                    emb.Text = Properties.Resources.DuplicateDatabaseName;
                    emb.Show(this);

                    return;
                }

                // Instantiate a new database object
                db = new Database(SqlServerSelection, sDatabaseName);

                // This may also be accomplished like so:
                // db = new Database();
                // db.Parent = SqlServerSelection;
                // db.Name = sDatabaseName;

                // Create a new file group named PRIMARY
                fg = new FileGroup(db, @"PRIMARY");

                // Create a new data file and add it to the file group's Files collection
                // Give the data file a physical filename using the master database path of the server
                df = new DataFile(fg, sDatabaseName + @"_Data0",
                    SqlServerSelection.Information.MasterDBPath + @"\"
                    + sDatabaseName + @"_Data0" + @".mdf");

                // Set the growth type to KB
                df.GrowthType = FileGrowthType.KB;

                // Set the growth size in KB
                df.Growth = 1024;

                // Set initial size in KB (optional)
                df.Size = 10240;

                // Set the maximum size in KB
                df.MaxSize = 20480;

                // Add file to file group
                fg.Files.Add(df);

                // Create a new data file and add it to the file group's Files collection
                // Give the data file a physical filename using the master database path of the server
                df = new DataFile(fg, sDatabaseName + @"_Data1",
                    SqlServerSelection.Information.MasterDBPath + @"\"
                    + sDatabaseName + @"_Data1" + @".ndf");

                // Set the growth type to KB
                df.GrowthType = FileGrowthType.KB;

                // Set the growth size in KB
                df.Growth = 1024;

                // Set initial size in KB (optional)
                df.Size = 2048;

                // Set the maximum size in KB
                df.MaxSize = 8192;

                // Add file to file group
                fg.Files.Add(df);

                // Add the new file group to the database's FileGroups collection
                db.FileGroups.Add(fg);

                // Create a new file group named SECONDARY
                fg = new FileGroup(db, @"SECONDARY");

                // Create a new data file and add it to the file group's Files collection
                // Give the data file a physical filename using the master database path of the server
                df = new DataFile(fg, sDatabaseName + @"_Data2",
                    SqlServerSelection.Information.MasterDBPath + @"\"
                    + sDatabaseName + @"_Data2" + @".ndf");

                // Set the growth type to KB
                df.GrowthType = FileGrowthType.KB;

                // Set the growth size in KB
                df.Growth = 512;

                // Set initial size in KB (optional)
                df.Size = 1024;

                // Set the maximum size in KB
                df.MaxSize = 4096;

                // Add file to file group
                fg.Files.Add(df);

                // Create a new data file and add it to the file group's Files collection
                // Give the data file a physical filename using the master database path
                df = new DataFile(fg, sDatabaseName + @"_Data3",
                    SqlServerSelection.Information.MasterDBPath + @"\"
                    + sDatabaseName + @"_Data3" + @".ndf");

                // Set the growth type to KB
                df.GrowthType = FileGrowthType.KB;

                // Set the growth size in KB
                df.Growth = 512; // In KB

                // Set initial size in KB (optional)
                df.Size = 1024; // Set initial size in KB (optional)

                // Set the maximum size in KB
                df.MaxSize = 4096;

                // Add file to file group
                fg.Files.Add(df);

                // Add the new file group to the database's FileGroups collection
                db.FileGroups.Add(fg);

                // Define the database transaction log.
                lf = new LogFile(db, sDatabaseName + @"_Log",
                    SqlServerSelection.Information.MasterDBPath + @"\" + sDatabaseName +
                    @"_Log" + @".ldf");

                // Set the growth type to KB
                lf.GrowthType = FileGrowthType.KB;

                // Set the growth size in KB
                lf.Growth = 1024; // In KB

                // Set initial size in KB (optional)
                lf.Size = 2048;  // Set initial size in KB (optional)

                // Set the maximum size in KB
                lf.MaxSize = 8192;  // In KB

                // Add file to file group
                db.LogFiles.Add(lf);

                // Create the database as defined.
                db.Create();

                // Refresh database list
                ShowDatabases(false);

                // Find and select the database just created
                DatabaseListViewItem =
                    DatabasesListView.FindItemWithText(sDatabaseName);
                DatabaseListViewItem.Selected = true;
                DatabaseListViewItem.EnsureVisible();
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                // Clean up.
                db = null;
                fg = null;
                df = null;
                lf = null;

                UpdateControls();

                this.Cursor = csr;  // Restore the original cursor
            }
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            String sDatabaseName;
            Cursor csr = null;
            Database db;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Use the selected database as the one to be deleted
                sDatabaseName = DatabasesListView.SelectedItems[0].Text;

                // Drop (Delete) the database
                db = SqlServerSelection.Databases[sDatabaseName];
                if (db != null)
                {
                    // Are you sure?  Default to No.
                    if (System.Windows.Forms.MessageBox.Show(this, string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReallyDrop, db.Name), this.Text,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2, 0) == DialogResult.No)
                    {
                        return;
                    }

                    db.Drop();
                    sbrStatus.Text = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.DatabaseDeleted, sDatabaseName);
                }

                // Refresh database list
                ShowDatabases(false);
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                UpdateControls();
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        private void ShowServerMessages_CheckedChangedCheckBox(object sender,
            System.EventArgs e)
        {
            if (ShowServerMessagesCheckBox.CheckState == CheckState.Checked)
            {
                SqlServerSelection.ConnectionContext.InfoMessage += new
                    SqlInfoMessageEventHandler(SqlInfoMessage);
                SqlServerSelection.ConnectionContext.ServerMessage += new
                    ServerMessageEventHandler(ServerMessage);
            }
            else
            {
                SqlServerSelection.ConnectionContext.InfoMessage -= new
                    SqlInfoMessageEventHandler(SqlInfoMessage);
                SqlServerSelection.ConnectionContext.ServerMessage -= new
                    ServerMessageEventHandler(ServerMessage);
            }
        }

        private void SqlInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            EventLogTextBox.AppendText(Properties.Resources.SqlInfoMessage 
                + e.ToString() + Environment.NewLine);
        }

        private void ServerMessage(object sender, ServerMessageEventArgs e)
        {
            EventLogTextBox.AppendText(Properties.Resources.ServerMessage 
                + e.ToString() + Environment.NewLine);
        }

        private void ServerStatementExecuted(object sender,
            StatementEventArgs e)
        {
            String sTmp = (e.ToString()).Replace("\n", Environment.NewLine);

            EventLogTextBox.AppendText(Properties.Resources.SqlStatementExecuted 
                + sTmp + Environment.NewLine);
        }

        private void ShowDatabases(bool selectDefault)
        {
            // Show the current databases on the server
            ListViewItem DatabaseListViewItem;
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear control
                DatabasesListView.Items.Clear();

                // Limit the properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Database),
                    new String[] { "Name", "Status", "CreateDate", "Size", 
                        "SpaceAvailable", "CompatibilityLevel" });

                foreach (Database db in SqlServerSelection.Databases)
                {
                    if (db.IsSystemObject == false && db.IsAccessible == true)
                    {
                        DatabaseListViewItem
                            = DatabasesListView.Items.Add(db.Name);
                        if ((db.Status & DatabaseStatus.Normal)
                            == DatabaseStatus.Normal)
                        {
                            DatabaseListViewItem.SubItems.Add(
                                db.CreateDate.ToString(
                                System.Globalization.CultureInfo.InvariantCulture));
                            DatabaseListViewItem.SubItems.Add(
                                db.Size.ToString(
                                System.Globalization.CultureInfo.InvariantCulture)
                                + " MB");
                            DatabaseListViewItem.SubItems.Add(
                                (db.SpaceAvailable / 1000.0).ToString(
                                System.Globalization.CultureInfo.InvariantCulture)
                                + " MB");
                            DatabaseListViewItem.SubItems.Add(
                                db.CompatibilityLevel.ToString());
                        }
                    }
                }

                if ((selectDefault == true) &&
                    (DatabasesListView.Items.Count > 0))
                {
                    DatabasesListView.Items[0].Selected = true;
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

        private void Databases_SelectedIndexChangedListView(System.Object
            sender, System.EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (DatabasesListView.SelectedItems.Count > 0)
            {
                DeleteButton.Enabled = true;
            }
            else
            {
                DeleteButton.Enabled = false;
            }

            if (NewDatabaseTextBox.Text.Length > 0)
            {
                CreateButton.Enabled = true;
            }
            else
            {
                CreateButton.Enabled = false;
            }
        }

        private void ShowSqlStatements_CheckedChangedCheckBox(object sender,
            System.EventArgs e)
        {
            if (ShowSqlStatementsCheckBox.CheckState == CheckState.Checked)
            {
                SqlServerSelection.ConnectionContext.StatementExecuted += new
                    StatementEventHandler(ServerStatementExecuted);
            }
            else
            {
                SqlServerSelection.ConnectionContext.StatementExecuted -= new
                    StatementEventHandler(ServerStatementExecuted);
            }
        }

        private void NewDatabaseTextBox_TextChanged(object sender, System.EventArgs e)
        {
            UpdateControls();
        }
    }
}