'============================================================================
'  File:    ManageTables.vb 
'
'  Summary: Implements a sample SMO Manage Tables utility in VB.NET.
'
'  Date:    April 14, 2005
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
Public Class ManageTables
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub ManageTables_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

                ' Refresh database list
                ShowDatabases(True)
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub ManageTables_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.SqlConnectionObject.State = ConnectionState.Open Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub Databases_SelectedIndexChangedComboBox(ByVal sender As Object, ByVal e As System.EventArgs) Handles DatabasesComboBox.SelectedIndexChanged
        ShowTables()
    End Sub

    Private Sub Tables_SelectedIndexChangedComboBox(ByVal sender As Object, ByVal e As System.EventArgs) Handles TablesComboBox.SelectedIndexChanged
        ShowColumns()
        UpdateButtons()
    End Sub

    Private Sub Columns_SelectedIndexChangedListView(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColumnsListView.SelectedIndexChanged
        UpdateButtons()
    End Sub

    Private Sub AddTableButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddTableButton.Click
        Dim db As Database
        Dim tbl As Table
        Dim col As Column
        Dim idx As Index
        Dim dflt As [Default]
        Dim csr As Cursor = Nothing
        Dim lviCount As Int32

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            ' Show the current tables for the selected database
            db = CType(DatabasesComboBox.SelectedItem, Database)
            If db.Tables.Contains(TableNameTextBox.Text) = False Then
                ' Create an empty string default
                dflt = db.Defaults("dfltEmptyString")
                If dflt Is Nothing Then
                    dflt = New [Default](db, "dfltEmptyString")
                    dflt.TextHeader = "CREATE DEFAULT [dbo].[dfltEmptyString] AS "
                    dflt.TextBody = "'';"
                    dflt.Create()
                End If

                ' Create a new table object
                tbl = New Table(db, TableNameTextBox.Text, db.DefaultSchema)

                ' Add the first column
                col = New Column(tbl, "Column1", DataType.Int)
                tbl.Columns.Add(col)
                col.Nullable = False
                col.Identity = True
                col.IdentitySeed = 1
                col.IdentityIncrement = 1

                ' Add the primary key index
                idx = New Index(tbl, "PK_" & TableNameTextBox.Text)
                tbl.Indexes.Add(idx)
                idx.IndexedColumns.Add(New IndexedColumn(idx, col.Name))
                idx.IsClustered = True
                idx.IsUnique = True
                idx.IndexKeyType = IndexKeyType.DriPrimaryKey

                ' Add the second column
                col = New Column(tbl, "Column2", DataType.NVarChar(1024))
                tbl.Columns.Add(col)
                col.AddDefaultConstraint(Nothing) ' Use SQL Server default naming
                col.DefaultConstraint.Text = My.Resources.DefaultConstraintText
                col.Nullable = False

                ' Add the third column
                col = New Column(tbl, "Column3", DataType.DateTime)
                tbl.Columns.Add(col)
                col.Nullable = False

                ' Create the table
                tbl.Create()

                ' Refresh list and select the one we just created
                ShowTables()

                ' Clear selected items
                TablesComboBox.SelectedIndex = -1

                ' Find the table just created
                For lviCount = 0 To TablesComboBox.Items.Count - 1
                    ' Can also use - foreach (ListViewItem TableListViewItem in TablesComboBox.Items)
                    If TablesComboBox.Items(lviCount).ToString() = tbl.ToString() Then
                        TablesComboBox.SelectedIndex = lviCount
                        Exit For
                    End If
                Next
            Else
                Dim emb As New ExceptionMessageBox()
                emb.Text = String.Format(System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.TableExists, _
                    TableNameTextBox.Text)
                emb.Show(Me)

            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub DeleteTableButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteTableButton.Click
        Dim tbl As Table
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            ' Show the current tables for the selected database
            If TablesComboBox.Items.Count > 0 Then
                tbl = CType(TablesComboBox.SelectedItem, Table)
                If Not (tbl Is Nothing) Then
                    ' Are you sure?  Default to No.
                    If System.Windows.Forms.MessageBox.Show(Me, String.Format( _
                        System.Globalization.CultureInfo.InvariantCulture, _
                        My.Resources.ReallyDrop, tbl.ToString()), Me.Text, _
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                        MessageBoxDefaultButton.Button2, 0) = Windows.Forms.DialogResult.No Then
                        Return
                    End If

                    tbl.Drop()
                End If

                ' Refresh list and select first entry
                ShowTables()
                If TablesComboBox.Items.Count > 0 Then
                    TablesComboBox.SelectedIndex = 0
                End If
            End If

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub ShowDatabases(ByVal selectDefault As Boolean)
        ' Show the current databases on the server
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Clear control
            DatabasesComboBox.Items.Clear()

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Database), New _
                String() {"Name", "IsSystemObject", "IsAccessible"})

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Table), New _
                String() {"Name", "CreateDate", "IsSystemObject"})

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Column), New _
                String() {"Name", "DataType", "SystemType", "Length", _
                "NumericPrecision", "NumericScale", "XmlSchemaNamespace", _
                "XmlSchemaNamespaceSchema", "DataTypeSchema", "Nullable", _
                "InPrimaryKey"})

            ' Add database objects to combobox; the default ToString will display the database name
            For Each db As Database In SqlServerSelection.Databases
                If db.IsSystemObject = False AndAlso db.IsAccessible = True Then
                    DatabasesComboBox.Items.Add(db)
                End If
            Next

            If selectDefault = True AndAlso DatabasesComboBox.Items.Count > 0 Then
                DatabasesComboBox.SelectedIndex = 0
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub ShowTables()
        Dim db As Database
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Clear the tables list
            TablesComboBox.Items.Clear()

            ' Show the current tables for the selected database
            If DatabasesComboBox.SelectedIndex >= 0 Then
                db = CType(DatabasesComboBox.SelectedItem, Database)

                For Each tbl As Table In db.Tables
                    If tbl.IsSystemObject = False Then
                        TablesComboBox.Items.Add(tbl)
                    End If
                Next

                ' Select the first item in the list
                If TablesComboBox.Items.Count > 0 Then
                    TablesComboBox.SelectedIndex = 0
                Else
                    ' Clear the table detail list
                    ColumnsListView.Items.Clear()
                End If
            End If

            UpdateButtons()
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub ShowColumns()
        Dim ColumnListViewItem As ListViewItem
        Dim tbl As Table
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Delay rendering until after list filled
            ColumnsListView.BeginUpdate()

            ' Clear the columns list
            ColumnsListView.Items.Clear()

            ' Show the current columns for the selected table
            If TablesComboBox.SelectedIndex >= 0 Then
                tbl = CType(TablesComboBox.SelectedItem, Table)
                For Each col As Column In tbl.Columns
                    ColumnListViewItem = ColumnsListView.Items.Add(col.Name)
                    ColumnListViewItem.SubItems.Add(col.DataType.Name)
                    ColumnListViewItem.SubItems.Add( _
                        col.DataType.MaximumLength.ToString( _
                        System.Globalization.CultureInfo.InvariantCulture))
                    ColumnListViewItem.SubItems.Add( _
                        col.Nullable.ToString())
                    ColumnListViewItem.SubItems.Add( _
                        col.InPrimaryKey.ToString())
                Next
            End If

            ' Now we render
            ColumnsListView.EndUpdate()

            UpdateButtons()
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub UpdateButtons()
        AddTableButton.Enabled = DatabasesComboBox.SelectedIndex >= 0
        DeleteTableButton.Enabled = TablesComboBox.SelectedIndex >= 0
    End Sub
End Class
