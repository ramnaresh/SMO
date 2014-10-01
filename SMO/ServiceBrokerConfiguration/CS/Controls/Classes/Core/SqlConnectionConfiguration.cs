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
using System.Collections;
using System.ComponentModel;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Broker;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace Microsoft.Samples.SqlServer
{
    class SqlConnectionConfiguration
    {
        private string m_ServerType  = "Database Engine";
        private string m_ServerName = "(local)";
        private SqlAuthenticationTypes m_Authentication = SqlAuthenticationTypes.Windows;

        public enum SqlAuthenticationTypes
        {
            Windows,
            SqlServer
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), DisplayName("Server Type"), DefaultValue("Database Engine"), ReadOnly(true)]
        public string ServerType
        {
            get
            {
                return m_ServerType;
            }
            set
            {
                m_ServerType = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), DisplayName("Server Name")]
        public string ServerName
        {
            get
            {
                return m_ServerName;
            }
            set
            {
                m_ServerName = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"),
        ReadOnly(true)]
        public SqlAuthenticationTypes Authentication
        {
            get
            {
                return m_Authentication;
            }
            set
            {
                m_Authentication = value;
            }
        }

    }
}
