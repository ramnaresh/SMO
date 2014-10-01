/*============================================================================
  File:    SmoBrowser.cs 

  Summary: Implements a sample SMO browser utility in C#.

  Date:    August 16, 2005
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
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Wmi;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class SmoBrowser : Form
    {
        // Use the Server object to connect to a specific server
        private Server SqlServerSelection;

        public SmoBrowser()
        {
            InitializeComponent();
        }

        private void SmoBrowser_Load(object sender, EventArgs e)
        {
            ServerConnection ServerConn = new ServerConnection();
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
                }
            }
            else
            {
                this.Close();
            }

            if (SqlServerSelection != null)
            {
                objectTreeView.BeginUpdate();

                // By default create Server and Managed Computer nodes
                objectTreeView.Nodes.Add(CreateNode(
                    String.Format(CultureInfo.CurrentUICulture,
                    Properties.Resources.SQLServerNodeName,
                    SqlServerSelection.Name),
                    SqlServerSelection));
                objectTreeView.Nodes.Add(CreateNode(
                    String.Format(CultureInfo.CurrentUICulture,
                    Properties.Resources.ManagedComputerNodeName,
                    SqlServerSelection.Name),
                    new ManagedComputer(SqlServerSelection.Name)));

                objectTreeView.EndUpdate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void OnBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.Nodes.Count == 1
                    && e.Node.Nodes[0].Text == Properties.Resources.NodePlaceHolder)
                {
                    e.Node.Nodes.RemoveAt(0);
                    if (e.Node.Tag is ICollection)
                    {
                        PopulateCollectionItems(e.Node);
                    }
                    else
                    {
                        PopulateExpandableProperties(e.Node);
                    }
                }
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        private void AfterSelect(object sender, TreeViewEventArgs e)
        {
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                this.propertyGrid1.SelectedObject = e.Node.Tag;
                this.textBox1.Text = GetScriptText(e.Node.Tag as IScriptable);
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
            finally
            {
                this.Cursor = oldCursor;
            }
        }

        private static string GetScriptText(IScriptable scriptable)
        {
            if (scriptable == null)
            {
                return String.Empty;
            }

            StringBuilder stringBuilder = new StringBuilder();
            StringCollection batches;
            try
            {
                batches = scriptable.Script();
            }
            catch {
                return string.Empty;
            }
            foreach (string batch in batches)
            {
                stringBuilder.AppendFormat(@"{0}\r\nGO\r\n", batch);
            }

            return stringBuilder.ToString();
        }

        private static TreeNode CreateNode(object item)
        {
            string name = null;
            PropertyDescriptor property;

            foreach (string propertyName in new string[] { @"Name", @"Urn" })
            {
                property = TypeDescriptor.GetProperties(item)[propertyName];
                if (property != null)
                {
                    name = property.GetValue(item).ToString();
                    break;
                }
            }

            if (name == null)
            {
                name = item.ToString();
            }

            return CreateNode(name, item);
        }

        private static TreeNode CreateNode(string name, object item)
        {
            TreeNode node = new TreeNode(name);

            node.Tag = item;
            if (item is ICollection || HasExpandableProperties(item))
            {
                node.Nodes.Add(Properties.Resources.NodePlaceHolder);
            }

            return node;
        }

        private static bool HasExpandableProperties(object item)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(item))
            {
                if (IsExpandableProperty(property))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsCollection(PropertyDescriptor property)
        {
            foreach (Type type in property.PropertyType.GetInterfaces())
            {
                if (type == typeof(ICollection))
                {
                    if (property.PropertyType.IsArray)
                    {
                        break;
                    }

                    if (property.Name == @"Properties")
                    {
                        break;
                    }

                    return true;
                }
            }

            return false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void PopulateExpandableProperties(TreeNode node)
        {
            object value;
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(node.Tag))
                {
                    if (!IsExpandableProperty(property))
                    {
                        continue;
                    }

                    try
                    {
                        value = property.GetValue(node.Tag);
                    }
                    catch (Exception ex)
                    {
                        value = String.Format(CultureInfo.CurrentUICulture,
                            Properties.Resources.ExceptionString,
                            ex.GetType(), ex.Message);
                    }

                    node.Nodes.Add(CreateNode(property.Name, value));
                }
            }
            finally
            {
                this.Cursor = oldCursor;
            }
        }

        private static bool IsExpandableProperty(PropertyDescriptor property)
        {
            if (!IsExpandablePropertyType(property.PropertyType))
            {
                return false;
            }

            if (property.PropertyType.IsArray)
            {
                if (!IsExpandablePropertyType(property.PropertyType.GetElementType()))
                {
                    return false;
                }
            }

            if (property.Name == @"Urn" ||
                property.Name == @"UserData" ||
                property.Name == @"Properties" ||
                property.Name == @"ExtendedProperties" ||
                property.Name == @"Parent" ||
                property.Name == @"Events")
            {
                return false;
            }

            return true;
        }

        private static bool IsExpandablePropertyType(Type type)
        {
            if (Type.GetTypeCode(type) != TypeCode.Object)
            {
                return false;
            }

            if (type == typeof(Guid) || type == typeof(DateTime))
            {
                return false;
            }

            return true;
        }

        private void PopulateCollectionItems(TreeNode node)
        {
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                foreach (object item in (ICollection)node.Tag)
                {
                    node.Nodes.Add(CreateNode(item));
                }
            }
            finally
            {
                this.Cursor = oldCursor;
            }
        }
    }
}