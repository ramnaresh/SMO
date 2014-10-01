'=====================================================================
'
'  File:     SmoEvents.vb
'  Summary:  Monitor login attempts and database create and drop
'  Date:     February 17, 2005
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
'======================================================= 

#Region "Using directives"

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports System.Threading

#End Region

Class SmoEvents
    Shared Sub Main()
        ' Default server is the local machine
        Dim server As New Server()

        AddHandler server.Events.ServerEvent, AddressOf Events_ServerEvent

        ' Trace events
        server.Events.SubscribeToEvents(ServerTraceEvent.AuditLogin + ServerTraceEvent.AuditLogout + ServerTraceEvent.AuditLoginFailed)

        ' DDL events
        server.Events.SubscribeToEvents(ServerEvent.CreateDatabase + ServerEvent.DropDatabase)

        Console.WriteLine("Starting events for server: {0}", server.Name)
        Console.WriteLine()
        Console.WriteLine("(Hit Ctrl-C or Ctrl-Break to quit.)")
        Console.WriteLine("Press any key to continue.")
        Console.ReadKey()

        ' Start receiving events
        server.Events.StartEvents()

        ' Wait indefinitely for events to occur
        Thread.Sleep(Timeout.Infinite)

        ' Unsubscribe from all the events when finished
        server.Events.UnsubscribeAllEvents()
    End Sub

    Shared Sub Events_ServerEvent(ByVal sender As Object, ByVal e As ServerEventArgs)
        Try
            Console.WriteLine("EventType: {0, -20} SPID: {1, 4} PostTime: {2, -20}", e.EventType, e.Spid, e.PostTime)
            Dim eventProperty As EventProperty
            For Each eventProperty In e.Properties
                If Not (eventProperty.Value Is Nothing) Then
                    Console.WriteLine(vbTab + "{0, -30} {1, -30}", eventProperty.Name, eventProperty.Value.ToString())
                Else
                    Console.WriteLine(vbTab + "{0,-30}", eventProperty.Name)
                End If
            Next eventProperty

            Console.WriteLine()
            Console.WriteLine(New String("="c, 79))
            Console.WriteLine()
        Catch ex As ApplicationException
            Console.WriteLine(ex.ToString())
        End Try
    End Sub
End Class
