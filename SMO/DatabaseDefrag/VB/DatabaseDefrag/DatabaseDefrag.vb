'============================================================================
'  File:    DatabaseDefrag.vb 
'
'  Summary: Implements a sample SMO SQL Server database defragment utility in VB.NET.
'
'  Date:    August 29, 2005
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
#Region "Using directives"

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.MessageBox


#End Region

Partial Class DatabaseDefrag 'The Partial modifier is only required on one class definition per project.
    Inherits Form
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub DatabaseDefrag_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim ServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            ' Display the main window first
            Me.Show()
            Application.DoEvents()

            ServerConn = New ServerConnection()
            scForm = New ServerConnect(ServerConn)
            dr = scForm.ShowDialog(Me)
            If dr = Windows.Forms.DialogResult.OK _
                AndAlso ServerConn.SqlConnectionObject.State = ConnectionState.Open Then
                SqlServerSelection = New Server(ServerConn)
                If Not (SqlServerSelection Is Nothing) Then
                    Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                    ' Limit the table properties returned to just those that we use
                    SqlServerSelection.SetDefaultInitFields(GetType(Table), _
                        New String() {"Schema", "Name", "IsSystemObject"})

                    ShowDatabases(True)
                End If
            Else
                Me.Close()
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub DatabaseDefrag_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        If Not (SqlServerSelection Is Nothing) Then
            If SqlServerSelection.ConnectionContext.IsOpen = True Then
                SqlServerSelection.ConnectionContext.Disconnect()
            End If
        End If
    End Sub

    Private Sub defragmentButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles defragmentButton.Click
        ' Defragment the selected database
        DefragDatabase()
    End Sub

    Private Sub DefragDatabase()
        Dim db As Database
        Dim csr As Cursor = Nothing

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            ' Clear control
            richTextBox1.Clear()

            db = CType(DatabasesComboBox.SelectedItem, Database)
            Dim tbl As Table
            For Each tbl In db.Tables
                If tbl.IsSystemObject = False Then
                    richTextBox1.AppendText(My.Resources.Table & tbl.ToString() & Environment.NewLine)

                    richTextBox1.ScrollToCaret()

                    ' Allow the text to display
                    Application.DoEvents()

                    tbl.RebuildIndexes(90)
                End If
            Next tbl
            '  At last display the 'Completed'
            richTextBox1.AppendText(My.Resources.Completed)
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

            ' Limit the database properties returned to just those that we use
            SqlServerSelection.SetDefaultInitFields(GetType(Database), New String() {"Name", "IsSystemObject", "IsAccessible"})

            ' Add database objects to combobox; the default ToString will display the database name
            Dim db As Database
            For Each db In SqlServerSelection.Databases
                If db.IsSystemObject = False AndAlso db.IsAccessible = True Then
                    DatabasesComboBox.Items.Add(db)
                End If
            Next db

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
End Class