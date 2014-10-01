/*============================================================================
  File:    LoadRegAssembly.cs

  Summary: Implements a sample SMO load and register assembly utility in C#.

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
    partial class LoadRegAssembly : Form
    {
        // Use the Server object to connect to a specific server
        private Server mySqlServer;

        public LoadRegAssembly()
        {
            InitializeComponent();
        }

        private void LoadRegAssembly_Load(object sender, System.EventArgs e)
        {
            ServerConnection myServerConn;
            ServerConnect scForm;
            DialogResult dr;

            // Display the main window first
            this.Show();
            Application.DoEvents();
            
            myServerConn = new ServerConnection();
            scForm = new ServerConnect(myServerConn);
            dr = scForm.ShowDialog(this);
            if ((dr == DialogResult.OK) &&
                (myServerConn.SqlConnectionObject.State
                == ConnectionState.Open))
            {
                mySqlServer = new Server(myServerConn);
                if (mySqlServer != null)
                {
                    this.Text = Properties.Resources.AppTitle + mySqlServer.Name;

                    // Refresh database list
                    ShowDatabases(true);
                    UpdateButtons();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void LoadRegAssembly_FormClosed(object sender, System.EventArgs e)
        {
            if (mySqlServer != null)
            {
                if (mySqlServer.ConnectionContext.SqlConnectionObject.State
                    == ConnectionState.Open)
                {
                    mySqlServer.ConnectionContext.Disconnect();
                }
            }
        }

        private void Databases_SelectedIndexChangedComboBox(System.Object
            sender, System.EventArgs e)
        {
            if (DatabasesComboBox.SelectedIndex >= 0)
            {
                ShowAssemblies(true);
            }

            UpdateButtons();
        }

        private void Assemblies_SelectedIndexChangedListView(System.Object
            sender, System.EventArgs e)
        {
            UpdateButtons();
        }

        private void AddAssemblyButton_Click(System.Object sender,
            System.EventArgs e)
        {
            Cursor csr = null;
            Database db;
            SqlAssembly asm;
            UserDefinedFunction udf;
            UserDefinedFunctionParameter parm;
            ListViewItem AssemblyListViewItem;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Get selected database
                db = (Database)DatabasesComboBox.SelectedItem;
                asm = new SqlAssembly(db, "UtilityConversion");
                asm.Owner = "dbo";
                asm.AssemblySecurityLevel = AssemblySecurityLevel.Safe;

                // This allows the assembly to be on a different server from SQL Server
                // Use string array version which serializes the assembly
                asm.Create(new String[] { AssemblyFileTextBox.Text });
                udf = new UserDefinedFunction(db, "StringToInt32");
                udf.TextMode = false;
                udf.ImplementationType = ImplementationType.SqlClr;
                udf.AssemblyName = "UtilityConversion";
                udf.ClassName = "Microsoft.Samples.SqlServer.Conversions";
                udf.MethodName = "StringToInt32";
                udf.FunctionType = UserDefinedFunctionType.Scalar;
                udf.DataType = DataType.Int;
                parm = new UserDefinedFunctionParameter(udf, "@Input");
                udf.Parameters.Add(parm);
                parm.DataType = DataType.NVarChar(255);
                udf.Create();
                ShowAssemblies(true);

                // Select the assembly just added
                AssemblyListViewItem = AssembliesListView.FindItemWithText(
                    asm.Name);
                AssemblyListViewItem.Selected = true;
                AssemblyListViewItem.EnsureVisible();
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

        private void DropAssemblyButton_Click(System.Object sender,
            System.EventArgs e)
        {
            Database db;
            SqlAssembly asm;
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor
                if (AssembliesListView.SelectedItems.Count > 0)
                {
                    // Get selected database
                    db = (Database)DatabasesComboBox.SelectedItem;
                    asm = db.Assemblies[AssembliesListView.SelectedItems[0].
                        Text];
                    if (asm != null)
                    {
                        // Are you sure?  Default to No.
                        if (System.Windows.Forms.MessageBox.Show(this,
                            string.Format(System.Globalization.CultureInfo.InvariantCulture,
                            Properties.Resources.ReallyDrop, asm.Name),
                            this.Text, MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2, 0) == DialogResult.No)
                        {
                            return;
                        }

                        // Drop all related User Defined Functions from assembly
                        for (int counter = db.UserDefinedFunctions.Count - 1;
                            counter >= 0; counter--)
                        {
                            if (db.UserDefinedFunctions[counter].AssemblyName ==
                                asm.Name)
                            {
                                db.UserDefinedFunctions[counter].Drop();
                            }
                        }

                        asm.Drop();
                    }
                }

                ShowAssemblies(true);
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

                // Limit the properties returned to just those that we use
                mySqlServer.SetDefaultInitFields(typeof(Database),
                    new String[] { "Name", "IsSystemObject", "IsAccessible" });

                // Add database objects to combobox; the default ToString 
                // will display the database name
                foreach (Database db in mySqlServer.Databases)
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

        private void ShowAssemblies(bool selectDefault)
        {
            // Show the current assemblies in the selected database
            Database db;
            ListViewItem AssemblyListViewItem;
            Cursor csr = null;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear control
                AssembliesListView.Items.Clear();

                // Limit the properties returned to just those that we use
                mySqlServer.SetDefaultInitFields(typeof(SqlAssembly),
                    new String[] { "Name", "VersionMajor", "VersionMinor", 
                        "VersionBuild", "VersionRevision" });
                db = (Database)DatabasesComboBox.SelectedItem;
                foreach (SqlAssembly sqlAssem in db.Assemblies)
                {
                    AssemblyListViewItem =
                        AssembliesListView.Items.Add(sqlAssem.Name);
                    AssemblyListViewItem.SubItems.Add(
                        sqlAssem.Version.Major.ToString(System.Globalization.CultureInfo.InvariantCulture)
                        + "." + sqlAssem.Version.Minor.ToString(System.Globalization.CultureInfo.InvariantCulture)
                        + "." + sqlAssem.Version.Build.ToString(System.Globalization.CultureInfo.InvariantCulture)
                        + "." + sqlAssem.Version.Revision.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    AssemblyListViewItem.SubItems.Add(
                        sqlAssem.CreateDate.ToString(System.Globalization.CultureInfo.InvariantCulture));
                }

                if (selectDefault && (AssembliesListView.Items.Count > 0))
                {
                    AssembliesListView.Items[0].Selected = true;
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

        private void UpdateButtons()
        {
            if ((DatabasesComboBox.SelectedIndex >= 0) &&
                (AssemblyFileTextBox.Text.Length > 0))
            {
                AddAssemblyButton.Enabled = true;
            }
            else
            {
                AddAssemblyButton.Enabled = false;
            }

            if (AssembliesListView.SelectedItems.Count > 0)
            {
                DropAssemblyButton.Enabled = true;
            }
            else
            {
                DropAssemblyButton.Enabled = false;
            }
        }
    }
}
