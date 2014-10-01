'============================================================================
'  File:    LoadRegAssembly.vb 
'
'  Summary: Implements a sample SMO load and register assembly utility in VB.NET.
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
Public Class LoadRegAssembly
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub LoadRegAssembly_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ServerConn = New ServerConnection
        scForm = New ServerConnect(ServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK _
            AndAlso ServerConn.SqlConnectionObject.State _
            = ConnectionState.Open Then
            SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                ' Refresh database list
                ShowDatabases(True)

                UpdateButtons()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub LoadRegAssembly_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.SqlConnectionObject.State = ConnectionState.Open Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub Databases_SelectedIndexChangedComboBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DatabasesComboBox.SelectedIndexChanged
        If DatabasesComboBox.SelectedIndex >= 0 Then
            ShowAssemblies(True)
        End If

        UpdateButtons()
    End Sub

    Private Sub Assemblies_SelectedIndexChangedListView(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AssembliesListView.SelectedIndexChanged
        UpdateButtons()
    End Sub

    Private Sub AddAssemblyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddAssemblyButton.Click
        Dim db As Database
        Dim asm As SqlAssembly
        Dim udf As UserDefinedFunction
        Dim parm As UserDefinedFunctionParameter
        Dim csr As Cursor = Nothing
        Dim AssemblyListViewItem As ListViewItem

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Get selected database
            db = CType(DatabasesComboBox.SelectedItem, Database)

            asm = New SqlAssembly(db, "UtilityConversion")
            asm.Owner = "dbo"
            asm.AssemblySecurityLevel = AssemblySecurityLevel.Safe

            ' This allows the assembly to be on a different server from SQL Server
            ' Use string array version which serializes the assembly
            asm.Create(New String() {AssemblyFileTextBox.Text})

            udf = New UserDefinedFunction(db, "StringToInt32")
            udf.TextMode = False
            udf.AssemblyName = "UtilityConversion"
            udf.ClassName = "Microsoft.Samples.SqlServer.Conversions"
            udf.MethodName = "StringToInt32"
            udf.FunctionType = UserDefinedFunctionType.Scalar
            udf.ImplementationType = ImplementationType.SqlClr
            udf.DataType = DataType.Int

            parm = New UserDefinedFunctionParameter(udf, "@Input")
            udf.Parameters.Add(parm)
            parm.DataType = DataType.NVarChar(255)

            udf.Create()

            ShowAssemblies(True)

            ' Select the assembly just added
            AssemblyListViewItem = AssembliesListView.FindItemWithText(asm.Name)
            AssemblyListViewItem.Selected = True
            AssemblyListViewItem.EnsureVisible()

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub DropAssemblyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DropAssemblyButton.Click
        Dim db As Database
        Dim asm As SqlAssembly
        Dim udf As UserDefinedFunction
        Dim csr As Cursor = Nothing
        Dim countUDF As Int32

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            If AssembliesListView.SelectedItems.Count > 0 Then
                ' Get selected database
                db = CType(DatabasesComboBox.SelectedItem, Database)

                asm = db.Assemblies(AssembliesListView.SelectedItems(0).Text)

                If Not (asm Is Nothing) Then
                    ' Are you sure?  Default to No.
                    If System.Windows.Forms.MessageBox.Show(Me, String.Format( _
                        System.Globalization.CultureInfo.InvariantCulture, _
                        My.Resources.ReallyDrop, asm.Name), Me.Text, _
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                        MessageBoxDefaultButton.Button2, 0) = Windows.Forms.DialogResult.No Then
                        Return
                    End If

                    ' Drop all related User Defined Functions from assembly
                    For countUDF = db.UserDefinedFunctions.Count - 1 To 0 Step -1
                        udf = db.UserDefinedFunctions(countUDF)
                        If udf.AssemblyName = asm.Name Then
                            udf.Drop()
                        End If
                    Next

                    asm.Drop()
                End If
            End If

            ShowAssemblies(True)

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
            SqlServerSelection.SetDefaultInitFields(GetType(Database), _
                New String() {"Name", "IsSystemObject", "IsAccessible"})

            ' Add database objects to combobox; the default ToString 
            ' will display the database name
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

    Private Sub ShowAssemblies(ByVal selectDefault As Boolean)
        ' Show the current assemblies in the selected database
        Dim db As Database
        Dim AssemblyListViewItem As ListViewItem
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            ' Clear control
            AssembliesListView.Items.Clear()

            ' Limit the properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(SqlAssembly), _
                New String() {"Name", "VersionMajor", "VersionMinor", _
                "VersionBuild", "VersionRevision"})

            db = CType(DatabasesComboBox.SelectedItem, Database)
            For Each sqlAssem As SqlAssembly In db.Assemblies
                AssemblyListViewItem _
                    = AssembliesListView.Items.Add(sqlAssem.Name)
                AssemblyListViewItem.SubItems.Add( _
                    sqlAssem.Version.Major.ToString(System.Globalization.CultureInfo.InvariantCulture) _
                    & "." & sqlAssem.Version.Minor.ToString(System.Globalization.CultureInfo.InvariantCulture) _
                    & "." & sqlAssem.Version.Build.ToString(System.Globalization.CultureInfo.InvariantCulture) _
                    & "." & sqlAssem.Version.Revision.ToString(System.Globalization.CultureInfo.InvariantCulture))
                AssemblyListViewItem.SubItems.Add( _
                    sqlAssem.CreateDate.ToString(System.Globalization.CultureInfo.InvariantCulture))
            Next

            If selectDefault AndAlso AssembliesListView.Items.Count > 0 Then
                AssembliesListView.Items(0).Selected = True
            End If

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)

        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub UpdateButtons()
        If DatabasesComboBox.SelectedIndex >= 0 _
            AndAlso AssemblyFileTextBox.Text.Length > 0 Then
            AddAssemblyButton.Enabled = True
        Else
            AddAssemblyButton.Enabled = False
        End If

        If AssembliesListView.SelectedItems.Count > 0 Then
            DropAssemblyButton.Enabled = True
        Else
            DropAssemblyButton.Enabled = False
        End If
    End Sub
End Class
