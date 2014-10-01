'============================================================================
'  File:    TextOutputForm.vb
'
'  Summary: Implements a sample text output form in VB.NET.
'
'  Date:    September 30, 2005
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
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Partial Public Class TextOutputForm 'The Partial modifier is only required on one class definition per project.
    Inherits Form

    Public Sub New(ByVal strings As StringCollection)
        InitializeComponent()

        If Not (strings Is Nothing) Then
            Dim str As String
            For Each str In strings
                OutputTextBox.AppendText(str + Environment.NewLine)
            Next str

            OutputTextBox.ScrollToCaret()
        End If
    End Sub

    Public Property OutputText() As String
        Get
            Return OutputTextBox.Text
        End Get
        Set(ByVal value As String)
            OutputTextBox.Text = value
        End Set
    End Property
End Class
