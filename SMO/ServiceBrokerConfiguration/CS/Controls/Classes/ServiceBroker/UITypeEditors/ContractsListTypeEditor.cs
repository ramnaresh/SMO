/*=====================================================================
  This file is part of Microsoft SQL Server Code Samples.
  
  Copyright (C) Microsoft Corporation.  All rights reserved.

 This source code is intended only as a supplement to Microsoft
 Development Tools and/or on-line documentation.  See these other
 materials for detailed information regarding Microsoft code samples.

 THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
 KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 PARTICULAR PURPOSE.
=====================================================================*/

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using System.Security.Permissions;
using Microsoft.SqlServer.Management.Smo.Broker;
using System.Globalization;

namespace Microsoft.Samples.SqlServer
{
    public class ContractsListTypeEditor : UITypeEditor
    {
        #region Private Member Variables
        private IWindowsFormsEditorService m_FormEditorService;
        #endregion

        //Create a DropDown style UITypeEditor 
        [PermissionSet(SecurityAction.LinkDemand, Name="FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(context);
        }

       
        //Show CheckedListBox and return value to PropertyGrid
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"),
        PermissionSet(SecurityAction.LinkDemand, Name="FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null
               && context.Instance != null
               && provider != null)
                try
                {
                    BrokerServiceConfiguration service = (BrokerServiceConfiguration)context.Instance;
                    // get the editor service
                    this.m_FormEditorService = (IWindowsFormsEditorService)
                       provider.GetService(typeof(IWindowsFormsEditorService));

                    // create the control for the UI
                    CheckedListBox editControl = new CheckedListBox();
                    editControl.ScrollAlwaysVisible = false;
                    editControl.Height = 70;
                    editControl.Width = 250;

                    editControl.BorderStyle = BorderStyle.None;
                    
                    // modify the control properties
                    FillInText(context, provider, editControl);

                    // editor service renders control and manages control events
                    this.m_FormEditorService.DropDownControl(editControl);

                    service.ContractNames.Clear();
                    foreach (object item in editControl.CheckedItems)
                    {
                        service.ContractNames.Add(item.ToString());
                    }

                    // return the updated newValue
                    return service.ContractNames;
                }
                finally
                {
                    this.m_FormEditorService = null;
                }
            else
                return value;

        }
        

        //Fill ListBox with contract names
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "provider"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public void FillInText(ITypeDescriptorContext context, IServiceProvider provider, CheckedListBox editControl)
        {
            BrokerServiceConfiguration service = (BrokerServiceConfiguration)context.Instance;
            string baseUrn = service.ServiceBroker.Parent.ExtendedProperties["BaseUrn"].Value.ToString();
            
            foreach (ServiceContract item in service.ServiceBroker.ServiceContracts)
            {
                if (item.Name.StartsWith(baseUrn + "contract", StringComparison.OrdinalIgnoreCase))
                {
                    bool check = false;
                    if (service.ContractNames != null &&
                        service.ContractNames.Contains(item.Name))
                    {
                        check = true;
                    }
                    editControl.Items.Add(item.Name, check);
                }
            }            
        }
    }
}

