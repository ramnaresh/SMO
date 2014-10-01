'============================================================================
'  File:    ManageDatabases.vb 
'
'  Summary: Implements an SMO create database sample in VB.NET.
'
'  Date:    June 06, 2005
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

Public Class ManageDatabases
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub ManageDatabases_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ServerConn = New ServerConnection()
        scForm = New ServerConnect(ServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK _
            AndAlso ServerConn.IsOpen = True Then
            SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                ' Refresh database list
                ShowDatabases(False)
            End If
        Else
            Me.Close()
        End If

        UpdateControls()
    End Sub

    Private Sub ManageDatabases_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.IsOpen = True Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub CreateButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateButton.Click
        ' Create the database
        Dim sDatabaseName As String
        Dim db As Database
        Dim fg As FileGroup
        Dim df As DataFile
        Dim lf As LogFile
        Dim csr As Cursor = Nothing
        Dim DatabaseListViewItem As ListViewItem

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            ' Get the name of the new database
            sDatabaseName = NewDatabaseTextBox.Text

            ' Check for new non-zero length name
            If sDatabaseName.Length = 0 Then
                System.Windows.Forms.MessageBox.Show(Me, _
                My.Resources.NoDatabaseName, Me.Text, _
                MessageBoxButtons.OK, MessageBoxIcon.Error, _
                MessageBoxDefaultButton.Button1, 0)

                Return
            End If

            ' Ensure we have the current list of databases to check.
            SqlServerSelection.Databases.Refresh()

            ' Refresh database list
            ShowDatabases(False)

            ' Is database name new and unique?
            If (SqlServerSelection.Databases.Contains(sDatabaseName)) Then
                System.Windows.Forms.MessageBox.Show(Me, _
                My.Resources.DuplicateDatabaseName, _
                Me.Text, MessageBoxButtons.OK, _
                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0)

                Return
            End If

            ' Instantiate a new database object
            db = New Database(SqlServerSelection, sDatabaseName)

            ' This may also be accomplished like so:
            ' db = new Database()
            ' db.Parent = SqlServerSelection
            ' db.Name = sDatabaseName

            ' Add a new file group named PRIMARY to the database's FileGroups collection
            fg = New FileGroup(db, "PRIMARY")

            ' Create a new data file and add it to the file group's Files collection
            ' Give the data file a physical filename using the master database path of the server
            df = New DataFile(fg, sDatabaseName & "_Data0", _
                SqlServerSelection.Information.MasterDBPath & "\" _
                    & sDatabaseName & "_Data0" & ".mdf")

            ' Set the size, growth, and maximum size of the data file
            df.GrowthType = FileGrowthType.KB
            df.Growth = 1024 ' In KB
            df.Size = 10240 ' Set initial size in KB (optional)
            df.MaxSize = 20480 ' In KB

            fg.Files.Add(df)

            ' Create a new data file and add it to the file group's Files collection
            ' Give the data file a physical filename using the master database path of the server
            df = New DataFile(fg, sDatabaseName & "_Data1", _
                SqlServerSelection.Information.MasterDBPath & "\" _
                    & sDatabaseName & "_Data1" & ".ndf")

            ' Set the size, growth, and maximum size of the data file
            df.GrowthType = FileGrowthType.KB
            df.Growth = 1024 ' In KB
            df.Size = 2048 ' Set initial size in KB (optional)
            df.MaxSize = 8192 ' In KB

            fg.Files.Add(df)

            ' Add the new file group to the database's FileGroups collection
            db.FileGroups.Add(fg)

            ' Add a new file group named PRIMARY to the database's FileGroups collection
            fg = New FileGroup(db, "SECONDARY")

            ' Create a new data file and add it to the file group's Files collection
            ' Give the data file a physical filename using the master database path of the server
            df = New DataFile(fg, sDatabaseName & "_Data2", _
                SqlServerSelection.Information.MasterDBPath & "\" _
                    & sDatabaseName & "_Data2" & ".ndf")

            ' Set the size, growth, and maximum size of the data file
            df.GrowthType = FileGrowthType.KB
            df.Growth = 512 ' In KB
            df.Size = 1024 ' Set initial size in KB (optional)
            df.MaxSize = 4096 ' In KB

            fg.Files.Add(df)

            ' Create a new data file and add it to the file group's Files collection
            ' Give the data file a physical filename using the master database path
            df = New DataFile(fg, sDatabaseName & "_Data3", _
                SqlServerSelection.Information.MasterDBPath & "\" _
                    & sDatabaseName & "_Data3" & ".ndf")

            ' Set the size, growth, and maximum size of the data file
            df.GrowthType = FileGrowthType.KB
            df.Growth = 512 ' In KB
            df.Size = 1024 ' Set initial size in KB (optional)
            df.MaxSize = 4096 ' In KB

            fg.Files.Add(df)

            ' Add the new file group to the database's FileGroups collection
            db.FileGroups.Add(fg)

            ' Define the database transaction log.
            lf = New LogFile(db, sDatabaseName & "_Log", _
                SqlServerSelection.Information.MasterDBPath & "\" _
                    & sDatabaseName & "_Log" & ".ldf")
            lf.GrowthType = FileGrowthType.KB
            lf.Growth = 1024 ' In KB
            lf.Size = 2048 ' Set initial size in KB (optional)
            lf.MaxSize = 8192 ' In KB

            db.LogFiles.Add(lf)

            ' Create the database as defined.
            db.Create()

            ' Refresh database list
            ShowDatabases(False)

            ' Find and select the database just created
            DatabaseListViewItem _
                = DatabasesListView.FindItemWithText(sDatabaseName)
            DatabaseListViewItem.Selected = True
            DatabaseListViewItem.EnsureVisible()
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            ' Clean up.
            db = Nothing
            fg = Nothing
            df = Nothing
            lf = Nothing

            UpdateControls()

            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub DeleteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteButton.Click
        Dim sDatabaseName As String
        Dim csr As Cursor = Nothing
        Dim db As Database

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Use the selected database as the one to be deleted
            sDatabaseName = DatabasesListView.SelectedItems(0).Text

            ' Drop (Delete) the database
            db = SqlServerSelection.Databases(sDatabaseName)
            If Not (db Is Nothing) Then
                ' Are you sure?  Default to No.
                If System.Windows.Forms.MessageBox.Show(Me, String.Format( _
                    System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.ReallyDrop, _
                    db.Name), Me.Text, MessageBoxButtons.YesNo, _
                        MessageBoxIcon.Question, _
                        MessageBoxDefaultButton.Button2, 0) = Windows.Forms.DialogResult.No Then
                    Return
                End If

                db.Drop()

                sbrStatus.Text = String.Format( _
                    System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.DatabaseDeleted, sDatabaseName)
            End If

            ' Refresh database list
            ShowDatabases(False)

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            UpdateControls()
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub ShowServerMessagesCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowServerMessagesCheckBox.CheckedChanged
        If ShowServerMessagesCheckBox.CheckState = CheckState.Checked Then
            AddHandler SqlServerSelection.ConnectionContext.InfoMessage, _
                AddressOf SqlInfoMessage
            AddHandler SqlServerSelection.ConnectionContext.ServerMessage, _
                AddressOf ServerMessage
        Else
            RemoveHandler SqlServerSelection.ConnectionContext.InfoMessage, _
                AddressOf SqlInfoMessage
            RemoveHandler SqlServerSelection.ConnectionContext.ServerMessage, _
                AddressOf ServerMessage
        End If
    End Sub

    Private Sub SqlInfoMessage(ByVal sender As Object, ByVal e As SqlInfoMessageEventArgs)
        EventLogTextBox.AppendText(My.Resources.SqlInfoMessage & e.ToString() _
            & Environment.NewLine)
    End Sub

    Private Sub ServerMessage(ByVal sender As Object, ByVal e As ServerMessageEventArgs)
        EventLogTextBox.AppendText(My.Resources.ServerMessage & e.ToString() _
            & Environment.NewLine)
    End Sub

    Private Sub ServerStatementExecuted(ByVal sender As Object, ByVal e As StatementEventArgs)
        Dim sTmp As String = e.ToString().Replace(vbLf, Environment.NewLine)
        EventLogTextBox.AppendText(My.Resources.SqlStatementExecuted & sTmp _
            & Environment.NewLine)
    End Sub

    Private Sub ShowDatabases(ByVal selectDefault As Boolean)
        ' Show the current databases on the server
        Dim DatabaseListViewItem As ListViewItem
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            ' Clear control
            DatabasesListView.Items.Clear()

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Database), _
                New String() {"Name", "IsSystemObject", "IsAccessible", _
                "Status", "CreateDate", "Size", _
                "SpaceAvailable", "CompatibilityLevel"})

            For Each db As Database In SqlServerSelection.Databases
                If db.IsSystemObject = False AndAlso db.IsAccessible = True Then
                    DatabaseListViewItem = DatabasesListView.Items.Add(db.Name)
                    If ((db.Status And DatabaseStatus.Normal) _
                        = DatabaseStatus.Normal) Then
                        DatabaseListViewItem.SubItems.Add( _
                            db.CreateDate.ToString( _
                            System.Globalization.CultureInfo.InvariantCulture))
                        DatabaseListViewItem.SubItems.Add( _
                            db.Size.ToString( _
                            System.Globalization.CultureInfo.InvariantCulture) _
                            & " MB")
                        DatabaseListViewItem.SubItems.Add( _
                            (db.SpaceAvailable / 1000.0).ToString( _
                            System.Globalization.CultureInfo.InvariantCulture) _
                            & " MB")
                        DatabaseListViewItem.SubItems.Add( _
                            db.CompatibilityLevel.ToString())
                    End If
                End If
            Next

            If selectDefault = True _
                AndAlso DatabasesListView.Items.Count > 0 Then
                DatabasesListView.Items(0).Selected = True
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub DatabasesListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DatabasesListView.SelectedIndexChanged
        UpdateControls()
    End Sub

    Private Sub UpdateControls()
        If (DatabasesListView.SelectedItems.Count > 0) Then
            DeleteButton.Enabled = True
        Else
            DeleteButton.Enabled = False
        End If

        If (NewDatabaseTextBox.Text.Length > 0) Then
            CreateButton.Enabled = True
        Else
            CreateButton.Enabled = False
        End If
    End Sub

    Private Sub ShowSqlStatementsCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowSqlStatementsCheckBox.CheckedChanged
        If ShowSqlStatementsCheckBox.CheckState = CheckState.Checked Then
            AddHandler SqlServerSelection.ConnectionContext.StatementExecuted, _
                AddressOf ServerStatementExecuted
        Else
            RemoveHandler SqlServerSelection.ConnectionContext.StatementExecuted, _
                AddressOf ServerStatementExecuted
        End If
    End Sub

    Private Sub NewDatabaseTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewDatabaseTextBox.TextChanged
        UpdateControls()
    End Sub
End Class
