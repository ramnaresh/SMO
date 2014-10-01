/*============================================================================
  File:    Program.cs 

  Summary: Implements a sample SMO SQL Server check identity values utility in C#.

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
#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

#endregion

namespace Microsoft.Samples.SqlServer
{
    class Program
    {
        static void Main()
        {
            Server server;
            System.Collections.Specialized.StringCollection checkIdentityStringCollection;
            bool HasIdentity;

            try
            {
                server = new Server();
                server.ConnectionContext.Connect();

                Console.WriteLine(DateTime.Now.ToString(Properties.Resources.TimeFormat,
                    System.Globalization.CultureInfo.CurrentCulture)
                    + Properties.Resources.Starting);

                // Limit the database properties returned to just those that we use
                server.SetDefaultInitFields(typeof(Database),
                    new String[] { "Name", "IsSystemObject", "IsAccessible" });

                server.SetDefaultInitFields(typeof(Table),
                    new String[] { "Schema", "Name" });

                server.SetDefaultInitFields(typeof(Column),
                    new String[] { "Name", "Identity" });

                foreach (Database db in server.Databases)
                {
                    if (db.IsSystemObject == false && db.IsAccessible == true)
                    {
                        Console.WriteLine(Environment.NewLine
                            + DateTime.Now.ToString(Properties.Resources.TimeFormat,
                            System.Globalization.CultureInfo.CurrentCulture)
                            + Properties.Resources.Database + db.Name);

                        foreach (Table tbl in db.Tables)
                        {
                            if (db.IsSystemObject == false)
                            {
                                // Assume table has no identity column
                                HasIdentity = false;

                                foreach (Column col in tbl.Columns)
                                {
                                    if (col.Identity == true)
                                    {
                                        HasIdentity = true;
                                        break;
                                    }
                                }

                                if (HasIdentity == true)
                                {
                                    Console.WriteLine(DateTime.Now.ToString(Properties.Resources.TimeFormat,
                                        System.Globalization.CultureInfo.CurrentCulture)
                                        + Properties.Resources.Table + tbl.ToString());

                                    try
                                    {
                                        checkIdentityStringCollection = tbl.CheckIdentityValue();
                                        foreach (string str in checkIdentityStringCollection)
                                        {
                                            Console.WriteLine(str);
                                        }
                                    }
                                    catch (FailedOperationException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    finally
                                    {
                                        Console.WriteLine(Environment.NewLine);
                                    }
                                }
                            }
                        }
                    }
                }

                Console.WriteLine(Environment.NewLine
                    + DateTime.Now.ToString(Properties.Resources.TimeFormat,
                    System.Globalization.CultureInfo.CurrentCulture)
                    + Properties.Resources.Ending);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine(Properties.Resources.Completed);
                Console.ReadLine();
            }
        }
    }
}
