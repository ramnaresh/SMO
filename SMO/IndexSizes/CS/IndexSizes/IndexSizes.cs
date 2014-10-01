/*============================================================================
  File:    IndexSizes.cs 

  Summary: Implements a sample SMO SQL Server index sizes utility in C#.

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
    partial class IndexSizes : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public IndexSizes()
        {
            InitializeComponent();
        }

        private void IndexSizes_Load(object sender, EventArgs e)
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
                    this.Text = Properties.Resources.AppTitle
                        + SqlServerSelection.Name;

                    // Limit the properties returned to just those that we use
                    SqlServerSelection.SetDefaultInitFields(typeof(Table),
                        new String[] { "Schema", "Name", "IsSystemObject" });

                    SqlServerSelection.SetDefaultInitFields(typeof(Index),
                        new String[] { "Name", "IndexKeyType", "SpaceUsed" });

                    ShowDatabases(true);
                }
            }
            else
            {
                this.Close();
            }
        }

        private void DisplayIndexSizes()
        {
            Database db;
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                db = (Database)DatabasesComboBox.SelectedItem;

                this.dataGridView1.Rows.Clear();

                foreach (Table tbl in db.Tables)
                {
                    if (tbl.IsSystemObject == false)
                    {
                        foreach (Index idx in tbl.Indexes)
                        {
                            if (idx.IndexKeyType != IndexKeyType.None)
                            {
                                continue; // Only show indexes
                            }

                            this.dataGridView1.Rows.Add(new object[] { 
                                tbl.ToString(), idx.Name, idx.SpaceUsed });
                        }
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

        private void DatabasesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SqlServerSelection != null)
            {
                DisplayIndexSizes();
            }
        }
    }
}
