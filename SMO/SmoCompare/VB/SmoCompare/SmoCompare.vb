'=====================================================================
'
'  File:    SmoCompare.vb
'  Summary: Main SMO comparison engine.
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
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text
Imports System.Xml
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Sdk.Sfc
Imports Microsoft.SqlServer


#End Region

Public Class SmoCompare
    Private configFile As String = "SmoCompare.xml"
    Private separatorLine As New String("-"c, 76)
    Private returnVal As Boolean = True
    Private level As Integer
    Private serverName1 As String
    Private loginName1 As String
    Private serverPassword1 As String
    Private serverName2 As String
    Private loginName2 As String
    Private serverPassword2 As String
    Private currentPropertyName As String = My.Resources.PropertyNameNotSet
    Private objServer1 As Server
    Private objServer2 As Server
    Private smoObject1 As SqlSmoObject
    Private smoObject2 As SqlSmoObject
    Private compareLogger As ILogger
    Private comparer As PropertyComparer
    Private collectionCanBeNull As ArrayList
    Private ignoreType As StringCollection
    Private ignoreProperty As StringCollection
    Private ignoreSchema As StringCollection
    Private ignoreObject As StringCollection
    Private ignorePropertyForType As ArrayList
    Private ignorePropertyForObject As ArrayList
    Private diffProps As ArrayList
    Private childrenOfObj1 As StringCollection
    Private childrenOfObj2 As StringCollection

    Public Sub New()
        Initialize()
    End Sub

    Public Sub New(ByVal logger As ILogger)
        Me.compareLogger = logger
        Initialize()
    End Sub

    Private Sub Initialize()
        comparer = New PropertyComparer()
        Me.childrenOfObj1 = New StringCollection()
        Me.childrenOfObj2 = New StringCollection()
        Me.ignoreObject = New StringCollection()
        Me.ignoreProperty = New StringCollection()
        Me.ignoreType = New StringCollection()
        Me.ignoreSchema = New StringCollection()
        Me.ignorePropertyForObject = New ArrayList(3)
        Me.ignorePropertyForType = New ArrayList(3)
        Me.collectionCanBeNull = New ArrayList(3)
        Me.diffProps = New ArrayList(3)

        Configure()
    End Sub

    ' Invariant: next three following arrays must be ZERO length 
    ' If the smoObject1 and smoObject2 are equal
    ' Array of properties (and values) that are found different

    Public ReadOnly Property DiffProps1() As ArrayList
        Get
            Return Me.diffProps
        End Get
    End Property

    ' Of Type DifferentProperties
    ' This array will contain URNs pointing to objects that are in first object only

    Public ReadOnly Property ChildrenOfObject1() As StringCollection
        Get
            Return Me.childrenOfObj1
        End Get
    End Property

    ' This array will contain URNs pointing to objects that are in second object only

    Public ReadOnly Property ChildrenOfObject2() As StringCollection
        Get
            Return Me.childrenOfObj2
        End Get
    End Property

    Public Property Server1() As String
        Get
            Return Me.serverName1
        End Get
        Set(ByVal value As String)
            Me.serverName1 = value
        End Set
    End Property

    Public Property Login1() As String
        Get
            Return Me.loginName1
        End Get
        Set(ByVal value As String)
            Me.loginName1 = value
        End Set
    End Property

    Public Property Password1() As String
        Get
            Return Me.serverPassword1
        End Get
        Set(ByVal value As String)
            Me.serverPassword1 = value
        End Set
    End Property

    Public Property Server2() As String
        Get
            Return Me.serverName2
        End Get
        Set(ByVal value As String)
            Me.serverName2 = value
        End Set
    End Property

    Public Property Login2() As String
        Get
            Return Me.loginName2
        End Get
        Set(ByVal value As String)
            Me.loginName2 = value
        End Set
    End Property

    Public Property Password2() As String
        Get
            Return Me.serverPassword2
        End Get
        Set(ByVal value As String)
            Me.serverPassword2 = value
        End Set
    End Property

    Private Property ReturnValue() As Boolean
        Get
            Return returnVal
        End Get
        Set(ByVal value As Boolean)
            returnVal = value
        End Set
    End Property

    Public Sub Clear()
        diffProps.Clear()
        Me.childrenOfObj1.Clear()
        Me.childrenOfObj2.Clear()
    End Sub

    Public Sub Reinitialize()
        Configure()
    End Sub

    ''' <summary>
    ''' Compare the two objects with each other.
    ''' </summary>
    ''' <param name="object1"></param>
    ''' <param name="object2"></param>
    ''' <returns></returns>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Private Function [Compare](ByVal object1 As SqlSmoObject, ByVal object2 As SqlSmoObject) As Boolean
        Try
            level += 1

            ' Invariant: the objects have the same Type!!!
            If object1.GetType().Name <> object2.GetType().Name Then
                LogError(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ObjectsHaveDifferentTypes, object1.GetType().Name, object2.GetType().Name))
                level -= 1
                Write(Environment.NewLine)
                Throw New ApplicationException(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ObjectsHaveDifferentTypes, object1.GetType().Name, object2.GetType().Name))
            End If

            If IsFromIndexCreation(object1) OrElse IsFromIndexCreation(object2) Then
                level -= 1
                Write(Environment.NewLine)
                Return True
            End If

            If IsSystemNamed(object1) OrElse IsSystemNamed(object2) Then
                level -= 1
                Write(Environment.NewLine)
                Return True
            End If

            ' Removed to facilitate comparisons of system objects.
            'if (IsSystemObject(object1) || IsSystemObject(object2))
            '{
            '    level--;
            '    Write(Environment.NewLine);
            '    return true;
            '}
            If ShouldIgnoreSchema(object1.Urn.GetAttribute("Schema")) OrElse ShouldIgnoreSchema(object2.Urn.GetAttribute("Schema")) Then
                level -= 1
                Write(Environment.NewLine)
                Return True
            End If

            If ShouldIgnore(object1.GetType().Name) Then
                level -= 1
                Write(Environment.NewLine)
                Return True
            End If

            If IsAutoCreated(object1) OrElse IsAutoCreated(object2) Then
                level -= 1
                Write(Environment.NewLine)
                Return True
            End If

            ' See if at least one of the object is in the ignore list; this way if the user wants to
            ' Ignore an object (let's say Col1) is enough to enter Col1 of the first obj in the RED list 
            ' Not both Col1 from both object (smoObject1 and smoObject2)
            If ShouldIgnore(object1.Urn) OrElse ShouldIgnore(object2.Urn) Then
                level -= 1
                Write(Environment.NewLine)
                Return True
            End If

            ' Iterate through all properties and ignore those from red list
            Dim pi1 As PropertyInfo() = object1.GetType().GetProperties()
            Dim pi2 As PropertyInfo() = object2.GetType().GetProperties()

            ' Sort these two arrays based on the property names
            Array.Sort(pi1, comparer)
            Array.Sort(pi2, comparer)

            ' Let's see if the number of properties are the same; 
            ' If not that means we play with diferent types...
            ' Which it shouldn't happen at this level...
            If pi1.Length <> pi2.Length Then
                ' This case is almost impossible
                ' But stuff happens hence extra tests applied :)
                level -= 1
                Write(Environment.NewLine)
                Throw New ApplicationException(String.Format(System.Threading.Thread.CurrentThread.CurrentCulture, My.Resources.DifferentNumberProperties, object1.Urn, object2.Urn, pi1.Length, pi2.Length))
            End If

            returnVal = returnVal And IterateProps(object1, object2, pi1, pi2)
            level -= 1
            Write(Environment.NewLine)

            Return returnVal
        Catch ex As ApplicationException
            WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ExceptionWhileComparing, object1.Urn, object2.Urn), MessageType.Error)
            WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.Exception, ex))
            level -= 1
            Write(Environment.NewLine)

            Return False
        End Try
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")> _
    Private Function IterateProps(ByVal object1 As SqlSmoObject, ByVal object2 As SqlSmoObject, ByVal pi1() As PropertyInfo, ByVal pi2() As PropertyInfo) As Boolean
        Dim propInfo1 As PropertyInfo
        Dim propInfo2 As PropertyInfo

        ' Iterate through all properties
        For k As Integer = 0 To pi1.Length - 1
            Try
                propInfo1 = pi1(k)
                propInfo2 = pi2(k)
                currentPropertyName = propInfo1.Name
                If ShouldIgnore(propInfo1) Then Continue For

                If ShouldIgnore(propInfo1.Name, object1.Urn) OrElse ShouldIgnore(propInfo2.Name, object2.Urn) Then Continue For

                If ShouldIgnore(propInfo1.Name, object1.GetType().Name) Then Continue For

                If ShouldIgnore(propInfo1.PropertyType.Name) Then Continue For

                ' RULE: We'll not compare property Name for the initial 
                ' Objects smoObject1 and smoObject2 entered by the user
                If propInfo1.Name = "Name" _
                    AndAlso level = 1 Then Continue For

                ' Check to see if the current prop is collection
                If Not (propInfo1.PropertyType.GetInterface("ICollection", True) Is Nothing) Then
                    AnalyzeCollection(propInfo1, propInfo2, object1, object2)
                    Continue For
                End If

                ' NB: This test must be AFTER the prior tests 
                ' Make sure you do not disturb this order!!!
                If Not IsInPropBag(propInfo1, object1) Then Continue For

                If Not (propInfo1.PropertyType.BaseType Is Nothing) Then
                    If propInfo1.PropertyType.BaseType.FullName = "System.ValueType" _
                        AndAlso propInfo2.PropertyType.BaseType.FullName = "System.ValueType" Then
                        returnVal = returnVal And CompareValueTypes(propInfo1, propInfo2, object1, object2)
                        Continue For
                    End If
                Else
                    If propInfo1.PropertyType.FullName = "System.ValueType" _
                        AndAlso propInfo2.PropertyType.FullName = "System.ValueType" Then
                        returnVal = returnVal And CompareValueTypes(propInfo1, propInfo2, object1, object2)
                        Continue For
                    End If
                End If

                If propInfo1.PropertyType.FullName = "System.String" _
                    AndAlso propInfo2.PropertyType.FullName = "System.String" Then
                    returnVal = returnVal And CompareStringTypes(propInfo1, propInfo2, object1, object2)
                    Continue For
                End If

                If propInfo1.PropertyType.IsEnum _
                    AndAlso propInfo2.PropertyType.IsEnum Then
                    returnVal = returnVal And CompareEnumTypes(propInfo1, propInfo2, object1, object2)

                    Continue For
                Else
                    ' The property Type is something other than "System.Types"
                    returnVal = returnVal And CompareAnyTypes(propInfo1, propInfo2, object1, object2)
                End If
            Catch ex As CollectionNotAvailableException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ExceptionWhileComparing, object1.Urn, object2.Urn))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ReadingProperty, currentPropertyName))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                WriteLine(separatorLine)

                Continue For
            Catch ex As InvalidVersionEnumeratorException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ExceptionWhileComparing, object1.Urn, object2.Urn))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ReadingProperty, currentPropertyName))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                WriteLine(separatorLine)

                Continue For
            Catch ex As UnsupportedVersionException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ExceptionWhileComparing, object1.Urn, object2.Urn))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ReadingProperty, currentPropertyName))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                WriteLine(separatorLine)

                Continue For
            Catch ex As UnknownPropertyException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ExceptionWhileComparing, object1.Urn, object2.Urn))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ReadingProperty, currentPropertyName))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                WriteLine(separatorLine)

                Continue For
            Catch ex As PropertyCannotBeRetrievedException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ExceptionWhileComparing, object1.Urn, object2.Urn))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ReadingProperty, currentPropertyName))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                WriteLine(separatorLine)

                Continue For
            Catch ex As InternalEnumeratorException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ExceptionWhileComparing, object1.Urn, object2.Urn))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ReadingProperty, currentPropertyName))
                If ex.Message.IndexOf("version") <> -1 _
                    AndAlso ex.Message.IndexOf("is not supported") <> -1 Then
                    WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                    WriteLine(separatorLine)

                    Continue For
                End If

                returnVal = returnVal And False
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.Exception, ex), MessageType.Error)
                WriteLine(separatorLine)
            Catch ex As ApplicationException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ExceptionWhileComparing, object1.Urn, object2.Urn))
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ReadingProperty, currentPropertyName))
                If Not (ex.InnerException Is Nothing) Then
                    If TypeOf ex.InnerException Is CollectionNotAvailableException Then
                        WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                        WriteLine(separatorLine)

                        Continue For
                    End If

                    If TypeOf ex.InnerException Is InvalidVersionEnumeratorException Then
                        WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                        WriteLine(separatorLine)

                        Continue For
                    End If

                    If TypeOf ex.InnerException Is UnsupportedVersionException Then
                        WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                        WriteLine(separatorLine)

                        Continue For
                    End If

                    If TypeOf ex.InnerException Is UnknownPropertyException Then
                        WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                        WriteLine(separatorLine)

                        Continue For
                    End If

                    If TypeOf ex.InnerException Is PropertyCannotBeRetrievedException Then
                        WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                        WriteLine(separatorLine)

                        Continue For
                    End If

                    If TypeOf ex.InnerException Is InternalEnumeratorException Then
                        If ex.Message.IndexOf("version") <> -1 _
                            AndAlso ex.Message.IndexOf("is not supported") <> -1 Then
                            WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.IgnoredException, ex), MessageType.Warning)
                            WriteLine(separatorLine)

                            Continue For
                        End If
                    End If
                End If

                returnVal = returnVal And False
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.Exception, ex), MessageType.Error)
                WriteLine(separatorLine)
            End Try
        Next

        Return returnVal
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Private Function CompareAnyTypes(ByVal propInfo1 As PropertyInfo, ByVal propInfo2 As PropertyInfo, ByVal object1 As SqlSmoObject, ByVal object2 As SqlSmoObject) As Boolean
        Dim ReturnValueLoc As Boolean = True
        Dim objTemp1 As Object = propInfo1.GetValue(object1, Nothing)
        Dim objTemp2 As Object = propInfo2.GetValue(object2, Nothing)

        If objTemp1 Is Nothing _
            AndAlso objTemp2 Is Nothing Then
            ' (i.e. DefaultConstraint)
            If Not CanBeNull(propInfo1.Name, object1.GetType().Name) Then
                Throw New ApplicationException(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.NullReferenceException, object1.Urn, object2.Urn, propInfo1.Name, propInfo2.Name))
            End If

            ' On else branch we do nothing; both props are null and they 
            ' are accepted with null values
            Return True
        End If

        If objTemp1.GetType().IsSubclassOf(GetType(SqlSmoObject)) _
            AndAlso objTemp2.GetType().IsSubclassOf(GetType(SqlSmoObject)) Then
            ReturnValueLoc = ReturnValueLoc And [Compare](CType(objTemp1, SqlSmoObject), CType(objTemp2, SqlSmoObject))

            Return ReturnValueLoc
        End If

        If objTemp1.GetType().FullName = "Microsoft.SqlServer.Management.Smo.DataType" _
            AndAlso objTemp2.GetType().FullName = "Microsoft.SqlServer.Management.Smo.DataType" Then
            Dim dt1 As DataType = TryCast(objTemp1, DataType)
            Dim dt2 As DataType = TryCast(objTemp2, DataType)

            ReturnValueLoc = ReturnValueLoc _
                And CType(IIf(dt1.SqlDataType = dt2.SqlDataType, True, False), Boolean)
            If Not ReturnValueLoc Then
                Dim temp As New DifferentProperties()
                temp.Urn1 = object1.Urn
                temp.Urn2 = object2.Urn
                temp.PropertyName = propInfo1.Name

                ' Here we store all the prop and values for DataType
                Dim objectValue1 As String = String.Format( _
                    System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.ObjectValue, dt1.MaximumLength, dt1.Name, _
                    dt1.NumericPrecision, dt1.NumericScale, dt1.Schema, dt1.SqlDataType)

                Dim objectValue2 As String = String.Format( _
                    System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.ObjectValue, dt2.MaximumLength, dt2.Name, _
                    dt2.NumericPrecision, dt2.NumericScale, dt2.Schema, dt2.SqlDataType)

                temp.ObjectValue1 = objectValue1
                temp.ObjectValue2 = objectValue2

                diffProps.Add(temp)
            End If

            Return ReturnValueLoc
        End If

        ReturnValueLoc = ReturnValueLoc And objTemp1.Equals(objTemp2)
        Return ReturnValueLoc
    End Function

    Private Function CompareEnumTypes(ByVal propInfo1 As PropertyInfo, ByVal propInfo2 As PropertyInfo, ByVal object1 As SqlSmoObject, ByVal object2 As SqlSmoObject) As Boolean
        Dim s1 As System.Enum = CType(propInfo1.GetValue(object1, Nothing), System.Enum)
        Dim s2 As System.Enum = CType(propInfo2.GetValue(object2, Nothing), System.Enum)
        Dim ReturnValueLoc As Boolean

        If (s1.CompareTo(s2) = 0) Then
            ReturnValueLoc = True
        Else
            ReturnValueLoc = False
        End If

        If Not ReturnValueLoc Then
            Dim temp As New DifferentProperties()
            temp.Urn1 = object1.Urn
            temp.Urn2 = object2.Urn
            temp.PropertyName = propInfo1.Name
            temp.ObjectValue1 = propInfo1.GetValue(object1, Nothing).ToString()
            temp.ObjectValue2 = propInfo2.GetValue(object2, Nothing).ToString()
            diffProps.Add(temp)
        End If

        Return ReturnValueLoc
    End Function

    Private Function CompareStringTypes(ByVal propInfo1 As PropertyInfo, ByVal propInfo2 As PropertyInfo, ByVal object1 As SqlSmoObject, ByVal object2 As SqlSmoObject) As Boolean
        Dim ReturnValueLoc As Boolean = False

        If propInfo1.Name = "TextBody" Then
            ReturnValueLoc = CType(IIf(CStr(propInfo1.GetValue(object1, Nothing)).TrimEnd() = CStr(propInfo2.GetValue(object2, Nothing)).TrimEnd(), True, False), Boolean)
        Else
            ReturnValueLoc = CType(IIf(CStr(propInfo1.GetValue(object1, Nothing)) = CStr(propInfo2.GetValue(object2, Nothing)), True, False), Boolean)
        End If

        If Not ReturnValueLoc Then
            Dim temp As New DifferentProperties()
            temp.Urn1 = object1.Urn
            temp.Urn2 = object2.Urn
            temp.PropertyName = propInfo1.Name
            temp.ObjectValue1 = propInfo1.GetValue(object1, Nothing).ToString()
            temp.ObjectValue2 = propInfo2.GetValue(object2, Nothing).ToString()
            diffProps.Add(temp)
        End If

        Return ReturnValueLoc
    End Function

    Private Function CompareValueTypes(ByVal propInfo1 As PropertyInfo, ByVal propInfo2 As PropertyInfo, ByVal object1 As SqlSmoObject, ByVal object2 As SqlSmoObject) As Boolean
        Dim obj1 As Object = propInfo1.GetValue(object1, Nothing)
        Dim obj2 As Object = propInfo2.GetValue(object2, Nothing)
        Dim ReturnValueLoc As Boolean = obj1.Equals(obj2)
        If Not ReturnValueLoc Then
            Dim temp As New DifferentProperties()
            temp.Urn1 = object1.Urn
            temp.Urn2 = object2.Urn
            temp.PropertyName = propInfo1.Name
            temp.ObjectValue1 = propInfo1.GetValue(object1, Nothing).ToString()
            temp.ObjectValue2 = propInfo2.GetValue(object2, Nothing).ToString()
            diffProps.Add(temp)
        End If

        Return ReturnValueLoc
    End Function

    Private Sub AnalyzeCollection(ByVal propInfo1 As PropertyInfo, ByVal propInfo2 As PropertyInfo, ByVal object1 As SqlSmoObject, ByVal object2 As SqlSmoObject)
        Dim iColl1 As ICollection = TryCast(propInfo1.GetValue(object1, Nothing), ICollection)
        Dim iColl2 As ICollection = TryCast(propInfo2.GetValue(object2, Nothing), ICollection)

        Dim enum1 As IEnumerator = iColl1.GetEnumerator()
        Dim enum2 As IEnumerator = iColl2.GetEnumerator()

        ' Populate the ChildrenOfObject1 and ChildrenOfObject2
        Populate1(enum1, enum2)
        Populate2(enum2, enum1)
    End Sub

    Public Overloads Function Start(ByVal urn1 As Urn, ByVal urn2 As Urn, ByVal compareContents As Boolean) As Boolean
        If Not compareContents Then
            Return Start(urn1, urn2)
        Else
            Return Start(urn1, urn2) _
                AndAlso CompareContent(urn1, urn2)
        End If
    End Function

    Public Overloads Function Start(ByVal urn1 As Urn, ByVal urn2 As Urn) As Boolean
        Setup(urn1, urn2)

        If smoObject1 Is Nothing Then
            Throw New ApplicationException( _
                String.Format(System.Threading.Thread.CurrentThread.CurrentCulture, _
                My.Resources.ObjectNotCreated, urn1))
        End If

        If smoObject2 Is Nothing Then
            Throw New ApplicationException( _
                String.Format(System.Threading.Thread.CurrentThread.CurrentCulture, _
                My.Resources.ObjectNotCreated, urn2))
        End If

        ' See if the object have the same Type; 
        If smoObject1.GetType().Name <> smoObject2.GetType().Name Then
            Throw New ApplicationException( _
                String.Format(System.Threading.Thread.CurrentThread.CurrentCulture, _
                My.Resources.ObjectsHaveDifferentTypes, _
                smoObject1.GetType().Name, smoObject2.GetType().Name))
        End If

        ' For small databases because of the huge number of SPs it takes a lot longer than it should.
        Dim TempUrn1 As Urn = urn1
        Dim TempUrn2 As Urn = urn2

        While TempUrn1.Type <> "Database"
            TempUrn1 = TempUrn1.Parent
            TempUrn2 = TempUrn2.Parent
            If TempUrn1 Is Nothing Then
                Exit While
            End If
        End While

        If urn1.Type = "Database" Then
            Dim database1 As Database = CType(Me.objServer1.GetSmoObject(TempUrn1), Database)
            database1.PrefetchObjects()

            Dim database2 As Database = CType(Me.objServer2.GetSmoObject(TempUrn2), Database)
            database2.PrefetchObjects()
        End If

        Return [Compare](smoObject1, smoObject2)
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Private Sub Setup(ByVal urn1 As Urn, ByVal urn2 As Urn)
        If serverName1 Is Nothing OrElse serverName2 Is Nothing Then
            Throw New ApplicationException(My.Resources.ServerPropertiesCannotBeNull)
        End If

        Me.objServer1 = New Server(serverName1)
        Me.objServer2 = New Server(serverName2)

        If loginName1 Is Nothing OrElse loginName1.Length = 0 Then
            Me.objServer1.ConnectionContext.LoginSecure = True
        Else
            Me.objServer1.ConnectionContext.LoginSecure = False
            Me.objServer1.ConnectionContext.Login = loginName1
            Me.objServer1.ConnectionContext.Password = Me.serverPassword1
        End If

        If loginName2 Is Nothing OrElse loginName2.Length = 0 Then
            Me.objServer2.ConnectionContext.LoginSecure = True
        Else
            Me.objServer2.ConnectionContext.LoginSecure = False
            Me.objServer2.ConnectionContext.Login = loginName2
            Me.objServer2.ConnectionContext.Password = Me.serverPassword2
        End If

        Try
            smoObject1 = Me.objServer1.GetSmoObject(urn1)
        Catch ex As ApplicationException
            WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ErrorCreatingFirstObject, ex))
            Throw New ApplicationException(My.Resources.ErrorCreatingFirstObjectException, ex)
        End Try

        Try
            smoObject2 = Me.objServer2.GetSmoObject(urn2)
        Catch ex As ApplicationException
            WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ErrorCreatingSecondObject, ex))
            Throw New ApplicationException(My.Resources.ErrorCreatingSecondObjectException, ex)
        End Try

        If smoObject1 Is Nothing Then
            Throw New ApplicationException(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ObjectNotCreated, urn1))
        End If

        If smoObject2 Is Nothing Then
            Throw New ApplicationException(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ObjectNotCreated, urn2))
        End If
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Private Sub Configure()
        Dim xmlType As XmlNode = Nothing
        Dim xmlSchema As XmlNode = Nothing
        Dim xmlProp As XmlNode = Nothing
        Dim xmlUrn As XmlNode = Nothing
        Dim nodeList As XmlNodeList = Nothing
        Dim xmlDoc As New XmlDocument()
        Dim pt As PropertyAndType
        Dim po As PropertyAndObject

        ReturnValue = True

        Dim testBin As String = Environment.GetEnvironmentVariable("TESTBIN")
        If testBin Is Nothing Then
            testBin = String.Empty
        End If

        Dim xmlr As New XmlTextReader(testBin & "\" & configFile)
        xmlr.ProhibitDtd = True
        Try
            xmlDoc.Load(xmlr)
        Catch
            xmlr = New XmlTextReader(configFile)
            xmlr.ProhibitDtd = True
            Try
                xmlDoc.Load(xmlr)
            Catch ex As ApplicationException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ErrorLoadingConfiguration, ex))
                Throw New ApplicationException(My.Resources.ErrorLoadingConfigurationException, ex)
            End Try
        End Try

        Dim docElem As XmlElement = xmlDoc.DocumentElement
        nodeList = docElem.GetElementsByTagName("Ignore")

        Dim k As Integer = 0
        For Each node As XmlNode In nodeList
            k += 1
            xmlType = node.Attributes.GetNamedItem("Type")
            xmlProp = node.Attributes.GetNamedItem("Prop")
            xmlUrn = node.Attributes.GetNamedItem("Urn")
            xmlSchema = node.Attributes.GetNamedItem("Schema")
            If xmlType Is Nothing _
                AndAlso xmlProp Is Nothing _
                AndAlso xmlUrn Is Nothing _
                AndAlso xmlSchema Is Nothing Then
                Throw New ApplicationException(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.BadConfigurationFile, k))
            End If

            If Not (xmlSchema Is Nothing) Then
                ignoreSchema.Add(xmlSchema.Value)
            ElseIf xmlProp Is Nothing _
                AndAlso xmlUrn Is Nothing Then
                ' Ignore the Type
                ignoreType.Add(xmlType.Value)
            ElseIf xmlType Is Nothing _
                AndAlso xmlProp Is Nothing Then
                ' Ignore the object
                ignoreObject.Add(xmlUrn.Value)
            ElseIf xmlType Is Nothing _
                AndAlso xmlUrn Is Nothing Then
                ' Ignore the property
                ignoreProperty.Add(xmlProp.Value)
            ElseIf xmlUrn Is Nothing Then
                ' Ignore a property for a specific Type
                pt = New PropertyAndType()
                pt.PropertyName = xmlProp.Value
                pt.Type = xmlType.Value
                ignorePropertyForType.Add(pt)
            ElseIf xmlType Is Nothing Then
                ' Ignore a property for the object
                po = New PropertyAndObject()
                po.PropertyName = xmlProp.Value
                po.Urn = New Uri(xmlUrn.Value)
                ignorePropertyForObject.Add(po)
            End If
        Next

        nodeList = docElem.GetElementsByTagName("CanBeNullReference")
        k = 0
        For Each node As XmlNode In nodeList
            k += 1
            xmlType = node.Attributes.GetNamedItem("Type")
            xmlProp = node.Attributes.GetNamedItem("Prop")
            If xmlType Is Nothing OrElse xmlProp Is Nothing Then
                Throw New ApplicationException(String.Format( _
                    System.Globalization.CultureInfo.InvariantCulture, _
                    My.Resources.BadConfigurationFileNullReference, k))
            End If

            pt = New PropertyAndType()
            pt.PropertyName = xmlProp.Value
            pt.Type = xmlType.Value
            collectionCanBeNull.Add(pt)
        Next
    End Sub

    Public Property Logger() As ILogger
        Get
            Return Me.compareLogger
        End Get

        Set(ByVal value As ILogger)
            Me.compareLogger = value
        End Set
    End Property

    Private Sub Write(ByVal msg As String)
        If Not (Me.compareLogger Is Nothing) Then
            Me.compareLogger.LogMessage(msg, MessageType.Info)
        End If
    End Sub

    Private Overloads Sub WriteLine(ByVal msg As String)
        If Not (Me.compareLogger Is Nothing) Then
            Me.compareLogger.LogMessage(msg, MessageType.Info)
        End If
    End Sub

    Private Overloads Sub WriteLine(ByVal msg As String, ByVal msgType As MessageType)
        If Not (Me.compareLogger Is Nothing) Then
            Me.compareLogger.LogMessage(msg, msgType)
        End If
    End Sub

    Private Sub LogError(ByVal msg As String)
        If Not (Me.compareLogger Is Nothing) Then
            Me.compareLogger.LogMessage(msg, MessageType.Error)
        End If
    End Sub

    Private Shared Function IsFromIndexCreation(ByVal obj As SqlSmoObject) As Boolean
        Dim pi As PropertyInfo = obj.GetType().GetProperty("IsFromIndexCreation", Type.GetType("System.Boolean"))
        If Not (pi Is Nothing) Then
            Return CBool(pi.GetValue(obj, Nothing))
        Else
            Return False
        End If
    End Function

    Private Shared Function IsSystemNamed(ByVal obj As SqlSmoObject) As Boolean
        Dim pi As PropertyInfo = obj.GetType().GetProperty("IsSystemNamed", Type.GetType("System.Boolean"))
        If Not (pi Is Nothing) Then
            Return CBool(pi.GetValue(obj, Nothing))
        Else
            Return False
        End If
    End Function

    Private Shared Function IsSystemObject(ByVal obj As SqlSmoObject) As Boolean
        Dim pi As PropertyInfo = obj.GetType().GetProperty("IsSystemObject", Type.GetType("System.Boolean"))
        If Not (pi Is Nothing) Then
            Return CBool(pi.GetValue(obj, Nothing))
        Else
            Return False
        End If
    End Function

    Private Shared Function IsAutoCreated(ByVal obj As SqlSmoObject) As Boolean
        Dim pi As PropertyInfo = obj.GetType().GetProperty("IsAutoCreated", Type.GetType("System.Boolean"))
        If Not (pi Is Nothing) Then
            Return CBool(pi.GetValue(obj, Nothing))
        Else
            Return False
        End If
    End Function

    Private Shared Function IsInPropBag(ByVal propInfo1 As PropertyInfo, ByVal object1 As SqlSmoObject) As Boolean
        Return object1.Properties.Contains(propInfo1.Name)
    End Function

    Private Overloads Function ShouldIgnore(ByVal prop As PropertyInfo) As Boolean
        Return ignoreProperty.Contains(prop.Name)
    End Function

    Private Overloads Function ShouldIgnore(ByVal type As String) As Boolean
        Return ignoreType.Contains(type)
    End Function

    Private Overloads Function ShouldIgnore(ByVal urnObject As Urn) As Boolean
        Return ignoreObject.Contains(urnObject.ToString())
    End Function

    Private Overloads Function ShouldIgnore(ByVal propertyName As String, ByVal urnObj As Urn) As Boolean
        Dim bRet As Boolean = False

        For k As Integer = 0 To ignorePropertyForObject.Count - 1
            If CType(ignorePropertyForObject(k), PropertyAndObject).PropertyName = propertyName _
                AndAlso CType(ignorePropertyForObject(k), PropertyAndObject).Urn.ToString() = urnObj.ToString() Then
                Return True
            End If
        Next

        Return bRet
    End Function

    Private Overloads Function ShouldIgnore(ByVal propertyName As String, ByVal type As String) As Boolean
        Dim bRet As Boolean = False

        For k As Integer = 0 To ignorePropertyForType.Count - 1
            If CType(ignorePropertyForType(k), PropertyAndType).PropertyName = propertyName _
                AndAlso CType(ignorePropertyForType(k), PropertyAndType).Type = type Then
                Return True
            End If
        Next

        Return bRet
    End Function

    Private Function ShouldIgnoreSchema(ByVal schema As String) As Boolean
        Dim bRet As Boolean = False

        For k As Integer = 0 To ignoreSchema.Count - 1
            If ignoreSchema(k) = schema Then
                Return True
            End If
        Next

        Return bRet
    End Function

    Private Function CanBeNull(ByVal propertyName As String, ByVal type As String) As Boolean
        Dim bRet As Boolean = False

        For k As Integer = 0 To collectionCanBeNull.Count - 1
            If CType(collectionCanBeNull(k), PropertyAndType).PropertyName = propertyName _
                AndAlso CType(collectionCanBeNull(k), PropertyAndType).Type = type Then
                Return True
            End If
        Next

        Return bRet
    End Function

    ''' <summary>
    ''' Populate the ChildrenOfObject1 and ChildrenOfObject2
    ''' </summary>
    ''' <param name="enum2"></param>
    ''' <param name="enum1"></param>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Private Sub Populate2(ByVal enum2 As IEnumerator, ByVal enum1 As IEnumerator)
        Dim nFound As Integer
        Dim objectName1 As String
        Dim objectName2 As String
        Dim namedObject1 As NamedSmoObject
        Dim namedObject2 As NamedSmoObject

        enum2.Reset()
        While enum2.MoveNext()
            enum1.Reset()
            nFound = 0
            While enum1.MoveNext()
                objectName1 = enum1.Current.ToString()
                objectName2 = enum2.Current.ToString()

                namedObject1 = TryCast(enum1.Current, NamedSmoObject)
                namedObject2 = TryCast(enum2.Current, NamedSmoObject)

                If Not (namedObject1 Is Nothing) Then
                    objectName1 = namedObject1.Name
                End If

                If Not (namedObject2 Is Nothing) Then
                    objectName2 = namedObject2.Name
                End If

                If objectName1 = objectName2 Then
                    ' Verify the schema name also
                    Dim piSchema1 As PropertyInfo = enum1.Current.GetType().GetProperty("Schema")
                    Dim piSchema2 As PropertyInfo = enum2.Current.GetType().GetProperty("Schema")
                    If Not (piSchema1 Is Nothing) _
                        AndAlso Not (piSchema2 Is Nothing) Then
                        If piSchema1.GetValue(enum1.Current, Nothing).ToString() = piSchema2.GetValue(enum2.Current, Nothing).ToString() Then
                            nFound = 1
                            If Not (namedObject1 Is Nothing) _
                                AndAlso Not (namedObject2 Is Nothing) Then
                                ReturnValue = ReturnValue And [Compare](namedObject1, namedObject2)
                            End If
                            Exit While
                        End If
                    ElseIf piSchema1 Is Nothing _
                        AndAlso piSchema2 Is Nothing Then
                        nFound = 1
                        If Not (namedObject1 Is Nothing) _
                            AndAlso Not (namedObject2 Is Nothing) Then
                            ReturnValue = ReturnValue And [Compare](namedObject1, namedObject2)
                        End If
                        Exit While
                    Else
                        ' In this case one is null the other is not
                        ' We throw because this case should never happen
                        Throw New ApplicationException( _
                            My.Resources.OneTypeHasSchema)
                    End If
                End If
            End While

            If nFound = 0 Then
                ' We have to check if the object found it is a system object; if it is a system object we don't care about it (ignore it)
                If Not IsSystemObject(CType(enum2.Current, SqlSmoObject)) _
                    AndAlso Not IsAutoCreated(CType(enum2.Current, SqlSmoObject)) _
                    AndAlso Not IsSystemNamed(CType(enum2.Current, SqlSmoObject)) _
                    AndAlso Not IsFromIndexCreation(CType(enum2.Current, SqlSmoObject)) Then
                    ChildrenOfObject2.Add(CType(enum2.Current, SqlSmoObject).Urn.ToString())
                    ReturnValue = ReturnValue And False
                End If
            End If
        End While
    End Sub

    ''' <summary>
    ''' Populate the ChildrenOfObject1 and ChildrenOfObject2
    ''' </summary>
    ''' <param name="enum1"></param>
    ''' <param name="enum2"></param>
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Private Sub Populate1(ByVal enum1 As IEnumerator, ByVal enum2 As IEnumerator)
        Dim nFound As Integer
        Dim objectName1 As String
        Dim objectName2 As String
        Dim namedObject1 As NamedSmoObject
        Dim namedObject2 As NamedSmoObject
        Dim piSchema1 As PropertyInfo
        Dim piSchema2 As PropertyInfo

        enum1.Reset()
        While enum1.MoveNext()
            enum2.Reset()
            nFound = 0
            While enum2.MoveNext()
                objectName1 = enum1.Current.ToString()
                objectName2 = enum2.Current.ToString()

                namedObject1 = TryCast(enum1.Current, NamedSmoObject)
                namedObject2 = TryCast(enum2.Current, NamedSmoObject)

                If Not (namedObject1 Is Nothing) Then
                    objectName1 = namedObject1.Name
                End If

                If Not (namedObject2 Is Nothing) Then
                    objectName2 = namedObject2.Name
                End If

                If objectName1 = objectName2 Then
                    ' Verify the schema name also
                    piSchema1 = enum1.Current.GetType().GetProperty("Schema")
                    piSchema2 = enum2.Current.GetType().GetProperty("Schema")
                    If Not (piSchema1 Is Nothing) _
                        AndAlso Not (piSchema2 Is Nothing) Then
                        If piSchema1.GetValue(enum2.Current, Nothing).ToString() = piSchema2.GetValue(enum2.Current, Nothing).ToString() Then
                            nFound = 1
                            Exit While
                        End If
                    ElseIf piSchema1 Is Nothing _
                        AndAlso piSchema2 Is Nothing Then
                        nFound = 1
                        Exit While
                    Else
                        ' In this case one is null the other is not
                        ' We throw because this case should never happen
                        Throw New ApplicationException(My.Resources.OneTypeHasSchema)
                    End If
                End If
            End While

            If nFound = 0 Then
                ' We have to check if the object found it is a system object; if it is a system object we don't care about it (ignore it)
                If Not IsSystemObject(CType(enum1.Current, SqlSmoObject)) _
                    AndAlso Not IsAutoCreated(CType(enum1.Current, SqlSmoObject)) _
                    AndAlso Not IsSystemNamed(CType(enum1.Current, SqlSmoObject)) _
                    AndAlso Not IsFromIndexCreation(CType(enum1.Current, SqlSmoObject)) Then
                    Me.childrenOfObj1.Add(CType(enum1.Current, SqlSmoObject).Urn.ToString())
                    ReturnValue = ReturnValue And False
                End If
            End If
        End While
    End Sub

    Public Function CompareContent(ByVal urn1 As Urn, ByVal urn2 As Urn) As Boolean
        Dim result As Boolean = True

        ' See if they are tables
        Dim table1 As Table = TryCast(Me.objServer1.GetSmoObject(urn1), Table)
        Dim table2 As Table = TryCast(Me.objServer2.GetSmoObject(urn2), Table)
        If Not (table1 Is Nothing) _
            AndAlso Not (table2 Is Nothing) Then
            Return CompareObjectContents(urn1, urn2, table1.Schema, table2.Schema)
        End If

        ' See if they are views
        Dim view1 As View = TryCast(Me.objServer1.GetSmoObject(urn1), View)
        Dim view2 As View = TryCast(Me.objServer2.GetSmoObject(urn2), View)

        If Not (view1 Is Nothing) _
            AndAlso Not (view2 Is Nothing) Then
            Return CompareObjectContents(urn1, urn2, view1.Schema, view2.Schema)
        End If

        Dim database1 As Database = TryCast(Me.objServer1.GetSmoObject(urn1), Database)
        Dim database2 As Database = TryCast(Me.objServer2.GetSmoObject(urn2), Database)

        If database1 Is Nothing OrElse database2 Is Nothing Then
            WriteLine(My.Resources.ObjectsNotDatabaseEtc, MessageType.Error)
            Return False
        End If

        ' Get all tables
        If database1.Tables.Count <> database2.Tables.Count Then
            WriteLine(My.Resources.DifferentNumberTables, MessageType.Error)
            Return False
        End If

        For k As Integer = 0 To database1.Tables.Count - 1
            result = result And CompareObjectContents(database1.Tables(k).Urn, database2.Tables(k).Urn, database1.Tables(k).Schema, database2.Tables(k).Schema)
        Next

        ' Get all views
        If database1.Views.Count <> database2.Views.Count Then
            WriteLine(My.Resources.DifferentNumberViews, MessageType.Error)
            Return False
        End If

        For k As Integer = 0 To database1.Views.Count - 1
            result = result And CompareObjectContents(database1.Views(k).Urn, database2.Views(k).Urn, database1.Views(k).Schema, database2.Views(k).Schema)
        Next

        Return result
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Private Function CompareObjectContents(ByVal urn1 As Urn, ByVal urn2 As Urn, ByVal schema1 As String, ByVal schema2 As String) As Boolean
        Dim sysSchemas As New ArrayList(New String() {"sys", "INFORMATION_SCHEMA"})
        Dim result As Boolean = True
        Dim objectName1 As String = Nothing
        Dim objectName2 As String = Nothing
        Dim databaseName1 As String = Nothing
        Dim databaseName2 As String = Nothing
        Dim command1 As String = Nothing
        Dim command2 As String = Nothing
        Dim myReader1 As SqlDataReader = Nothing
        Dim myReader2 As SqlDataReader = Nothing
        Dim type1 As Type = Nothing
        Dim type2 As Type = Nothing
        Dim value1 As Object = Nothing
        Dim value2 As Object = Nothing
        Dim notEnd1 As Boolean = False
        Dim notEnd2 As Boolean = False
        Dim bAux As Boolean = True
        Dim nRows As Integer = 0

        If sysSchemas.Contains(schema1) OrElse sysSchemas.Contains(schema2) Then
            Return True
        End If
        WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.Comparing, urn1, urn2))

        Try
            Try
                ' This has to be a database
                databaseName1 = urn1.Parent.GetAttribute("Name")
                objectName1 = urn1.GetAttribute("Name")
                command1 = String.Format(System.Globalization.CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}].[{2}]", databaseName1, schema1, objectName1)
                myReader1 = Me.objServer1.ConnectionContext.ExecuteReader(command1)
            Catch ex As ApplicationException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ErrorReadingFirst, objectName1, ex), MessageType.Error)
                Return False
            End Try

            Try
                ' This has to be a database
                databaseName2 = urn2.Parent.GetAttribute("Name")
                objectName2 = urn2.GetAttribute("Name")
                command2 = String.Format(System.Globalization.CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}].[{2}]", databaseName2, schema2, objectName2)
                myReader2 = Me.objServer2.ConnectionContext.ExecuteReader(command2)
            Catch ex As ApplicationException
                WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ErrorReadingSecond, objectName2, ex), MessageType.Error)

                Return False
            End Try

            If myReader1.FieldCount <> myReader2.FieldCount Then
                WriteLine(My.Resources.FieldCountDiffers, MessageType.Error)
                result = False
            Else
                notEnd1 = myReader1.Read()
                notEnd2 = myReader2.Read()
                While notEnd1 _
                    AndAlso notEnd2
                    bAux = True
                    nRows += 1

                    For k As Integer = 0 To myReader1.FieldCount - 1
                        type1 = myReader1.GetFieldType(k)
                        type2 = myReader2.GetFieldType(k)
                        value1 = myReader1.GetValue(k)
                        value2 = myReader2.GetValue(k)
                        bAux = value1.Equals(value2)
                        result = result And bAux
                        If bAux = False Then
                            WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.Values, value1.ToString(), value2.ToString()))
                        End If

                        bAux = type1.Equals(type2)
                        result = result And bAux
                        If bAux = False Then
                            WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.Types, type1.FullName, type2.FullName))
                        End If
                    Next

                    notEnd1 = myReader1.Read()
                    notEnd2 = myReader2.Read()
                End While

                If notEnd1 <> notEnd2 Then
                    result = False
                    WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.MoreRows, nRows), MessageType.Error)
                End If
            End If
        Catch ex As ApplicationException
            WriteLine(String.Format(System.Globalization.CultureInfo.InvariantCulture, My.Resources.ErrorCreatingFirstObject, ex))
            Throw New ApplicationException(My.Resources.ErrorCreatingFirstObjectException, ex)
        Finally
            myReader1.Close()
            myReader2.Close()
        End Try

        Return result
    End Function
End Class
