'============================================================================
'  File:    Program.vb
'
'  Summary: Implements a sample SMO SQL Server check identity values utility in VB.NET.
'
'  Date:    September 09, 2005
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
#Region "Using directives"

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo

#End Region

Class Program

    Shared Sub Main()
        Dim server As Server
        Dim checkIdentityStringCollection As System.Collections.Specialized.StringCollection

        Try
            server = New Server()
            server.ConnectionContext.Connect()

            Console.WriteLine(DateTime.Now.ToString(My.Resources.TimeFormat, _
                System.Globalization.CultureInfo.InvariantCulture) _
                & My.Resources.Starting)

            ' Limit the database properties returned to just those that we use
            server.SetDefaultInitFields(GetType(Database), _
                New String() {"Name", "IsSystemObject", "IsAccessible"})

            server.SetDefaultInitFields(GetType(Table), New String() {"Name"})

            server.ConnectionContext.Connect()

            For Each db As Database In server.Databases
                If db.IsSystemObject = False AndAlso db.IsAccessible = True Then
                    Console.WriteLine(Environment.NewLine _
                        & DateTime.Now.ToString(My.Resources.TimeFormat, _
                        System.Globalization.CultureInfo.InvariantCulture) _
                        & My.Resources.Database & db.Name)

                    For Each tbl As Table In db.Tables
                        If tbl.IsSystemObject = False Then
                            Console.WriteLine(DateTime.Now.ToString(My.Resources.TimeFormat, _
                                System.Globalization.CultureInfo.InvariantCulture) _
                                & My.Resources.Table & tbl.ToString())

                            Try
                                checkIdentityStringCollection = tbl.CheckIdentityValue()
                                For Each str As String In checkIdentityStringCollection
                                    Console.WriteLine(str)
                                Next
                            Catch ex As FailedOperationException
                                Console.WriteLine(ex.Message)
                            Finally
                                Console.WriteLine(Environment.NewLine)
                            End Try
                        End If
                    Next
                End If
            Next

            Console.WriteLine(Environment.NewLine _
                & DateTime.Now.ToString(My.Resources.TimeFormat, System.Globalization.CultureInfo.InvariantCulture) _
                & My.Resources.Ending)
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            Console.WriteLine()
            Console.WriteLine("Completed. (Press Enter to continue.)")
            Console.ReadLine()
        End Try
    End Sub
End Class