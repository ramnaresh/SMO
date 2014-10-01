/*============================================================================
  File:    TDETestApplication.cs 

  Summary: Implements a sample code demonstrate TDE feature using SMO in C#.

  Date:    June 25, 2008
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
using System.Data;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
#endregion

namespace TDETestApplication
{
    class SmoTdeSampleTest
    {
        static void Main()
        {

            String serverName = @".";
            String dbName = @"SmoTdeSampleDB";
            String certificateName = @"SmoTdeSampleCert";
                        
            //ScriptingOptions to be used globally
            ScriptingOptions so = new ScriptingOptions();
            so.IncludeHeaders = true;
            so.IncludeIfNotExists = true;


            //Getting the Server Object
            Server server = new Server(serverName);

            //Creating new Database
            Database db = new Database(server, dbName);
            db.Create();

            Database masterDb = server.Databases["Master"];
         
            //Creating certificate 
            Certificate certificate = new Certificate(masterDb, certificateName);
            certificate.StartDate = DateTime.Today;
            certificate.Subject = "My certificate";
            certificate.Create();
                       
            try
            {
                //Create Database Encryption 
                DatabaseEncryptionKey dbEk = new DatabaseEncryptionKey();
                dbEk.Parent = db;
                dbEk.EncryptionAlgorithm = DatabaseEncryptionAlgorithm.Aes256;
                dbEk.EncryptionType = DatabaseEncryptionType.ServerCertificate;
                dbEk.EncryptorName = certificateName;

                Console.WriteLine("Scripting Database Encryption Key Status \n");
                StringCollection sc = dbEk.Script(so);

                Console.WriteLine("T-SQL for create\n");
                foreach (string s in sc)
                {
                    Console.WriteLine(s);
                }
                dbEk.Create();

                //Changing the execution mode to Execute and Capture Sql
                //This is just to capture T-SQL , can skip this if wanted.
                server.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteAndCaptureSql;

                //Altering EncryptionKey
                db.DatabaseEncryptionKey.EncryptionAlgorithm = DatabaseEncryptionAlgorithm.TripleDes;
                db.DatabaseEncryptionKey.EncryptionType = DatabaseEncryptionType.ServerCertificate;
                db.DatabaseEncryptionKey.EncryptorName = certificateName;
                db.DatabaseEncryptionKey.Alter();

                //Showing the TSQL generated
                Console.WriteLine("\nT-SQL for Alter\n");
                foreach (string s in server.ConnectionContext.CapturedSql.Text)
                {
                    Console.WriteLine(s);
                }

                //Clearing the already stored TSQL
                server.ConnectionContext.CapturedSql.Clear();

                //Changing the execution mode back to normal
                server.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteSql;

                //ALtering the EncryptionAlgorithm directly
                db.DatabaseEncryptionKey.Regenerate(DatabaseEncryptionAlgorithm.Aes256);
                                
                
            }
            finally
            {
                db.Drop();
                masterDb.Certificates[certificateName].Drop();
             }

        }
    }
}
