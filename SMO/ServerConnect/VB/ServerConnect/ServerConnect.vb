'============================================================================
'  File:    ServerConnect.vb 
'
'  Summary: Implements a sample SMO server connection utility in VB.NET.
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
Public Class ServerConnect
    ' Class variables
    Private ServerConn As ServerConnection
    Private bError As Boolean

    Public Sub New(ByVal connection As ServerConnection)
        '
        ' Required for Windows Form Designer support
        '
        InitializeComponent()

        ServerConn = connection
    End Sub

    Private Sub ConnectCommandButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectCommandButton.Click
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            bError = False ' Assume no error

            ' Recreate connection if necessary
            If ServerConn Is Nothing Then
                ServerConn = New ServerConnection
            End If

            ' Fill in necessary information
            ServerConn.ServerInstance = ServerNamesComboBox.Text

            ' Setup capture and execute to be able to display script
            ServerConn.SqlExecutionModes = SqlExecutionModes.ExecuteAndCaptureSql
            ServerConn.ConnectTimeout = CType(TimeoutUpDown.Value, Int32)
            If WindowsAuthenticationRadioButton.Checked = True Then
                ' Use Windows authentication
                ServerConn.LoginSecure = True
            Else
                ' Use SQL Server authentication
                ServerConn.LoginSecure = False
                ServerConn.Login = UserNameTextBox.Text
                ServerConn.Password = PasswordTextBox.Text
            End If

            If DisplayEventsCheckBox.CheckState = CheckState.Checked Then
                AddHandler ServerConn.InfoMessage, _
                    AddressOf OnSqlInfoMessage
                AddHandler ServerConn.ServerMessage, _
                    AddressOf OnServerMessage
                AddHandler ServerConn.SqlConnectionObject.StateChange, _
                    AddressOf OnStateChange
                AddHandler ServerConn.StatementExecuted, _
                    AddressOf OnStatementExecuted
            End If

            ' Go ahead and connect
            ServerConn.Connect()

        Catch ex As ConnectionFailureException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
            bError = True

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
            bError = True

        Finally
            ' Restore the original cursor
            Me.Cursor = csr
        End Try
    End Sub

    Private Sub CancelCommandButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelCommandButton.Click
        ServerConn = Nothing

        Me.Close()
    End Sub

    Private Sub WindowsAuthenticationRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WindowsAuthenticationRadioButton.CheckedChanged
        ProcessWindowsAuthentication()
    End Sub

    Private Sub ServerConnect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetServerList()

        ProcessWindowsAuthentication()
    End Sub

    Private Sub ServerConnect_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If bError = True Then
            e.Cancel = True
        End If

        ' Reset error condition
        bError = False
    End Sub

    Private Sub GetServerList()
        Dim dt As DataTable

        ' Get list of SQL Servers
        dt = SmoApplication.EnumAvailableSqlServers(False)

        If (dt.Rows.Count > 0) Then
            ' Load server names into combo box
            For Each dr As DataRow In dt.Rows
                ServerNamesComboBox.Items.Add(dr("Name"))
            Next

            ' Default to this machine 
            ServerNamesComboBox.SelectedIndex _
                = ServerNamesComboBox.FindStringExact( _
                Environment.MachineName)

            ' If this machine is not a SQL server 
            'then select the first server in the list
            If (ServerNamesComboBox.SelectedIndex < 0) Then
                ServerNamesComboBox.SelectedIndex = 0
            End If
        Else
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.NoSqlServers
            emb.Show(Me)
        End If
    End Sub

    Private Sub ProcessWindowsAuthentication()
        If WindowsAuthenticationRadioButton.Checked = True Then
            UserNameTextBox.Enabled = False
            PasswordTextBox.Enabled = False
        Else
            UserNameTextBox.Enabled = True
            PasswordTextBox.Enabled = True
        End If
    End Sub

    Private Sub OnSqlInfoMessage(ByVal sender As Object, ByVal args As System.Data.SqlClient.SqlInfoMessageEventArgs)
        Dim err As SqlError
        Dim emb As ExceptionMessageBox

        For Each err In args.Errors
            emb = New ExceptionMessageBox()
            emb.Text = String.Format(System.Globalization.CultureInfo.InvariantCulture, _
                My.Resources.SqlError, err.Source, _
                err.Class, err.State, err.Number, err.LineNumber, _
                err.Procedure, err.Server, err.Message)
            emb.Show(Me)
        Next
    End Sub

    Private Sub OnServerMessage(ByVal sender As Object, ByVal args As ServerMessageEventArgs)
        Dim err As SqlError = args.Error

        Dim emb As New ExceptionMessageBox()
        emb.Text = String.Format(System.Globalization.CultureInfo.InvariantCulture, _
        My.Resources.SqlError, err.Source, _
            err.Class, err.State, err.Number, err.LineNumber, _
            err.Procedure, err.Server, err.Message)
        emb.Show(Me)
    End Sub

    Private Sub OnStateChange(ByVal sender As Object, ByVal args As StateChangeEventArgs)
        If (Me.IsDisposed = False) Then
            Dim emb As New ExceptionMessageBox()
            emb.Text = String.Format(System.Globalization.CultureInfo.InvariantCulture, _
                My.Resources.StateChanged, _
                args.OriginalState.ToString(), args.CurrentState.ToString())
            emb.Show(Me)
        End If
    End Sub

    Private Sub OnStatementExecuted(ByVal sender As Object, ByVal args As StatementEventArgs)
        Dim emb As New ExceptionMessageBox()
        emb.Text = args.SqlStatement
        emb.Show(Me)
    End Sub
End Class