/*============================================================================
  File:    BackupRestore.cs

  Summary: Implements a sample SMO backup and restore utility in C#.

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
using System.IO;

// SMO namespaces
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class BackupRestore : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public BackupRestore()
        {
            InitializeComponent();
        }

        private void GetBackupFileButton_Click(object sender, EventArgs e)
        {
            if (BackupFileTextBox.Text.Length > 0)
            {
                this.openFileDialog1.InitialDirectory
                    = Path.GetDirectoryName(BackupFileTextBox.Text);
                this.openFileDialog1.FileName
                    = Path.GetFileName(BackupFileTextBox.Text);
            }

            if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                BackupFileTextBox.Text = this.openFileDialog1.FileName;
            }
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            Cursor csr = null;
            Backup backup;
            Database db;
            BackupDeviceItem backupDeviceItem;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Get database object from combobox
                db = (Database)DatabasesComboBox.SelectedItem;

                // Backup a complete database to disk
                // Create a new Backup object instance
                backup = new Backup();

                // Backup database action
                backup.Action = BackupActionType.Database;

                // Set backup description
                backup.BackupSetDescription
                    = string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.SampleBackup, db.Name);

                // Set backup name
                backup.BackupSetName
                    = string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.BackupSetName, db.Name);

                // Set database name
                backup.Database = db.Name;

                // Create a file backup device
                backupDeviceItem = new BackupDeviceItem(
                    BackupFileTextBox.Text, DeviceType.File);

                // Add a new backup device
                backup.Devices.Add(backupDeviceItem);

                // Only store this backup in the set
                backup.Initialize = true;

                // Set the media name
                backup.MediaName = Properties.Resources.MediaName;

                // Set the media description
                backup.MediaDescription = Properties.Resources.MediaDescription;

                // Notify this program every 5% 
                backup.PercentCompleteNotification = 5;

                // Set the backup file retention to the current server run value
                backup.RetainDays = db.Parent.Configuration.MediaRetention.RunValue;

                // Skip the tape header, because we are writing to a file
                backup.SkipTapeHeader = true;

                // Unload the tape after the backup completes
                backup.UnloadTapeAfter = true;

                // Add event handler to show progress
                backup.PercentComplete += new PercentCompleteEventHandler(
                    this.ProgressEventHandler);

                // Generate and print script.
                ResultsTextBox.AppendText(Properties.Resources.GeneratedScript);
                ScrollToBottom();

                // Scripting here is strictly for text display purposes.
                String script = backup.Script(SqlServerSelection);

                ResultsTextBox.AppendText(script + Environment.NewLine);
                ScrollToBottom();
                ResultsTextBox.AppendText(Properties.Resources.BackingUp);
                ScrollToBottom();
                UpdateStatus(0);

                // Actual backup starts here.
                backup.SqlBackup(SqlServerSelection);

                ResultsTextBox.AppendText(Properties.Resources.BackupComplete);
                ScrollToBottom();
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

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            // Restore the complete database to disk
            Cursor csr = null;
            Restore restore;
            Database db;
            BackupDeviceItem backupDeviceItem;

            // Are you sure?  Default to No.
            if (System.Windows.Forms.MessageBox.Show(this,
                string.Format(System.Globalization.CultureInfo.InvariantCulture,
                Properties.Resources.ReallyRestore,
                DatabasesComboBox.Text), this.Text, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                0) == DialogResult.No)
            {
                return;
            }

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Get database object from combobox
                db = (Database)DatabasesComboBox.SelectedItem;

                // Create a new Restore object instance
                restore = new Restore();

                // Restore database action
                restore.Action = RestoreActionType.Database;

                // Set database name
                restore.Database = db.Name;

                // Create a file backup device
                backupDeviceItem = new BackupDeviceItem(
                    BackupFileTextBox.Text, DeviceType.File);

                // Add database backup device
                restore.Devices.Add(backupDeviceItem);

                // Notify this program every 5% 
                restore.PercentCompleteNotification = 5;

                // Replace the existing database
                restore.ReplaceDatabase = true;

                // Unload the backup device (tape)
                restore.UnloadTapeAfter = true;

                // add event handler to show progress
                restore.PercentComplete += new PercentCompleteEventHandler(
                    this.ProgressEventHandler);

                // generate and print script
                ResultsTextBox.AppendText(Properties.Resources.GeneratedScript);

                System.Collections.Specialized.StringCollection strColl =
                    restore.Script(SqlServerSelection);

                foreach (string script in strColl)
                {
                    ResultsTextBox.AppendText(script + Environment.NewLine);
                }

                ResultsTextBox.AppendText(Properties.Resources.Restoring);
                UpdateStatus(0);

                // Actual restore begins here
                restore.SqlRestore(SqlServerSelection);
                ResultsTextBox.AppendText(Properties.Resources.RestoreComplete);
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void BackupRestore_Load(object sender, EventArgs e)
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
                if ((dr == DialogResult.OK) &&
                    (ServerConn.SqlConnectionObject.State == ConnectionState.Open))
                {
                    SqlServerSelection = new Server(ServerConn);
                    if (SqlServerSelection != null)
                    {
                        this.Text = Properties.Resources.AppTitle
                            + SqlServerSelection.Name;

                        // Refresh database list
                        ShowDatabases(true);
                    }
                }
                else
                {
                    this.Close();
                }

                if (SqlServerSelection != null)
                {
                    BackupFileTextBox.Text = SqlServerSelection.Settings.
                        BackupDirectory + @"\SmoDemoBackup.bak";
                }
            }
            catch (Exception ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        private void BackupRestore_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SqlServerSelection != null)
            {
                if (SqlServerSelection.ConnectionContext.SqlConnectionObject.State
                    == ConnectionState.Open)
                {
                    SqlServerSelection.ConnectionContext.Disconnect();
                }
            }
        }

        void ProgressEventHandler(object sender, PercentCompleteEventArgs e)
        {
            ResultsTextBox.AppendText(Properties.Resources.ProgressCharacter);
            ScrollToBottom();
            UpdateStatus(e.Percent);
        }

        private void UpdateStatus(int pct)
        {
            statusBar1.Text = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                Properties.Resources.CompletedPercent, pct);
            statusBar1.Refresh();
        }

        private void ScrollToBottom()
        {
            ResultsTextBox.Select();
            ResultsTextBox.SelectionStart = ResultsTextBox.Text.Length;
            ResultsTextBox.ScrollToCaret();
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

                // Limit the properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Database),
                    new String[] { "Name", "IsSystemObject", "IsAccessible" });

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
    }
}