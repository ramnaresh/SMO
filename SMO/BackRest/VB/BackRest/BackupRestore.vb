'============================================================================
'  File:    BackupRestore.vb 
'
'  Summary: Implements a sample SMO backup and restore utility in VB.NET.
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

Public Class BackupRestore
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub BackupRestore_Load(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ' Load and display the server selection dialog
        ServerConn = New ServerConnection()
        scForm = New ServerConnect(ServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK _
            AndAlso ServerConn.SqlConnectionObject.State = ConnectionState.Open Then
            SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                ' Refresh database list
                ShowDatabases(True)
            End If
        Else
            Me.Close()
        End If

        If Not SqlServerSelection Is Nothing Then
            BackupFileTextBox.Text _
                = SqlServerSelection.Settings.BackupDirectory _
                & "\SmoDemoBackup.bak"
        End If
    End Sub

    Private Sub BackupRestore_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.SqlConnectionObject.State _
                = ConnectionState.Open Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub BackupButton_Click(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles BackupButton.Click
        Dim csr As Cursor = Nothing
        Dim backup As Backup
        Dim db As Database
        Dim backupDeviceItem As BackupDeviceItem

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Get database object from combobox
            db = CType(DatabasesComboBox.SelectedItem, Database)

            ' Backup a complete database to disk

            ' Create a new Backup object instance
            backup = New Backup()

            ' Backup database action
            backup.Action = BackupActionType.Database

            ' Set backup description
            backup.BackupSetDescription = "Sample Backup of " & db.Name

            ' Set backup name
            backup.BackupSetName = db.Name & " Backup"

            ' Set database name
            backup.Database = db.Name

            ' Create a file backup device
            backupDeviceItem = New BackupDeviceItem( _
                BackupFileTextBox.Text, DeviceType.File)

            ' Add a new backup device
            backup.Devices.Add(backupDeviceItem)

            ' Only store this backup in the set
            backup.Initialize = True

            ' Set the media name
            backup.MediaName = "Set 1"

            ' Set the media description
            backup.MediaDescription = "Sample Backup Media Set # 1"

            ' Notify this program every 5% 
            backup.PercentCompleteNotification = 5

            ' Set the backup file retention to the current server run value
            backup.RetainDays = db.Parent.Configuration.MediaRetention.RunValue

            ' Skip the tape header, because we are writing to a file
            backup.SkipTapeHeader = True

            ' Unload the tape after the backup completes
            backup.UnloadTapeAfter = True

            ' Add event handler to show progress
            AddHandler backup.PercentComplete, _
                AddressOf Me.ProgressEventHandler

            ' Generate and print script
            ResultsTextBox.AppendText(My.Resources.GeneratedScript)
            ScrollToBottom()

            ' Scripting here is strictly for text display purposes
            Dim script As String = backup.Script(SqlServerSelection)
            ResultsTextBox.AppendText(script & Environment.NewLine)
            ScrollToBottom()

            ResultsTextBox.AppendText(My.Resources.BackingUp)
            ScrollToBottom()
            UpdateStatus(0)

            backup.SqlBackup(SqlServerSelection)

            ResultsTextBox.AppendText(My.Resources.BackupComplete)
            ScrollToBottom()
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            ' Restore the original cursor
            Me.Cursor = csr
        End Try
    End Sub

    Private Sub ProgressEventHandler(ByVal sender As Object, _
        ByVal e As PercentCompleteEventArgs)
        ResultsTextBox.AppendText(My.Resources.ProgressCharacter)
        ScrollToBottom()
        UpdateStatus(e.Percent)
    End Sub

    Private Sub UpdateStatus(ByVal pct As Integer)
        statusBar1.Text = String.Format(System.Globalization.CultureInfo.InvariantCulture, _
            My.Resources.CompletedPercent, pct)
        statusBar1.Refresh()
    End Sub

    ''' <summary>
    ''' Restore the complete database to disk
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RestoreButton_Click(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles RestoreButton.Click
        Dim csr As Cursor = Nothing
        Dim restore As Restore
        Dim db As Database
        Dim backupDeviceItem As BackupDeviceItem

        ' Are you sure?  Default to No.
        If System.Windows.Forms.MessageBox.Show(Me, _
            String.Format(System.Globalization.CultureInfo.InvariantCulture, _
            My.Resources.ReallyRestore, DatabasesComboBox.Text), _
            Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button2, 0) = Windows.Forms.DialogResult.No Then
            Return
        End If

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Get database object from combobox
            db = CType(DatabasesComboBox.SelectedItem, Database)

            ' Create a new Restore object instance
            restore = New Restore

            ' Restore database action
            restore.Action = RestoreActionType.Database

            ' Set database name
            restore.Database = db.Name

            ' Create a file backup device
            backupDeviceItem = New BackupDeviceItem( _
                BackupFileTextBox.Text, DeviceType.File)

            ' Add database backup device
            restore.Devices.Add(backupDeviceItem)

            ' Notify this program every 5% 
            restore.PercentCompleteNotification = 5

            ' Replace the existing database
            restore.ReplaceDatabase = True

            ' Unload the backup device (tape)
            restore.UnloadTapeAfter = True

            ' add event handler to show progress
            AddHandler restore.PercentComplete, _
                AddressOf Me.ProgressEventHandler

            ' Generate and print script
            ResultsTextBox.AppendText(My.Resources.GeneratedScript)

            Dim strColl As System.Collections.Specialized.StringCollection _
                = restore.Script(SqlServerSelection)
            For Each str As String In strColl
                ResultsTextBox.AppendText(str & Environment.NewLine)
            Next

            ResultsTextBox.AppendText(My.Resources.Restoring)
            UpdateStatus(0)

            restore.SqlRestore(SqlServerSelection)

            ResultsTextBox.AppendText(My.Resources.RestoreComplete)
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            ' Restore the original cursor
            Me.Cursor = csr
        End Try
    End Sub

    Private Sub GetBackupFileButton_Click(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles GetBackupFileButton.Click
        If (BackupFileTextBox.Text.Length > 0) Then
            Me.OpenFileDialog1.InitialDirectory _
                = Path.GetDirectoryName(BackupFileTextBox.Text)
            Me.OpenFileDialog1.FileName = Path.GetFileName(BackupFileTextBox.Text)
        End If

        If (Me.OpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
            BackupFileTextBox.Text = Me.OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub ScrollToBottom()
        ResultsTextBox.Select()
        ResultsTextBox.SelectionStart = ResultsTextBox.Text.Length
        ResultsTextBox.ScrollToCaret()
    End Sub

    ''' <summary>
    ''' Show the current databases on the server
    ''' </summary>
    ''' <param name="selectDefault"></param>
    ''' <remarks></remarks>
    Private Sub ShowDatabases(ByVal selectDefault As Boolean)
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Clear control
            DatabasesComboBox.Items.Clear()

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Database), _
                New String() {"Name", "IsSystemObject", "IsAccessible"})

            ' Add database objects to combobox; the default ToString will display the database name
            For Each db As Database In SqlServerSelection.Databases
                If db.IsSystemObject = False AndAlso db.IsAccessible = True Then
                    DatabasesComboBox.Items.Add(db)
                End If
            Next

            If selectDefault = True _
                AndAlso DatabasesComboBox.Items.Count > 0 Then
                DatabasesComboBox.SelectedIndex = 0
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub
End Class
