'============================================================================
'  File:    SqlServerList.vb
'
'  Summary: Implements a sample SMO SQL server list utility in VB.NET.
'
'  Date:    January 25, 2005
'----------------------------------------------------------------------------
'  This file is part of the Microsoft SQL Server Code Samples.
'
'  Copyright (C) Microsoft Corporation.  All rights reserved.
'
'  This source code is intended only as a supplement to Microsoft
'  Development Tools and/or on-line documentation.  See these other
'  materials for detailed information regarding Microsoft code samples.
'
'  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
'  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'  PARTICULAR PURPOSE.
'============================================================================

Public Class SqlServerList

    Private Sub SqlServerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim csr As Cursor = Nothing
        Dim dt As DataTable

        Me.Show()
        Application.DoEvents()

        csr = Me.Cursor   ' Save the old cursor
        Me.Cursor = Cursors.WaitCursor    ' Display the waiting cursor

        'Get list of servers
        dt = SmoApplication.EnumAvailableSqlServers(False)
        serverListBox1.DataSource = dt
        serverListBox1.DisplayMember = "Name"

        'Get list of servers
        dt = SqlDataSourceEnumerator.Instance.GetDataSources()
        serverListBox2.DataSource = dt
        serverListBox2.DisplayMember = "ServerName"

        Me.Cursor = csr ' Restore the original cursor
    End Sub
End Class
