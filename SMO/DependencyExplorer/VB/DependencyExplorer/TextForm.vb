'============================================================================
'  File:    TextForm.vb 
'
'  Summary: Implements a text display form in VB.NET.
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

Public Class TextForm
    Public Sub DisplayText(ByVal display As String)
        Me.TextBox.Text = display
    End Sub

    Public Sub DisplayText(ByVal sc As Specialized.StringCollection)
        If (sc Is Nothing) Then
            Throw New ArgumentNullException("sc")
        End If

        Me.TextBox.Text = String.Empty
        For Each s As String In sc
            Me.TextBox.AppendText(s & vbLf)
        Next
    End Sub

    Public Sub DisplayText(ByVal propertyCollection As Microsoft.SqlServer.Management.Smo.PropertyCollection)
        Dim sb As New System.Text.StringBuilder
        Dim prop As Microsoft.SqlServer.Management.Smo.Property

        If (propertyCollection Is Nothing) Then
            Throw New ArgumentNullException("propertyCollection")
        End If

        For Each prop In propertyCollection
            If prop.Retrieved = True Then
                sb.AppendFormat("{0,-20} {1}", prop.Name, prop.Value.ToString())
                sb.Append(vbCrLf)
            End If
        Next

        Me.TextBox.Text = sb.ToString()
    End Sub
End Class
