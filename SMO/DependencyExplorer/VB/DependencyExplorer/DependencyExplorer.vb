'============================================================================
'  File:    DependencyExplorer.vb 
'
'  Summary: Implements a sample SMO dependency explorer utility in VB.NET.
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

Public Class DependencyExplorer
#Region "Form Load"
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Public Delegate Sub RefreshDatabaseTables()
    Private refreshDbTables As RefreshDatabaseTables

    Private Sub DependencyExplorer_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DatabasesComboBox.Enabled = False
        Me.DatabasesComboBox.DisplayMember = My.Resources.Name
        Me.DatabasesComboBox.Text = My.Resources.SelectDatabase

        Me.TableListView.View = Windows.Forms.View.Details
        Me.TableListView.Columns.Add(My.Resources.Name, 120, HorizontalAlignment.Left)
        Me.TableListView.Columns.Add(My.Resources.Schema, 120, HorizontalAlignment.Left)
        Me.TableListView.Columns.Add(My.Resources.CreateDate, 120, HorizontalAlignment.Left)
        Me.TableListView.Columns.Add(My.Resources.RowCount, 120, HorizontalAlignment.Right)
        Me.TableListView.Columns.Add(My.Resources.DataSize, 120, HorizontalAlignment.Right)
        Me.TableListView.Columns.Add(My.Resources.Urn, 120, HorizontalAlignment.Left)
        Me.TableListView.GridLines = True
        Me.TableListView.FullRowSelect = True
        Me.TableListView.Items.Clear()

        Me.StatusBar.Text = My.Resources.NotConnected

        Me.ConnectToServer()

        refreshDbTables = New RefreshDatabaseTables(AddressOf RefreshTables)
    End Sub

    Private Sub DependencyExplorer_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Dim database As Database

        ' Get selected database from DatabaseComboBox
        database = CType(Me.DatabasesComboBox.SelectedItem, Database)

        If Not database Is Nothing Then
            ' Stop receiving events
            database.Events.StopEvents()

            ' Unsubscribe to table events
            database.Events.UnsubscribeAllEvents()
        End If

        SqlServerSelection = Nothing
        refreshDbTables = Nothing
    End Sub

    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Me.Close()
    End Sub

    Private Sub ConnectToServer()
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ' Load and display the server selection dialog
        ServerConn = New ServerConnection()

        ' Set Application name
        ServerConn.ApplicationName = Application.ProductName

        scForm = New ServerConnect(ServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK AndAlso _
            ServerConn.SqlConnectionObject.State = ConnectionState.Open Then
            Me.SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                ' Refresh database list
                ShowDatabases(True)

                ' Show server information on StatusBar
                Me.StatusBar.Text = SqlServerSelection.Name & " " _
                    & SqlServerSelection.Information.VersionString & " " _
                    & SqlServerSelection.Information.ProductLevel
            End If
        End If

        scForm = Nothing
    End Sub

    Private Sub ShowDatabases(ByVal selectDefault As Boolean)
        ' Show the current databases on the server
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            DatabasesComboBox.Enabled = False

            ' Clear control
            DatabasesComboBox.Items.Clear()

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Database), _
                New String() {"Name", "IsSystemObject", "IsAccessible"})

            ' Add database objects to combobox
            ' The default ToString() will display the database name in the list
            For Each db As Database In SqlServerSelection.Databases
                If db.IsSystemObject = False AndAlso db.IsAccessible = True Then
                    DatabasesComboBox.Items.Add(db)
                End If
            Next

            If selectDefault = True AndAlso DatabasesComboBox.Items.Count > 0 Then
                DatabasesComboBox.SelectedIndex = 0
            End If

            DatabasesComboBox.Enabled = True

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub
#End Region

#Region "Connect"
    Private Sub ConnectMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectMenuItem.Click
        ' Clear controls and apply defaults
        Me.DatabasesComboBox.Items.Clear()
        Me.TableListView.Items.Clear()
        Me.DatabasesComboBox.Enabled = False
        Me.DatabasesComboBox.Text = My.Resources.SelectDatabase
        Me.StatusBar.Text = My.Resources.NotConnected

        Me.ConnectToServer()

        ' Enable combo box
        Me.DatabasesComboBox.Enabled = True
    End Sub
#End Region

#Region "Select Database"
    Private Sub DatabaseComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DatabasesComboBox.SelectedIndexChanged
        Dim database As Database

        ' Get selected database from DatabaseComboBox
        database = CType(Me.DatabasesComboBox.SelectedItem, Database)

        ' Refresh the table list normally
        Me.RefreshTables()

        If Not database Is Nothing Then
            Me.SetDatabaseEventHandler(database)
        End If
    End Sub

    Private Sub AddTableItem(ByVal table As Table)
        Dim item As New ListViewItem(table.Name)

        item.SubItems.Add(table.Schema)
        item.SubItems.Add(table.CreateDate.ToString(DateTimeFormatInfo.CurrentInfo))
        item.SubItems.Add(table.RowCount.ToString(DateTimeFormatInfo.InvariantInfo))
        item.SubItems.Add(table.DataSpaceUsed.ToString(DateTimeFormatInfo.InvariantInfo))
        item.SubItems.Add(table.Urn.ToString())

        ' Add a reference to the table for convenience
        item.Tag = table

        ' Add the item to the list
        Me.TableListView.Items.Add(item)
    End Sub
