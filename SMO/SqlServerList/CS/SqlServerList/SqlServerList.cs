/*============================================================================
  File:    SqlServerList.cs 

  Summary: Implements a sample SMO SQL server list utility in C#.

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
using System.Data.Sql;
using System.Drawing;
using System.Windows.Forms;
// SMO namespaces
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

#endregion

namespace Microsoft.Samples.SqlServer
{
    partial class SQLServerList : Form
    {
        public SQLServerList()
        {
            InitializeComponent();
        }

        private void SQLServerList_Load(object sender, EventArgs e)
        {
            Cursor csr = null;
            DataTable dt;

            csr = this.Cursor;   // Save the old cursor
            this.Cursor = Cursors.WaitCursor;   // Display the waiting cursor

            //Get list of servers
            dt = SmoApplication.EnumAvailableSqlServers(false);
            serverListBox1.DataSource = dt;
            serverListBox1.DisplayMember = "Name";

            //Get list of servers
            dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            serverListBox2.DataSource = dt;
            serverListBox2.DisplayMember = "ServerName";

            this.Cursor = csr;  // Restore the original cursor
        }
    }
}