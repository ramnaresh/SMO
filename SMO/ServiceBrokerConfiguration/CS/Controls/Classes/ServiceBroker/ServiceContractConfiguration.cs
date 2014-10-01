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
using System.Data;
using System.Text;

namespace Microsoft.Samples.SqlServer
{
    //Wrapper class for use within the PropertyGrid control
    public class ServiceContractConfiguration : ConfigurationBase, IConfiguration
    {
        private string m_Name;
        private Hashtable m_MessageTypeMappings = new Hashtable();

        public ServiceContractConfiguration(ServiceBroker serviceBroker) : base(serviceBroker)
        {
            base.Urn = new Uri(base.Urn.ToString() + "contract/");
        }
        
        public ServiceContractConfiguration(string fullName, ServiceBroker serviceBroker)
            : base(serviceBroker)
        {
            if (this.ServiceBroker.ServiceContracts.Contains(fullName))
            {
                ServiceContract item = this.ServiceBroker.ServiceContracts[fullName];

                base.Urn = new Uri(base.Urn.ToString() + "contract/");

                m_Name = item.Name.Substring
                    (base.Urn.ToString().Length, item.Name.Length - base.Urn.ToString().Length);

                foreach (MessageTypeMapping mtm in item.MessageTypeMappings)
                {
                    m_MessageTypeMappings.Add(mtm.Name, mtm.MessageSource.ToString());
                }
                
            }

        }

        public string Export(string fileName)
        {
            //Export the T-SQL script
            base.Export(this.Name + ".Contract", this.SqlScript);

            return this.SqlScript;
        }

        public void Import(string fileName)
        {
            //Not supported in this sample.
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Microsoft.Samples.SqlServer.ConfigurationBase.AddOutputRow(System.Data.DataTable,System.String)")]
        public DataSet Query()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
            sb.AppendLine("SELECT * FROM sys.service_contracts WHERE name = '" + this.FullName + "'");

            DataSet ds = this.ServiceBroker.Parent.Parent.ConnectionContext.ExecuteWithResults(sb.ToString());

            //Add "OutputTextTable" DataTable to DataSet. A DataSet is used to provide output flexibility.
            DataTable tbl = base.OutputTextTable();

            ServiceContract item;
            if (this.ServiceBroker.ServiceContracts.Contains(this.FullName))
            {
                item = this.ServiceBroker.ServiceContracts[this.FullName];
                if (item.MessageTypeMappings.Count > 0)
                {
                    base.AddOutputRow(tbl, "Message Types:");
                    foreach (NamedSmoObject map in item.MessageTypeMappings)
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
                return base.Urn + m_Name; ;
            }
        }

        [Browsable(false)]
        public string SqlScript
        {
            //Hard-coded to Script()[0] for the sample
            get
            {
                return this.ServiceBroker.ServiceContracts[this.FullName].Script()[0];
            }
        }

        public void Create()
        {
            //Create Contract
            ServiceContract serviceContract = null;

            if (!this.ServiceBroker.ServiceContracts.Contains(this.FullName))
            {
                // Create service contract
                serviceContract = new ServiceContract(
                        this.ServiceBroker, this.FullName);

                CreateMessageTypeMappings(serviceContract);

                serviceContract.Create();

            }

        }

        //Create the MessageTypeMappings for this contract
        private void CreateMessageTypeMappings(ServiceContract item)
        {
            MessageTypeMapping mtm;
            IDictionaryEnumerator enumerator = this.MessageTypeMappings.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!item.MessageTypeMappings.Contains(enumerator.Key.ToString()))
                {
                    mtm = new MessageTypeMapping(item, enumerator.Key.ToString());

                    mtm.MessageSource = (MessageSource)enumerator.Value;

                    item.MessageTypeMappings.Add(mtm);
                }
            }
        }
       
        public void Drop()
        {
            if (this.ServiceBroker.ServiceContracts.Contains(this.FullName))
            {
                // Create contract
                ServiceContract item =
                    this.ServiceBroker.ServiceContracts[this.FullName];

                item.Drop();
            }
        }

        public void Alter()
        {
            bool mappingChanged = false;

            if (this.ServiceBroker.ServiceContracts.Contains(this.FullName))
            {
                // Get contract
                ServiceContract contract =
                    this.ServiceBroker.ServiceContracts[this.FullName];

                //Check if mapping was dropped or added
                if (this.MessageTypeMappings.Count != contract.MessageTypeMappings.Count)
                {
                    mappingChanged = true;
                }
                else
                {
                    //Check if MessageSource was changed.
                    foreach (MessageTypeMapping mtm in contract.MessageTypeMappings)
                    {
                        if (!this.MessageTypeMappings.ContainsValue(mtm.MessageSource.ToString()))
                        {
                            mappingChanged = true;
                            break;
                        }
                    }

                }

                //Drop/create only if mapping has changed.
                if (mappingChanged)
                {
                    //First drop the contract
                    this.Drop();

                    //Then re-create the contract
                    this.Create();
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"),
        Editor(typeof(MessageTypeMapTypeEditor), typeof(UITypeEditor))]
        public Hashtable MessageTypeMappings
        {
            get
            {
                return m_MessageTypeMappings;

            }
            set { m_MessageTypeMappings = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value.Trim();
            }
        }
    }
}
