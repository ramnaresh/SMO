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
    public class QueueNameListTypeEditor : UITypeEditor
    {
        #region Private Member Variables
        private IWindowsFormsEditorService m_FormEditorService;
        #endregion

        [PermissionSet(SecurityAction.LinkDemand, Name="FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(context);
        }

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
                    ListBox editControl = new ListBox();
                    editControl.Click += new EventHandler(ListClick);

                    editControl.ScrollAlwaysVisible = false;
                    editControl.Height = 70;
                    editControl.Width = 250;

                    editControl.BorderStyle = BorderStyle.None;
                    
                    // modify the control properties
                    FillInList(context, editControl);

                    // editor service renders control and manages control events
                    this.m_FormEditorService.DropDownControl(editControl);

                    if (editControl.SelectedItem == null)
                        return service.QueueName;
                    else
                        // return the updated newValue
                        return editControl.SelectedItem;
                }
                finally
                {
                    this.m_FormEditorService = null;
                }
            else
                return value;

        }

        private void ListClick(object sender, EventArgs args)
        {
            this.m_FormEditorService.CloseDropDown();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void FillInList(ITypeDescriptorContext context, ListBox editControl)
        {
            BrokerServiceConfiguration service = (BrokerServiceConfiguration)context.Instance;
            editControl.Items.Clear();

            foreach (ServiceQueue item in service.ServiceBroker.Queues)
            {
                editControl.Items.Add(String.Format(CultureInfo.InvariantCulture, item.Name));
            }
            
        }
    }
}