#End Region

#Region "Script Without Dependencies"
    Private Sub WithoutDependenciesMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithoutDependenciesMenuItem.Click
        Dim table As Table
        Dim form As TextForm

        ' Just make sure something is selected
        If Me.TableListView.SelectedIndices.Count = 0 Then
            Exit Sub
        End If

        ' It's the first one as we only allow one selection
        table = CType(Me.TableListView.SelectedItems(0).Tag, Table)

        form = New TextForm()
        form.DisplayText(table.Script())
        form.Show()
    End Sub
#End Region

#Region "Script With Dependencies"
    Private Sub WithDependenciesMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithDependenciesMenuItem.Click
        Dim table As Table

        ' Just make sure something is selected
        If Me.TableListView.SelectedIndices.Count = 0 Then
            Exit Sub
        End If

        ' It's the first one as we only allow one selection
        table = CType(Me.TableListView.SelectedItems(0).Tag, Table)

        ' Show script with ShowText sub
        ShowText(table.Script(ScriptOption.WithDependencies + ScriptOption.Default))
    End Sub
#End Region

#Region "Capture Mode"
    Private Sub ShowDropDdlmenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowDropDdlmenuItem.Click
        Dim table As Table

        ' Just make sure something is selected
        If Me.TableListView.SelectedIndices.Count = 0 Then
            Exit Sub
        End If

        ' It's the first one as we only allow one selection
        table = CType(Me.TableListView.SelectedItems(0).Tag, Table)

        ' Set execution mode to capture
        table.Parent.Parent.ConnectionContext.SqlExecutionModes _
            = SqlExecutionModes.CaptureSql

        ' Drop the table
        table.RebuildIndexes(10)
        table.RecalculateSpaceUsage()
        table.DisableAllIndexes()

        ' Reset execution mode
        table.Parent.Parent.ConnectionContext.SqlExecutionModes _
            = SqlExecutionModes.ExecuteSql

        ' Display captured text with ShowText sub
        ShowText(table.Parent.Parent.ConnectionContext.CapturedSql.Text)

        ' Clear capture buffer
        table.Parent.Parent.ConnectionContext.CapturedSql.Text.Clear()
    End Sub
#End Region

#Region "Helper Code"
    Private Shared Sub ShowText(ByVal sc As Specialized.StringCollection)
        Dim form As New TextForm

        form.DisplayText(sc)
        form.Show()
    End Sub

    Private Sub SetDatabaseEventHandler(ByVal database As Database)
        Dim databaseEventSet As New DatabaseEventSet

        ' Subscribe to table events
        databaseEventSet.CreateTable = True
        databaseEventSet.DropTable = True
        databaseEventSet.AlterTable = True

        database.Events.SubscribeToEvents(databaseEventSet, _
            New ServerEventHandler(AddressOf Me.MyEventHandler))
        database.Events.StartEvents()
    End Sub

    Private Sub MyEventHandler(ByVal sender As Object, ByVal e As ServerEventArgs)
        Me.Invoke(refreshDbTables)
    End Sub

    Private Sub RefreshTables()
        Dim csr As Cursor = Nothing
        Dim database As Database

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            Me.TableListView.BeginUpdate()

            ' Clear the list
            Me.TableListView.Items.Clear()

            ' Get selected database from DatabaseComboBox
            database = CType(Me.DatabasesComboBox.SelectedItem, Database)

            ' Set default initial fields for table
            database.Parent.SetDefaultInitFields(GetType(Table), _
                "Name", "CreateDate", "RowCount", "DataSpaceUsed", _
                "IsSystemObject")

            ' Get the updated list of tables
            database.Tables.Refresh()

            ' Iterate user Tables and add to ListView Using AddTableItem
            For Each table As Table In database.Tables
                If table.IsSystemObject = False Then
                    Me.AddTableItem(table)
                End If
            Next

            ' Resize columns
            Me.TableListView.AutoResizeColumns( _
                ColumnHeaderAutoResizeStyle.ColumnContent)

            ' Select the first item
            If Me.TableListView.Items.Count > 0 Then
                Me.TableListView.Items(0).Selected = True
            End If

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            Me.TableListView.EndUpdate()

            ' Restore the original cursor
            Me.Cursor = csr
        End Try
    End Sub
#End Region

#Region "Show Dependencies"
    Private Sub ShowDependenciesmenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowDependenciesmenuItem.Click
        Dim table As Table
        Dim deps As DependencyTree
        Dim scripter As Scripter
        Dim smoObjects(0) As SqlSmoObject
        Dim form As New DependencyForm()

        ' Just make sure something is selected
        If Me.TableListView.SelectedIndices.Count = 0 Then
            Exit Sub
        End If

        ' It's the first one as we only allow one selection
        table = CType(Me.TableListView.SelectedItems(0).Tag, Table)

        ' Declare and instantiate new Scripter object
        scripter = New Scripter(table.Parent.Parent)

        ' Declare array of SqlSmoObjects
        smoObjects(0) = table

        ' Discover dependencies
        deps = scripter.DiscoverDependencies(smoObjects, True)

        ' Show dependencies
        form.ShowDependencies(table.Parent.Parent, deps)
        form.Show()
    End Sub
#End Region
End Class
