/*============================================================================
  File:    SMOPing.cs 

  Summary: Implements a sample SMO SQL server ping utility in C#.

  Date:    January 25, 2005
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
using System.Globalization;
using System.Data;

// SMO
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;

#endregion

// ************************************************************************
// SMOPing.cs
// 
// The purpose of this tool is to test the ability to instantiate an SMO object
// and connect to the server indicated. Check which version of the server is 
// running and use the appropriate object model based on the SQL Server version.
//
// Usage:
// SMOPing [-S] [-U -P | -E] [-V] [-v] [-?]
//        -S server name, defaults to local server
//        -U user name, if it is not provided then use Windows Authentication for the connection
//        -P password, defaults to "" (blank)
//        -E integrated security, defaults to false
//        -T connection timeout (seconds)
//        -V verbose mode, defaults to false
//        -v display version, defaults to false
//        -? help and usage information
// ************************************************************************
namespace Microsoft.Samples.SqlServer
{
    /// <summary>
    /// Summary description for SMOPing.
    /// </summary>
    class SMOPing
    {
        // Class variables
        private static String ServerValue = @".";
        private static String UserValue = string.Empty;
        private static String PasswordValue = string.Empty;
        private static Boolean IntegratedSecurityFlag;
        private static Boolean VerboseFlag;
        private static Boolean VersionFlag;
        private static Boolean HelpFlag;
        private static ServerConnection ServerConn;
        private static Int32 TimeoutValue = 5;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")] 
        static int Main(string[] args)
        {
            Int32 ExitStatus;

            // Assume failure
            ExitStatus = 1;

            try
            {
                // Get this machine name
                ServerValue = Environment.MachineName;

                if (Parse(args) == true)
                {
                    if (HelpFlag == false)
                    {
                        Ping();
                    }
                    else
                    {
                        ExitStatus = 0;    // Success
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    Properties.Resources.Error, 
                    ex.Message));
            }
            finally
            {
#if DEBUG
                Console.WriteLine();
                Console.WriteLine(Properties.Resources.PressEnter);
                Console.ReadLine();
#endif
            }
            
            return (ExitStatus);
        }

        /// <summary>
        /// Simple command argument parser.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static Boolean Parse(String[] args)
        {
            Boolean OkFlag = true; // Assume everything is OK

            foreach (String Arguments in args)
            {
                switch (Arguments.Substring(0, 2))
                {
                    case @"-S": // Server
                        ServerValue = Arguments.Substring(2);
                        break;

                    case @"-U": // User Login ID - can be overriden with -E
                        UserValue = Arguments.Substring(2);
                        break;

                    case @"-P": // Password
                        PasswordValue = Arguments.Substring(2);
                        break;

                    case @"-E": // Integrated security - overrides -U and -P
                        IntegratedSecurityFlag = true;
                        break;

                    case @"-T": // Timeout
                        TimeoutValue = Convert.ToInt32(Arguments.Substring(2),
                            NumberFormatInfo.InvariantInfo);
                        break;

                    case @"-V": // Verbose mode
                        VerboseFlag = true;
                        break;

                    case @"-v": // SQL Server Version
                        VersionFlag = true;
                        break;

                    case @"-?": // Help
                        Console.WriteLine(Properties.Resources.Help);
                        HelpFlag = true;
                        break;

                    default:
                        Console.WriteLine(Properties.Resources.ArgumentError,
                            Arguments);
                        OkFlag = false;
                        break;
                }
            }

            return (OkFlag);
        }

        /// <summary>
        /// Instantiate an SMO object connect to the server and retrieve the server information
        /// </summary>
        /// <returns></returns>
        static Boolean Ping()
        {
            Boolean ReturnValue = false;

            try
            {
                // If there is no input for the user ID, use Windows 
                // Authentication for the connection.
                if (UserValue.Length == 0)
                {
                    IntegratedSecurityFlag = true;
                }

                ServerConn = new ServerConnection();

                // Fill in necessary information
                ServerConn.ServerInstance = ServerValue;
                ServerConn.ConnectTimeout = TimeoutValue;
                if (IntegratedSecurityFlag == true)
                {
                    // Use Windows authentication
                    ServerConn.LoginSecure = true;
                    Console.WriteLine(Properties.Resources.IntegratedSecurity);
                }
                else
                {
                    // Use SQL Server authentication
                    ServerConn.LoginSecure = false;
                    ServerConn.Login = UserValue;
                    ServerConn.Password = PasswordValue;
                    Console.WriteLine(Properties.Resources.SQLSecurity);
                }

                // Optional
                //ServerConn.Connect();

                // Write the server name and user name values to the console
                Console.WriteLine(Properties.Resources.SqlServerName, 
                    ServerConn.TrueName);
                Console.WriteLine(Properties.Resources.SqlServerLogin,
                    ServerConn.TrueLogin);

                if (VersionFlag == true)
                {
                    // Write the server version to the console
                    Console.WriteLine(Properties.Resources.SqlServerVersion,
                        ServerConn.ServerVersion.Major,
                        ServerConn.ServerVersion.Minor,
                        ServerConn.ServerVersion.BuildNumber);
                }

                if (VerboseFlag == true)
                {
                    // Write the server connection property values to the console
                    Console.WriteLine(Properties.Resources.ExecutionMode,
                        ServerConn.SqlExecutionModes.ToString());
                    Console.WriteLine(Properties.Resources.MaxPoolSize,
                        ServerConn.MaxPoolSize);
                    Console.WriteLine(Properties.Resources.MinPoolSize,
                        ServerConn.MinPoolSize);
                    Console.WriteLine(Properties.Resources.NetworkProtocol,
                        ServerConn.NetworkProtocol);
                    Console.WriteLine(Properties.Resources.PacketSize,
                        ServerConn.PacketSize);
                    Console.WriteLine(
                        Properties.Resources.PooledConnectionLifetime,
                        ServerConn.PooledConnectionLifetime);
                    Console.WriteLine(Properties.Resources.StatementTimeout,
                        ServerConn.StatementTimeout);
                    Console.WriteLine(Properties.Resources.ConnectTimeout,
                        ServerConn.ConnectTimeout);
                    Console.WriteLine(Properties.Resources.ConnectionString,
                        ServerConn.ConnectionString);
                }

                ReturnValue = true;
            }
            catch (ConnectionFailureException ex)
            {
                Console.WriteLine(ex.ToString());

                return (ReturnValue);
            }
            catch (SmoException ex)
            {
                Console.WriteLine(ex.ToString());

                return (ReturnValue);
            }
            finally
            {
                // Close the server connection
                if (ServerConn != null)
                {
                    if (ServerConn.SqlConnectionObject.State 
                        == ConnectionState.Open)
                    {
                        ServerConn.Disconnect();
                    }
                }
            }

            return (ReturnValue);
        }
    }
}
