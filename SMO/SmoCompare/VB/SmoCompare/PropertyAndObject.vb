'=====================================================================
'
'  File:    ProperyAndObject.vb
'  Summary: Property and Object data type
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
''' Pair of property name and object
''' </summary>

Public Structure PropertyAndObject
    Private propName As String
    Private objUri As Uri

    Public Sub New(ByVal propName As String, ByVal urn As Uri)
        Me.propName = propName
        Me.objUri = urn
    End Sub

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        'Check for null and compare run-time types.
        If obj Is Nothing _
            OrElse Me.GetType() IsNot obj.GetType() Then
            Return False
        End If

        Dim po As PropertyAndObject = CType(obj, PropertyAndObject)

        Return Me.propName = po.propName _
            AndAlso Me.objUri = po.objUri
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me.GetHashCode()
    End Function

    Public Shared Operator =(ByVal leftHandSide As PropertyAndObject, ByVal rightHandSide As PropertyAndObject) As Boolean
        Return (leftHandSide.propertyName = rightHandSide.propertyName) _
                AndAlso (leftHandSide.urn = rightHandSide.urn)
    End Operator

    Public Shared Operator <>(ByVal leftHandSide As PropertyAndObject, ByVal rightHandSide As PropertyAndObject) As Boolean
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

    Public Property Urn() As Uri
        Get
            Return Me.objUri
        End Get
        Set(ByVal value As Uri)
            Me.objUri = value
        End Set
    End Property
End Structure
