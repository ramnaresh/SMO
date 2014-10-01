/*============================================================================
  File:    ServerConnect.cs 

  Summary: Implements a sample SMO server connection utility in C#.

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
using System.Text;
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
    public partial class ServerConnect : Form
    {
        // Class variables
        private ServerConnection ServerConn;
        private Boolean ErrorFlag;

        public ServerConnect(ServerConnection connection)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            ServerConn = connection;
        }

        /// <summary>
        /// Setup the dialog by loading the servers into the list and select the first one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerConnect_Load(object sender, EventArgs e)
        {
            GetServerList();

            ProcessWindowsAuthentication();
        }

        private void WindowsAuthenticationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ProcessWindowsAuthentication();
        }

        private void CancelCommandButton_Click(object sender, EventArgs e)
        {
            ServerConn = null;
            this.Close();
        }

        private void ConnectCommandButton_Click(object sender, EventArgs e)
        {
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                ErrorFlag = false; // Assume no error

                // Recreate connection if necessary
                if (ServerConn == null)
                {
                    ServerConn = new ServerConnection();
                }

                // Fill in necessary information
                ServerConn.ServerInstance = ServerNamesComboBox.Text;

                // Setup capture and execute to be able to display script
                ServerConn.SqlExecutionModes = SqlExecutionModes.
                    ExecuteAndCaptureSql;

                // Set connection timeout
                ServerConn.ConnectTimeout = (Int32)TimeoutUpDown.Value;
                if (WindowsAuthenticationRadioButton.Checked == true)
                {
                    // Use Windows authentication
                    ServerConn.LoginSecure = true;
                }
                else
                {
                    // Use SQL Server authentication
                    ServerConn.LoginSecure = false;
                    ServerConn.Login = UserNameTextBox.Text;
                    ServerConn.Password = PasswordTextBox.Text;
                }

                if (DisplayEventsCheckBox.CheckState == CheckState.Checked)
                {
                    ServerConn.InfoMessage
                        += new SqlInfoMessageEventHandler(OnSqlInfoMessage);
                    ServerConn.ServerMessage
                        += new ServerMessageEventHandler(OnServerMessage);
                    ServerConn.SqlConnectionObject.StateChange
                        += new StateChangeEventHandler(OnStateChange);
                    ServerConn.StatementExecuted
                        += new StatementEventHandler(OnStatementExecuted);
                }

                // Go ahead and connect
                ServerConn.Connect();
            }
            catch (ConnectionFailureException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);

                ErrorFlag = true;
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);

                ErrorFlag = true;
            }
            finally
            {
                this.Cursor = csr;  // Restore the original cursor
            }
        }

        private void ServerConnect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ErrorFlag == true)
            {
                e.Cancel = true;
            }

            // Reset error condition
            ErrorFlag = false;
        }

        private void GetServerList()
        {
            DataTable dt;

            // Set local server as default
            dt = SmoApplication.EnumAvailableSqlServers(false);
            if (dt.Rows.Count > 0)
            {
                // Load server names into combo box
                foreach (DataRow dr in dt.Rows)
                {
                    ServerNamesComboBox.Items.Add(dr["Name"]);
                }

                // Default to this machine 
                ServerNamesComboBox.SelectedIndex
                    = ServerNamesComboBox.FindStringExact(
                    System.Environment.MachineName);

                // If this machine is not a SQL server 
                // then select the first server in the list
                if (ServerNamesComboBox.SelectedIndex < 0)
                {
                    ServerNamesComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.NoSqlServers;
                emb.Show(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessWindowsAuthentication()
        {
            if (WindowsAuthenticationRadioButton.Checked == true)
            {
                UserNameTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;
            }
            else
            {
                UserNameTextBox.Enabled = true;
                PasswordTextBox.Enabled = true;
            }
        }

        private void OnSqlInfoMessage(object sender,
            System.Data.SqlClient.SqlInfoMessageEventArgs args)
        {
            foreach (SqlError err in args.Errors)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = String.Format(System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.SqlError,
                    err.Source, err.Class, err.State, err.Number, err.LineNumber,
                    err.Procedure, err.Server, err.Message);
                emb.Show(this);
            }
        }

        private void OnServerMessage(object sender,
            ServerMessageEventArgs args)
        {
            SqlError err = args.Error;

            ExceptionMessageBox emb = new ExceptionMessageBox();
            emb.Text = String.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                Properties.Resources.SqlError,
                err.Source, err.Class, err.State, err.Number, err.LineNumber,
                err.Procedure, err.Server, err.Message);
            emb.Show(this);
        }

        private void OnStateChange(object sender, StateChangeEventArgs args)
        {
            if (this.IsDisposed == false)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.StateChanged,
                    args.OriginalState.ToString(), args.CurrentState.ToString());
                emb.Show(this);
            }
        }

        private void OnStatementExecuted(object sender,
            StatementEventArgs args)
        {
            ExceptionMessageBox emb = new ExceptionMessageBox();
            emb.Text = args.SqlStatement;
            emb.Show(this);
        }
    }
}