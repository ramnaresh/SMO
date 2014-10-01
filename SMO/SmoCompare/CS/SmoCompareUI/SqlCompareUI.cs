/*=====================================================================

  File:      SqlCompareUI.cs
  Summary:   Main user interface window
  Date:         08-20-2004

---------------------------------------------------------------------
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
=======================================================================*/

#region Using directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class SqlCompareUI : Form
    {
        private readonly string defaultServer = @"(local)";
        private SmoCompare sqlCompare;
        private bool areEqual;
        private bool serverChanged1 = true;
        private bool serverChanged2 = true;
        private ServerConnection serverConn1;
        private ServerConnection serverConn2;
        private Urn paramUrn1;
        private Urn paramUrn2;
        private SqlObjectBrowser objectBrowser;
        private ScriptPanel objScriptPanel;

        delegate void WorkerThreadFinished(object objError);

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SqlCompareUI()
        {
            InitializeComponent();

            try
            {
                this.sqlCompare = new SmoCompare();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void serverTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.serverChanged1 = true;
            urnTextBox1.Text = string.Empty;
        }

        private void passwordTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.serverChanged1 = true;
            urnTextBox1.Text = string.Empty;
        }

        private void loginTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.serverChanged1 = true;
            this.urnTextBox1.Text = string.Empty;
        }

        private void serverTextBox2_TextChanged(object sender, EventArgs e)
        {
            this.serverChanged2 = true;
            this.urnTextBox2.Text = string.Empty;
        }

        private void passwordTextBox2_TextChanged(object sender, EventArgs e)
        {
            this.serverChanged2 = true;
            this.urnTextBox2.Text = string.Empty;
        }

        private void loginTextBox2_TextChanged(object sender, EventArgs e)
        {
            this.serverChanged2 = true;
            this.urnTextBox2.Text = string.Empty;
        }

        /// <summary>
        /// Select the first object to compare.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void objectBrowse1Button_Click(object sender, EventArgs e)
        {
            if (serverTextBox1.Text == null || serverTextBox1.Text.Length == 0)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.FirstServerMustBeSpecified;
                emb.Show(this);

                return;
            }

            if (this.serverChanged1)
            {
                this.serverConn1 = new ServerConnection(serverTextBox1.Text);
                this.serverConn1.NonPooledConnection = false;
                this.serverConn1.LoginSecure = loginTextBox1.Text.Trim().Length == 0 ? true : false;
                if (!this.serverConn1.LoginSecure)
                {
                    this.serverConn1.Login = loginTextBox1.Text;
                    this.serverConn1.Password = passwordTextBox1.Text;
                }

                this.serverChanged1 = false;
            }

            this.objectBrowser = new SqlObjectBrowser(this.serverConn1);
            if (this.objectBrowser.Connected == true)
            {
                if (this.objectBrowser.Urn != null
                    && DialogResult.OK == this.objectBrowser.ShowDialog(this))
                {
                    this.urnTextBox1.Text = this.objectBrowser.Urn;
                }
            }
        }

        /// <summary>
        /// Select the second object to compare.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void objectBrowse2Button_Click(object sender, EventArgs e)
        {
            if (serverTextBox2.Text == null || serverTextBox2.Text.Length == 0)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.SecondServerMustBeSpecified;
                emb.Show(this);

                return;
            }

            if (serverChanged2)
            {
                this.serverConn2 = new ServerConnection(this.serverTextBox2.Text);
                this.serverConn2.NonPooledConnection = false;
                this.serverConn2.LoginSecure = loginTextBox2.Text.Trim().Length
                    == 0 ? true : false;
                if (!this.serverConn2.LoginSecure)
                {
                    this.serverConn2.Login = loginTextBox2.Text;
                    this.serverConn2.Password = passwordTextBox2.Text;
                }

                serverChanged2 = false;
            }

            this.objectBrowser = new SqlObjectBrowser(this.serverConn2);
            if (this.objectBrowser.Connected == true)
            {
                if (DialogResult.OK == this.objectBrowser.ShowDialog(this))
                {
                    this.urnTextBox2.Text = this.objectBrowser.Urn;
                }
            }
        }

        /// <summary>
        /// Handle the shallow comparison
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shallowCompareButton_Click(object sender, EventArgs e)
        {
            Cursor csr = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            string[] items;
            ListViewItem listViewItem;

            try
            {
                if (ValidateFields())
                {
                    try
                    {
                        object val1 = null, val2 = null;

                        CleanTheForm();

                        shallowCompareButton.Refresh();
                        shallowCompareButton.Enabled = false;

                        this.paramUrn1 = (Urn)this.urnTextBox1.Text;
                        this.paramUrn2 = (Urn)this.urnTextBox2.Text;

                        Server server1 = new Server(this.serverConn1);
                        Server server2 = new Server(this.serverConn2);

                        SqlSmoObject object1 = server1.GetSmoObject(urnTextBox1.Text);
                        SqlSmoObject object2 = server2.GetSmoObject(urnTextBox2.Text);

                        object1.Refresh();
                        object2.Refresh();

                        foreach (Property prop in object2.Properties)
                        {
                            try
                            {
                                val1 = object1.Properties[prop.Name].Value;
                                val2 = object2.Properties[prop.Name].Value;
                                if (val1 != null && val2 != null)
                                {
                                    if (!val1.Equals(val2))
                                    {
                                        items = new string[5];
                                        items[0] = prop.Name;
                                        items[1] = object1.Urn;
                                        items[2] = object2.Urn;
                                        if (val1 != null)
                                        {
                                            items[3] = val1.ToString();
                                        }
                                        else
                                        {
                                            items[3] = "null";
                                        }

                                        if (val2 != null)
                                        {
                                            items[4] = val2.ToString();
                                        }
                                        else
                                        {
                                            items[4] = "null";
                                        }

                                        listViewItem = new ListViewItem(items);
                                        differencesListView.Items.Add(listViewItem);
                                        differencesListView.Refresh();
                                    }
                                }
                            }
                            catch (PropertyCannotBeRetrievedException)
                            {
                                // Ignore properties that can't be accessed
                            }
                        }

                        Database database1 = object1 as Database;
                        Database database2 = object2 as Database;

                        if (database1 != null && database2 != null)
                        {
                            foreach (Property p in database1.DatabaseOptions.Properties)
                            {
                                // If the props differ in value update the destination database
                                if (!p.IsNull
                                    && !database1.DatabaseOptions.Properties[p.Name].IsNull
                                    && database1.DatabaseOptions.Properties[p.Name].Writable)
                                {
                                    if (!database1.DatabaseOptions.Properties[p.Name].Value.Equals
                                    (database2.DatabaseOptions.Properties[p.Name].Value))
                                    {
                                        items = new string[5];
                                        items[0] = p.Name;
                                        items[1] = object1.Urn + "/Options";
                                        items[2] = object2.Urn + "/Options";
                                        if (val1 != null)
                                        {
                                            items[3] = database1.DatabaseOptions
                                                .Properties[p.Name].Value.ToString();
                                        }
                                        else
                                        {
                                            items[3] = "null";
                                        }

                                        if (val2 != null)
                                        {
                                            items[4] = database2.DatabaseOptions
                                                   .Properties[p.Name].Value.ToString();
                                        }
                                        else
                                        {
                                            items[4] = "null";
                                        }

                                        listViewItem = new ListViewItem(items);
                                        differencesListView.Items.Add(listViewItem);
                                        differencesListView.Refresh();
                                    }
                                }
                            }
                        }

                        if (differencesListView.Items.Count == 0)
                        {
                            ExceptionMessageBox emb = new ExceptionMessageBox();
                            emb.Text = Properties.Resources.BothObjectsEqual;
                            emb.Show(this);
                        }
                    }
                    catch (SmoException ex)
                    {
                        ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                        emb.Show(this);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                shallowCompareButton.Enabled = true;
                shallowCompareButton.Refresh();
                this.Cursor = csr;
            }
        }

        /// <summary>
        /// Handle in depth comparison
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inDepthCompareButton_Click(object sender, EventArgs e)
        {
            Cursor csr = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (ValidateFields())
                {
                    InitializeSqlCompare();
                    try
                    {
                        CleanTheForm();

                        inDepthCompareButton.Refresh();
                        inDepthCompareButton.Enabled = false;

                        paramUrn1 = (Urn)this.urnTextBox1.Text;
                        paramUrn2 = (Urn)this.urnTextBox2.Text;

                        ThreadStart threadStart
                            = new ThreadStart(this.StartComparing);
                        Thread worker = new Thread(threadStart);
                        worker.Start();
                    }
                    catch (ThreadAbortException ex)
                    {
                        ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                        emb.Show(this);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = csr;
            }
        }

        /// <summary>
        /// Generate script to make object 1 identical in props as object 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genScript1to2Button_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                objScriptPanel = new ScriptPanel(this.serverConn1, GetScriptToMakeIdenticalObject1());
                objScriptPanel.ShowDialog(this);
            }
        }

        /// <summary>
        /// Generate script to make object 2 identical in props as object 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genScript2to1Button_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                objScriptPanel = new ScriptPanel(this.serverConn2, GetScriptToMakeIdenticalObject2());
                objScriptPanel.ShowDialog(this);
            }
        }

        private void CleanTheForm()
        {
            objectListView1.Items.Clear();
            objectListView1.Refresh();
            objectListView2.Items.Clear();
            objectListView2.Refresh();
            differencesListView.Items.Clear();
            differencesListView.Refresh();

            // Clear all buffers from SqlCompare object
            this.sqlCompare.Clear();
            this.sqlCompare.Reinitialize();
        }

        private void ReportResult()
        {
            if (!this.areEqual)
            {
                // Objects which are in smoObject1 only
                foreach (string urn in sqlCompare.ChildrenOfObject1)
                {
                    objectListView1.Items.Add(urn);
                    objectListView1.Refresh();
                }

                // Objects which are in smoObject2 only
                foreach (string urn in sqlCompare.ChildrenOfObject2)
                {
                    objectListView2.Items.Add(urn);
                    objectListView2.Refresh();
                }

                string[] items;
                ListViewItem listViewItem;

                // Properties which are different in value
                foreach (DifferentProperties prop in sqlCompare.DiffProps1)
                {
                    items = new string[5];
                    items[0] = prop.PropertyName;
                    items[1] = prop.Urn1;
                    items[2] = prop.Urn2;
                    items[3] = prop.ObjectValue1;
                    items[4] = prop.ObjectValue2;

                    listViewItem = new ListViewItem(items);
                    differencesListView.Items.Add(listViewItem);
                    differencesListView.Refresh();
                }
            }
            else
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.BothObjectsEqual;
                emb.Show(this);
            }
        }

        // ASSERT: objError is null in case of success
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        private void WorkerThreadFinishedImpl(object error)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new WorkerThreadFinished(
                    this.WorkerThreadFinishedImpl), new object[] { error });
            }
            else
            {
                if (error != null)
                {
                    ExceptionMessageBox emb
                        = new ExceptionMessageBox((Exception)error);
                    emb.Show(this);
                }

                this.inDepthCompareButton.Enabled = true;
                this.inDepthCompareButton.Refresh();
                this.Cursor = Cursors.Default;

                ReportResult();
            }
        }

        private void StartComparing()
        {
            WorkerThreadFinished reportDelegate
                = new WorkerThreadFinished(this.WorkerThreadFinishedImpl);

            try
            {
                // Assume they are not equal
                this.areEqual = false;
                this.areEqual = sqlCompare.Start(this.paramUrn1.ToString(),
                    this.paramUrn2.ToString());

                reportDelegate(null);
            }
            catch (ApplicationException ex)
            {
                reportDelegate(ex);
            }
            catch (SmoException ex)
            {
                reportDelegate(ex);
            }
        }

        private void InitializeSqlCompare()
        {
            // Server
            this.sqlCompare.Server1 = serverTextBox1.Text;
            this.sqlCompare.Server2 = serverTextBox2.Text;

            // Login
            this.sqlCompare.Login1 = loginTextBox1.Text;
            this.sqlCompare.Login2 = loginTextBox2.Text;

            // Password
            this.sqlCompare.Password1 = passwordTextBox1.Text;
            this.sqlCompare.Password2 = passwordTextBox2.Text;
        }

        private bool ValidateFields()
        {
            // Server connection
            if (this.serverConn1 == null)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.SelectObject1Urn;
                emb.Show(this);

                return false;
            }

            if (this.serverConn2 == null)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.SelectObject2Urn;
                emb.Show(this);

                return false;
            }

            // Urn
            if (urnTextBox1.Text.Length == 0 || urnTextBox2.Text.Length == 0)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.Object1AndObject2NotSet;
                emb.Show(this);

                return false;
            }

            if (urnTextBox1.Text == null || urnTextBox2.Text == null)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.Object1AndObject2NotSet;
                emb.Show(this);

                return false;
            }

            // Server
            if (serverTextBox1.Text == null || serverTextBox1.Text.Length == 0)
            {
                serverTextBox1.Text = defaultServer;
            }

            if (serverTextBox2.Text == null || serverTextBox2.Text.Length == 0)
            {
                serverTextBox2.Text = defaultServer;
            }

            // Password
            if (passwordTextBox1.Text == null)
            {
                passwordTextBox1.Text = string.Empty;
            }

            if (passwordTextBox2.Text == null)
            {
                passwordTextBox2.Text = string.Empty;
            }

            // Login
            if (loginTextBox1.Text == null)
            {
                loginTextBox1.Text = string.Empty;
            }

            if (loginTextBox2.Text == null)
            {
                loginTextBox2.Text = string.Empty;
            }

            return true;
        }

        private StringCollection GetScriptToMakeIdenticalObject1()
        {
            StringCollection collScript = new StringCollection();
            Server server1 = new Server(this.serverConn1);
            Server server2 = new Server(this.serverConn2);
            SqlSmoObject object1 = server1.GetSmoObject(urnTextBox1.Text);
            SqlSmoObject object2 = server2.GetSmoObject(urnTextBox2.Text);
            Database database1 = object1 as Database;
            Database database2 = object2 as Database;

            server1.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;

            object1.Refresh();
            object2.Refresh();

            foreach (Property p in object2.Properties)
            {
                try
                {
                    if (p.Writable && object2.Properties[p.Name].Value != null &&
                        !object1.Properties[p.Name].Value.Equals(object2.Properties[p.Name].Value))
                    {
                        object1.Properties[p.Name].Value = object2.Properties[p.Name].Value;
                    }
                }
                catch (PropertyCannotBeRetrievedException)
                {
                    // Ignore properties that can't be accessed
                }
            }

            if (database1 != null && database2 != null)
            {
                foreach (Property p in database1.DatabaseOptions.Properties)
                {
                    // If the props differ in value update the destination database
                    if (!p.IsNull
                        && !database1.DatabaseOptions.Properties[p.Name].IsNull
                        && database1.DatabaseOptions.Properties[p.Name].Writable)
                    {
                        if (!database1.DatabaseOptions.Properties[p.Name].Value.Equals
                        (database2.DatabaseOptions.Properties[p.Name].Value))
                        {
                            database1.DatabaseOptions.Properties[p.Name].Value
                                = database2.DatabaseOptions.Properties[p.Name].Value;
                        }
                    }
                }
            }

            server1.ConnectionContext.CapturedSql.Clear();
            server2.ConnectionContext.CapturedSql.Clear();

            try
            {
                IAlterable alterableObject1 = object1 as IAlterable;
                if (alterableObject1 != null)
                {
                    alterableObject1.Alter();
                    collScript = server1.ConnectionContext.CapturedSql.Text;
                }
            }
            catch (FailedOperationException)
            {
                //Ignore
            }

            server1.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteSql;
            if (collScript.Count == 0)
            {
                collScript.Add(Properties.Resources.SelectedObjectsIdentical);
            }

            return collScript;
        }

        private StringCollection GetScriptToMakeIdenticalObject2()
        {
            StringCollection collScript = new StringCollection();
            Server server1 = new Server(this.serverConn2);
            Server server2 = new Server(this.serverConn1);
            SqlSmoObject object1 = server1.GetSmoObject(this.urnTextBox2.Text);
            SqlSmoObject object2 = server2.GetSmoObject(this.urnTextBox1.Text);
            Database database1 = object1 as Database;
            Database database2 = object2 as Database;

            server1.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;

            object1.Refresh();
            object2.Refresh();

            foreach (Property p in object2.Properties)
            {
                try
                {
                    if (p.Writable && object2.Properties[p.Name].Value != null &&
                        !object1.Properties[p.Name].Value.Equals(object2.Properties[p.Name].Value))
                        object1.Properties[p.Name].Value = object2.Properties[p.Name].Value;
                }
                catch (PropertyCannotBeRetrievedException)
                {
                    // Ignore properties that can't be accessed
                }
            }

            if (database1 != null && database2 != null)
            {
                foreach (Property p in database1.DatabaseOptions.Properties)
                {
                    // If the props differ in value update the destination database
                    if (!p.IsNull
                        && !database1.DatabaseOptions.Properties[p.Name].IsNull
                        && database1.DatabaseOptions.Properties[p.Name].Writable)
                    {
                        if (!database1.DatabaseOptions.Properties[p.Name].Value
                            .Equals(database2.DatabaseOptions.Properties[p.Name].Value))
                        {
                            database1.DatabaseOptions.Properties[p.Name].Value =
                            database2.DatabaseOptions.Properties[p.Name].Value;
                        }
                    }
                }
            }

            server1.ConnectionContext.CapturedSql.Clear();
            server2.ConnectionContext.CapturedSql.Clear();

            IAlterable alterableObject1 = object1 as IAlterable;
            if (alterableObject1 != null)
            {
                try
                {
                    alterableObject1.Alter();
                    collScript = server1.ConnectionContext.CapturedSql.Text;
                }
                catch (SmoException ex)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                    emb.Show(this);
                }
            }

            server1.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteSql;
            if (collScript.Count == 0)
            {
                collScript.Add(Properties.Resources.SelectedObjectsIdentical);
            }

            return collScript;
        }
    }
}