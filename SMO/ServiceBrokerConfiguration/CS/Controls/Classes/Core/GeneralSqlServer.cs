/*============================================================================
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.ComponentModel;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Broker;
using Microsoft.SqlServer.Management.Common;
using System.Globalization;
using System.Collections;

namespace Microsoft.Samples.SqlServer
{
    //Utility class for some general SQL members
    class GeneralSqlServer
    {
        private string m_DatabaseName;
        private Server m_Server;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.ApplicationException.#ctor(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public void Connect(string sqlInstance)
        {
            Database database;
            if (String.IsNullOrEmpty(this.DatabaseName))
            {
                throw new ApplicationException("DatabaseName Is Null");
            }
            else
            {
                try
                {
                    m_Server = new Server(sqlInstance);
                    
                    m_Server.ConnectionContext.LoginSecure = true;

                    if (m_Server.Databases[this.DatabaseName] == null)
                    {
                        // Instantiate a new database object
                        database = new Database(m_Server, this.DatabaseName);

                        // Create the database as defined.
                        database.Create();
                    }

                    m_Server.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteAndCaptureSql;
                }
                catch (Exception)
                {
                    //Handle a more specific exception in a production application
                    throw new ApplicationException("Connection Exception");
                }
            }
        }

        public GeneralSqlServer(string databaseName)
        {
            m_DatabaseName = databaseName;
        }

        public Server Server
        {
            get
            {
                return m_Server;
            }
        }


        public ServiceBroker ServiceBroker
        {
            get
            {
                return m_Server.Databases[this.DatabaseName].ServiceBroker;
            }
        }

        public string DatabaseName
        {
            get
            {
                return m_DatabaseName;
            }
            set
            {
                m_DatabaseName = value;
            }
        }

        
    }
}
