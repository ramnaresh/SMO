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
using System.Data;
using System.Text;

namespace Microsoft.Samples.SqlServer
{
    
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RemoteServiceBindingConfiguration : ConfigurationBase, IConfiguration
    {
        private string m_Name;
        private string m_Owner;
        private string m_CertificateUser;
        private string m_RemoteService;
        private bool m_IsAnonymous;

        public RemoteServiceBindingConfiguration(ServiceBroker serviceBroker)
            : base(serviceBroker)
        {
            base.Urn = new Uri(base.Urn.ToString() + "RemoteServiceBinding/");
        }

        public RemoteServiceBindingConfiguration(string fullName, ServiceBroker serviceBroker)
            : base(serviceBroker)
        {
            if (this.ServiceBroker.RemoteServiceBindings.Contains(fullName))
            {
                RemoteServiceBinding item = this.ServiceBroker.RemoteServiceBindings[fullName];
               
                base.Urn = new Uri(base.Urn.ToString() + "RemoteServiceBinding/");

                m_Name = item.Name.Substring
                    (base.Urn.ToString().Length, item.Name.Length - base.Urn.ToString().Length);
                
                m_Owner = item.Owner;
                m_CertificateUser = item.CertificateUser;
                m_RemoteService = item.RemoteService;
                m_IsAnonymous = item.IsAnonymous;
            }

        }
        

        public string Export(string fileName)
        {
            //Export the T-SQL script
            base.Export(this.Name + ".ReportServiceBinding", this.SqlScript);

            return this.SqlScript;
            
        }
        public void Import(string fileName)
        {
            //Not implemented
        }

        public DataSet Query()
        {
            //
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
            sb.AppendLine("select * from sys.remote_service_bindings WHERE name = '" + this.FullName + "'");

            return this.ServiceBroker.Parent.Parent.ConnectionContext.ExecuteWithResults(sb.ToString());
        }

        [Browsable(false)]
        public string FullName
        {
            get
            {
                return base.Urn + m_Name;
            }
        }

        [Browsable(false)]
        public string SqlScript
        {
            get
            {
                return this.ServiceBroker.RemoteServiceBindings[this.FullName].Script()[0];
            }
        }

        public void Create()
        {
            //Not implemented in sample. See BrokerServiceConfiguration
        }

        public void Drop()
        {
            //Not implemented in sample. See BrokerServiceConfiguration
        }

        public void Alter()
        {

            //Not implemented in this sample. See BrokerServiceConfiguration
        }


        [ReadOnly(true)]
        public string Name
        {
            get
            {

                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        [ReadOnly(true)]
        public string Owner
        {
            get
            {

                return m_Owner;
            }
            set
            {
                m_Owner = value;
            }
        }

        [ReadOnly(true)]
        public string CertificateUser
        {
            get
            {

                return m_CertificateUser;
            }
            set
            {
                m_CertificateUser = value;
            }
        }

        [ReadOnly(true)]
        public string RemoteService 
        {
            get
            {

                return m_RemoteService;
            }
            set
            {
                m_RemoteService = value;
            }
        }
        [ReadOnly(true)]
        public bool IsAnonymous
        {
            get
            {

                return m_IsAnonymous;
            }
            set
            {
                m_IsAnonymous = value;
            }
        }
    }
        
}
