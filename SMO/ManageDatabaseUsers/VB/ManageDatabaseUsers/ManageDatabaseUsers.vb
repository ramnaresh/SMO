'============================================================================
'  File:    ManageDatabaseUsers.vb 
'
'  Summary: Implements a sample SMO Manage Database Users utility in VB.NET.
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
Public Class ManageDatabaseUsers
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub ManageDatabaseUsers_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

    Private Sub ManageDatabaseUsers_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.SqlConnectionObject.State = ConnectionState.Open Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub AddUserButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddUserButton.Click
        Dim db As Database
        Dim usr As User
        Dim UserListViewItem As ListViewItem

        Try
            db = CType(DatabasesComboBox.SelectedItem, Database)

            ' Create the user object
            usr = New User(db, UserNameTextBox.Text)
            usr.Login = LoginsComboBox.Text
            usr.Create()

            ShowUsers()

            ' Select the user we just created and make sure it's viewable
            UserListViewItem _
            = UsersListView.FindItemWithText(UserNameTextBox.Text)
            UserListViewItem.Selected = True
            UserListViewItem.EnsureVisible()
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub

    Private Sub DeleteUserButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteUserButton.Click
        Dim db As Database
        Dim usr As User

        Try
            db = CType(DatabasesComboBox.SelectedItem, Database)
            usr = db.Users(UsersListView.SelectedItems(0).Text)
            If Not (usr Is Nothing) Then
                ' Are you sure?  Default to No.
                If System.Windows.Forms.MessageBox.Show(Me, String.Format( _
                    System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.ReallyDrop, usr.Name), Me.Text, _
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                    MessageBoxDefaultButton.Button2, 0) _
                        = Windows.Forms.DialogResult.No Then
                    Return
                End If

                usr.Drop()
            End If

            ShowUsers()

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub

    Private Sub Databases_SelectedIndexChangedComboBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DatabasesComboBox.SelectedIndexChanged
        ShowUsers()

        ' Select the first item in the list
        If UsersListView.Items.Count > 0 Then
            UsersListView.Items(0).Selected = True
            UsersListView.Items(0).EnsureVisible()
        End If

        UpdateButtons()
    End Sub

    Private Sub Users_SelectedIndexChangedListView(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsersListView.SelectedIndexChanged
        UpdateButtons()
    End Sub

    Private Sub UserName_TextChangedTextBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserNameTextBox.TextChanged
        UpdateButtons()
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
            SqlServerSelection.SetDefaultInitFields(GetType(Database), _
                New String() {"Name", "IsSystemObject", "IsAccessible"})

            ' Add database objects to combobox; the default ToString 
            ' will display the database name
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

    Private Sub ShowUsers()
        Dim csr As Cursor = Nothing
        Dim db As Database
        Dim UserListViewItem As ListViewItem

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            ' Clear the user list
            UsersListView.Items.Clear()

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(User), _
                New String() {"Name", "Login", "ID"})

            ' Show the current users for the selected database
            If DatabasesComboBox.SelectedIndex >= 0 Then
                db = CType(DatabasesComboBox.SelectedItem, Database)

                ' Load list of logins
                Dim logins As New SortedList(SqlServerSelection.Logins.Count)
                For Each userLogin As Login In SqlServerSelection.Logins
                    logins.Add(userLogin.Name, Nothing)
                Next

                ' Add users to list view and remove from logins
                For Each usr As User In db.Users
                    UserListViewItem = UsersListView.Items.Add(usr.Name)
                    UserListViewItem.SubItems.Add(usr.Login)
                    UserListViewItem.SubItems.Add(usr.ID.ToString( _
                        System.Globalization.CultureInfo.InvariantCulture))

                    ' Remove from logins
                    logins.Remove(usr.Login)
                Next

                ' Populate the logins combo box
                LoginsComboBox.Items.Clear()
                For Each dictLogin As DictionaryEntry In logins
                    ' Eliminate sa login from list
                    If (CStr(dictLogin.Key) <> "sa") Then
                        LoginsComboBox.Items.Add(dictLogin.Key)
                    End If
                Next

                ' Select the first item in the list
                If UsersListView.Items.Count > 0 Then
                    UsersListView.Items(0).Selected = True
                    UsersListView.Items(0).EnsureVisible()
                End If
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub UpdateButtons()
        If UserNameTextBox.Text.Length > 0 _
            AndAlso LoginsComboBox.SelectedIndex >= 0 Then
            AddUserButton.Enabled = True
        Else
            AddUserButton.Enabled = False
        End If

        If UsersListView.SelectedItems.Count > 0 Then
            DeleteUserButton.Enabled = True
        Else
            DeleteUserButton.Enabled = False
        End If
    End Sub

    Private Sub Logins_SelectedIndexChangedComboBox(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginsComboBox.SelectedIndexChanged
        UpdateButtons()
    End Sub
End Class
