/*============================================================================
  File:    DatabaseSpace.cs 

  Summary: Implements a sample SMO SQL Server database space utility in C#.

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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.MessageBox;

namespace Microsoft.Samples.SqlServer
{
    public partial class DatabaseSpace : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public DatabaseSpace()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

                    // Refresh database space list
                    ListDatabaseSpace();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void ListDatabaseSpace()
        {
            try
            {
                this.dataGridView1.Rows.Clear();

                foreach (Database db in SqlServerSelection.Databases)
                {
                    if (db.IsSystemObject == false)
                    {
                        if (db.LogFiles.Count > 0)
                        {
                            this.dataGridView1.Rows.Add(new object[] { db.Name, 
                                db.Size, db.SpaceAvailable / 1024.0, 
                                db.LogFiles[0].Size / 1024.0, 
                                db.LogFiles[0].UsedSpace / 1024.0 });
                        }
                        else
                        {
                            this.dataGridView1.Rows.Add(new object[] { db.Name, 
                                db.Size, db.SpaceAvailable / 1024.0, 
                                0.0, 0.0 });
                        }
                    }
                }
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }
    }
}
