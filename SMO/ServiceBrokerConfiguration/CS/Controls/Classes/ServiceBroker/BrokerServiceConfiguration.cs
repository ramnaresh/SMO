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
using System.Data;

namespace Microsoft.Samples.SqlServer
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BrokerServiceConfiguration : ConfigurationBase, IConfiguration
    {
		private string m_Name;
        private ArrayList m_ContractNames = new ArrayList();
        private string m_QueueName;
        private bool m_EnableRemoteServiceBinding;
        private string m_ServiceOwnerName = String.Empty;

        public BrokerServiceConfiguration(ServiceBroker serviceBroker)
            : base(serviceBroker)
		{
            base.Urn = new Uri(base.Urn.ToString() + "service/");
        }

        public BrokerServiceConfiguration(string fullName, ServiceBroker serviceBroker)
            : base(serviceBroker)
		{
            if (this.ServiceBroker.Services.Contains(fullName))
            {
                BrokerService item = this.ServiceBroker.Services[fullName];

                base.Urn = new Uri(base.Urn.ToString() + "service/");

                m_Name = item.Name.Substring
                    (base.Urn.ToString().Length, item.Name.Length - base.Urn.ToString().Length);

                m_QueueName = item.QueueName;
                this.ServiceOwnerName = item.Owner;

                //Fill ContractNames from ServiceContractMapping
                foreach (ServiceContractMapping map in item.ServiceContractMappings)
                {
                    m_ContractNames.Add(map.Name);
                }

                //CertificateConfiguration for this Service
                //CertificateName is store in ExtendedProperties to match the Service to the Certificate
                if (item.ExtendedProperties.Contains("CertificateName"))
                {
                    CertificateConfiguration cert = new
                        CertificateConfiguration(item.ExtendedProperties["CertificateName"].Value.ToString(),
                        this.ServiceBroker);
                    this.Certificate = cert;

                }

                //RemoteServiceBinding for this Service if it exists
                string bindingFullName = base.BaseUrn + "RemoteServiceBinding/" + this.Name + "Binding";
                if (this.ServiceBroker.RemoteServiceBindings.Contains(bindingFullName))
                {
                    this.EnableRemoteService = true;
                    RemoteServiceBinding binding = this.ServiceBroker.
                        RemoteServiceBindings[bindingFullName];

                    this.RemoteServiceBinding.CertificateUser = binding.CertificateUser;
                    this.RemoteServiceBinding.IsAnonymous = binding.IsAnonymous;
                    this.AllowAnonymous = binding.IsAnonymous;
                    this.RemoteServiceBinding.Name = binding.Name;
                    this.RemoteServiceBinding.Owner = binding.Owner;
                    this.RemoteServiceBinding.RemoteService = binding.RemoteService;
                }

            }
		}

        public string Export(string fileName)
        {
            //Export Service Listing
            StringBuilder dropScriptSB =  new StringBuilder();
            StringBuilder contractsSB = new StringBuilder();
            Hashtable tempMessageTypes = new Hashtable();
            StringBuilder messageTypesSB = new StringBuilder();
            StringBuilder queueSB = new StringBuilder();
            StringBuilder serviceSB = new StringBuilder();
            StringBuilder generalSB = new StringBuilder();
           
            ServiceContract contract;
            MessageType msgType;

            //CREATE USER script
            if (this.ServiceBroker.Parent.Users.Contains(this.ServiceOwnerName))
            {
                generalSB.AppendLine("CREATE USER [" + this.ServiceOwnerName + "] WITHOUT LOGIN");
                generalSB.AppendLine();
            }

            //CREATE CERTIFICATE script
            if (this.ServiceBroker.Parent.Certificates.Contains(this.Certificate.Name))
            {
                string fullFileName = base.ScriptPath + @"\" + this.Name + ".Service.crt";
                generalSB.AppendLine("CREATE CERTIFICATE [" + this.Certificate.Name + "]");
                generalSB.AppendLine("AUTHORIZATION [" + this.ServiceOwnerName + "]");
                generalSB.AppendLine("FROM FILE= '" + fullFileName);
                generalSB.AppendLine();

                //Export certificate
                CertificateConfiguration cert = new CertificateConfiguration(this.Certificate.Name, this.ServiceBroker);
                cert.Export(fullFileName);
            }

            //GRANT SEND script
            generalSB.AppendLine("GRANT SEND ON SERVICE::[" + this.FullName + "]");
            generalSB.AppendLine("TO [" + this.ServiceOwnerName + "]");
            generalSB.AppendLine();
            
            //Drop service script
            dropScriptSB.AppendLine("--Drop service");
            dropScriptSB.AppendLine("IF EXISTS (SELECT * FROM sys.services ");
            dropScriptSB.AppendLine("WHERE name = '" + this.FullName + "')");
            dropScriptSB.AppendLine("BEGIN");
            dropScriptSB.AppendLine("DROP SERVICE [" +  this.FullName + "]" );
            dropScriptSB.AppendLine("END") ;
            dropScriptSB.AppendLine("GO");
            dropScriptSB.AppendLine();


            //Build Contracts script
            foreach (String item in this.ContractNames)
            {
                contract = this.ServiceBroker.ServiceContracts[item];
                contractsSB.AppendLine(contract.Script()[0]);
                contractsSB.AppendLine();
                
                //Drop Contracts script
                dropScriptSB.AppendLine("--Drop contract: " + item);
                dropScriptSB.AppendLine("IF EXISTS (SELECT * FROM sys.service_contracts ");
                dropScriptSB.AppendLine("WHERE name = '" + item + "')");
                dropScriptSB.AppendLine("BEGIN");
                dropScriptSB.AppendLine("DROP CONTRACT [" +  item + "]") ;
                dropScriptSB.AppendLine("END") ;
                dropScriptSB.AppendLine("GO");
                dropScriptSB.AppendLine();

                
                //Build MessageTypes Hashtable to prevent duplicate MessageTypes
                foreach (MessageTypeMapping map in contract.MessageTypeMappings)
                {
                    msgType = this.ServiceBroker.MessageTypes[map.Name];
                    
                    if (!tempMessageTypes.Contains(msgType.Name))
                        tempMessageTypes.Add(msgType.Name, msgType.Script()[0].ToString());  
                }
            }

            //Build MessageType script
            IDictionaryEnumerator enumerator = tempMessageTypes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                messageTypesSB.AppendLine(enumerator.Value.ToString());
                messageTypesSB.AppendLine();

                //Drop MessageType's script
                dropScriptSB.AppendLine("--Drop MessageType: " + enumerator.Key.ToString());
                dropScriptSB.AppendLine("IF EXISTS (SELECT * FROM sys.service_message_types ");
                dropScriptSB.AppendLine("WHERE name = '" + enumerator.Key.ToString() + "')");
                dropScriptSB.AppendLine("BEGIN");
                dropScriptSB.AppendLine("DROP MESSAGE TYPE [" + enumerator.Key.ToString() + "]");
                dropScriptSB.AppendLine("END");
                dropScriptSB.AppendLine("GO");
                dropScriptSB.AppendLine();
            }

            //Drop Queue Script
            dropScriptSB.AppendLine("--Drop Queue: " + this.QueueName);
            dropScriptSB.AppendLine("IF EXISTS (SELECT * FROM sys.service_queues ");
            dropScriptSB.AppendLine("WHERE name = '" + this.QueueName + "')");
            dropScriptSB.AppendLine("BEGIN");
            dropScriptSB.AppendLine("DROP QUEUE [" + this.QueueName + "]");
            dropScriptSB.AppendLine("END");
            dropScriptSB.AppendLine("GO");
            dropScriptSB.AppendLine();
            
            //Build Queue script
            queueSB.AppendLine(this.ServiceBroker.Queues[this.QueueName].Script()[0].ToString());
            queueSB.AppendLine();

            //Build Service script
            serviceSB.AppendLine(this.ServiceBroker.Services[this.FullName].Script()[0].ToString());
            
            string finalScript =
                generalSB.ToString() +
                dropScriptSB.ToString() +
                messageTypesSB.ToString() +
                contractsSB.ToString() +
                queueSB.ToString() +
                serviceSB.ToString();

            base.Export(this.Name + ".Service", finalScript);

            return finalScript;

        }

        public void Import(string fileName)
        {
            //Not support in this version. Use SQL Server Management Studio to import.
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Microsoft.Samples.SqlServer.ConfigurationBase.AddOutputRow(System.Data.DataTable,System.String)")]
        public DataSet Query()
        {
            //Output a DataSet for this Service
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
            sb.AppendLine("SELECT * FROM sys.services WHERE name = '" + this.FullName + "'");

            DataSet ds = this.ServiceBroker.Parent.Parent.ConnectionContext.ExecuteWithResults(sb.ToString());

            //Add "OutputTextTable" DataTable to the DataSet. This is used to output the service contracts as text.
            DataTable tbl = base.OutputTextTable();
            base.AddOutputRow(tbl, this.FullName);

            BrokerService service;
            if (this.ServiceBroker.Services.Contains(this.FullName))
            {
                service = this.ServiceBroker.Services[this.FullName];
                if (service.ServiceContractMappings.Count > 0)
                {
                    base.AddOutputRow(tbl, "Contracts:");
                    foreach (ServiceContractMapping map in service.ServiceContractMappings)
                    {
                        base.AddOutputRow(tbl, "\t" + map.Name);
                    }
                }
            }
            
            ds.Tables.Add(tbl);
            return ds;

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
                StringBuilder sb = new StringBuilder();

                //Add Service script
                sb.AppendLine(this.ServiceBroker.Services[this.FullName].Script()[0]);
                sb.AppendLine();

                //Add Binding script
                string bindingFullName = base.BaseUrn + "RemoteServiceBinding/" + this.Name + "Binding";
                if (this.ServiceBroker.RemoteServiceBindings.Contains(bindingFullName))
                {
                    sb.AppendLine(this.ServiceBroker.RemoteServiceBindings[bindingFullName].Script()[0]);
                    sb.AppendLine();
                }

                return sb.ToString();
            }
        }


        private bool m_GrantReceive = false;
        public bool GrantReceive
        {
            get
            {
                return m_GrantReceive;
            }
            set
            {
                m_GrantReceive = value;
            }
        }

        public void Create()
        {
            BrokerService brokerService = null;
            ExtendedProperty prop = null;
            if (!this.ServiceBroker.Services.Contains(this.FullName))
            {
                // Create initiator service
                brokerService = new BrokerService(this.ServiceBroker, this.FullName);
                brokerService.QueueName = this.QueueName;
                brokerService.Owner = this.ServiceOwnerName;

                foreach (object item in this.ContractNames)
                {
                    ServiceContractMapping brokerServiceContract
                        = new ServiceContractMapping(brokerService, item.ToString());
                    brokerService.ServiceContractMappings.Add(brokerServiceContract);
                }

                if (!String.IsNullOrEmpty(this.ServiceOwnerName))
                {
                    CreateUser();
                }

                //Create Certificate if EnableRemoteServiceBinding = true and AllowAnonymous = false
                if (m_EnableRemoteServiceBinding
                    && !String.IsNullOrEmpty(this.ServiceOwnerName) && !this.AllowAnonymous)
                {
                    prop = new ExtendedProperty(brokerService, 
                        "CertificateName", this.Certificate.Name);
                    
                    this.Certificate.Owner = this.ServiceOwnerName;
                    this.Certificate.Create();
                }

                //Create ReportServiceBinding
                if (m_EnableRemoteServiceBinding && !String.IsNullOrEmpty(this.ServiceOwnerName))
                {
                    this.CreateRemoteServiceBinding();
                }

                //Grant Receive
                if (this.GrantReceive)
                {
                    CreateGrantReceive();
                }

                brokerService.Create();

                //Create property last
                if (prop != null)
                    prop.Create();
            }
        }

        private void CreateRemoteServiceBinding()
        {
            if (!this.ServiceBroker.RemoteServiceBindings.Contains(this.BindingFullName))
            {
                RemoteServiceBinding bind =
                    new RemoteServiceBinding(this.ServiceBroker, this.BindingFullName);
                bind.IsAnonymous = this.AllowAnonymous;
                bind.CertificateUser = this.ServiceOwnerName;
                bind.Owner = this.ServiceOwnerName;
                bind.RemoteService = this.FullName;

                bind.Create();
            }
        }

        private void CreateUser()
        {
            //Create Service User
            if (!this.ServiceBroker.Parent.Users.Contains(this.ServiceOwnerName))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
                sb.AppendLine("CREATE USER [" + this.ServiceOwnerName + "] WITHOUT LOGIN");
                this.ServiceBroker.Parent.Parent.ConnectionContext.ExecuteNonQuery(sb.ToString());
            }
        }

        private bool m_AllowAnonymous;
        public bool AllowAnonymous
        {
            get
            {
                return m_AllowAnonymous;
            }
            set
            {
                this.RemoteServiceBinding.IsAnonymous = value;
                m_AllowAnonymous = value;
            }
        }

        private void CreateGrantReceive()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
            sb.AppendLine("GRANT RECEIVE ON [" + this.QueueName + "] TO [" + this.ServiceOwnerName + "]");
            this.ServiceBroker.Parent.Parent.ConnectionContext.ExecuteNonQuery(sb.ToString());
        }

        private string BindingFullName
        {
            get
            {
                return base.BaseUrn + "RemoteServiceBinding/" + this.Name + "Binding";
            }
        }

        public void Drop()
        {
            if (this.ServiceBroker.Services.Contains(this.FullName))
            {
                this.ServiceBroker.Services[this.FullName].Drop();
                
                if (this.ServiceBroker.RemoteServiceBindings.Contains(this.BindingFullName))
                {
                    RemoteServiceBinding binding = this.ServiceBroker.
                        RemoteServiceBindings[this.BindingFullName];
                    binding.Drop();
                }
            }

            //Drop Certificate
            if (m_EnableRemoteServiceBinding && !this.AllowAnonymous)
            {
                this.DropCertificate();
            }           
        }

        private void DropCertificate()
        {
            if (this.ServiceBroker.Parent.Certificates.Contains(this.Name))
            {
                Certificate cert = this.ServiceBroker.Parent.Certificates[this.Name];
                cert.Drop();
            }
        }     

        public void Alter()
        {
            BrokerService brokerService;
            ExtendedProperty prop = null;

            if (this.ServiceBroker.Services.Contains(this.FullName))
            {
                brokerService = this.ServiceBroker.Services[this.FullName];

                brokerService.QueueName = this.QueueName;
                
                //Drop Contracts that user removed
                foreach (ServiceContractMapping map in brokerService.ServiceContractMappings)
                {
                    if (!this.ContractNames.Contains(map.Name))
                    {
                        if (map.State != SqlSmoState.ToBeDropped)
                        {
                            map.MarkForDrop(true);
                        }
                    }
                }
                //Add Contracts that user added
                foreach (string name in this.ContractNames)
                {
                    if (!brokerService.ServiceContractMappings.Contains(name))
                    {
                        ServiceContractMapping brokerServiceContract
                        = new ServiceContractMapping(brokerService, name);
                        brokerService.ServiceContractMappings.Add(brokerServiceContract);
                    }
                
                }

                brokerService.Alter();

                //Drop RemoteServiceBinding and Certificate
                if (!m_EnableRemoteServiceBinding)
                {

                    if (this.ServiceBroker.RemoteServiceBindings.Contains(this.BindingFullName))
                    {
                        RemoteServiceBinding binding = this.ServiceBroker.
                            RemoteServiceBindings[this.BindingFullName];
                        binding.Drop();
                    }

                    this.DropCertificate();
                    //Drop extended property
                    if (brokerService.ExtendedProperties.Contains("CertificateName"))
                        brokerService.ExtendedProperties["CertificateName"].Drop();
                }

                //Change RemoteServiceBinding
                if (m_EnableRemoteServiceBinding && !String.IsNullOrEmpty(this.ServiceOwnerName))
                {
                    if (this.ServiceBroker.RemoteServiceBindings.Contains(this.BindingFullName))
                    {
                        RemoteServiceBinding binding = this.ServiceBroker.
                            RemoteServiceBindings[this.BindingFullName];
                        binding.IsAnonymous = this.AllowAnonymous;
                        //Can't change service owner binding.Owner = this.ServiceOwnerName;

                        binding.Alter();
                    }
                }

                //Create Certificate if EnableRemoteServiceBinding = true and AllowAnonymous = false
                if (m_EnableRemoteServiceBinding
                    && !String.IsNullOrEmpty(this.ServiceOwnerName) && !this.AllowAnonymous)
                {
                    if (!brokerService.ExtendedProperties.Contains("CertificateName"))
                        prop = new ExtendedProperty(brokerService,
                            "CertificateName", this.Certificate.Name);

                    this.Certificate.Owner = this.ServiceOwnerName;
                    this.Certificate.Create();
                }

                //Create ReportServiceBinding
                if (m_EnableRemoteServiceBinding && !String.IsNullOrEmpty(this.ServiceOwnerName))
                {
                    this.CreateRemoteServiceBinding();
                }

                if (this.GrantReceive)
                {
                    CreateGrantReceive();
                }

                //Create property last
                if (prop != null)
                    prop.Create();
            }
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), 
        Editor(typeof(Microsoft.Samples.SqlServer.ContractsListTypeEditor), typeof(UITypeEditor))
        ]
        public ArrayList ContractNames
        {
            get
            {
                return m_ContractNames;
            }
            set
            {
                m_ContractNames = value;
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
     
        [Editor(typeof(Microsoft.Samples.SqlServer.QueueNameListTypeEditor), typeof(UITypeEditor))]
        public string QueueName
        {
            get
            {

                return m_QueueName;
            }
            set
            {
                if (this.ServiceBroker.Queues.Contains(value))
                {
                    m_QueueName = value;
                }
            }
        }

        [Editor(typeof(Microsoft.Samples.SqlServer.OwnerNameListTypeEditor), 
            typeof(UITypeEditor)), Category("Service User")]
        public string ServiceOwnerName
        {
            get
            {

                return m_ServiceOwnerName;
            }
            set
            {
                m_ServiceOwnerName = value;
                this.RemoteServiceBinding.Owner = value;
            }
        }

        public bool EnableRemoteService
        {
            get
            {
                return m_EnableRemoteServiceBinding;
            }
            set
            {
                m_EnableRemoteServiceBinding = value;

                if (value)
                {
                    this.Certificate.Issuer = this.Name;
                    this.Certificate.Name = this.Name;
                    this.Certificate.Subject = this.Name;

                    this.RemoteServiceBinding.CertificateUser = this.Certificate.Name;
                    this.RemoteServiceBinding.IsAnonymous = this.AllowAnonymous;
                    this.RemoteServiceBinding.Name = this.Name;

                    this.RemoteServiceBinding.RemoteService = this.Name;
                }
                else
                {
                    this.Certificate.Issuer = String.Empty;
                    this.Certificate.Name = String.Empty;
                    this.Certificate.Subject = String.Empty;

                    this.RemoteServiceBinding.CertificateUser = String.Empty;
                    this.RemoteServiceBinding.Name = String.Empty;

                    this.RemoteServiceBinding.RemoteService = String.Empty;
                }
            }
        }

        private CertificateConfiguration m_CertificateConfiguration;
        public CertificateConfiguration Certificate
        {
            get
            {
                if (m_CertificateConfiguration == null)
                {
                    m_CertificateConfiguration = new CertificateConfiguration(this.ServiceBroker);
                    m_CertificateConfiguration.Parent = this;

                    m_CertificateConfiguration.ExpirationDate = DateTime.Today.AddDays(1);
                    m_CertificateConfiguration.StartDate = DateTime.Today;
                    m_CertificateConfiguration.Name = String.Empty;
                    
                }
                return m_CertificateConfiguration;
            }
            set
            {
                m_CertificateConfiguration = value;
            }
        }

        private RemoteServiceBindingConfiguration m_RemoteServiceBindingConfiguration;
        public RemoteServiceBindingConfiguration RemoteServiceBinding
        {
            get
            {
                if (m_RemoteServiceBindingConfiguration == null)
                {
                    m_RemoteServiceBindingConfiguration = new RemoteServiceBindingConfiguration(this.ServiceBroker);
                }
                return m_RemoteServiceBindingConfiguration;
            }
            set
            {
                m_RemoteServiceBindingConfiguration = value;
            }
        }

    }
}
