/*============================================================================
  File:    VerifyBackup.cs 

  Summary: Implements a sample SMO backup verification utility in C#.

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

// SMO namespaces
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class VerifyBackup : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;
        private BackupDeviceCollection BackupDeviceList;
        private Restore sqlRestore;

        public VerifyBackup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Display the main window and get the user's server selection when 
        /// the form is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyBackup_Load(object sender, System.EventArgs e)
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

                    // Refresh device list
                    GetBackupDevicesList();
                }
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Close the SQL Server connection when the form closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyBackup_FormClosed(object sender, System.EventArgs e)
        {
            if (SqlServerSelection != null)
            {
                if (SqlServerSelection.ConnectionContext.SqlConnectionObject.State ==
                    ConnectionState.Open)
                {
                    SqlServerSelection.ConnectionContext.Disconnect();
                }
            }
        }

        /// <summary>
        /// Handle the selection change in the backup device combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupDeviceComboBox_SelectedIndexChanged(object sender,
            System.EventArgs e)
        {
            if (BackupDeviceComboBox.SelectedIndex >= 0)
            {
                BackupContentsListView.Items.Clear();
                GetBackupDeviceInfo(BackupDeviceComboBox.Text);
            }

            UpdateButtons();
        }

        /// <summary>
        /// Handle the user changing the selected item in the listview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupContentsListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateButtons();
        }

        /// <summary>
        /// Handle the VerifyButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyButton_Click(object sender, System.EventArgs e)
        {
            // Verify the backup set
            VerifyBackupSet(BackupDeviceComboBox.Text);
        }

        /// <summary>
        /// Get the contents of the backup device.
        /// </summary>
        /// <param name="BackupDeviceName"></param>
        private void GetBackupDeviceContents(string BackupDeviceName)
        {
            Cursor csr = null;
            DataTable dataTable;
            ListViewItem DeviceListViewItem;
            BackupDeviceItem backupDeviceItem;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Create the restore object
                sqlRestore = new Restore();

                // Create a file backup device
                backupDeviceItem = new BackupDeviceItem(
                    BackupDeviceName, DeviceType.LogicalDevice);

                // Add the backup device to the restore object
                sqlRestore.Devices.Add(backupDeviceItem);

                // Get the backup header information
                dataTable = sqlRestore.ReadBackupHeader(SqlServerSelection);

                // Create the columns in the listview
                BackupContentsListView.Columns.Clear();
                foreach (DataColumn dtCol in dataTable.Columns)
                {
                    BackupContentsListView.Columns.Add(dtCol.ColumnName, 100,
                        HorizontalAlignment.Left);
                }

                // Populate the listview
                BackupContentsListView.Items.Clear();
                for (int rowCount = 0; rowCount < dataTable.Rows.Count; rowCount++)
                {
                    DeviceListViewItem = BackupContentsListView.Items.Add(
                        dataTable.Rows[rowCount][0].ToString());
                    for (int columnCount = 1; columnCount < dataTable.Columns.Count; columnCount++)
                    {
                        DeviceListViewItem.SubItems.Add(
                            dataTable.Rows[rowCount][columnCount].ToString());
                    }
                }

                // Select the first item in the list
                if (BackupContentsListView.Items.Count > 0)
                {
                    BackupContentsListView.Items[0].Selected = true;
                    BackupContentsListView.Items[0].EnsureVisible();
                }
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            catch (ExecutionFailureException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        /// <summary>
        /// Get list of backup devices and add them to the combobox.
        /// </summary>
        private void GetBackupDevicesList()
        {
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear controls
                BackupDeviceComboBox.Items.Clear();

                // Limit the properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(BackupDevice),
                    new String[] { "Name", "BackupDeviceType", 
                        "PhysicalLocation" });

                // Get the appropriate BackupDevice object from the BackupDevices
                // collection of a connected SQLServer object.
                BackupDeviceList = SqlServerSelection.BackupDevices;

                // Get devices
                foreach (BackupDevice bkupDevice in BackupDeviceList)
                {
                    // Add device name to remove combo box
                    BackupDeviceComboBox.Items.Add(bkupDevice.Name);
                }

                if (BackupDeviceComboBox.Items.Count > 0)
                {
                    BackupDeviceComboBox.SelectedIndex = 0;
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

        /// <summary>
        /// Get information for the selected backup device.
        /// </summary>
        /// <param name="BackupDeviceName"></param>
        private void GetBackupDeviceInfo(string BackupDeviceName)
        {
            BackupDevice bkupDevice;

            // Get the appropriate BackupDevice object from the BackupDevices
            // collection of a connected SQLServer object.
            BackupDeviceList = SqlServerSelection.BackupDevices;

            // Get selected device
            bkupDevice = BackupDeviceList[BackupDeviceName];

            DeviceTypeLabel.Text = bkupDevice.BackupDeviceType.ToString();
            StatusLabel.Text = bkupDevice.State.ToString();
            LocationLabel.Text = bkupDevice.PhysicalLocation;
        }

        /// <summary>
        /// Verify the selected backup set.
        /// </summary>
        /// <param name="BackupDeviceName"></param>
        private void VerifyBackupSet(string BackupDeviceName)
        {
            Cursor csr = null;
            Boolean verified;
            BackupDeviceItem backupDeviceItem;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Create a Restore object
                // Fire any events
                sqlRestore = new Restore();

                // Create a file backup device
                backupDeviceItem = new BackupDeviceItem(
                    BackupDeviceName, DeviceType.LogicalDevice);

                // Add the backup device to the restore object
                sqlRestore.Devices.Add(backupDeviceItem);

                // Call the SQLVerify method of the Restore object using the SQLServer object
                verified = sqlRestore.SqlVerify(SqlServerSelection, true);

                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.VerifiedResult,
                    verified.ToString(System.Globalization.CultureInfo.InvariantCulture));
                emb.Show(this);

                return;
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                sqlRestore = null;
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        /// <summary>
        /// Handle the ReadHeaderButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadHeaderButton_Click(object sender, System.EventArgs e)
        {
            GetBackupDeviceContents(BackupDeviceComboBox.Text);
        }

        /// <summary>
        /// Update the buttons based on the whether objects are selected or not.
        /// </summary>
        private void UpdateButtons()
        {
            if (BackupDeviceComboBox.SelectedIndex >= 0)
            {
                ReadHeaderButton.Enabled = true;
            }
            else
            {
                ReadHeaderButton.Enabled = false;
            }

            if (BackupContentsListView.SelectedItems.Count > 0)
            {
                VerifyButton.Enabled = true;
            }
            else
            {
                VerifyButton.Enabled = false;
            }
        }
    }
}