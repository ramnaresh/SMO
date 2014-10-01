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
    class EndpointConfiguration : ConfigurationBase, IConfiguration
    {
        private EndpointState m_EndpointState = EndpointState.Started;
        private ProtocolType m_ProtocolType = ProtocolType.Tcp;
        private EndpointType m_EndpointType = EndpointType.ServiceBroker;
        private string m_Name;
        private int m_ListenerPort = 4022;
        private CertificateConfiguration m_CertificateConfiguration;

        public EndpointConfiguration(ServiceBroker serviceBroker)
            : base(serviceBroker)
        {
            base.Urn = new Uri(base.Urn.ToString() + "endpoint/");
            this.Name = base.Server.Name;
        }

        public EndpointConfiguration(string fullName, ServiceBroker serviceBroker)
            : base(serviceBroker)
		{
            if (base.Server.Endpoints.Contains(fullName))
            {
                Endpoint item = base.Server.Endpoints[fullName];

                base.Urn = new Uri(base.Urn.ToString() + "endpoint/");

                m_Name = item.Name.Substring
                    (base.Urn.ToString().Length, item.Name.Length - base.Urn.ToString().Length);

                Certificate cert = this.Server.Databases["Master"]
                    .Certificates[item.Payload.ServiceBroker.Certificate];
                this.Certificate.Name = cert.Name;
                this.Certificate.Issuer = cert.Issuer;
                this.Certificate.Owner = cert.Owner;
                this.Certificate.StartDate = cert.StartDate;
                this.Certificate.ExpirationDate = cert.ExpirationDate;
                this.Certificate.Subject = cert.Subject;
                
            }
		}

        public void Create()
        {
            if (!base.Server.Endpoints.Contains(this.FullName))
            {
                Endpoint endPoint = new Endpoint(base.Server, this.FullName);

                endPoint.EndpointType = this.EndpointType;
                endPoint.ProtocolType = this.ProtocolType;
                endPoint.Protocol.Tcp.ListenerPort = this.ListenerPort;

                //Create the certificate in Master database for this Endpoint
                CreateMasterCertificate();

                endPoint.Payload.ServiceBroker.Certificate = this.Certificate.Name;
                endPoint.Payload.ServiceBroker.EndpointAuthenticationOrder = EndpointAuthenticationOrder.Certificate;

                endPoint.Create();
            }
        }

        private void CreateMasterCertificate()
        {
            //create certificate [<computer_name>] 
            //with subject = N'<computer_name>'; 
            if (!this.ServiceBroker.Parent.Parent.Databases["Master"].Certificates
                .Contains(this.Certificate.Name))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("USE MASTER");
                sb.AppendLine("CREATE CERTIFICATE [" + 
                    this.Certificate.Name + "] WITH SUBJECT = N'" + 
                    this.Certificate.Subject + "'");
                this.ServiceBroker.Parent.Parent.ConnectionContext.ExecuteNonQuery(sb.ToString());
            }
        }

        public void Alter() 
        {
            //Not implemented since properties are read only for the sample.
        }

        public void Drop()
        {
            if (base.Server.Endpoints.Contains(this.FullName))
            {
                base.Server.Endpoints[this.FullName].Drop();
            }
               
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), ReadOnly(true)]
        public EndpointState EndpointState
        {
            get
            {
                return m_EndpointState;
            }
            set
            {
                m_EndpointState = value;
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), ReadOnly(true)]
        public ProtocolType ProtocolType
        {
            get
            {
                return m_ProtocolType;
            }
            set
            {
                m_ProtocolType = value;
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), ReadOnly(true)]
        public EndpointType EndpointType
        {
            get
            {
                return m_EndpointType;
            }
            set
            {
                m_EndpointType = value;
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), ReadOnly(true)]
        public int ListenerPort
        {
            get
            {
                return m_ListenerPort;
            }
            set
            {
                m_ListenerPort = value;
            }

        }

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

        public string Export(string fileName)
        {
            base.Export(this.Name + ".EndPoint", this.SqlScript);

            return this.SqlScript;
        }

        public void Import(string fileName)
        {
            //Not support in this sample.
        }

        public DataSet Query()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
            sb.AppendLine("SELECT * FROM sys.endpoints WHERE name = '" + this.FullName + "'");

            return this.ServiceBroker.Parent.Parent.ConnectionContext.ExecuteWithResults(sb.ToString());
        }


        [Browsable(false)]
        public string FullName
        {
            get
            {
                return base.Urn + m_Name; ;
            }
        }

        [Browsable(false)]
        public string SqlScript
        {
            get
            {
                return this.ServiceBroker.Parent.Parent.Endpoints[this.FullName].Script()[0];
            }
        }

        public CertificateConfiguration Certificate
        {
            get
            {
                //Create a CertificateConfiguration for this Endpoint
                if (m_CertificateConfiguration == null)
                {
                    m_CertificateConfiguration = new CertificateConfiguration(this.ServiceBroker);
                    m_CertificateConfiguration.Parent = this;

                    m_CertificateConfiguration.ExpirationDate = DateTime.Today.AddDays(1);
                    m_CertificateConfiguration.StartDate = DateTime.Today;
                    m_CertificateConfiguration.Name = this.ServiceBroker.Parent.Parent.Name;
                    m_CertificateConfiguration.Owner = m_CertificateConfiguration.Name;
                    m_CertificateConfiguration.Subject = m_CertificateConfiguration.Name;
                    m_CertificateConfiguration.Issuer = m_CertificateConfiguration.Name;
                }
                return m_CertificateConfiguration;
            }
        }
    }
}
