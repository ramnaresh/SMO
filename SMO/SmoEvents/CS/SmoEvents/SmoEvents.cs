/*=====================================================================

  File:     SmoEvents.cs
  Summary:  Monitor login attempts and database create and drop
  Date:     February 17, 2005

---------------------------------------------------------------------
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
======================================================= */

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Threading;

#endregion

namespace Microsoft.Samples.SqlServer
{
    class SmoEvents
    {
        static void Main()
        {
            // Default server is the local machine
            Server server = new Server();

            server.Events.ServerEvent
                += new ServerEventHandler(Events_ServerEvent);

            // Trace events
            server.Events.SubscribeToEvents(
                ServerTraceEvent.AuditLogin
                + ServerTraceEvent.AuditLogout
                + ServerTraceEvent.AuditLoginFailed);

            // DDL events
            server.Events.SubscribeToEvents(ServerEvent.CreateDatabase
                + ServerEvent.DropDatabase);

            Console.WriteLine(@"Starting events for server: {0}", server.Name);
            Console.WriteLine();
            Console.WriteLine(@"(Hit Ctrl-C or Ctrl-Break to quit.)");
            Console.WriteLine(@"Press any key to continue.");
            Console.ReadKey();

            // Start receiving events
            server.Events.StartEvents();

            // Wait indefinitely for events to occur
            Thread.Sleep(Timeout.Infinite);

            // Unsubscribe from all the events when finished
            server.Events.UnsubscribeAllEvents();
        }

        static void Events_ServerEvent(object sender, ServerEventArgs e)
        {
            try
            {
                Console.WriteLine(
                    @"EventType: {0, -20} SPID: {1, 4} PostTime: {2, -20}",
                    e.EventType, e.Spid, e.PostTime);
                foreach (EventProperty eventProperty in e.Properties)
                {
                    if (eventProperty.Value != null)
                    {
                        Console.WriteLine("\t{0, -30} {1, -30}",
                            eventProperty.Name,
                            eventProperty.Value.ToString());
                    }
                    else
                    {
                        Console.WriteLine("\t{0,-30}", eventProperty.Name);
                    }
                }

                Console.WriteLine();
                Console.WriteLine(new String('=', 79));
                Console.WriteLine();
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
