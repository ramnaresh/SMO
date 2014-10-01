'============================================================================
'  File:    ScriptTable.vb
'
'  Summary: Implements a sample SMO table scripting utility in VB.NET.
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

Public Class ScriptTable
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub ScriptTableButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScriptTableButton.Click
        Dim csr As Cursor = Nothing
        Dim sDatabaseName As String
        Dim strColl As Specialized.StringCollection
        Dim scrptr As Scripter
        'Dim tmpSmoObjects() As SqlSmoObject
        Dim smoObjects() As SqlSmoObject
        Dim tbl As Table
        Dim count As Int32

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Use the selected database
            sDatabaseName = DatabasesComboBox.Text
            If sDatabaseName.Length = 0 Then
                Dim emb As New ExceptionMessageBox()
                emb.Text = My.Resources.NoDatabaseSelected
                emb.Show(Me)

                Return
            End If

            ' Create scripter object
            scrptr = New Scripter(SqlServerSelection)
            If ScriptDropCheckBox.CheckState = CheckState.Checked Then
                scrptr.Options.ScriptDrops = True
            Else
                scrptr.Options.ScriptDrops = False
            End If

            AddHandler scrptr.DiscoveryProgress, _
                AddressOf Me.ScriptTable_DiscoveryProgressReport
            AddHandler scrptr.ScriptingProgress, _
                AddressOf Me.ScriptTable_ScriptingProgressReport

            If TablesComboBox.SelectedIndex >= 0 Then
                ' Get selected table
                smoObjects = New SqlSmoObject(0) {}
                tbl = CType(TablesComboBox.SelectedItem, Table)
                If tbl.IsSystemObject = False Then
                    smoObjects(0) = tbl
                End If

                If (DependenciesCheckBox.CheckState = CheckState.Checked) Then
                    scrptr.Options.WithDependencies = True
                Else
                    scrptr.Options.WithDependencies = False
                End If

                strColl = scrptr.Script(smoObjects)

                ' Clear control
                ScriptTextBox.Clear()
                count = 0
                For Each str As String In strColl
                    count += 1
                    sbrStatus.Text = String.Format( _
                        System.Globalization.CultureInfo.InvariantCulture, _
                        My.Resources.AppendingScript, count, strColl.Count)

                    ScriptTextBox.AppendText(str)
                    ScriptTextBox.AppendText(My.Resources.Go)
                Next
            Else
                Dim emb As New ExceptionMessageBox()
                emb.Text = My.Resources.ChooseTable
                emb.Show(Me)
            End If

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            ' Clean up
            sbrStatus.Text = My.Resources.Done

            ' Restore the original cursor
            Me.Cursor = csr
        End Try
    End Sub
    Private Sub Databases_SelectedIndexChangedComboBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DatabasesComboBox.SelectedIndexChanged
        ShowTables()

        If DatabasesComboBox.SelectedIndex >= 0 Then
            ScriptTableButton.Enabled = True
        Else
            ScriptTableButton.Enabled = False
        End If
    End Sub

    Private Sub ScriptTable1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ServerConn = New ServerConnection
        scForm = New ServerConnect(ServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK AndAlso ServerConn.SqlConnectionObject.State _
            = ConnectionState.Open Then
            SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                ' Refresh database list
                ShowDatabases(True)
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub ScriptTable1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.SqlConnectionObject.State _
                = ConnectionState.Open Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub ScriptTable_DiscoveryProgressReport(ByVal sender As System.Object, ByVal e As ProgressReportEventArgs)
        sbrStatus.Text = String.Format( _
            System.Globalization.CultureInfo.InvariantCulture, _
            My.Resources.Discovering, e.TotalCount, e.Total)
        sbrStatus.Refresh()
    End Sub

    Private Sub ScriptTable_ScriptingProgressReport(ByVal sender As System.Object, ByVal e As ProgressReportEventArgs)
        sbrStatus.Text = String.Format( _
            System.Globalization.CultureInfo.InvariantCulture, _
            My.Resources.Scripting, e.TotalCount, e.Total)
        sbrStatus.Refresh()
    End Sub

    Private Sub ShowDatabases(ByVal selectDefault As Boolean)
        ' Show the current databases on the server
        Try
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

            If (selectDefault = True) AndAlso _
                (DatabasesComboBox.Items.Count > 0) Then
                DatabasesComboBox.SelectedIndex = 0
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub

    Private Sub ShowTables()
        Dim db As Database
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor   ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor   ' Display the waiting cursor

            ' Clear the tables list
            TablesComboBox.Items.Clear()

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Table), _
                New String() {"Name", "CreateDate", "IsSystemObject"})

            ' Show the current tables for the selected database
            If (DatabasesComboBox.SelectedIndex >= 0) Then
                ' Get database object from combobox
                db = CType(DatabasesComboBox.SelectedItem, Database)

                For Each tbl As Table In db.Tables
                    If tbl.IsSystemObject = False Then
                        TablesComboBox.Items.Add(tbl)
                    End If
                Next

                ' Select the first item in the list
                If (TablesComboBox.Items.Count > 0) Then
                    TablesComboBox.SelectedIndex = 0
                End If
            End If

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr  ' Restore the original cursor
        End Try
    End Sub
End Class
