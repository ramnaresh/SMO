/*============================================================================
  File:    SqlService.cs 

  Summary: Implements a sample SMO SQL Server service utility in C#.

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
using Microsoft.SqlServer.Management.Smo.Wmi;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class SqlService : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public SqlService()
        {
            InitializeComponent();
        }

        private void SqlService_Load(object sender, System.EventArgs e)
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
                    GetServices();
                }
            }
            else
            {
                this.Close();
            }

            UpdateButtons();
        }

        /// <summary>
        /// Disconnect from the server when form closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SqlService_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
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

        private void Services_SelectedIndexChangedListView(System.Object sender,
            System.EventArgs e)
        {
            UpdateButtons();
        }

        private void RefreshButton_Click(System.Object sender,
            System.EventArgs e)
        {
            GetServices();
            UpdateButtons();
        }

        private void StartButton_Click(System.Object sender,
            System.EventArgs e)
        {
            // Start a stopped service
            Cursor csr = null;
            Service svc;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                svc = GetSelectedService();

                if (svc != null)
                {
                    if (svc.ServiceState == ServiceState.Stopped)
                    {
                        sbrStatus.Text = Properties.Resources.Starting;
                        sbrStatus.Refresh();
                        svc.Start();
                    }

                    // Wait for service state to change
                    WaitServiceStateChange(svc, ServiceState.Running);

                    RefreshService(svc);
                    sbrStatus.Text = Properties.Resources.Ready;
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

        private void WaitServiceStateChange(Service svc, ServiceState ss)
        {
            DateTime StopTime;

            StopTime = DateTime.Now.AddSeconds((double)TimeoutUpDown.Value);
            while ((svc.ServiceState != ss) && (DateTime.Now < StopTime))
            {
                System.Diagnostics.Debug.WriteLine(svc.ServiceState.ToString());
                System.Diagnostics.Debug.WriteLine(StopTime.ToLongTimeString());
                System.Threading.Thread.Sleep(250);
                svc.Refresh();
            }
        }

        private void StopButton_Click(System.Object sender, System.EventArgs e)
        {
            // Stop a running service
            Cursor csr = null;
            Service svc;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                svc = GetSelectedService();

                if (svc != null)
                {
                    if (svc.ServiceState == ServiceState.Running)
                    {
                        sbrStatus.Text = Properties.Resources.Stopping;
                        sbrStatus.Refresh();
                        svc.Stop();
                    }

                    // Wait for service state to change
                    WaitServiceStateChange(svc, ServiceState.Stopped);

                    RefreshService(svc);
                    sbrStatus.Text = Properties.Resources.Ready;
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

        private void PauseButton_Click(System.Object sender,
            System.EventArgs e)
        {
            // Pause a running service
            Cursor csr = null;
            Service svc;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor
                svc = GetSelectedService();
                if (svc != null)
                {
                    if (svc.ServiceState == ServiceState.Running)
                    {
                        sbrStatus.Text = Properties.Resources.Pausing;
                        sbrStatus.Refresh();
                        svc.Pause();
                    }

                    // Wait for service state to change
                    WaitServiceStateChange(svc, ServiceState.Paused);

                    RefreshService(svc);
                    sbrStatus.Text = Properties.Resources.Ready;
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

        private void ResumeButton_Click(System.Object sender,
            System.EventArgs e)
        {
            // Resume a paused service
            Cursor csr = null;
            Service svc;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor
                svc = GetSelectedService();
                if (svc != null)
                {
                    if (svc.ServiceState == ServiceState.Paused)
                    {
                        sbrStatus.Text = Properties.Resources.Resuming;
                        sbrStatus.Refresh();
                        svc.Resume();
                    }

                    // Wait for service state to change
                    WaitServiceStateChange(svc, ServiceState.Running);

                    RefreshService(svc);
                    sbrStatus.Text = Properties.Resources.Ready;
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

        private void GetServices()
        {
            Cursor csr = null;
            ManagedComputer mc;
            ListViewItem ServiceListViewItem;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear control
                ServicesListView.Items.Clear();
                mc = new ManagedComputer(SqlServerSelection.Name);

                foreach (Service svc in mc.Services)
                {
                    ServiceListViewItem = ServicesListView.Items.Add(svc.Name);
                    ServiceListViewItem.SubItems.Add(svc.DisplayName);
                    ServiceListViewItem.SubItems.Add(svc.ServiceState.ToString());
                    ServiceListViewItem.SubItems.Add(svc.StartMode.ToString());
                    ServiceListViewItem.SubItems.Add(svc.ServiceAccount);
                    ServiceListViewItem.SubItems.Add(svc.State.ToString());
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

        private void RefreshService(Service svc)
        {
            // Update the service status field
            try
            {
                svc.Refresh();

                ListViewItem ServiceListViewItem;

                ServiceListViewItem = ServicesListView.SelectedItems[0];
                ServiceListViewItem.SubItems[2].Text = svc.ServiceState.ToString();
                UpdateButtons();
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        private Service GetSelectedService()
        {
            // Return the service selected in the list
            ManagedComputer mc;

            if (ServicesListView.SelectedItems.Count > 0)
            {
                mc = new ManagedComputer(SqlServerSelection.Name);
                return mc.Services[ServicesListView.SelectedItems[0].Text];
            }
            else
            {
                return null;
            }
        }

        private void UpdateButtons()
        {
            // Update all buttons based on the service state
            Service svc = GetSelectedService();

            if (svc != null)
            {
                PauseButton.Enabled =
                    (svc.ServiceState == ServiceState.Running) &&
                    (svc.AcceptsPause);
                ResumeButton.Enabled =
                    (svc.ServiceState == ServiceState.Paused);
                StartButton.Enabled =
                    (svc.ServiceState == ServiceState.Stopped);
                StopButton.Enabled =
                    (svc.ServiceState == ServiceState.Running) &&
                    (svc.AcceptsStop);
            }
            else
            {
                PauseButton.Enabled = false;
                ResumeButton.Enabled = false;
                StartButton.Enabled = false;
                StopButton.Enabled = false;
            }
        }
    }
}