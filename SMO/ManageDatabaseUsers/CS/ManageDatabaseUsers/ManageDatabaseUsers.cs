/*============================================================================
  File:    ManageDatabaseUsers.cs 

  Summary: Implements a sample SMO Manage Database Users utility in C#.

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
using System.Collections;

// SMO namespaces
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class ManageDatabaseUsers : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public ManageDatabaseUsers()
        {
            InitializeComponent();
        }

        private void ManageDatabaseUsers_Load(object sender, System.EventArgs e)
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
                SqlServerSelection = new Server(myServerConn);
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
        }

        private void ManageDatabaseUsers_FormClosed(object sender, System.EventArgs e)
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

        private void AddUserButton_Click(System.Object sender,
            System.EventArgs e)
        {
            Database db;
            User usr;

            try
            {
                db = (Database)DatabasesComboBox.SelectedItem;

                // Create the user object
                usr = new User(db, UserNameTextBox.Text);
                usr.Login = LoginsComboBox.Text;
                usr.Create();
                ShowUsers();

                // Select the user we just created and make sure it's viewable
                ListViewItem UsersListViewItem = UsersListView.
                    FindItemWithText(UserNameTextBox.Text);

                UsersListViewItem.Selected = true;
                UsersListViewItem.EnsureVisible();
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        private void DeleteUserButton_Click(System.Object sender,
            System.EventArgs e)
        {
            Database db;
            User usr;

            try
            {
                db = (Database)DatabasesComboBox.SelectedItem;
                usr = db.Users[UsersListView.SelectedItems[0].Text];
                if (usr != null)
                {
                    // Are you sure?  Default to No.
                    if (System.Windows.Forms.MessageBox.Show(this, string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.ReallyDrop, usr.Name), this.Text,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2, 0) == DialogResult.No)
                    {
                        return;
                    }

                    usr.Drop();
                }

                ShowUsers();
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        private void Databases_SelectedIndexChangedComboBox(System.Object
            sender, System.EventArgs e)
        {
            ShowUsers();

            // Select the first item in the list
            if (UsersListView.Items.Count > 0)
            {
                UsersListView.Items[0].Selected = true;
                UsersListView.Items[0].EnsureVisible();
            }

            UpdateButtons();
        }

        private void Users_SelectedIndexChangedListView(System.Object sender,
            System.EventArgs e)
        {
            UpdateButtons();
        }

        private void UserName_TextChangedTextBox(System.Object sender,
            System.EventArgs e)
        {
            UpdateButtons();
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

                // Add database objects to combobox; the default 
                // ToString will display the database name
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

        private void ShowUsers()
        {
            Cursor csr = null;
            Database db;
            ListViewItem UsersListViewItem;

            try
            {
                csr = this.Cursor;   // Save the old cursor
                this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

                // Clear the user list
                UsersListView.Items.Clear();

                // Limit the properties returned to just those that we use
                SqlServerSelection.SetDefaultInitFields(typeof(User),
                    new string[] { "Name", "Login", "ID" });

                // Show the current users for the selected database
                if (DatabasesComboBox.SelectedIndex >= 0)
                {
                    db = (Database)DatabasesComboBox.SelectedItem;

                    // Load list of logins
                    SortedList logins =
                        new SortedList(SqlServerSelection.Logins.Count);

                    foreach (Login userLogin in SqlServerSelection.Logins)
                    {
                        logins.Add(userLogin.Name, null);
                    }

                    // Add users to list view and remove from logins
                    foreach (User usr in db.Users)
                    {
                        UsersListViewItem = UsersListView.Items.Add(usr.Name);
                        UsersListViewItem.SubItems.Add(usr.Login);
                        UsersListViewItem.SubItems.Add(usr.ID.ToString(
                            System.Globalization.CultureInfo.InvariantCulture));

                        // Remove from logins
                        logins.Remove(usr.Login);
                    }

                    // Populate the logins combo box
                    LoginsComboBox.Items.Clear();
                    foreach (DictionaryEntry dictLogin in logins)
                    {
                        // Eliminate sa login from list
                        if ((string)dictLogin.Key != "sa")
                        {
                            LoginsComboBox.Items.Add(dictLogin.Key);
                        }
                    }

                    // Select the first item in the list
                    if (UsersListView.Items.Count > 0)
                    {
                        UsersListView.Items[0].Selected = true;
                        UsersListView.Items[0].EnsureVisible();
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

        private void UpdateButtons()
        {
            if ((UserNameTextBox.Text.Length > 0) &&
                (LoginsComboBox.SelectedIndex >= 0))
            {
                AddUserButton.Enabled = true;
            }
            else
            {
                AddUserButton.Enabled = false;
            }

            if (UsersListView.SelectedItems.Count > 0)
            {
                DeleteUserButton.Enabled = true;
            }
            else
            {
                DeleteUserButton.Enabled = false;
            }
        }

        private void Logins_SelectedIndexChangedComboBox(object sender,
            System.EventArgs e)
        {
            UpdateButtons();
        }
    }
}