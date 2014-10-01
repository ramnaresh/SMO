'============================================================================
'  File:    SqlService.vb 
'
'  Summary: Implements a sample SMO SQL Server service utility in VB.NET.
'
'  Date:    January 25, 2005
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

Public Class SqlService
    ' Use the Server object to connect to a specific server
    Private SqlServerSelection As Server

    Private Sub SqlService_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myServerConn As ServerConnection
        Dim scForm As ServerConnect
        Dim dr As DialogResult

        ' Display the main window first
        Me.Show()
        Application.DoEvents()

        myServerConn = New ServerConnection
        scForm = New ServerConnect(myServerConn)
        dr = scForm.ShowDialog(Me)
        If dr = Windows.Forms.DialogResult.OK _
            AndAlso myServerConn.SqlConnectionObject.State _
                = ConnectionState.Open Then
            SqlServerSelection = New Server(myServerConn)
            If Not (SqlServerSelection Is Nothing) Then
                Me.Text = My.Resources.AppTitle & SqlServerSelection.Name

                ' Refresh database list
                GetServices()
            End If
        Else
            Me.Close()
        End If

        UpdateButtons()
    End Sub

    Private Sub Services_SelectedIndexChangedListView(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServicesListView.SelectedIndexChanged
        UpdateButtons()
    End Sub

    Private Sub RefreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshButton.Click
        GetServices()
        UpdateButtons()
    End Sub

    Private Sub StartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartButton.Click
        ' Start a stopped service
        Dim csr As Cursor = Nothing
        Dim svc As Service

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            svc = GetSelectedService()
            If Not (svc Is Nothing) Then
                If svc.ServiceState = ServiceState.Stopped Then
                    sbrStatus.Text = My.Resources.Starting
                    sbrStatus.Refresh()

                    svc.Start()
                End If

                ' Wait for service state to change
                WaitServiceStateChange(svc, ServiceState.Running)

                RefreshService(svc)

                sbrStatus.Text = My.Resources.Ready
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub StopButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopButton.Click
        ' Stop a running service
        Dim csr As Cursor = Nothing
        Dim svc As Service

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            svc = GetSelectedService()
            If Not (svc Is Nothing) Then
                If svc.ServiceState = ServiceState.Running Then
                    sbrStatus.Text = My.Resources.Stopping
                    sbrStatus.Refresh()

                    svc.Stop()
                End If

                ' Wait for service state to change
                WaitServiceStateChange(svc, ServiceState.Stopped)

                RefreshService(svc)

                sbrStatus.Text = My.Resources.Ready
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub PauseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseButton.Click
        ' Pause a running service
        Dim csr As Cursor = Nothing
        Dim svc As Service

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            svc = GetSelectedService()
            If Not (svc Is Nothing) Then
                If svc.ServiceState = ServiceState.Running Then
                    sbrStatus.Text = My.Resources.Pausing
                    sbrStatus.Refresh()

                    svc.Pause()
                End If

                ' Wait for service state to change
                WaitServiceStateChange(svc, ServiceState.Paused)

                RefreshService(svc)

                sbrStatus.Text = My.Resources.Ready
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub ResumeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResumeButton.Click
        ' Resume a paused service
        Dim csr As Cursor = Nothing
        Dim svc As Service

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor
            svc = GetSelectedService()
            If Not (svc Is Nothing) Then
                If svc.ServiceState = ServiceState.Paused Then
                    sbrStatus.Text = My.Resources.Resuming
                    sbrStatus.Refresh()

                    svc.Resume()
                End If

                ' Wait for service state to change
                WaitServiceStateChange(svc, ServiceState.Running)

                RefreshService(svc)

                sbrStatus.Text = My.Resources.Ready
            End If
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub WaitServiceStateChange(ByVal svc As Service, ByVal ss As ServiceState)
        Dim StopTime As DateTime

        StopTime = DateTime.Now.AddSeconds(CType(TimeoutUpDown.Value, Double))

        While ((svc.ServiceState <> ss) And (DateTime.Now < StopTime))
            System.Diagnostics.Debug.WriteLine(svc.ServiceState.ToString())
            System.Diagnostics.Debug.WriteLine(StopTime.ToLongTimeString())
            System.Threading.Thread.Sleep(250)
            svc.Refresh()
        End While
    End Sub

    Private Sub GetServices()
        Dim csr As Cursor = Nothing
        Dim mc As ManagedComputer
        Dim ServiceListViewItem As ListViewItem

        Try
            csr = Me.Cursor ' Save the old cursor
            Me.Cursor = Cursors.WaitCursor ' Display the waiting cursor

            ' Clear control
            ServicesListView.Items.Clear()
            mc = New ManagedComputer(SqlServerSelection.Name)
            For Each svc As Service In mc.Services
                ServiceListViewItem = ServicesListView.Items.Add(svc.Name)
                ServiceListViewItem.SubItems.Add(svc.DisplayName)
                ServiceListViewItem.SubItems.Add(svc.ServiceState.ToString())
                ServiceListViewItem.SubItems.Add(svc.StartMode.ToString())
                ServiceListViewItem.SubItems.Add(svc.ServiceAccount)
                ServiceListViewItem.SubItems.Add(svc.State.ToString())
            Next
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        Finally
            Me.Cursor = csr ' Restore the original cursor
        End Try
    End Sub

    Private Sub RefreshService(ByVal svc As Service)
        ' Update the service status field
        Try
            svc.Refresh()

            Dim ServiceListViewItem As ListViewItem

            ServiceListViewItem = ServicesListView.SelectedItems(0)
            ServiceListViewItem.SubItems(2).Text = svc.ServiceState.ToString()

            UpdateButtons()
        Catch ex As SmoException
            Dim emb As New ExceptionMessageBox(ex)
            emb.Show(Me)
        End Try
    End Sub

    Private Function GetSelectedService() As Service
        ' Return the service selected in the list
        Dim mc As ManagedComputer

        If ServicesListView.SelectedItems.Count > 0 Then
            mc = New ManagedComputer(SqlServerSelection.Name)
            Return mc.Services(ServicesListView.SelectedItems(0).Text)
        Else
            Return Nothing
        End If
    End Function

    Private Sub UpdateButtons()
        ' Update all buttons based on the service state
        Dim svc As Service = GetSelectedService()
        If Not (svc Is Nothing) Then
            PauseButton.Enabled = svc.ServiceState = ServiceState.Running _
                AndAlso svc.AcceptsPause
            ResumeButton.Enabled = svc.ServiceState = ServiceState.Paused
            StartButton.Enabled = svc.ServiceState = ServiceState.Stopped
            StopButton.Enabled = svc.ServiceState = ServiceState.Running _
                AndAlso svc.AcceptsStop
        Else
            PauseButton.Enabled = False
            ResumeButton.Enabled = False
            StartButton.Enabled = False
            StopButton.Enabled = False
        End If
    End Sub
End Class
