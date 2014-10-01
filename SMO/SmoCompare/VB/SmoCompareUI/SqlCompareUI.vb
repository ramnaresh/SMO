'=====================================================================
'
'  File:    SqlCompareUI.vb
'  Summary: Main user interface window
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
Imports System.Windows.Forms
Imports System.Threading

Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Sdk.Sfc
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer
Imports Microsoft.SqlServer.MessageBox


#End Region

Partial Class SqlCompareUI 'The Partial modifier is only required on one class definition per project.
    Inherits Form
    Private defaultServer As String = "(local)"
    Private sqlCompare As SmoCompare
    Private areEqual As Boolean
    Private serverChanged1 As Boolean = True
    Private serverChanged2 As Boolean = True
    Private serverConn1 As ServerConnection
    Private serverConn2 As ServerConnection
    Private paramUrn1 As Urn
    Private paramUrn2 As Urn
    Private objectBrowser As SqlObjectBrowser
    Private objScriptPanel As ScriptPanel

    Delegate Sub WorkerThreadFinished(ByVal objError As Object)

    ''' <summary>
    ''' Default constructor.
    ''' </summary>
    Public Sub New()
        InitializeComponent()

        Try
            Me.sqlCompare = New SmoCompare()
        Catch
            Throw
        End Try
    End Sub

    Private Sub serverTextBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles serverTextBox1.TextChanged
        Me.serverChanged1 = True
        urnTextBox1.Text = String.Empty
    End Sub

    Private Sub passwordTextBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles passwordTextBox1.TextChanged
        Me.serverChanged1 = True
        urnTextBox1.Text = String.Empty
    End Sub

    Private Sub loginTextBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles loginTextBox1.TextChanged
        Me.serverChanged1 = True
        Me.urnTextBox1.Text = String.Empty
    End Sub

    Private Sub serverTextBox2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles serverTextBox2.TextChanged
        Me.serverChanged2 = True
        Me.urnTextBox2.Text = String.Empty
    End Sub

    Private Sub passwordTextBox2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles passwordTextBox2.TextChanged
        Me.serverChanged2 = True
        Me.urnTextBox2.Text = String.Empty
    End Sub

    Private Sub loginTextBox2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles loginTextBox2.TextChanged
        Me.serverChanged2 = True
        Me.urnTextBox2.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Select the first object to compare.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub objectBrowse1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles objectBrowse1Button.Click
        If serverTextBox1.Text Is Nothing OrElse serverTextBox1.Text.Length = 0 Then
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.FirstServerMustBeSpecified
            emb.Show(Me)

            Return
        End If

        If Me.serverChanged1 Then
            Me.serverConn1 = New ServerConnection(serverTextBox1.Text)
            Me.serverConn1.NonPooledConnection = False
            Me.serverConn1.LoginSecure = CType(IIf(loginTextBox1.Text.Trim().Length = 0, True, False), Boolean)
            If Not Me.serverConn1.LoginSecure Then
                Me.serverConn1.Login = loginTextBox1.Text
                Me.serverConn1.Password = passwordTextBox1.Text
            End If

            Me.serverChanged1 = False
        End If

        Me.objectBrowser = New SqlObjectBrowser(Me.serverConn1)
        If Me.objectBrowser.Connected = True Then
            If Not (Me.objectBrowser.Urn Is Nothing) _
                AndAlso Windows.Forms.DialogResult.OK = Me.objectBrowser.ShowDialog(Me) Then
                Me.urnTextBox1.Text = Me.objectBrowser.Urn
            End If
        End If
    End Sub

    ''' <summary>
    ''' Select the second object to compare.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub objectBrowse2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles objectBrowse2Button.Click
        If serverTextBox2.Text Is Nothing OrElse serverTextBox2.Text.Length = 0 Then
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.SecondServerMustBeSpecified
            emb.Show(Me)

            Return
        End If

        If serverChanged2 Then
            Me.serverConn2 = New ServerConnection(Me.serverTextBox2.Text)
            Me.serverConn2.NonPooledConnection = False
            Me.serverConn2.LoginSecure = CType(IIf(loginTextBox2.Text.Trim().Length = 0, True, False), Boolean)
            If Not Me.serverConn2.LoginSecure Then
                Me.serverConn2.Login = loginTextBox2.Text
                Me.serverConn2.Password = passwordTextBox2.Text
            End If

            serverChanged2 = False
        End If

        Me.objectBrowser = New SqlObjectBrowser(Me.serverConn2)
        If Me.objectBrowser.Connected = True Then
            If Me.objectBrowser.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Me.urnTextBox2.Text = Me.objectBrowser.Urn
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handle the shallow comparison
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub shallowCompareButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles shallowCompareButton.Click
        Dim csr As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Dim items() As String
        Dim listViewItem As ListViewItem

        Try
            If ValidateFields() Then
                Try
                    Dim val1 As Object = Nothing
                    Dim val2 As Object = Nothing

                    CleanTheForm()

                    shallowCompareButton.Refresh()
                    shallowCompareButton.Enabled = False

                    Me.paramUrn1 = CType(Me.urnTextBox1.Text, Urn)
                    Me.paramUrn2 = CType(Me.urnTextBox2.Text, Urn)

                    Dim server1 As New Server(Me.serverConn1)
                    Dim server2 As New Server(Me.serverConn2)

                    Dim object1 As SqlSmoObject = server1.GetSmoObject(urnTextBox1.Text)
                    Dim object2 As SqlSmoObject = server2.GetSmoObject(urnTextBox2.Text)

                    object1.Refresh()
                    object2.Refresh()

                    For Each prop As [Property] In object2.Properties
                        Try
                            val1 = object1.Properties(prop.Name).Value
                            val2 = object2.Properties(prop.Name).Value
                            If Not (val1 Is Nothing) AndAlso Not (val2 Is Nothing) Then
                                If Not val1.Equals(val2) Then
                                    items = New String(4) {}
                                    items(0) = prop.Name
                                    items(1) = object1.Urn
                                    items(2) = object2.Urn
                                    If Not (val1 Is Nothing) Then
                                        items(3) = val1.ToString()
                                    Else
                                        items(3) = "null"
                                    End If

                                    If Not (val2 Is Nothing) Then
                                        items(4) = val2.ToString()
                                    Else
                                        items(4) = "null"
                                    End If

                                    listViewItem = New ListViewItem(items)
                                    differencesListView.Items.Add(listViewItem)
                                    differencesListView.Refresh()
                                End If
                            End If
                        Catch ex As PropertyCannotBeRetrievedException
                            ' Ignore properties that can't be accessed
                        End Try
                    Next

                    Dim database1 As Database = TryCast(object1, Database)
                    Dim database2 As Database = TryCast(object2, Database)

                    If Not (database1 Is Nothing) _
                        AndAlso Not (database2 Is Nothing) Then
                        For Each p As [Property] In database1.DatabaseOptions.Properties
                            ' If the props differ in value update the destination database
                            If Not p.IsNull _
                                AndAlso Not database1.DatabaseOptions.Properties(p.Name).IsNull _
                                AndAlso database1.DatabaseOptions.Properties(p.Name).Writable Then
                                If Not database1.DatabaseOptions.Properties(p.Name).Value.Equals(database2.DatabaseOptions.Properties(p.Name).Value) Then
                                    items = New String(4) {}
                                    items(0) = p.Name
                                    items(1) = object1.Urn.ToString() & "/Options"
                                    items(2) = object2.Urn.ToString() & "/Options"
                                    If Not (val1 Is Nothing) Then
                                        items(3) = database1.DatabaseOptions.Properties(p.Name).Value.ToString()
                                    Else
                                        items(3) = "null"
                                    End If

                                    If Not (val2 Is Nothing) Then
                                        items(4) = database2.DatabaseOptions.Properties(p.Name).Value.ToString()
                                    Else
                                        items(4) = "null"
                                    End If

                                    listViewItem = New ListViewItem(items)
                                    differencesListView.Items.Add(listViewItem)
                                    differencesListView.Refresh()
                                End If
                            End If
                        Next
                    End If

                    If differencesListView.Items.Count = 0 Then
                        Dim emb As New ExceptionMessageBox()
                        emb.Text = My.Resources.BothObjectsEqual
                        emb.Show(Me)
                    End If
                Catch ex As SmoException
                    Dim emb As New ExceptionMessageBox(ex)
                    emb.Show(Me)
                End Try
            End If
        Catch ex As ApplicationException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            shallowCompareButton.Enabled = True
            shallowCompareButton.Refresh()
            Me.Cursor = csr
        End Try
    End Sub

    ''' <summary>
    ''' Handle in depth comparison
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub inDepthCompareButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles inDepthCompareButton.Click
        Dim csr As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            If ValidateFields() Then
                InitializeSqlCompare()
                Try
                    CleanTheForm()

                    inDepthCompareButton.Refresh()
                    inDepthCompareButton.Enabled = False

                    paramUrn1 = CType(Me.urnTextBox1.Text, Urn)
                    paramUrn2 = CType(Me.urnTextBox2.Text, Urn)

                    Dim threadStart As New ThreadStart(AddressOf Me.StartComparing)
                    Dim worker As New Thread(threadStart)
                    worker.Start()
                Catch ex As ThreadAbortException
                    Dim emb As New ExceptionMessageBox(ex)
                    emb.Show(Me)
                End Try
            End If
        Catch ex As ApplicationException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr
        End Try
    End Sub

    ''' <summary>
    ''' Generate script to make object 1 identical in props as object 2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub genScript1to2Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles genScript1to2Button.Click
        If ValidateFields() Then
            objScriptPanel = New ScriptPanel(Me.serverConn1, GetScriptToMakeIdenticalObject1())
            objScriptPanel.ShowDialog(Me)
        End If
    End Sub

    ''' <summary>
    ''' Generate script to make object 2 identical in props as object 1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub genScript2to1Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles genScript2to1Button.Click
        If ValidateFields() Then
            objScriptPanel = New ScriptPanel(Me.serverConn2, GetScriptToMakeIdenticalObject2())
            objScriptPanel.ShowDialog(Me)
        End If
    End Sub

    Private Sub CleanTheForm()
        objectListView1.Items.Clear()
        objectListView1.Refresh()
        objectListView2.Items.Clear()
        objectListView2.Refresh()
        differencesListView.Items.Clear()
        differencesListView.Refresh()

        ' Clear all buffers from SqlCompare object
        Me.sqlCompare.Clear()
        Me.sqlCompare.Reinitialize()
    End Sub

    Private Sub ReportResult()
        If Not Me.areEqual Then
            ' Objects which are in smoObject1 only
            For Each urn As String In sqlCompare.ChildrenOfObject1
                objectListView1.Items.Add(urn)
                objectListView1.Refresh()
            Next

            ' Objects which are in smoObject2 only
            For Each urn As String In sqlCompare.ChildrenOfObject2
                objectListView2.Items.Add(urn)
                objectListView2.Refresh()
            Next

            Dim items() As String
            Dim listViewItem As ListViewItem

            ' Properties which are different in value
            For Each prop As DifferentProperties In sqlCompare.DiffProps1
                items = New String(4) {}
                items(0) = prop.PropertyName
                items(1) = prop.Urn1
                items(2) = prop.Urn2
                items(3) = prop.ObjectValue1
                items(4) = prop.ObjectValue2

                listViewItem = New ListViewItem(items)
                differencesListView.Items.Add(listViewItem)
                differencesListView.Refresh()
            Next
        Else
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.BothObjectsEqual
            emb.Show(Me)
        End If
    End Sub

    ' ASSERT: objError is null in case of success
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")> _
    Private Sub WorkerThreadFinishedImpl(ByVal [error] As Object)
        If Me.InvokeRequired = True Then
            Me.Invoke(New WorkerThreadFinished(AddressOf Me.WorkerThreadFinishedImpl), New Object() {[error]})
        Else
            If Not ([error] Is Nothing) Then
                Dim emb As New ExceptionMessageBox(CType([error], Exception))
                emb.Show(Me)
            End If

            Me.inDepthCompareButton.Enabled = True
            Me.inDepthCompareButton.Refresh()
            Me.Cursor = Cursors.Default

            ReportResult()
        End If
    End Sub

    Private Sub StartComparing()
        Dim reportDelegate As New WorkerThreadFinished(AddressOf Me.WorkerThreadFinishedImpl)

        Try
            ' Assume they are not equal
            Me.areEqual = False
            Me.areEqual = sqlCompare.Start(Me.paramUrn1.ToString(), Me.paramUrn2.ToString())

            reportDelegate(Nothing)
        Catch ex As ApplicationException
            reportDelegate(ex)
        Catch ex As SmoException
            reportDelegate(ex)
        End Try
    End Sub

    Private Sub InitializeSqlCompare()
        ' Server
        Me.sqlCompare.Server1 = serverTextBox1.Text
        Me.sqlCompare.Server2 = serverTextBox2.Text

        ' Login
        Me.sqlCompare.Login1 = loginTextBox1.Text
        Me.sqlCompare.Login2 = loginTextBox2.Text

        ' Password
        Me.sqlCompare.Password1 = passwordTextBox1.Text
        Me.sqlCompare.Password2 = passwordTextBox2.Text
    End Sub

    Private Function ValidateFields() As Boolean
        ' Server connection
        If Me.serverConn1 Is Nothing Then
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.SelectObject1Urn
            emb.Show(Me)

            Return False
        End If

        If Me.serverConn2 Is Nothing Then
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.SelectObject2Urn
            emb.Show(Me)

            Return False
        End If

        ' Urn
        If urnTextBox1.Text.Length = 0 OrElse urnTextBox2.Text.Length = 0 Then
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.Object1AndObject2NotSet
            emb.Show(Me)

            Return False
        End If

        If urnTextBox1.Text Is Nothing OrElse urnTextBox2.Text Is Nothing Then
            Dim emb As New ExceptionMessageBox()
            emb.Text = My.Resources.Object1AndObject2NotSet
            emb.Show(Me)

            Return False
        End If

        ' Server
        If serverTextBox1.Text Is Nothing OrElse serverTextBox1.Text.Length = 0 Then
            serverTextBox1.Text = defaultServer
        End If

        If serverTextBox2.Text Is Nothing OrElse serverTextBox2.Text.Length = 0 Then
            serverTextBox2.Text = defaultServer
        End If

        ' Password
        If passwordTextBox1.Text Is Nothing Then
            passwordTextBox1.Text = String.Empty
        End If

        If passwordTextBox2.Text Is Nothing Then
            passwordTextBox2.Text = String.Empty
        End If

        ' Login
        If loginTextBox1.Text Is Nothing Then
            loginTextBox1.Text = String.Empty
        End If

        If loginTextBox2.Text Is Nothing Then
            loginTextBox2.Text = String.Empty
        End If

        Return True
    End Function

    Private Function GetScriptToMakeIdenticalObject1() As StringCollection
        Dim collScript As New StringCollection()
        Dim server1 As New Server(Me.serverConn1)
        Dim server2 As New Server(Me.serverConn2)
        Dim object1 As SqlSmoObject = server1.GetSmoObject(urnTextBox1.Text)
        Dim object2 As SqlSmoObject = server2.GetSmoObject(urnTextBox2.Text)
        Dim database1 As Database = TryCast(object1, Database)
        Dim database2 As Database = TryCast(object2, Database)

        server1.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql

        object1.Refresh()
        object2.Refresh()

        For Each p As [Property] In object2.Properties
            Try
                If p.Writable _
                    AndAlso Not (object2.Properties(p.Name).Value Is Nothing) _
                    AndAlso Not object1.Properties(p.Name).Value.Equals(object2.Properties(p.Name).Value) Then
                    object1.Properties(p.Name).Value = object2.Properties(p.Name).Value
                End If
            Catch ex As PropertyCannotBeRetrievedException
                ' Ignore properties that can't be accessed
            End Try
        Next

        If Not (database1 Is Nothing) AndAlso Not (database2 Is Nothing) Then
            For Each p As [Property] In database1.DatabaseOptions.Properties
                ' If the props differ in value update the destination database
                If Not p.IsNull _
                    AndAlso Not database1.DatabaseOptions.Properties(p.Name).IsNull _
                    AndAlso database1.DatabaseOptions.Properties(p.Name).Writable Then
                    If Not database1.DatabaseOptions.Properties(p.Name).Value.Equals(database2.DatabaseOptions.Properties(p.Name).Value) Then
                        database1.DatabaseOptions.Properties(p.Name).Value = database2.DatabaseOptions.Properties(p.Name).Value
                    End If
                End If
            Next
        End If

        server1.ConnectionContext.CapturedSql.Clear()
        server2.ConnectionContext.CapturedSql.Clear()

        Try
            Dim alterableObject1 As IAlterable = TryCast(object1, IAlterable)
            If Not (alterableObject1 Is Nothing) Then
                alterableObject1.Alter()
                collScript = server1.ConnectionContext.CapturedSql.Text
            End If
        Catch ex As FailedOperationException
            'Ignore
        End Try

        server1.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteSql
        If collScript.Count = 0 Then
            collScript.Add(My.Resources.SelectedObjectsIdentical)
        End If

        Return collScript
    End Function

    Private Function GetScriptToMakeIdenticalObject2() As StringCollection
        Dim collScript As New StringCollection()
        Dim server1 As New Server(Me.serverConn2)
        Dim server2 As New Server(Me.serverConn1)
        Dim object1 As SqlSmoObject = server1.GetSmoObject(Me.urnTextBox2.Text)
        Dim object2 As SqlSmoObject = server2.GetSmoObject(Me.urnTextBox1.Text)
        Dim database1 As Database = TryCast(object1, Database)
        Dim database2 As Database = TryCast(object2, Database)

        server1.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql

        object1.Refresh()
        object2.Refresh()

        For Each p As [Property] In object2.Properties
            Try
                If p.Writable _
                    AndAlso Not (object2.Properties(p.Name).Value Is Nothing) _
                    AndAlso Not object1.Properties(p.Name).Value.Equals(object2.Properties(p.Name).Value) Then
                    object1.Properties(p.Name).Value = object2.Properties(p.Name).Value
                End If
            Catch ex As PropertyCannotBeRetrievedException
                ' Ignore properties that can't be accessed
            End Try
        Next

        If Not (database1 Is Nothing) AndAlso Not (database2 Is Nothing) Then
            For Each p As [Property] In database1.DatabaseOptions.Properties
                ' If the props differ in value update the destination database
                If Not p.IsNull _
                    AndAlso Not database1.DatabaseOptions.Properties(p.Name).IsNull _
                    AndAlso database1.DatabaseOptions.Properties(p.Name).Writable Then
                    If Not database1.DatabaseOptions.Properties(p.Name).Value.Equals(database2.DatabaseOptions.Properties(p.Name).Value) Then
                        database1.DatabaseOptions.Properties(p.Name).Value = database2.DatabaseOptions.Properties(p.Name).Value
                    End If
                End If
            Next
        End If

        server1.ConnectionContext.CapturedSql.Clear()
        server2.ConnectionContext.CapturedSql.Clear()

        Dim alterableObject1 As IAlterable = TryCast(object1, IAlterable)
        If Not (alterableObject1 Is Nothing) Then
            Try
                alterableObject1.Alter()
                collScript = server1.ConnectionContext.CapturedSql.Text
            Catch ex As SmoException
                Dim emb As New ExceptionMessageBox(ex)
                emb.Show(Me)
            End Try
        End If

        server1.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteSql
        If collScript.Count = 0 Then
            collScript.Add(My.Resources.SelectedObjectsIdentical)
        End If

        Return collScript
    End Function
End Class