'============================================================================
'  File:    VerifyBackup.vb 
'
'  Summary: Implements a sample SMO backup verification utility in VB.NET.
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

Public Class VerifyBackup
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server
    Private BackupDeviceList As BackupDeviceCollection
    Private sqlRestore As Restore

    Private Sub VerifyBackup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ServerConn = New ServerConnection
        scForm = New ServerConnect(ServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK AndAlso ServerConn.SqlConnectionObject.State = ConnectionState.Open Then
            SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                ' Refresh device list
                GetBackupDevicesList()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub VerifyBackup_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.SqlConnectionObject.State = ConnectionState.Open Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub BackupDeviceComboBox_SelectedIndexChangedComboBox(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackupDeviceComboBox.SelectedIndexChanged
        If BackupDeviceComboBox.SelectedIndex >= 0 Then
            BackupContentsListView.Items.Clear()
            GetBackupDeviceInfo(BackupDeviceComboBox.Text)
        End If

        UpdateButtons()
    End Sub

    Private Sub BackupContentsListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupContentsListView.SelectedIndexChanged
        UpdateButtons()
    End Sub

    Private Sub VerifyButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles VerifyButton.Click
        ' Verify the backup set
        VerifyBackupSet(BackupDeviceComboBox.Text)
    End Sub

    Private Sub GetBackupDeviceContents(ByVal BackupDeviceName As String)
        Dim csr As Cursor = Nothing
        Dim dataTable As DataTable
        Dim DeviceListViewItem As ListViewItem
        Dim backupDeviceItem As BackupDeviceItem

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Create the restore object
            sqlRestore = New Restore

            ' Create a file backup device
            backupDeviceItem = New BackupDeviceItem( _
                BackupDeviceName, DeviceType.LogicalDevice)

            ' Add the backup device to the restore object 
            sqlRestore.Devices.Add(backupDeviceItem)

            ' Get the backup header information
            dataTable = sqlRestore.ReadBackupHeader(SqlServerSelection)

            ' Create the columns in the listview
            BackupContentsListView.Columns.Clear()
            For ColumnCount As Integer = 0 To dataTable.Columns.Count - 1
                BackupContentsListView.Columns.Add(dataTable.Columns(ColumnCount).ColumnName, _
                    100, HorizontalAlignment.Left)
            Next

            ' Populate the listview
            BackupContentsListView.Items.Clear()
            For RowCount As Integer = 0 To dataTable.Rows.Count - 1
                DeviceListViewItem = BackupContentsListView.Items.Add( _
                    dataTable.Rows(RowCount)(0).ToString())
                For ColumnCount As Integer = 1 To dataTable.Columns.Count - 1
                    DeviceListViewItem.SubItems.Add(dataTable.Rows(RowCount)(ColumnCount).ToString())
                Next
            Next

            ' Select the first item in the list
            If BackupContentsListView.Items.Count > 0 Then
                BackupContentsListView.Items(0).Selected = True
                BackupContentsListView.Items(0).EnsureVisible()
            End If

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Catch ex As ExecutionFailureException
            ' handle the case of a bad backupset
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub GetBackupDevicesList()
        Dim csr As Cursor = Nothing
        Dim bkupDevice As BackupDevice

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Clear controls
            BackupDeviceComboBox.Items.Clear()

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(BackupDevice), _
                New String() {"Name", "BackupDeviceType", "PhysicalLocation"})

            ' Get the appropriate BackupDevice object from the BackupDevices
            ' collection of a connected SQLServer object.
            BackupDeviceList = SqlServerSelection.BackupDevices

            ' Get devices
            For Each bkupDevice In BackupDeviceList
                ' Add device name to remove combo box
                BackupDeviceComboBox.Items.Add(bkupDevice.Name)
            Next

            If BackupDeviceComboBox.Items.Count > 0 Then
                BackupDeviceComboBox.SelectedIndex = 0
            End If
        Catch ex As SmoException
            ' handle the case of a bad backupset
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub GetBackupDeviceInfo(ByVal BackupDeviceName As String)
        Dim bkupDevice As BackupDevice

        ' Get the appropriate BackupDevice object from the BackupDevices
        ' collection of a connected SQLServer object.
        BackupDeviceList = SqlServerSelection.BackupDevices

        ' Get selected device
        bkupDevice = BackupDeviceList(BackupDeviceName)

        DeviceTypeLabel.Text = bkupDevice.BackupDeviceType.ToString()
        StatusLabel.Text = bkupDevice.State.ToString()
        LocationLabel.Text = bkupDevice.PhysicalLocation
    End Sub

    Private Sub VerifyBackupSet(ByVal BackupDeviceName As String)
        Dim csr As Cursor = Nothing
        Dim verified As [Boolean]
        Dim backupDeviceItem As BackupDeviceItem

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Create a Restore object
            ' Fire any events
            sqlRestore = New Restore()

            ' Create a file backup device
            backupDeviceItem = New BackupDeviceItem( _
                BackupDeviceName, DeviceType.LogicalDevice)

            ' Add the backup device to the restore object 
            sqlRestore.Devices.Add(backupDeviceItem)

            ' Call the SQLVerify method of the Restore object using the SQLServer object
            verified = sqlRestore.SqlVerify(SqlServerSelection, True)

            Dim emb As New ExceptionMessageBox()
            emb.Text = String.Format( _
                System.Globalization.CultureInfo.InvariantCulture, _
                My.Resources.VerifiedResult, _
                verified.ToString(System.Globalization.CultureInfo.InvariantCulture))
            emb.Show(Me)

            Return

        Catch ex As SmoException
            ' handle the case of a bad backupset
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            sqlRestore = Nothing
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub ReadHeaderButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadHeaderButton.Click
        GetBackupDeviceContents(BackupDeviceComboBox.Text)
    End Sub

    Private Sub UpdateButtons()
        If BackupDeviceComboBox.SelectedIndex >= 0 Then
            ReadHeaderButton.Enabled = True
        Else
            ReadHeaderButton.Enabled = False
        End If

        If (BackupContentsListView.SelectedItems.Count > 0) Then
            VerifyButton.Enabled = True
        Else
            VerifyButton.Enabled = False
        End If
    End Sub
End Class
