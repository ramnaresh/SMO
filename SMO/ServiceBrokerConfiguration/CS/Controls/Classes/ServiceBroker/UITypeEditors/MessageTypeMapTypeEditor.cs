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
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Collections;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo.Broker;
using System.Security.Permissions;
using System.Globalization;

namespace Microsoft.Samples.SqlServer
{
    public class MessageTypeMapTypeEditor : UITypeEditor
	{
        private IWindowsFormsEditorService m_FormEditorService;

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"), PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, 
			IServiceProvider provider, object value)
		{
			object retvalue = value;
			
			//Get the editor service from the provider, to perform the dropdown.
			if (provider != null)
                m_FormEditorService = (IWindowsFormsEditorService)
					provider.GetService(typeof(IWindowsFormsEditorService));

            if (m_FormEditorService != null)
			{
                ServiceContractConfiguration contract = (ServiceContractConfiguration)context.Instance;

                MessageTypeMapControl editor =
                    new MessageTypeMapControl();

                FillList(contract, editor);

                m_FormEditorService.DropDownControl(editor);

                //File ListView with MessageTypeMappings
                contract.MessageTypeMappings.Clear();
                for (int i = 0; i < editor.MessageMapListView.Items.Count; i++)
                {
                    ListViewItem lvItem = editor.MessageMapListView.Items[i];
                    if (lvItem.Checked)
                    {
                        contract.MessageTypeMappings.Add(lvItem.SubItems[0].Text,
                            MessageSourceFromString(lvItem.SubItems[1].Text));
                    }
                }

                //Done with editor so dispose
                editor.Dispose();

                return contract.MessageTypeMappings;
			}

			return retvalue;
        }

        //Convert MessageSource string to type
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private MessageSource MessageSourceFromString(string messageSourceName)
        {
            MessageSource ms = MessageSource.Initiator;
            if (messageSourceName == MessageSource.Initiator.ToString())
                ms = MessageSource.Initiator;
            else if (messageSourceName == MessageSource.Target.ToString())
                ms = MessageSource.Target;
            else if (messageSourceName == MessageSource.InitiatorAndTarget.ToString())
                ms = MessageSource.InitiatorAndTarget;
            return ms;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void FillList(ServiceContractConfiguration contract, MessageTypeMapControl editor)
        {
            
            // Add data to the ListView.
            ListViewItem listviewitem;
            string messageSource;

            string baseUrn = contract.ServiceBroker.Parent.ExtendedProperties["BaseUrn"].Value.ToString();

            foreach (MessageType item in contract.ServiceBroker.MessageTypes)
            {
                if (item.Name.StartsWith(baseUrn + "messagetype", StringComparison.OrdinalIgnoreCase))
                {
                    //Default message source name
                    messageSource = "Initiator";
                    // Create ListView data.
                    listviewitem = new ListViewItem(item.Name);

                    if (contract.MessageTypeMappings.Contains(item.Name))
                    {
                        listviewitem.Checked = true;
                        messageSource = contract.MessageTypeMappings[item.Name].ToString();
                    }

                    listviewitem.SubItems.Add(messageSource);
                    editor.MessageMapListView.Items.Add(listviewitem);
                }
            }
        }
	}
}
