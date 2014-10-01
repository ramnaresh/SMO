'=====================================================================
'
'  File:    DifferentProps.vb
'  Summary: Different Properties class to contain the results of property comparisons.
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
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Sdk.Sfc
Imports Microsoft.SqlServer


#End Region

Public Class DifferentProperties
    Private objUrn1 As Urn
    Private objUrn2 As Urn
    Private objPropertyName As String
    Private objValue1 As String
    Private objValue2 As String

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        'Check for null and compare run-time types.
        If obj Is Nothing _
            OrElse Me.GetType() IsNot obj.GetType() Then
            Return False
        End If

        Dim dp As DifferentProperties = CType(obj, DifferentProperties)

        Return Me.objPropertyName = dp.objPropertyName _
            AndAlso Me.objUrn1 = dp.objUrn1 _
            AndAlso Me.objUrn2 = dp.objUrn2 _
            AndAlso Me.objValue1 = dp.objValue1 _
            AndAlso Me.objValue2 = dp.objValue2
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me.GetHashCode()
    End Function

    Public Shared Operator =(ByVal leftHandSide As DifferentProperties, ByVal rightHandSide As DifferentProperties) As Boolean
        Return (leftHandSide.objPropertyName = rightHandSide.objPropertyName) _
            AndAlso (leftHandSide.objUrn1 = rightHandSide.objUrn1) _
            AndAlso (leftHandSide.objUrn2 = rightHandSide.objUrn2) _
            AndAlso (leftHandSide.objValue1 = rightHandSide.objValue1) _
            AndAlso (leftHandSide.objValue2 = rightHandSide.objValue2)
    End Operator

    Public Shared Operator <>(ByVal leftHandSide As DifferentProperties, ByVal rightHandSide As DifferentProperties) As Boolean
        Return Not (leftHandSide = rightHandSide)
    End Operator

    ''' <summary>
    ''' First object Urn
    ''' </summary>
    ''' <value></value>
    Public Property Urn1() As Urn
        Get
            Return Me.objUrn1
        End Get
        Set(ByVal value As Urn)
            Me.objUrn1 = value
        End Set
    End Property

    ''' <summary>
    ''' Second object Urn
    ''' </summary>
    ''' <value></value>
    Public Property Urn2() As Urn
        Get
            Return Me.objUrn2
        End Get
        Set(ByVal value As Urn)
            Me.objUrn2 = value
        End Set
    End Property

    ''' <summary>
    ''' Property name
    ''' </summary>
    ''' <value></value>
    Public Property PropertyName() As String
        Get
            Return Me.objPropertyName
        End Get
        Set(ByVal value As String)
            Me.objPropertyName = value
        End Set
    End Property

    ''' <summary>
    ''' First object property value 
    ''' </summary>
    ''' <value></value>
    Public Property ObjectValue1() As String
        Get
            Return Me.objValue1
        End Get
        Set(ByVal value As String)
            Me.objValue1 = value
        End Set
    End Property

    ''' <summary>
    ''' Second object property value 
    ''' </summary>
    ''' <value></value>
    Public Property ObjectValue2() As String
        Get
            Return Me.objValue2
        End Get
        Set(ByVal value As String)
            Me.objValue2 = value
        End Set
    End Property
End Class
