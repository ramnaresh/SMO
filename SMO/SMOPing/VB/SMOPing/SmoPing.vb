'============================================================================
'  File:    SMOPing.vb 
'
'  Summary: Implements a sample SMO SQL server ping utility in VB.NET.
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

' ************************************************************************
' SMOPing.vb
' 
' The purpose of this tool is to test the ability to instantiate an SMO object
' and connect to the server indicated. Check which version of the server is 
' running and use the appropriate object model based on the SQL Server version.
'
' Usage:
' SMOPing [-S] [-U -P | -E] [-T] [-V] [-v] [-?]
'        -S server name, defaults to local server
'        -U user name, if it is not provided then use Windows Authentication for the connection
'        -P password, defaults to "" (blank)
'        -E integrated security, defaults to false
'        -T connection timeout (seconds)
'        -V verbose mode, defaults to false
'        -v display version, defaults to false
'        -? help and usage information
' ************************************************************************
Module SmoPing
    ' Class variables
    Private ServerValue As String = "."
    Private UserValue As String = String.Empty
    Private PasswordValue As String = String.Empty
    Private IntegratedSecurityFlag As [Boolean]
    Private VerboseFlag As [Boolean]
    Private VersionFlag As [Boolean]
    Private HelpFlag As [Boolean]
    Private ServerConn As ServerConnection
    Private TimeoutValue As Integer = 5

    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Main(ByVal args As String()) As Integer
        Dim iExitStatus As Integer = 1 ' Assume failure.

        Try
            ' Get this machine name
            ServerValue = Environment.MachineName

            If Parse(args) = True Then
                If HelpFlag = False Then
                    Ping()
                    iExitStatus = 0   ' Success
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(String.Format( _
                System.Globalization.CultureInfo.InvariantCulture, _
                My.Resources.ErrorMessage, ex.Message))
        Finally
#If DEBUG Then
            Console.WriteLine()
            Console.WriteLine(My.Resources.PressEnter)

            Console.ReadLine()
#End If
        End Try

        Return iExitStatus
    End Function

    ''' <summary>
    ''' Simple command argument parser
    ''' </summary>

    Function Parse(ByVal args() As String) As [Boolean]
        Dim OkFlag As [Boolean] = True ' Assume everything is OK
        Dim Arguments As String

        For Each Arguments In args
            Select Case Arguments.Substring(0, 2)
                Case "-S" ' Server
                    ServerValue = Arguments.Substring(2)

                Case "-U" ' User Login ID - can be overriden with -E
                    UserValue = Arguments.Substring(2)

                Case "-P" ' Password
                    PasswordValue = Arguments.Substring(2)

                Case "-E" ' Integrated security - overrides -U and -P
                    IntegratedSecurityFlag = True

                Case "-T" ' Timeout
                    TimeoutValue = Convert.ToInt32(Arguments.Substring(2), _
                        NumberFormatInfo.InvariantInfo)

                Case "-V" ' Verbose mode
                    VerboseFlag = True

                Case "-v" ' SQL Server Version
                    VersionFlag = True

                Case "-?" ' Help
                    Console.WriteLine(my.Resources.Help)
                    HelpFlag = True

                Case Else
                    Console.WriteLine(My.Resources.ArgumentError, Arguments)
                    OkFlag = False
            End Select
        Next

        Return OkFlag
    End Function

    ' ************************************************************************
    ' Ping - instantiate an SMO object connect to the server and retrieve 
    '      the server information
    ' ************************************************************************
    Function Ping() As [Boolean]
        Dim ReturnValue As [Boolean] = False

        Try
            ' If there is no input for the user ID, 
            ' use Windows Authentication for the connection.
            If UserValue.Length = 0 Then
                IntegratedSecurityFlag = True
            End If

            ServerConn = New ServerConnection()

            ' Fill in necessary information
            ServerConn.ServerInstance = ServerValue
            ServerConn.ConnectTimeout = TimeoutValue
            If IntegratedSecurityFlag = True Then
                ' Use Windows authentication
                ServerConn.LoginSecure = True
                Console.WriteLine(My.Resources.IntegratedSecurity)
            Else
                ' Use SQL Server authentication
                ServerConn.LoginSecure = False
                ServerConn.Login = UserValue
                ServerConn.Password = PasswordValue
                Console.WriteLine(My.Resources.SQLSecurity)
            End If

            ServerConn.Connect()

            ' Write the server name and user name values to the console
            Console.WriteLine(My.Resources.SqlServerName, _
                ServerConn.TrueName)
            Console.WriteLine(My.Resources.SqlServerLogin, _
                ServerConn.TrueLogin)

            If VersionFlag = True Then
                ' Write the server version to the console
                Console.WriteLine(My.Resources.SqlServerVersion, _
                    ServerConn.ServerVersion.Major, _
                    ServerConn.ServerVersion.Minor, _
                    ServerConn.ServerVersion.BuildNumber)
            End If

            If VerboseFlag = True Then
                ' Write the server connection property values to the console
                Console.WriteLine(My.Resources.ExecutionMode, _
                    ServerConn.SqlExecutionModes)
                Console.WriteLine(My.Resources.MaxPoolSize, _
                    ServerConn.MaxPoolSize)
                Console.WriteLine(My.Resources.MinPoolSize, _
                    ServerConn.MinPoolSize)
                Console.WriteLine(My.Resources.NetworkProtocol, _
                    ServerConn.NetworkProtocol)
                Console.WriteLine(My.Resources.PacketSize, _
                    ServerConn.PacketSize)
                Console.WriteLine(My.Resources.PooledConnectionLifetime, _
                    ServerConn.PooledConnectionLifetime)
                Console.WriteLine(My.Resources.StatementTimeout, _
                    ServerConn.StatementTimeout)
                Console.WriteLine(My.Resources.ConnectTimeout, _
                    ServerConn.ConnectTimeout)
                Console.WriteLine(My.Resources.ConnectionString, _
                    ServerConn.ConnectionString)
            End If

            ReturnValue = True
        Catch ex As ConnectionFailureException
            Console.WriteLine(ex.ToString())

            Return ReturnValue
        Catch ex As SmoException
            Console.WriteLine(ex.ToString())

            Return ReturnValue
        Finally
            ' Close the server connection
            If Not (ServerConn Is Nothing) Then
                If ServerConn.SqlConnectionObject.State _
                    = ConnectionState.Open Then
                    ServerConn.Disconnect()
                End If
            End If
        End Try

        Return ReturnValue
    End Function
End Module
