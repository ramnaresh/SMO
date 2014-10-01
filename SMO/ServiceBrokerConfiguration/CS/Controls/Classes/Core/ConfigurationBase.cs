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

using Microsoft.SqlServer.Management.Smo.Broker;
using Microsoft.SqlServer.Management.Smo;
using System.ComponentModel;
using System;
using System.IO;
using System.Data;

namespace Microsoft.Samples.SqlServer
{
    //Base class for configuration classes
    public abstract class ConfigurationBase
    {
        private ServiceBroker m_ServiceBroker;

        protected ConfigurationBase(ServiceBroker serviceBroker)
        {
            if (serviceBroker != null)
            {
                m_ServiceBroker = serviceBroker;
                
                //BaseUrn is used in the sample to provide a consistent Urn
                if (this.ServiceBroker.Parent.ExtendedProperties.Contains("BaseUrn"))
                {
                    this.Urn = new Uri(this.BaseUrn);
                }
            }
        }

        //OutputTextTable is used to output any text values as part of an objects .Query method.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1306:SetLocaleForDataTypes")]
        protected DataTable OutputTextTable()
        {       
            DataTable tbl = new DataTable("OutputTextTable");
            DataColumn col = new DataColumn();
            col.DataType = Type.GetType("System.String");
            col.ColumnName = "text";
            tbl.Columns.Add(col);

            return tbl;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected void AddOutputRow(DataTable dataTable, string text)
        {
            DataRow row = null;
            if (dataTable != null)
            {
                row = dataTable.NewRow();
                row["text"] = text;
                dataTable.Rows.Add(row);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        protected string BaseUrn
        {
            get
            {
                string urn = String.Empty;
                if (this.ServiceBroker.Parent.ExtendedProperties.Contains("BaseUrn"))
                {
                   urn = this.ServiceBroker.Parent.ExtendedProperties["BaseUrn"].Value.ToString();
                }
                return urn;
            }
        }

        [Browsable(false)]
        public ServiceBroker ServiceBroker
        {
            get
            {
                return m_ServiceBroker;
            }
        }

        [Browsable(false)]
        public Server Server
        {
            get
            {
                return m_ServiceBroker.Parent.Parent;
            }
        }

        protected void Export(string fileName, string text)
        {
            string path = String.Empty;

            if (this.ServiceBroker.Parent.ExtendedProperties.Contains("ScriptPath"))
            {
                path = this.ServiceBroker.Parent.ExtendedProperties["ScriptPath"].Value.ToString();

                using (StreamWriter sr = File.CreateText(path + @"\" + fileName + ".sql"))
                {
                    sr.Write(text);
                }
                
            }
        }

        protected string ScriptPath
        {
            get
            {
                string path = String.Empty;

                if (this.ServiceBroker.Parent.ExtendedProperties.Contains("ScriptPath"))
                {
                    path = this.ServiceBroker.Parent.ExtendedProperties["ScriptPath"].Value.ToString();
                }

                return path;
            }

        }

        private Uri m_Urn;
        [ReadOnly(true)]
        public Uri Urn
        {
            get
            {
                return m_Urn;
            }
            set
            {
                m_Urn = value;
            }

        }
    }
}
