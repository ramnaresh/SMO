'============================================================================
'  File:    ServerInfo.vb 
'
'  Summary: Implements a sample SMO server information utility in VB.NET.
'
'  Date:    January 25, 2005
'------------------------------------------------------------------------------
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
Public Class ServerInfo
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub ServerInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ' Create the server connection
        ServerConn = New ServerConnection

        ' Create the server connect dialog
        scForm = New ServerConnect(ServerConn)

        ' Display the server connect dialog
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK AndAlso ServerConn.SqlConnectionObject.State _
            = ConnectionState.Open Then
            SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                ' Refresh server information
                ShowServerInformation()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub ServerInfo_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.SqlConnectionObject.State _
                = ConnectionState.Open Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub RefreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshButton.Click
        ShowServerInformation()
    End Sub

    Private Sub ShowServerInformation()
        Dim ServerListViewItem As ListViewItem

        Try
            ServerListView.BeginUpdate()

            ' Clear control
            ServerListView.Items.Clear()

            ' Initialize the server settings
            SqlServerSelection.Settings.Initialize(True)

            ' Initialize the server information settings
            SqlServerSelection.Information.Initialize(True)

            ' Initialize the server
            SqlServerSelection.Initialize(True)

            ' Iterate through all the properties and add each one to the list
            For Each prop As [Property] In SqlServerSelection.Settings.Properties
                ServerListViewItem = ServerListView.Items.Add(prop.Name)
                If (Not prop.Value Is Nothing) Then
                    ServerListViewItem.SubItems.Add(prop.Value.ToString())
                End If
            Next

            ' Iterate through all the properties and add each one to the list
            For Each prop As [Property] In SqlServerSelection.Properties
                ServerListViewItem = ServerListView.Items.Add(prop.Name)
                If (Not prop.Value Is Nothing) Then
                    ServerListViewItem.SubItems.Add(prop.Value.ToString())
                End If
            Next

            ServerListView.EndUpdate()

            ConnectionListView.BeginUpdate()

            ' Clear control
            ConnectionListView.Items.Clear()

            For Each prop As [Property] In SqlServerSelection.Information.Properties
                ServerListViewItem = ConnectionListView.Items.Add(prop.Name)
                If (Not prop.Value Is Nothing) Then
                    ServerListViewItem.SubItems.Add(prop.Value.ToString())
                End If
            Next

            ConnectionListView.EndUpdate()

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub
End Class
