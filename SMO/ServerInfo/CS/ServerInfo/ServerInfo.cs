/*============================================================================
  File:    ServerInfo.cs 

  Summary: Implements a sample SMO server information utility in C#.

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
    partial class ServerInfo : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public ServerInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load main form and display the server connect dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerInfo_Load(object sender, EventArgs e)
        {
            ServerConnection ServerConn;
            ServerConnect scForm;
            DialogResult dr;

            // Display the main window first
            this.Show();
            Application.DoEvents();

            // Create the server connection
            ServerConn = new ServerConnection();

            // Create the server connect dialog
            scForm = new ServerConnect(ServerConn);

            // Display the server connect dialog
            dr = scForm.ShowDialog(this);
            if ((dr == DialogResult.OK) &&
                (ServerConn.SqlConnectionObject.State == ConnectionState.Open))
            {
                SqlServerSelection = new Server(ServerConn);
                if (SqlServerSelection != null)
                {
                    this.Text = Properties.Resources.AppTitle + SqlServerSelection.Name;

                    // Refresh server information
                    ShowServerInformation();
                }
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Disconnect from the server when form closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerInfo_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
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
        /// Display the server information in the listview.
        /// </summary>
        private void ShowServerInformation()
        {
            ListViewItem ServerListViewItem;

            try
            {
                ServerListView.BeginUpdate();

                // Clear control
                ServerListView.Items.Clear();

                // Initialize the server settings
                SqlServerSelection.Settings.Initialize(true);

                // Initialize the server information settings
                SqlServerSelection.Information.Initialize(true);

                // Initialize the server
                SqlServerSelection.Initialize(true);

                // Iterate through all the properties and add each one to the list
                foreach (Property prop in SqlServerSelection.Settings.
                    Properties)
                {
                    ServerListViewItem = ServerListView.Items.Add(prop.
                        Name);
                    ServerListViewItem.SubItems.Add(prop.Value == null ?
                        string.Empty : prop.Value.ToString());
                }

                // Iterate through all the properties and add each one to the list
                foreach (Property prop in SqlServerSelection.Properties)
                {
                    ServerListViewItem = ServerListView.Items.Add(prop.Name);
                    ServerListViewItem.SubItems.Add(prop.Value == null ?
                        string.Empty : prop.Value.ToString());
                }

                ServerListView.EndUpdate();

                ConnectionListView.BeginUpdate();

                // Clear control
                ConnectionListView.Items.Clear();

                // Iterate through all the properties and add each one to the list
                foreach (Property prop in SqlServerSelection.Information.Properties)
                {
                    ServerListViewItem = ConnectionListView.Items.Add(
                        prop.Name);
                    ServerListViewItem.SubItems.Add(prop.Value == null ?
                        string.Empty : prop.Value.ToString());
                }

                ConnectionListView.EndUpdate();
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        /// <summary>
        /// Handle Refresh click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, System.EventArgs e)
        {
            ShowServerInformation();
        }
    }
}