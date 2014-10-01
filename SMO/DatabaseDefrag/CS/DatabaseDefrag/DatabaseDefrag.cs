/*============================================================================
  File:    DatabaseDefrag.cs 

  Summary: Implements a sample SMO SQL Server database defragment utility in C#.

  Date:    August 29, 2005
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
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class DatabaseDefrag : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public DatabaseDefrag()
        {
            InitializeComponent();
        }

        private void DatabaseDefrag_Load(object sender, EventArgs e)
        {
            ServerConnection ServerConn;
            ServerConnect scForm;
            DialogResult dr;

            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

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

                        // Limit the table properties returned to just those that we use
                        SqlServerSelection.SetDefaultInitFields(typeof(Table),
                            new String[] { "Schema", "Name", "IsSystemObject" });

                        ShowDatabases(true);
                    }
                }
                else
                {
                    this.Close();
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

        private void DatabaseDefrag_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SqlServerSelection != null)
            {
                if (SqlServerSelection.ConnectionContext.IsOpen == true)
                {
                    SqlServerSelection.ConnectionContext.Disconnect();
                }
            }
        }

        private void defragmentButton_Click(object sender, EventArgs e)
        {
            // Defragment the selected database
            DefragDatabase();
        }

        private void DefragDatabase()
        {
            Database db;
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear control
                richTextBox1.Clear();

                db = (Database)DatabasesComboBox.SelectedItem;
                foreach (Table tbl in db.Tables)
                {
                    if (tbl.IsSystemObject == false)
                    {
                        richTextBox1.AppendText(Properties.Resources.Table
                            + tbl.ToString() + Environment.NewLine);

                        richTextBox1.ScrollToCaret();

                        // Allow the text to display
                        Application.DoEvents();

                        tbl.RebuildIndexes(90);
                    }
                }
                //  At last display the "completed"
                richTextBox1.AppendText(Properties.Resources.Completed);
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
