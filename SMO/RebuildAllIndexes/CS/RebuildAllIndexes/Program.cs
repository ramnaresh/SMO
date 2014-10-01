/*============================================================================
  File:    Program.cs 

  Summary: Implements a sample SMO SQL Server rebuild all indexes utility in C#.

  Date:    August 29, 2005
------------------------------------------------------------------------------
  This file is part of the Microsoft SQL Server Code Samples.

  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Microsoft.Samples.SqlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server srvr;
            Database db;

            if (args.Length == 0)
            {
                Console.WriteLine(Properties.Resources.SpecifyDatabase);
            }
            else
            {
                srvr = new Server();
                if (srvr.Databases.Contains(args[0]))
                {
                    db = srvr.Databases[args[0]];

                    Console.WriteLine(Properties.Resources.RebuildingIndexes, args[0]);
                    foreach (Table tbl in db.Tables)
                    {
                        if (tbl.IsSystemObject == false)
                        {
                            Console.WriteLine(Properties.Resources.Rebuilding + tbl.ToString());
                            tbl.RebuildIndexes(90);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(Properties.Resources.DatabaseNotFound, args[0]);
                }
            }

            Console.WriteLine();
            Console.WriteLine(Properties.Resources.Completed);
            Console.ReadLine();
        }
    }
}
