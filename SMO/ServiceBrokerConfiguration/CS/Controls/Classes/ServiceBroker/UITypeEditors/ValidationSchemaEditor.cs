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
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Collections;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Broker;
using System.Security.Permissions;

namespace Microsoft.Samples.SqlServer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class ValidationSchemaEditor : UITypeEditor
	{
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

        MessageTypeConfiguration service;
        ValidationSchemaForm form;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        public override object EditValue(ITypeDescriptorContext context, 
			IServiceProvider provider, object value)
		{
			//Always return a valid value.
			object retvalue = value;

			IWindowsFormsEditorService srv = null;
			
			//Get the forms editor service from the provider, to display the form.
			if (provider != null)
				srv = (IWindowsFormsEditorService)
					provider.GetService(typeof(IWindowsFormsEditorService));

			if (srv != null)
			{
				form = new ValidationSchemaForm();
                service = (MessageTypeConfiguration)context.Instance;

                form.SchemasComboBox.Items.Clear();
                form.SchemasComboBox.Items.Add("(none)");

                form.SchemasComboBox.SelectedIndexChanged += new System.EventHandler(this.SchemasComboBox_SelectedIndexChanged);

                IDictionaryEnumerator enumerator = service.Schemas.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    form.SchemasComboBox.Items.Add(enumerator.Key.ToString() );
                }

                foreach (XmlSchemaCollection schema in service.ServiceBroker.Parent.XmlSchemaCollections)
                {
                    form.SchemasComboBox.Items.Add(schema.Name);
                }

                if (String.IsNullOrEmpty(service.ValidationXmlSchemaCollection))
                    form.SchemasComboBox.Text = "(none)";
                else
                    form.SchemasComboBox.Text = service.ValidationXmlSchemaCollection;
                
                form.SchemaTexBox.Text = service.ValidationXmlSchemaCollectionSchema;

                if (srv.ShowDialog(form) == DialogResult.OK)
                    retvalue = form.SchemaTexBox.Text;
               
			}
			
			return retvalue;
		}

        private void SchemasComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form.SchemasComboBox.Text == "(none)")
            {
                form.SchemaTexBox.Text = String.Empty;
                service.ValidationXmlSchemaCollection = String.Empty;
                service.ValidationXmlSchemaCollectionSchema = String.Empty;
            }
            else
            {
                if (service.Schemas.Contains(form.SchemasComboBox.Text))
                {
                    form.SchemaTexBox.Text = service.Schemas[form.SchemasComboBox.Text].ToString();

                }
                else
                {
                    form.SchemaTexBox.Text =
                        service.ServiceBroker.Parent.XmlSchemaCollections[form.SchemasComboBox.Text].Text;
                }
                service.ValidationXmlSchemaCollection = form.SchemasComboBox.Text;
                service.ValidationXmlSchemaCollectionSchema = form.SchemaTexBox.Text;
            }
        }        
	}
}
