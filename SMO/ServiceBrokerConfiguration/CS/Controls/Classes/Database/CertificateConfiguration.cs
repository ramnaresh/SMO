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
using System.Text;
using System.Data;
using System.IO;

namespace Microsoft.Samples.SqlServer
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CertificateConfiguration : ConfigurationBase, IConfiguration
    {
		private string m_Name;
        private string m_Owner;
        private Uri m_Urn;
        private DateTime m_ExpirationDate;
        private DateTime m_StartDate;
        private string m_Issuer;
        private string m_Subject;
        private string m_EncryptionPassword;
        private Database database;

        private Object m_Parent;

        public CertificateConfiguration(ServiceBroker serviceBroker)
            : base(serviceBroker)
		{
            if (serviceBroker != null)
                this.database = serviceBroker.Parent;
        }
        
       public CertificateConfiguration(string fullName, ServiceBroker serviceBroker)
            : base(serviceBroker)
        {
            if (this.ServiceBroker.Parent.Certificates.Contains(fullName))
            {
                Certificate cert = this.ServiceBroker.Parent.Certificates[fullName];
                this.Name = cert.Name;
                this.Issuer = cert.Issuer;
                this.Owner = cert.Owner;
                this.StartDate = cert.StartDate;
                this.ExpirationDate = cert.ExpirationDate;
                this.Subject = cert.Subject;
            }
       }
    
        public string Export(string fileName)
        {
            Certificate cert = this.ServiceBroker.Parent.Certificates[this.Name];
            if (File.Exists(fileName))
                File.Delete(fileName);

            cert.Export(fileName);

            return "Certificate Exported";

        }

        [Browsable(false)]
        public Object Parent
        {
            get
            {
                return m_Parent;
            }
            set
            {
                m_Parent = value;
            }
        }
        public DataSet Query()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
            sb.AppendLine("select name, pvt_key_encryption_type_desc, is_active_for_begin_dialog, issuer_name from sys.certificates WHERE name = '" + this.FullName + "'");

            return this.ServiceBroker.Parent.Parent.ConnectionContext.ExecuteWithResults(sb.ToString());
        }


        [Browsable(false)]
        public string FullName
        {
            get
            {
                return m_Urn + m_Name; ;
            }
        }

        [Browsable(false)]
        public string SqlScript
        {
            get
            {
                return "Not implemented in this sample.";
            }
        }

        //Creates a certificate on current database
        public void Create()
        {
            if (!database.Certificates.Contains(this.Name))
            {
                // Create service contract
                Certificate cert = new Certificate(database, this.Name);
                cert.ActiveForServiceBrokerDialog = true;
                
                cert.StartDate = this.StartDate;
                cert.ExpirationDate = this.ExpirationDate;

                if (!String.IsNullOrEmpty(this.Owner) && this.Owner.ToString() != "(not used)")
                    cert.Owner = this.Owner;

                cert.Subject = this.Subject;
                cert.Create();
            }
        }

        public void Drop()
        {
            if (database.Certificates.Contains(this.Name))
            {
                database.Certificates[this.Name].Drop();
            }
        }

        public void Alter()
        {
            //Not implemented in this sample.
        }

        public void Import(string fileName)
        {
            //Not implemented in this sample.
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

        
        [Browsable(false)]
        public new Uri Urn
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

        [Browsable(false)]
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

        //Readonly for sample
        [ReadOnly(true)]
        public DateTime ExpirationDate
        {
            get
            {

                return m_ExpirationDate;
            }
            set
            {
                m_ExpirationDate = value;
            }
        }

        //Readonly for sample
        [ReadOnly(true)]
        public DateTime StartDate
        {
            get
            {

                return m_StartDate;
            }
            set
            {
                m_StartDate = value;
            }
        }

        [ReadOnly(true)]
        public string Issuer
        {
            get
            {

                return m_Issuer;
            }
            set
            {
                m_Issuer = value;
            }
        }

        [ReadOnly(true)]
        public string Subject
        {
            get
            {

                return m_Subject;
            }
            set
            {
                m_Subject = value;
            }
        }

        [Browsable(false)]
        public string EncryptionPassword
        {
            get
            {

                return m_EncryptionPassword;
            }
            set
            {
                m_EncryptionPassword = value;
            }
        }
    }
}
