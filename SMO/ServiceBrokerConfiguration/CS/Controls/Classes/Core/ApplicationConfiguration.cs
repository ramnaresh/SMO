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
using controlProperties = Microsoft.Samples.SqlServer.Properties;

namespace Microsoft.Samples.SqlServer
{
    //Used to store application configuration properties within the current database. 
    //Class uses ExtendedProperties to store the export path in the database to leverage
    //SQL security.
    class ApplicationConfiguration : ConfigurationBase, IConfiguration
    {
        private string m_BaseUrn = "http://smo.sample/";
        //Default path for sample
        private string m_ScriptPath = Path.GetTempPath();
        private Hashtable m_Properties = new Hashtable();
        private string m_Name;

        const string BaseUrnPropertyName = "BaseUrn";
        const string ScriptPathPropertyName = "ScriptPath";

        public ApplicationConfiguration(ServiceBroker serviceBroker)
            : base(serviceBroker)
        {
            if (serviceBroker != null)
            {
                //Create internal properties list for Create method
                m_Properties.Add(BaseUrnPropertyName, m_BaseUrn);
                m_Properties.Add(ScriptPathPropertyName, m_ScriptPath);

                if (this.ServiceBroker.Parent.ExtendedProperties.Contains(BaseUrnPropertyName))
                {
                    m_BaseUrn = this.ServiceBroker.Parent.ExtendedProperties[BaseUrnPropertyName].Value.ToString();
                    m_Properties[BaseUrnPropertyName] = m_BaseUrn;
                }
                if (this.ServiceBroker.Parent.ExtendedProperties.Contains(ScriptPathPropertyName))
                {
                    m_ScriptPath = this.ServiceBroker.Parent.ExtendedProperties[ScriptPathPropertyName].Value.ToString();
                    m_Properties[ScriptPathPropertyName] = m_ScriptPath;
                }
            }
        }

        //Readonly for sample.
        [ReadOnly(true)]
        public new string BaseUrn
        {
            get
            {
                return m_BaseUrn;
            }
        }
        
        
        [Browsable(false)]
        public string FullName
        {
            get
            {
                return null;
            }
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public new string ScriptPath
        {
            get
            {
                return m_ScriptPath;
            }
            set
            {
                m_ScriptPath = value;
            }
        }

        [Browsable(false)]
        public string SqlScript
        {
            get
            {
                //Not implemented.
                return null;
            }
        }

        public void Create() 
        {
            ExtendedProperty prop;

            //Drop the properties
            this.Drop();

            IDictionaryEnumerator enumerator = m_Properties.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!this.ServiceBroker.Parent.ExtendedProperties.Contains(enumerator.Key.ToString()))
                {
                    prop = new ExtendedProperty(this.ServiceBroker.Parent,
                       enumerator.Key.ToString(), enumerator.Value.ToString());
                    prop.Create();
                }
            }    
        }

        public void Drop() 
        {
            ExtendedProperty prop;
            IDictionaryEnumerator enumerator = m_Properties.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (this.ServiceBroker.Parent.ExtendedProperties.Contains(enumerator.Key.ToString()))
                {
                    prop = this.ServiceBroker.Parent.ExtendedProperties[enumerator.Key.ToString()];
                    prop.Drop();
                }
            }
        }

        public void Alter() 
        {
            ExtendedProperty prop;
            IDictionaryEnumerator enumerator = m_Properties.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (this.ServiceBroker.Parent.ExtendedProperties.Contains(enumerator.Key.ToString()))
                {
                    prop = this.ServiceBroker.Parent.ExtendedProperties[enumerator.Key.ToString()];
                    if (prop.Value.ToString() != enumerator.Value.ToString())
                    {
                        prop.Value = enumerator.Value.ToString();
                        prop.Alter();
                    }
                }
            }
        }

        public string Export(string fileName)
        { 
            return "Not supported";
        }

        public void Import(string fileName)
        {
            //Not implemented.
        }

        public DataSet Query()
        {
            //Not implemented.
            return null;
        }


    }
}
