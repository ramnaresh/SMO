/*============================================================================
  File:    CreateStoredProcedures.cs 

  Summary: Implements a sample SMO create stored procedures utility in C#.

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
using System.Text;

// SMO namespaces
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class CreateStoredProcs : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public CreateStoredProcs()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            Cursor csr = null;
            Database db;
            Schema spSchema;

            // Timing
            DateTime start = DateTime.Now;

            if (System.Windows.Forms.MessageBox.Show(this, Properties.Resources.ReallyCreate,
                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2, 0) == DialogResult.No)
            {
                return;
            }

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear control
                ScriptTextBox.Clear();

                db = (Database)DatabasesComboBox.SelectedItem;

                // Create SmoDemo schema to contain stored procedures
                if (db.Schemas.Contains("SmoDemo") == false)
                {
                    spSchema = new Schema(db, "SmoDemo");
                    spSchema.Create();
                }
                else
                {
                    spSchema = db.Schemas["SmoDemo"];
                }

                // Limit the table properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Table),
                    new String[] { "Name" });

                // Limit the column properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Column),
                    new String[] { "Name", "DataType", "Length", "InPrimaryKey" });

                // Limit the stored procedure properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(StoredProcedure),
                    new String[] { "Name", "Schema" });

                // Create a SELECT stored procedure for each table
                foreach (Table tbl in db.Tables)
                {
                    if (db.IsSystemObject == false)
                    {
                        CreateSelectProcedure(spSchema, tbl);
                    }
                }

                ScriptTextBox.AppendText(Properties.Resources.Completed);

                ScrollToBottom();

                sbrStatus.Text = Properties.Resources.Ready;
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                // Clean up.
                this.Cursor = csr;  // Restore the original cursor

                // Timing
                TimeSpan diff = DateTime.Now - start;

                ScriptTextBox.AppendText(String.Format(
                    System.Threading.Thread.CurrentThread.CurrentCulture,
                    Environment.NewLine + Properties.Resources.ElapsedTime,
                    (diff.Minutes * 60) + diff.Seconds, diff.Milliseconds));
            }
        }

        private void CreateStoredProcs_Load(object sender, EventArgs e)
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

        private void CreateStoredProcs_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SqlServerSelection != null)
            {
                if (SqlServerSelection.ConnectionContext.SqlConnectionObject.State == ConnectionState.Open)
                {
                    SqlServerSelection.ConnectionContext.Disconnect();
                }
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

                // Limit the properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(Database),
                    new String[] { "Name", "IsSystemObject", "IsAccessible" });

                // Add database object to combobox; the default ToString will display the database name
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

        private void CreateSelectProcedure(Schema spSchema, Table tbl)
        {
            String procName;
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbSelect = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            StoredProcedure sp;
            StoredProcedureParameter parm;

            try
            {
                // Create stored procedure name from user entry and table name
                procName = PrefixTextBox.Text + tbl.Name + @"Select";

                if (DropOnlyCheckBox.CheckState == CheckState.Checked)
                {
                    DropStoredProcedure(procName, spSchema);
                }
                else
                {
                    DropStoredProcedure(procName, spSchema);
                    ScriptTextBox.AppendText(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.CreatingStoredProcedure,
                        spSchema.ToString(), BracketObjectName(procName))
                        + Environment.NewLine);
                    ScrollToBottom();

                    // Create the new stored procedure object
                    sp = new StoredProcedure(tbl.Parent, procName, spSchema.Name);
                    sp.TextMode = false;
                    foreach (Column col in tbl.Columns)
                    {
                        // Select columns
                        if (sbSelect.Length > 0)
                        {
                            sbSelect.Append(", " + Environment.NewLine);
                        }

                        // Note: this does not fix object names with embedded brackets
                        sbSelect.Append("\t[");
                        sbSelect.Append(col.Name);
                        sbSelect.Append(@"]");

                        // Create parameters and where clause from indexed fields
                        if (col.InPrimaryKey == true)
                        {
                            // Parameter columns
                            parm = new StoredProcedureParameter(sp, "@"
                                + col.Name);
                            parm.DataType = col.DataType;
                            parm.DataType.MaximumLength
                                = col.DataType.MaximumLength;
                            sp.Parameters.Add(parm);

                            // Where columns
                            if (sbWhere.Length > 0)
                            {
                                sbWhere.Append(" " + Environment.NewLine + "\tAND ");
                            }

                            // Note: this does not fix object names with embedded brackets
                            sbWhere.Append(@"[");
                            sbWhere.Append(col.Name);
                            sbWhere.Append(@"] = @");
                            sbWhere.Append(col.Name);
                        }
                    }

                    // Put where clause into string
                    if (sbWhere.Length > 0)
                    {
                        sbWhere.Insert(0, @"WHERE ");
                    }

                    sbrStatus.Text = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.Creating, procName);
                    sbSQL.Append("SELECT ");
                    sbSQL.Append(sbSelect);
                    sbSQL.Append(" " + Environment.NewLine + "FROM ");
                    sbSQL.Append(tbl.ToString());
                    sbSQL.Append(" " + Environment.NewLine);
                    sbSQL.Append(sbWhere);
                    sp.TextBody = sbSQL.ToString();
                    sp.Create();
                }
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                // Clean up.
                sbSQL = null;
                sbSelect = null;
                sbWhere = null;
                sp = null;
                parm = null;
            }
        }

        private void DropStoredProcedure(string procName, Schema spSchema)
        {
            Database db = (Database)DatabasesComboBox.SelectedItem;

            if (db.StoredProcedures.Contains(procName, spSchema.Name) == true)
            {
                ScriptTextBox.AppendText(string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.DroppingStoredProcedure,
                    spSchema.ToString(), BracketObjectName(procName)) +
                    Environment.NewLine);
                ScrollToBottom();

                // Drop the existing stored procedure
                db.StoredProcedures[procName, spSchema.Name].Drop();
            }
        }

        private static string BracketObjectName(string objectName)
        {
            StringBuilder tempString = new StringBuilder(128);

            tempString.Append(@"[");
            tempString.Append(objectName.Replace(@"[", @"[[").Replace(@"]", @"]]"));
            tempString.Append(@"]");

            return tempString.ToString();
        }

        private void ScrollToBottom()
        {
            ScriptTextBox.Select();
            ScriptTextBox.SelectionStart = ScriptTextBox.Text.Length;
            ScriptTextBox.ScrollToCaret();
        }
    }
}