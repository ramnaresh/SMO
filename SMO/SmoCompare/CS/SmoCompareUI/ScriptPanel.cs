/*=====================================================================

  File:      ScriptPanel.cs
  Summary:   Window to display the change script
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
using System.Text;
using System.Windows.Forms;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.MessageBox;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class ScriptPanel : Form
    {
        private ServerConnection serverConn;
        private StringCollection collScript;

        public ScriptPanel(ServerConnection serverConn, StringCollection collScript)
        {
            InitializeComponent();

            this.serverConn = serverConn;
            this.collScript = collScript;
            panelTextBox.Clear();

            foreach (string line in collScript)
            {
                panelTextBox.AppendText(line + Environment.NewLine);
                panelTextBox.AppendText(Properties.Resources.Go);
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            // Perform run on the connection         
            // Connect to SQL Server
            Server server = new Server(serverConn);
            try
            {
                server.ConnectionContext.ExecuteNonQuery(collScript, ExecutionTypes.ContinueOnError);
            }
            catch (SmoException ex)
            {
                ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                emb.Show(this);
            }
        }
    }
}