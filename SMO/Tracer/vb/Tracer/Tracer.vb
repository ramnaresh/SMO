'=====================================================================
'  File:    Tracer.vb
'
'  Summary: Implements a sample SMO SQL Server trace utility in VB.NET.
'
'  Date:    June 13, 2005
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

Module Tracer
    ''' <summary>
    ''' Starts and executes the main program.
    ''' </summary>
    Sub Main()
        Console.Title = String.Format(System.Globalization.CultureInfo.InvariantCulture, _
            My.Resources.PressEsc, [Assembly].GetExecutingAssembly().GetModules()(0).Name)

        SQLTraceLiveReader()
    End Sub

    ''' <summary>
    ''' Configure the trace to run against the local SQL Server.
    ''' </summary>
    Private Sub SQLTraceLiveReader()
        Dim traceServerReader As TraceServer
        Dim sci As SqlConnectionInfo
        Dim traceConfigFileName As String
        Dim programFilesPath As String

        ' Setup connection to the SQL Server
        traceServerReader = New TraceServer()

        ' Use the local SQL Server
        sci = New SqlConnectionInfo()
        sci.UseIntegratedSecurity = True

        ' Test for SQL Express
        Dim srvr As New Server(sci.ServerName)
        If srvr.Information.Edition <> "Express Edition" Then
            ' Configure the reader
            ' Use the Standard profiler configuration
            traceConfigFileName = My.Settings.TraceConfigFile
            programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            traceServerReader.InitializeAsReader(sci, programFilesPath & traceConfigFileName)

            ' Start reading the trace information
            SQLTraceReader(traceServerReader)
        Else
            Console.WriteLine("SQL Express is not supported for tracing")
        End If
    End Sub

    ''' <summary>
    ''' Read and display the trace information.
    ''' </summary>
    ''' <param name="dataReader"></param>
    Private Sub SQLTraceReader(ByVal dataReader As IDataReader)
        Dim nEventNum As Integer = 0
        Dim value As Object
        Dim valueString As String
        Dim cki As ConsoleKeyInfo
        Dim separator As New String("-"c, 79)

        Try
            While dataReader.Read()
                ' Write the event number
                Console.Write("{0}" + vbTab, nEventNum)

                ' Write each fields name and data type
                For i As Integer = 0 To dataReader.FieldCount - 1
                    Console.Write("{0}({1})" + vbTab, dataReader.GetName(i), _
                        dataReader.GetDataTypeName(i))
                Next

                Console.WriteLine()

                ' Write separator line
                Console.WriteLine(separator)

                ' Write each fields value
                For counter As Integer = 0 To dataReader.FieldCount - 1
                    value = dataReader.GetValue(counter)
                    If Not (value Is Nothing) Then
                        valueString = value.ToString()
                    Else
                        valueString = "<NULL>"
                    End If

                    Console.Write(valueString)
                    Console.Write(vbTab)
                Next

                Console.WriteLine()

                nEventNum += 1

                ' Watch for the user to press the ESC key and terminate while loop
                If Console.KeyAvailable = True Then
                    cki = Console.ReadKey(True)
                    If cki.Key = ConsoleKey.Escape Then
                        Exit While
                    End If
                End If
            End While
        Finally
            ' Close the reader
            dataReader.Close()
        End Try
    End Sub
End Module
