/*=====================================================================

  File:      SqlObjectBrowser.cs
  Summary:   Object browser window to allow object selection
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class SqlObjectBrowser : Form
    {
        private Urn objUrn;
        private Server server;
        private bool connected;

        public bool Connected
        {
            get
            {
                return connected;
            }
        }

        public SqlObjectBrowser(ServerConnection serverConn)
        {
            InitializeComponent();

            // Connect to SQL Server
            server = new Server(serverConn);
            try
            {
                server.ConnectionContext.Connect();

                // In case connection succeeded we add the sql server node as root in object explorer (treeview)
                TreeNode tn = new TreeNode();
                tn.Text = server.Name;
                tn.Tag = server.Urn;
                this.objUrn = server.Urn;
                objectBrowserTreeView.Nodes.Add(tn);

                AddDummyNode(tn);

                connected = true;
            }
            catch (ConnectionException)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox();
                emb.Text = Properties.Resources.ConnectionFailed;
                emb.Show(this);
            }
            catch (ApplicationException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        public Urn Urn
        {
            get
            {
                return this.objUrn;
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (objectBrowserTreeView.SelectedNode.Tag != null)
            {
                this.objUrn = (Urn)objectBrowserTreeView.SelectedNode.Tag;
            }

            this.Visible = false;
        }

        private void cancelCommandButton_Click(object sender, EventArgs e)
        {
            this.objUrn = null;
            this.Visible = false;
        }

        private void objectBrowserTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();

            AddDummyNode(e.Node);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void objectBrowserTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            // Remove dummy node
            e.Node.Nodes.RemoveAt(0);
            SqlSmoObject smoObj = null;
            Urn urnNode = (Urn)e.Node.Tag;

            try
            {
                smoObj = server.GetSmoObject(urnNode);
                AddCollections(e, smoObj);
            }
            catch (UnsupportedVersionException)
            {
                // Right now don't do anything... but you might change the 
                // Icon so it will emphasize the version issue
            }
            catch (SmoException)
            {
                try
                {
                    AddItemInCollection(e, server);
                }
                catch (Exception except)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox(except);
                    emb.Show(this);
                }
            }
            catch (ApplicationException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }

        private static void AddItemInCollection(System.Windows.Forms.TreeViewEventArgs node, Server sqlServer)
        {
            Urn urnNode = (Urn)node.Node.Tag;
            SqlSmoObject smoObj = sqlServer.GetSmoObject(urnNode.Parent);
            TreeNode tn;
            NamedSmoObject namedObj;

            PropertyInfo p = smoObj.GetType().GetProperty(node.Node.Text);
            if (p != null)
            {
                ICollection iColl = p.GetValue(smoObj, null) as ICollection;
                if (iColl != null)
                {
                    IEnumerator enum1 = iColl.GetEnumerator();
                    while (enum1.MoveNext())
                    {
                        tn = new TreeNode();
                        namedObj = enum1.Current as NamedSmoObject;
                        if (namedObj != null)
                        {
                            tn.Text = namedObj.Name;
                        }
                        else
                        {
                            tn.Text = ((SqlSmoObject)enum1.Current).Urn;
                        }

                        tn.Tag = ((SqlSmoObject)enum1.Current).Urn;
                        node.Node.Nodes.Add(tn);

                        AddDummyNode(tn);
                    }
                }
                else
                {
                    Console.WriteLine(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        Properties.Resources.PropertyNotICollection, p.Name));
                }
            }
            else
            {
                Console.WriteLine(string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.PropertyNotFound, node.Node.Text));
            }
        }

        /// <summary>
        /// Add the dummy node.
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        private static void AddDummyNode(TreeNode tn)
        {
            TreeNode tnD;

            tnD = new TreeNode();
            tnD.Tag = @"dummy";
            tn.Nodes.Add(tnD);
        }

        /// <summary>
        /// Adds children to the current node. basically all collection of a SqlSmoObject object
        /// </summary>
        /// <param name="node"></param>
        /// <param name="smoObj"></param>
        private static void AddCollections(System.Windows.Forms.TreeViewEventArgs node, SqlSmoObject smoObj)
        {
            TreeNode tn;
            Type t = smoObj.GetType();
            foreach (System.Reflection.PropertyInfo p in t.GetProperties())
            {
                if (p.Name != "SystemMessages"
                    && p.Name != "UserDefinedMessages")
                {
                    // If it is collection we create a node for it
                    if (p.PropertyType.IsSubclassOf(typeof(AbstractCollectionBase)))
                    {
                        tn = new TreeNode();
                        tn.Text = p.Name;
                        tn.Tag = new Urn(smoObj.Urn + @"/" + p.Name);
                        node.Node.Nodes.Add(tn);

                        AddDummyNode(tn);
                    }
                }
            }

            // Verify if we are at the server level and add the JobServer too 
            if (smoObj is Server)
            {
                tn = new TreeNode();
                tn.Text = Properties.Resources.JobServer;
                tn.Tag = new Urn(smoObj.Urn + @"/" + Properties.Resources.JobServer);
                node.Node.Nodes.Add(tn);

                AddDummyNode(tn);
            }

            smoObj = null;
        }

        private void SqlObjectBrowser_Load(object sender, EventArgs e)
        {
            // Display the first level of nodes
            if (objectBrowserTreeView.Nodes.Count > 0)
            {
                objectBrowserTreeView.Nodes[0].Expand();
            }
        }
    }
}