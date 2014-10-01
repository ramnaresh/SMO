/*=====================================================================
  File:     Tracer.cs

  Summary:  Implements a sample SMO SQL Server trace utility in C#.

  Date:     June 13, 2005
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
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Data;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Trace;

#endregion

namespace Microsoft.Samples.SqlServer
{
    class Tracer
    {
        /// <summary>
        /// Starts and executes the main program.
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {
            Console.Title = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                Properties.Resources.PressEsc,
                Assembly.GetExecutingAssembly().GetModules()[0].Name);

            SQLTraceLiveReader();
        }

        /// <summary>
        /// Configure the trace to run against the local SQL Server.
        /// </summary>
        private static void SQLTraceLiveReader()
        {
            TraceServer traceServerReader;
            SqlConnectionInfo sci;
            string traceConfigFileName;
            string programFilesPath;

            // Setup connection to the SQL Server
            traceServerReader = new TraceServer();

            // Use the local SQL Server
            sci = new SqlConnectionInfo();
            sci.UseIntegratedSecurity = true;

            // Test for SQL Express
            Server srvr = new Server(sci.ServerName);
            if (srvr.Information.Edition != @"Express Edition")
            {
                // Configure the reader
                // Use the Standard profiler configuration
                traceConfigFileName = Properties.Settings.Default.TraceConfigFile;
                programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                traceServerReader.InitializeAsReader(sci, programFilesPath + traceConfigFileName);

                // Start reading the trace information
                SQLTraceReader(traceServerReader);
            }
            else
            {
                Console.WriteLine("SQL Express is not supported for tracing");
            }
        }

        /// <summary>
        /// Read and display the trace information.
        /// </summary>
        /// <param name="dataReader"></param>
        private static void SQLTraceReader(IDataReader dataReader)
        {
            int nEventNum = 0;
            object value;
            string valueString;
            ConsoleKeyInfo cki;
            string separator = new string('-', 79);

            try
            {
                while (dataReader.Read())
                {
                    // Write the event number
                    Console.Write("{0}\t", nEventNum);

                    // Write each fields name and data type
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        Console.Write("{0}({1})\t", dataReader.GetName(i),
                            dataReader.GetDataTypeName(i));
                    }

                    Console.WriteLine();

                    // Write separator line
                    Console.WriteLine(separator);

                    // Write each fields value
                    for (int counter = 0;
                        counter < dataReader.FieldCount; counter++)
                    {
                        value = dataReader.GetValue(counter);
                        if (value != null)
                        {
                            valueString = value.ToString();
                        }
                        else
                        {
                            valueString = "<NULL>";
                        }

                        Console.Write(valueString);
                        Console.Write("\t");
                    }

                    Console.WriteLine();

                    nEventNum++;

                    // Watch for the user to press the ESC key and terminate while loop
                    if (Console.KeyAvailable == true)
                    {
                        cki = Console.ReadKey(true);
                        if (cki.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                }
            }
            finally
            {
                // Close the reader
                dataReader.Close();
            }
        }
    }
}
