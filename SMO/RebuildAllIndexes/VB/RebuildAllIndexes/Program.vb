'============================================================================
'  File:    Program.vb 
'
'  Summary: Implements a sample SMO SQL Server rebuild all indexes utility in VB.NET.
'
'  Date:    August 29, 2005
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
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo

Class Program
    Shared Sub Main(ByVal args() As String)
        Dim srvr As Server
        Dim db As Database

        If args.Length = 0 Then
            Console.WriteLine(My.Resources.SpecifyDatabase)
        Else
            srvr = New Server()
            If srvr.Databases.Contains(args(0)) Then
                db = srvr.Databases(args(0))

                Console.WriteLine(My.Resources.RebuildingIndexes, args(0))
                Dim tbl As Table
                For Each tbl In db.Tables
                    If tbl.IsSystemObject = False Then
                        Console.WriteLine(My.Resources.Rebuilding & tbl.ToString())
                        tbl.RebuildIndexes(90)
                    End If
                Next tbl
            Else
                Console.WriteLine(My.Resources.DatabaseNotFound, args(0))
            End If
        End If

        Console.WriteLine()
        Console.WriteLine(My.Resources.Completed)
        Console.ReadLine()
    End Sub
End Class