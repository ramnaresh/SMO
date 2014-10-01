'============================================================================
'  File:    Conversions.vb 
'
'  Summary: Implements a sample CLR Assembly in VB.NET for use with the SQLCLR.
'
'  Date:    March 14, 2005
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
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports Microsoft.SqlServer.Server
Imports System.Text
Imports System.Globalization

Partial Public NotInheritable Class Conversions
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Returns the hexadecimal string representation of the given binary
    ''' </summary>
    ''' <param name="sb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Microsoft.SqlServer.Server.SqlFunction()> _
    Public Shared Function BinaryToString(ByVal sb() As Byte) As String
        Dim i As Integer

        ' If input is NULL then return NULL;
        If sb Is Nothing Then
            Return Nothing
        End If

        Dim lensb As Integer = sb.Length

        ' Use StringBuilder instead of String for efficiency
        ' Create a StringBuilder with enough capacity to hold the 
        ' hexadecimal representation
        Dim strb As New StringBuilder("0x", lensb * 2 + 2)

        For i = 0 To lensb - 1
            strb.Append(sb(i).ToString("X2", NumberFormatInfo.InvariantInfo))
        Next

        Return strb.ToString()
    End Function

    ''' <summary>
    ''' Returns the hexadecimal representation of the given binary
    ''' </summary>
    ''' <param name="ss"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Microsoft.SqlServer.Server.SqlFunction()> _
    Public Shared Function HexStringToInt32(ByVal ss As String) As Int32
        ' If input is NULL then return zero;
        If ss Is Nothing Then
            Return 0
        Else
            Return Convert.ToInt32(ss, 16)
        End If
    End Function

    ''' <summary>
    ''' Returns the numeric equivalent of the hexadecimal representation
    ''' </summary>
    ''' <param name="ss"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Microsoft.SqlServer.Server.SqlFunction()> _
    Public Shared Function StringToInt32(ByVal ss As String) As Integer
        ' If input is NULL then return zero;
        If ss Is Nothing Then
            Return 0
        Else
            Return Convert.ToInt32(ss, NumberFormatInfo.InvariantInfo)
        End If
    End Function
End Class
