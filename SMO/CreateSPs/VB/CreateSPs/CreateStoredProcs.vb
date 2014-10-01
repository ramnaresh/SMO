'============================================================================
'  File:    CreateStoredProcedures.vb 
'
'  Summary: Implements a sample SMO create stored procedures utility in VB.NET.
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
Public Class CreateStoredProcedures
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub CreateStoredProcedures_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        ServerConn = New ServerConnection
        scForm = New ServerConnect(ServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK AndAlso _
            ServerConn.SqlConnectionObject.State = ConnectionState.Open Then
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

    Private Sub CreateStoredProcedures_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.SqlConnectionObject.State _
                = ConnectionState.Open Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub ExecuteButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExecuteButton.Click
        Dim csr As Cursor = Nothing
        Dim db As Database
        Dim spSchema As Schema

        ' Timing
        Dim start As DateTime = DateTime.Now

        If System.Windows.Forms.MessageBox.Show(Me, My.Resources.ReallyCreate, _
            Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button2, 0) = Windows.Forms.DialogResult.No Then
            Return
        End If

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Clear the textbox control
            ScriptTextBox.Clear()

            db = CType(DatabasesComboBox.SelectedItem, Database)

            ' Create SmoDemo schema to contain stored procedures
            If (db.Schemas.Contains("SmoDemo") = False) Then
                spSchema = New Schema(db, "SmoDemo")
                spSchema.Create()
            Else
                spSchema = db.Schemas("SmoDemo")
            End If

            ' Limit the table properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Table), _
                New String() {"Name"})

            ' Limit the column properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Column), _
                New String() {"Name", "DataType", "Length", "InPrimaryKey"})

            ' Limit the stored procedure properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(StoredProcedure), _
                New String() {"Name", "Schema"})

            ' Create a SELECT stored procedure for each table
            For Each tbl As Table In db.Tables
                If tbl.IsSystemObject = False Then
                    CreateSelectProcedure(spSchema, tbl)
                End If
            Next

            ScriptTextBox.AppendText(My.Resources.Completed)

            ScrollToBottom()

            sbrStatus.Text = My.Resources.Ready

        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            ' Clean up.
            Me.Cursor = csr ' Restore the original cursor
            ' Timing
            Dim diff As TimeSpan = DateTime.Now.Subtract(start)
            ScriptTextBox.AppendText(String.Format( _
                System.Threading.Thread.CurrentThread.CurrentCulture, _
                vbCrLf & My.Resources.ElapsedTime, (diff.Minutes * 60) _
                + diff.Seconds, diff.Milliseconds))
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

    Private Sub CreateSelectProcedure(ByVal spSchema As Schema, ByVal tbl As Table)
        Dim procName As String
        Dim sbSQL As New StringBuilder
        Dim sbSelect As New StringBuilder
        Dim sbWhere As New StringBuilder
        Dim sp As StoredProcedure
        Dim parm As StoredProcedureParameter
        Dim col As Column

        Try
            ' Create stored procedure name from user entries
            procName = PrefixTextBox.Text & tbl.Name & "Select"

            If (DropOnlyCheckBox.CheckState = CheckState.Checked) Then
                DropStoredProcedure(procName, spSchema)
            Else
                DropStoredProcedure(procName, spSchema)

                ScriptTextBox.AppendText(String.Format( _
                    System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.CreatingStoredProcedure, _
                    spSchema.ToString(), BracketObjectName(procName)) _
                    & Environment.NewLine)

                ScrollToBottom()

                ' Create the new stored procedure object
                sp = New StoredProcedure(tbl.Parent, procName, spSchema.Name)
                sp.TextMode = False
                For Each col In tbl.Columns
                    ' Select columns
                    If sbSelect.Length > 0 Then
                        sbSelect.Append(", " & vbCrLf)
                    End If

                    ' Note: this does not fix object names with embedded brackets
                    sbSelect.Append(vbTab & "[")
                    sbSelect.Append(col.Name)
                    sbSelect.Append("]")

                    ' Create parameters and where clause from indexed fields
                    If col.InPrimaryKey = True Then
                        ' Parameter columns
                        parm = New StoredProcedureParameter(sp, "@" & col.Name)
                        parm.DataType = col.DataType
                        parm.DataType.MaximumLength _
                            = col.DataType.MaximumLength
                        sp.Parameters.Add(parm)

                        ' Where columns
                        If sbWhere.Length > 0 Then
                            sbWhere.Append(" " & vbCrLf & vbTab & "AND ")
                        End If

                        ' Note: this does not fix object names 
                        ' with embedded brackets
                        sbWhere.Append("[")
                        sbWhere.Append(col.Name)
                        sbWhere.Append("] = @")
                        sbWhere.Append(col.Name)
                    End If
                Next

                ' Put where clause into string
                If sbWhere.Length > 0 Then
                    sbWhere.Insert(0, "WHERE ")
                End If

                sbrStatus.Text = String.Format( _
                    System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.Creating, procName)

                sbSQL.Append("SELECT ")
                sbSQL.Append(sbSelect)
                sbSQL.Append(" " & vbCrLf & "FROM ")
                sbSQL.Append(tbl.ToString())
                sbSQL.Append(" " & vbCrLf)
                sbSQL.Append(sbWhere)

                sp.TextBody = sbSQL.ToString()

                sp.Create()
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            ' Clean up.
            sbSQL = Nothing
            sbSelect = Nothing
            sbWhere = Nothing
            sp = Nothing
            parm = Nothing
        End Try
    End Sub

    Private Sub DropStoredProcedure(ByVal procName As String, ByVal spSchema As Schema)
        Dim db As Database = CType(Me.DatabasesComboBox.SelectedItem, Database)

        If (db.StoredProcedures.Contains(procName, spSchema.Name) = True) Then

            Me.ScriptTextBox.AppendText(String.Format( _
                System.Globalization.CultureInfo.InvariantCulture, _
                My.Resources.DroppingStoredProcedure, spSchema.ToString(), _
                BracketObjectName(procName)) & Environment.NewLine)
            ScrollToBottom()

            ' Drop the existing stored procedure
            db.StoredProcedures(procName, spSchema.Name).Drop()
        End If
    End Sub

    Private Shared Function BracketObjectName(ByVal objectName As String) As String
        Dim tempString As StringBuilder = New StringBuilder(128)

        tempString.Append("[")
        tempString.Append(objectName.Replace("[", "[[").Replace("]", "]]"))
        tempString.Append("]")

        Return tempString.ToString()
    End Function

    Private Sub ScrollToBottom()
        ScriptTextBox.Select()
        ScriptTextBox.SelectionStart = ScriptTextBox.Text.Length
        ScriptTextBox.ScrollToCaret()
        Application.DoEvents()
    End Sub
End Class
