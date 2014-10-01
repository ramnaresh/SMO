'=====================================================================
'
'  File:    PropertyAndType.vb
'  Summary: Property and Type data type
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


#End Region

''' <summary>
''' Pair of property name and Type
''' </summary>

Public Structure PropertyAndType
    Private propName As String
    Private propType As String

    Public Sub New(ByVal propertyName As String, ByVal type As String)
        Me.propName = propertyName
        Me.propType = type
    End Sub

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        'Check for null and compare run-time types.
        If obj Is Nothing _
            OrElse Me.GetType() IsNot obj.GetType() Then
            Return False
        End If

        Dim pt As PropertyAndType = CType(obj, PropertyAndType)

        Return Me.propName = pt.propName _
            AndAlso Me.propType = pt.Type
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me.GetHashCode()
    End Function

    Public Shared Operator =(ByVal leftHandSide As PropertyAndType, ByVal rightHandSide As PropertyAndType) As Boolean
        Return (leftHandSide.propName = rightHandSide.propName) _
            AndAlso (leftHandSide.propType = rightHandSide.propType)
    End Operator

    Public Shared Operator <>(ByVal leftHandSide As PropertyAndType, ByVal rightHandSide As PropertyAndType) As Boolean
        Return Not (leftHandSide = rightHandSide)
    End Operator

    Public Property PropertyName() As String
        Get
            Return Me.propName
        End Get
        Set(ByVal value As String)
            Me.propName = value
        End Set
    End Property

    Public Property Type() As String
        Get
            Return Me.propType
        End Get
        Set(ByVal value As String)
            Me.propType = value
        End Set
    End Property
End Structure
