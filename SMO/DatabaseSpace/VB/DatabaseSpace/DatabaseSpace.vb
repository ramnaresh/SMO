'============================================================================
'  File:    DatabaseSpace.vb 
'
'  Summary: Implements a sample SMO SQL Server database space utility in VB.NET.
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
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.MessageBox

Partial Public Class DatabaseSpace 'The Partial modifier is only required on one class definition per project.
    Inherits Form
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
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
            AndAlso ServerConn.SqlConnectionObject.State = ConnectionState.Open Then
            SqlServerSelection = New Server(ServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle + SqlServerSelection.Name

                ' Refresh database space list
                ListDatabaseSpace()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub ListDatabaseSpace()
        Try
            Me.dataGridView1.Rows.Clear()

            Dim db As Database
            For Each db In SqlServerSelection.Databases
                If db.IsSystemObject = False Then
                    If db.LogFiles.Count > 0 Then
                        Me.dataGridView1.Rows.Add(New Object() {db.Name, db.Size, db.SpaceAvailable / 1024.0, db.LogFiles(0).Size / 1024.0, db.LogFiles(0).UsedSpace / 1024.0})
                    Else
                        Me.dataGridView1.Rows.Add(New Object() {db.Name, db.Size, db.SpaceAvailable / 1024.0, 0.0, 0.0})
                    End If
                End If
            Next db
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub
End Class