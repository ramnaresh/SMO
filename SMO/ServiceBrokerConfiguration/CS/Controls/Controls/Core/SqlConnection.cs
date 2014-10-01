/*============================================================================
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
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.ComponentModel;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Broker;
using Microsoft.SqlServer.Management.Common;
using System.Globalization;
using System.Collections;
using Microsoft.SqlServer.MessageBox;
using System.Drawing;

namespace Microsoft.Samples.SqlServer.Controls
{
    //General purpose contol to connect to a SQL instance and fill an object tree with Service Broker objects.
    //Change the ClassName constants and FillTree values to show other objects that implement IConfiguration
    public partial class SqlConnectionControl : UserControl
    {
        private SqlConnectionConfiguration sqlConfig;
        private GeneralSqlServer sqlServer;
        private EventHandler onConnected;
        private ApplicationConfiguration appConfig;
        private TreeNode rootNode;
        private IConfiguration m_OptionsConfiguration;
        private ObjectsSplitPanel m_ObjectsSplitPanel;
        private TreeNode parentNode;

        const string defaultDatabase = "ssb_ConfigurationSample";
        const string MessageTypeClassName =
            "Microsoft.Samples.SqlServer.MessageTypeConfiguration";
        const string ContractClassName = "Microsoft.Samples.SqlServer.ServiceContractConfiguration";
        const string QueueClassName = "Microsoft.Samples.SqlServer.ServiceQueueConfiguration";
        const string ServiceClassName = "Microsoft.Samples.SqlServer.BrokerServiceConfiguration";
        const string TargetInstanceEndpoint = "Microsoft.Samples.SqlServer.EndpointConfiguration";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum TreeNodeObjectType
        {
            RootObject, ParentNode, ObjectNode
        }

        public SqlConnectionControl()
        {
            InitializeComponent();

            ConnectionContainer.Panel2Collapsed = true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                sqlServer = new GeneralSqlServer(defaultDatabase);

                sqlServer.Connect(sqlConfig.ServerName);

                this.FillDatabases();

                OptionsButton.Enabled = true;

                this.HideConnectPanel(sqlServer.Server.Name);

                CancelButton.Enabled = true;
                OnConnected(new EventArgs());

            }
            catch (Exception ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        //The sample is hard-coded for ServiceBroker type objects.  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.TreeNodeCollection.Add(System.String,System.String,System.String,System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.TreeNodeCollection.Add(System.String,System.String)")]
        public void FillTree()
        {
            if (this.ServiceBroker != null)
            {
                m_OptionsConfiguration = new ApplicationConfiguration(this.ServiceBroker);
                appConfig = (ApplicationConfiguration)m_OptionsConfiguration;
                objectsTreeView.Nodes.Clear();

                if (this.ServiceBroker.Parent.ExtendedProperties.Contains("BaseUrn"))
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        objectsTreeView.BeginUpdate();
                        //Configure UI objects

                        this.NewStripButton.DropDownItems.Clear();

                        rootNode = objectsTreeView.Nodes.Add("ServiceBroker", "Service Broker");
                        rootNode.Tag = TreeNodeObjectType.RootObject;

                        //This could use a config file
                        this.FillObject("Message Type",
                            MessageTypeClassName, this.ServiceBroker.MessageTypes);
                        this.FillObject("Contract",
                            ContractClassName, this.ServiceBroker.ServiceContracts);
                        this.FillObject("Queue",
                            QueueClassName, this.ServiceBroker.Queues);
                        this.FillObject("Service",
                            ServiceClassName, this.ServiceBroker.Services);

                        this.FillObject("Target Instance Endpoint",
                            TargetInstanceEndpoint,
                            this.ServiceBroker.Parent.Parent.Endpoints);

                        objectsTreeView.ExpandAll();
                        if (objectsTreeView.Nodes.Count > 0)
                        {
                            objectsTreeView.Nodes[0].EnsureVisible();
                        }
                    }
                    finally
                    {
                        objectsTreeView.EndUpdate();
                        Cursor.Current = Cursors.Default;
                    }
                }
                else
                {
                    objectsTreeView.Nodes.Add
                        (String.Empty, "BaseUrn not created. Select Options to create", "Error.bmp", "Error.bmp");
                }
            }
        }

        private TreeNode AddNode(string text, string key)
        {
            TreeNode node = null;

            if (text.StartsWith(appConfig.BaseUrn, StringComparison.OrdinalIgnoreCase))
            {
                node = parentNode.Nodes.Add(text, text, key, key);
                node.Tag = TreeNodeObjectType.ObjectNode;
            }


            return node;
        }

        private TreeNode FillObject(string parentCaption, string key, SimpleObjectCollectionBase collection)
        {
            TreeNode node = null;
            parentNode = this.ParentNode(parentCaption, key);
            this.FillNewButton(parentCaption, key);

            //BrokerObjectBase
            foreach (NamedSmoObject item in collection)
            {
                //item.Name
                node = this.AddNode(item.Name, key);
            }

            return node;
        }

        private TreeNode FillObject(string parentCaption, string key, SchemaCollectionBase collection)
        {
            TreeNode node = null;
            parentNode = this.ParentNode(parentCaption, key);
            this.FillNewButton(parentCaption, key);

            //ScriptSchemaObjectBase
            foreach (NamedSmoObject item in collection)
            {
                node = this.AddNode(item.Name, key);
            }

            return node;
        }

        private void FillNewButton(string name, string key)
        {
            ToolStripItem item = this.NewStripButton.DropDownItems.Add(name);
            item.Tag = key;
        }

        private TreeNode ParentNode(string parentCaption, string key)
        {
            TreeNode node;

            node = rootNode.Nodes.Add(key, parentCaption + "s");

            node.Tag = TreeNodeObjectType.ParentNode;
            return node;
        }
        
        public event EventHandler Connected
        {
            add
            {
                onConnected += value;
            }
            remove
            {
                onConnected -= value;
            }
        }
        
        protected virtual void OnConnected(EventArgs e)
        {
            if (onConnected != null)
            {
                onConnected(this.Server, e);
            }
        }
      
        public IConfiguration OptionsConfiguration
        {

            get 
            {
                return m_OptionsConfiguration;
            }
            set 
            {
                m_OptionsConfiguration = value;
            }
        }

        public ObjectsSplitPanel ObjectsSplitPanel
        {

            get
            {
                return m_ObjectsSplitPanel;
            }
            set
            {
                m_ObjectsSplitPanel = value;
            }
        }

        public Server Server
        {
            get
            {
                return sqlServer.Server;
            }
        }

        public ServiceBroker ServiceBroker
        {

            get
            {
                if (sqlServer != null)
                    return sqlServer.ServiceBroker;
                else
                    return null;
            }
        }

        private void FillDatabases()
        {
            DatabasesComboBox.Items.Clear();

            foreach (Database db in sqlServer.Server.Databases)
            {
                if (!db.IsSystemObject)
                    DatabasesComboBox.Items.Add(db.Name);
            }
            DatabasesComboBox.SelectedItem = sqlServer.DatabaseName;

        }

        private void ShowConnectPanel()
        {
            ConnectionContainer.Panel1Collapsed = false;
            ConnectionContainer.Panel2Collapsed = true;
        }

        private void HideConnectPanel(string serverLabelText)
        {
            ConnectionContainer.Panel1Collapsed = true;
            ConnectionContainer.Panel2Collapsed = false;

            ServerLabel.Text = serverLabelText;
        }

        private void SqlConnectionControl_Load(object sender, EventArgs e)
        {

            sqlConfig = new SqlConnectionConfiguration();
            ObjectGrid.SelectedObject = sqlConfig;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.HideConnectPanel(sqlServer.Server.Name);
        }

        private void ServerLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.ShowConnectPanel();
        }

        private void DatabasesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fill Tree for this Database
            sqlServer.DatabaseName = DatabasesComboBox.SelectedItem.ToString();
            this.NewStripButton.DropDownItems.Clear();

            this.FillTree();
        }
    }
}
