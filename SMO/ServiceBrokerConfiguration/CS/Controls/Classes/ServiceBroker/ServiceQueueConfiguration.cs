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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Broker;
using System.Drawing.Design;
using System.Globalization;
using System.Data;
using System.Text;

namespace Microsoft.Samples.SqlServer 
{
    //Wrapper class for use within the PropertyGrid control
    public class ServiceQueueConfiguration : ConfigurationBase, IConfiguration
	{
		private string m_Name;
        private bool m_IsEnqueueEnabled;
        private ActivationExecutionContext m_ActivationExecutionContext =
            ActivationExecutionContext.Owner;
        private bool m_IsRetentionEnabled;
        private string m_IsActivationEnabled = "NotSet";
        private string m_ExecutionContextPrincipal = "guest";
        private string m_ProcedureName = "QueueProcedure";
        private short m_MaxReaders = 1;

        public ServiceQueueConfiguration(ServiceBroker serviceBroker): base(serviceBroker)
		{
            base.Urn = new Uri(base.Urn.ToString() + "queue/");
		}

        public ServiceQueueConfiguration(string fullName, ServiceBroker serviceBroker)
            : base(serviceBroker)
		{
            if (this.ServiceBroker.Queues.Contains(fullName))
            {
                ServiceQueue item = this.ServiceBroker.Queues[fullName];


                base.Urn = new Uri(base.Urn.ToString() + "queue/");

                m_Name = item.Name.Substring
                    (base.Urn.ToString().Length, item.Name.Length - base.Urn.ToString().Length);   
                
                m_IsActivationEnabled = item.IsActivationEnabled.ToString();
                m_IsRetentionEnabled = item.IsRetentionEnabled;
                m_IsEnqueueEnabled = item.IsEnqueueEnabled;
                m_ProcedureName = item.ProcedureName;               
                m_MaxReaders = item.MaxReaders;
                m_ActivationExecutionContext = item.ActivationExecutionContext;
            }
		}


        public string Export(string fileName)
        {
            //Export the T-SQL script
            base.Export(this.Name + ".Queue", this.SqlScript);

            return this.SqlScript;
        }


        public void Import(string fileName)
        {
            //Not implemented in sample.
        }

        public DataSet Query()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE " + this.ServiceBroker.Parent.Name);
            sb.AppendLine("SELECT * FROM sys.service_queues WHERE name = '" + this.FullName + "'");

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
                return this.ServiceBroker.Queues[this.FullName].Script()[0];
            }
        }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)")]
        public void Create()
        {
            if (!this.ServiceBroker.Queues.Contains(this.FullName))
            {
                // Create queue
                ServiceQueue queue = new ServiceQueue(
                    this.ServiceBroker, this.FullName, "dbo");

                this.FillQueueObject(queue);

                queue.Create();
            }
        }

        //Sample only creates a procedure name with an empty procedure body. You can use TSQL to create the
        //procedure script.
        private void CreateProcedure(string procedureName)
        {
            if (!this.ServiceBroker.Parent.StoredProcedures.Contains(procedureName))
            {
                StoredProcedure sproc = new StoredProcedure
                    (this.ServiceBroker.Parent, procedureName);
                sproc.TextMode = false;
                sproc.TextBody = String.Empty;
                sproc.Create();
            }

        }

        public void Drop()
        {
            if (this.ServiceBroker.Queues.Contains(this.FullName))
            {
                this.ServiceBroker.Queues[this.FullName].Drop();
            }
        }

        public void Alter()
        {
            ServiceQueue queue = null;

            if (this.ServiceBroker.Queues.Contains(this.FullName))
            {
                // Create queue
                queue = this.ServiceBroker.Queues[this.FullName];

                this.FillQueueObject(queue);

                queue.Alter();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToBoolean(System.String)")]
        private void FillQueueObject(ServiceQueue queue)
        {
            if (this.IsActivationEnabled != "NotSet")
            {
                if (!String.IsNullOrEmpty(this.ProcedureName))
                {
                    queue.ProcedureName = this.ProcedureName;
                }

                queue.IsActivationEnabled = Convert.ToBoolean(this.IsActivationEnabled);
                queue.ExecutionContextPrincipal = this.ExecutionContextPrincipal;
                if (queue.IsActivationEnabled)
                {
                    CreateProcedure(m_ProcedureName);
                }
            }

            queue.IsRetentionEnabled = this.IsRetentionEnabled;
            queue.IsEnqueueEnabled = this.IsEnqueueEnabled;

            if (this.ExecutionContextPrincipal != null)
                queue.ExecutionContextPrincipal = this.ExecutionContextPrincipal;
            if (this.MaxReaders > 0)
                queue.MaxReaders = this.MaxReaders;

            queue.ActivationExecutionContext = this.ActivationExecutionContext;
        }

		public bool IsEnqueueEnabled 
		{
			get 
			{
				return m_IsEnqueueEnabled;
			}
			set 
			{
				m_IsEnqueueEnabled = value;
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

        public ActivationExecutionContext ActivationExecutionContext 
		{
			get 
			{
                return m_ActivationExecutionContext;
			}
			set 
			{

                m_ActivationExecutionContext = value;
			}
		}

        public bool IsRetentionEnabled
        {
            get
            {
                return m_IsRetentionEnabled;
            }
            set
            {

                m_IsRetentionEnabled = value;
            }
        }

        [Editor(typeof(Microsoft.Samples.SqlServer.NotSetListBoxTypeEditor), typeof(UITypeEditor))]
        public string IsActivationEnabled
        {
            get
            {
                return m_IsActivationEnabled;
            }
            set
            {

                m_IsActivationEnabled = value;
            }
        }

        [Editor(typeof(Microsoft.Samples.SqlServer.OwnerNameListTypeEditor),
            typeof(UITypeEditor))]
        public string ExecutionContextPrincipal 
		{
			get 
			{
                return m_ExecutionContextPrincipal;
			}
			set 
			{

                m_ExecutionContextPrincipal = value;
			}
		}

        public string ProcedureName 
		{
			get 
			{
                return m_ProcedureName;
			}
			set 
			{

                m_ProcedureName = value;
			}
		}

        public short MaxReaders 
		{
			get 
			{
                return m_MaxReaders;
			}
			set 
			{

                m_MaxReaders = value;
			}
		}
	}
}
