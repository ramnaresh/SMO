'=====================================================================
'
'  File:    ScriptPanel.vb
'  Summary: Window to display the change script
'  Date:    08-20-2004
'
'---------------------------------------------------------------------
'  This file is part of the Microsoft SQL Server Code Samples.
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
'=======================================================================

#Region "Using directives"

Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer
Imports Microsoft.SqlServer.MessageBox


#End Region

Partial Class ScriptPanel 'The Partial modifier is only required on one class definition per project.
    Inherits Form
    Private serverConn As ServerConnection
    Private collScript As StringCollection

    Public Sub New(ByVal serverConn As ServerConnection, ByVal collScript As StringCollection)
        InitializeComponent()

        Me.serverConn = serverConn
        Me.collScript = collScript
        panelTextBox.Clear()

        For Each line As String In collScript
            panelTextBox.AppendText(line & Environment.NewLine)
            panelTextBox.AppendText(My.Resources.Go)
        Next
    End Sub

    Private Sub runButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles runButton.Click
        ' Perform run on the connection         
        ' Connect to SQL Server
        Dim server As New Server(serverConn)
        Try
            server.ConnectionContext.ExecuteNonQuery(collScript, ExecutionTypes.ContinueOnError)
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub
End Class
