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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Broker;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.MessageBox;
using System.Reflection;

namespace Microsoft.Samples.SqlServer.Controls
{
    //Control that hosts SqlConnection and ObjectsSplitPanel. 
    public partial class TaskPane : UserControl
    {     
        const string ServiceClassName = "Microsoft.Samples.SqlServer.BrokerServiceConfiguration";
        const string ConfigurationTabPageName = "ConfigurationTabPage";
        const string ObjectsTabPageName = "ObjectsTabPage";

        private EventHandler onQueryComplete;

        private enum operations
        {
            New, Show, Drop, Mappings, Default
        }

       
        public event EventHandler QueryComplete
        {
            add
            {
                onQueryComplete += value;
            }
            remove
            {
                onQueryComplete -= value;
            }
        }

        //OnQueryComplete(new EventArgs());
        protected virtual void OnQueryComplete(EventArgs e)
        {
            if (onQueryComplete != null)
            {
                onQueryComplete(this.ConfigurationPanel.ActiveEditObject.Configuration, e);
            }
        }


        public TaskPane()
        {
            InitializeComponent();

            SqlConnectionControl.Connected += new System.EventHandler(this.SqlConnectionControl_Connected);
            SqlConnectionControl.ObjectsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ObjectsTreeView_AfterSelect);
            SqlConnectionControl.ObjectsTreeView.DoubleClick += new System.EventHandler(this.ObjectsTreeView_DoubleClick);
            SqlConnectionControl.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            SqlConnectionControl.NewStripButton.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.NewButton_DropDownItemClicked);
            ConfigurationPanel.PropertyValueChanged += new PropertyValueChangedEventHandler(ConfigurationPanel_PropertyValueChanged);
            ConfigurationPanel.NewStripButton.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.NewButton_DropDownItemClicked);
            ConfigurationPanel.CreateStripButton.Click += new System.EventHandler(this.ConfigurationPanel_StripButton_Click);
            ConfigurationPanel.QueryStripButton.Click += new System.EventHandler(this.ConfigurationPanel_StripButton_Click);
        }

        //Uses reflection to show a new PropertyGrid object for any IConfiguration object
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Microsoft.Samples.SqlServer.Controls.ObjectsSplitPanel.AddObject(System.String,Microsoft.Samples.SqlServer.IConfiguration)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private IConfiguration NewObject(string classType, string caption, int height)
        {
            EditObject obj;
            IConfiguration config = null;
            try
            {
                ConfigurationPanel.EditObjectButton.Text = caption;

                Assembly a = Assembly.GetAssembly(this.GetType());

                object[] args = new object[1];
                args[0] = SqlConnectionControl.ServiceBroker;
                //Create a new instance of class type stored in classType. 
                config = (IConfiguration)a.CreateInstance
                    (classType, true, BindingFlags.CreateInstance, null, args,
                    System.Globalization.CultureInfo.CurrentCulture, null);

                obj = ConfigurationPanel.AddObject("(New)", config);
                obj.Expand(height);
                obj.ButtonVisible = false;


            }
            catch (Exception ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }

            return config;
        }

        //Uses reflection to show a specific object in the PropertyGrid for any IConfiguration object
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private IConfiguration ShowObject(string classType, string name, string caption)
        {
            EditObject obj;
            IConfiguration config = null;

            try
            {
                ConfigurationPanel.EditObjectButton.Text = "Edit - " + caption;

                Assembly a = Assembly.GetAssembly(this.GetType());

                object[] args = new object[2];
                args[0] = name;
                args[1] = SqlConnectionControl.ServiceBroker;
                //Create an instance of class type stored in classType using object name. 
                config = (IConfiguration)a.CreateInstance
                    (classType, true, BindingFlags.CreateInstance, null, args,
                    System.Globalization.CultureInfo.CurrentCulture, null);

                obj = ConfigurationPanel.AddObject(name, config);
                obj.Expand(250);
                obj.ButtonVisible = false;
                ConfigurationPanel.QueryObject(config);
            }
            catch (Exception ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }

            return config;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private void ConfigurationPanel_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            IConfiguration config = (IConfiguration)ConfigurationPanel.ActiveEditObject.ObjectGrid.SelectedObject;
            
            if (!ConfigurationPanel.CreateStripButton.Enabled && !String.Equals(e.ChangedItem.Value, e.OldValue))
            {
                //Always lock the name value since the sample does not allow a name change.
                if (String.Equals(e.ChangedItem.Label, "Name"))
                {
                    config.Name = e.OldValue.ToString();
                    ConfigurationPanel.ActiveEditObject.ObjectGrid.Refresh();
                }
            }
        }

        private void SqlConnectionControl_Connected(object sender, EventArgs e)
        {
            ConfigurationPanel.EditToolStrip.Enabled = true;
            FillNewMenu();
        }

        private void ConfigurationPanel_StripButton_Click(object sender, EventArgs e)
        {
            OnQueryComplete(new EventArgs());
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Microsoft.Samples.SqlServer.Controls.ObjectsSplitPanel.AddObject(System.String,Microsoft.Samples.SqlServer.IConfiguration)")]
        private void OptionsButton_Click(object sender, EventArgs e)
        {
            EditObject obj;
            ConfigurationPanel.RemoveAllObjects();

            ConfigurationPanel.EditObjectButton.Text = "Application Configuration";
            
            obj = ConfigurationPanel.AddObject("Application Configuration", 
                SqlConnectionControl.OptionsConfiguration);
            obj.ButtonVisible = false;
           
            ConfigurationPanel.ActiveEditObject = obj;

            ConfigurationPanel.SetStripButtonState(ObjectsSplitPanel.operations.New);
            this.TaskTabControl.SelectTab(ConfigurationTabPageName); 

        }

        private void ObjectsTreeView_DoubleClick(object sender, EventArgs e)
        {
            TreeView tree = (TreeView)sender;
            if (tree.SelectedNode.Tag != null)
                if (String.Equals(tree.SelectedNode.Tag.ToString(), 
                    SqlConnectionControl.TreeNodeObjectType.ObjectNode.ToString()))
                    this.TaskTabControl.SelectTab(ConfigurationTabPageName); 
        }

        private void NewButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ConfigurationPanel.RemoveAllObjects();
            ConfigurationPanel.SetStripButtonState(ObjectsSplitPanel.operations.New);
            int height = 250;
            this.NewObject(e.ClickedItem.Tag.ToString(), e.ClickedItem.Text, height);

            this.TaskTabControl.SelectTab(ConfigurationTabPageName);
         }

        //Show the object selected in the ObjectsTree
        private void ObjectsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ConfigurationPanel.RemoveAllObjects();

                //Show Objects
                if (e.Node.Tag != null)
                {
                    if (String.Equals(e.Node.Tag.ToString(),
                        SqlConnectionControl.TreeNodeObjectType.ObjectNode.ToString()))
                    {
                        ConfigurationPanel.SetStripButtonState(ObjectsSplitPanel.operations.Show);
                        this.ShowObject(e.Node.Parent.Name, e.Node.Text, e.Node.Text);
                    }

                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        
        private void FillNewMenu()
        {
            this.ConfigurationPanel.NewStripButton.DropDownItems.Clear();

            //Fill from SqlConnection
            ToolStripItem newItem;
            foreach (ToolStripItem item in this.SqlConnectionControl.NewStripButton.DropDownItems)
            {
                newItem = this.ConfigurationPanel.NewStripButton.DropDownItems.Add(item.Text);
                newItem.Tag = item.Tag;
            }
        }

       
        private void TaskTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TaskTabControl.SelectedTab.Name == ObjectsTabPageName)
            {
                this.SqlConnectionControl.FillTree();
            }
        }
    }
}
