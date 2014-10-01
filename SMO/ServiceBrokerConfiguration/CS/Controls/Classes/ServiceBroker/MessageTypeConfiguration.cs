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
using System.Data.SqlClient;
using System.Text;


namespace Microsoft.Samples.SqlServer
{
    //Wrapper class for use within the PropertyGrid control
    public class MessageTypeConfiguration : ConfigurationBase, IConfiguration
    {
		private string m_Name;
        private MessageTypeValidation m_MessageTypeValidation;
        private string m_ValidationXmlSchemaCollectionSchema;
        private string m_ValidationXmlSchemaCollection;
        private string m_Owner = "dbo";
        private Hashtable m_Schemas = new Hashtable();
        
        public MessageTypeConfiguration(ServiceBroker serviceBroker): base(serviceBroker)
        {
            base.Urn = new Uri(base.Urn.ToString() + "messagetype/");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"), Browsable(false)]
        public Hashtable Schemas
        {
            get
            {
                return m_Schemas;
            }
            set
            {
                m_Schemas = value;
            }
        }

        public MessageTypeConfiguration(string fullName, ServiceBroker serviceBroker)
            : base(serviceBroker)
        {
            if (this.ServiceBroker.MessageTypes.Contains(fullName))
            {
                MessageType msgType = this.ServiceBroker.MessageTypes[fullName];
                m_MessageTypeValidation = msgType.MessageTypeValidation;

                base.Urn = new Uri(base.Urn.ToString() + "messagetype/");

                m_Name = msgType.Name.Substring
                    (base.Urn.ToString().Length, msgType.Name.Length - base.Urn.ToString().Length);
                
                m_Owner = msgType.Owner;
                if (msgType.MessageTypeValidation == MessageTypeValidation.XmlSchemaCollection)
                {
                    m_ValidationXmlSchemaCollection = msgType.ValidationXmlSchemaCollection;
                    m_ValidationXmlSchemaCollectionSchema = msgType.ValidationXmlSchemaCollectionSchema;
                    if (this.ServiceBroker.Parent.XmlSchemaCollections.Contains
                        (this.ValidationXmlSchemaCollection))
                    {
                        XmlSchemaCollection schema = this.ServiceBroker.Parent.
                            XmlSchemaCollections[msgType.ValidationXmlSchemaCollection];
                        this.ValidationXmlSchemaCollectionSchema = schema.Text;
                    }
                }
            }

        }

        public string Export(string fileName)
        {
            //Export the T-SQL script
            base.Export(this.Name + ".MessageType", this.SqlScript);

            return this.SqlScript;
        }

        public void Import(string fileName)
        {
            //Not implemented for this sample.
        }

        public DataSet Query()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
            sb.AppendLine("SELECT * FROM sys.service_message_types WHERE name = '" + this.FullName + "'");
            
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
                return this.ServiceBroker.MessageTypes[this.FullName].Script()[0];
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public void Create()
        {
            if (!this.ServiceBroker.MessageTypes.Contains(this.FullName))
            {
                // Create message types
                MessageType msgType = new MessageType(
                    this.ServiceBroker, this.FullName);

                msgType.MessageTypeValidation = this.MessageTypeValidation;

                if (msgType.MessageTypeValidation == MessageTypeValidation.XmlSchemaCollection)
                {
                    if(!this.ServiceBroker.Parent.XmlSchemaCollections.Contains
                        (this.ValidationXmlSchemaCollection) && ! String.IsNullOrEmpty(this.ValidationXmlSchemaCollectionSchema))
                    {
                        XmlSchemaCollection schema = new XmlSchemaCollection
                            (this.ServiceBroker.Parent, this.ValidationXmlSchemaCollection);
                        schema.Text = this.ValidationXmlSchemaCollectionSchema;
                        schema.Create();
                    }

                    msgType.ValidationXmlSchemaCollection = this.ValidationXmlSchemaCollection;
                }
                msgType.Create();
            }
        }

        public void Drop()
        {
            if (this.ServiceBroker.MessageTypes.Contains(this.FullName))
            {
                // Create message types
                MessageType msgType =
                    this.ServiceBroker.MessageTypes[this.FullName];

                msgType.Drop();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public void Alter()
        {
            MessageType msgType = null;
            if (this.ServiceBroker.MessageTypes.Contains(this.FullName))
            {
                // Create message types
                msgType = this.ServiceBroker.MessageTypes[this.FullName];

                msgType.MessageTypeValidation = this.MessageTypeValidation;

                if (msgType.MessageTypeValidation == MessageTypeValidation.XmlSchemaCollection)
                {
                    if (!String.IsNullOrEmpty(this.ValidationXmlSchemaCollection) ||
                    !String.IsNullOrEmpty(this.ValidationXmlSchemaCollectionSchema))
                    {
                        //Must drop and re-create
                        this.Drop();

                        //Re-create the contract
                        this.Create();
                    }
                }
                else
                {
                    msgType.Alter();
                }
            }
        }

        [Editor(typeof(ValidationSchemaEditor), typeof(UITypeEditor)), ReadOnly(true)]
        public string ValidationXmlSchemaCollectionSchema
        {
            set
            {
                m_ValidationXmlSchemaCollectionSchema = value;
            }
            get
            {
                return m_ValidationXmlSchemaCollectionSchema;
            }
        }

        [Editor(typeof(ValidationSchemaEditor), typeof(UITypeEditor)), ReadOnly(true)]
        public string ValidationXmlSchemaCollection
        {
            set
            {
                m_ValidationXmlSchemaCollection = value;
            }
            get
            {
                return m_ValidationXmlSchemaCollection;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public MessageTypeValidation MessageTypeValidation
        {
            get
            {
                return m_MessageTypeValidation;
            }
            set
            {
                m_MessageTypeValidation = value;
            }

        }        
    }
}
