'=====================================================================
'
'  File:    PropertyComparer.vb
'  Summary: Compares properties of the objects.
'  Date:    08-20-2004
'
'---------------------------------------------------------------------
'
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
Imports System.Text
Imports System.Collections
Imports System.Reflection


#End Region

Public Class PropertyComparer
    Implements IComparer

    Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements Collections.IComparer.Compare
        Dim pi1 As PropertyInfo = TryCast(x, PropertyInfo)
        Dim pi2 As PropertyInfo = TryCast(y, PropertyInfo)

        If pi1 Is Nothing AndAlso pi2 Is Nothing Then
            Return 0
        End If

        If pi1 Is Nothing Then
            Return 1
        End If

        If pi2 Is Nothing Then
            Return -1
        End If

        Return pi1.Name.CompareTo(pi2.Name)
    End Function
End Class
