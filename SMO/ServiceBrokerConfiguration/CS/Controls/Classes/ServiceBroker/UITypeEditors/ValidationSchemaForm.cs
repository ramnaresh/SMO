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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Samples.SqlServer
{
	public class ValidationSchemaForm : System.Windows.Forms.Form
    {
        private ComboBox schemasComboBox;

        private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.TextBox schemaTexBox;
        private System.Windows.Forms.Label label1;
		

		private System.ComponentModel.Container components = null;

		public ValidationSchemaForm()
		{
			
			InitializeComponent();
		}

        public ComboBox SchemasComboBox
        {
            get
            {
                return this.schemasComboBox;
            }
        }

        public TextBox SchemaTexBox
        {
            get
            {
                return this.schemaTexBox;
            }
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
		{
            this.btnAccept = new System.Windows.Forms.Button();
            this.schemaTexBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.schemasComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAccept.Location = new System.Drawing.Point(415, 24);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(59, 24);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // schemaTexBox
            // 
            this.schemaTexBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.schemaTexBox.Location = new System.Drawing.Point(5, 51);
            this.schemaTexBox.Multiline = true;
            this.schemaTexBox.Name = "schemaTexBox";
            this.schemaTexBox.ReadOnly = true;
            this.schemaTexBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.schemaTexBox.Size = new System.Drawing.Size(404, 180);
            this.schemaTexBox.TabIndex = 1;
            this.schemaTexBox.Text = "(Select Schema)";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Validation Schema";
            // 
            // schemasComboBox
            // 
            this.schemasComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.schemasComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.schemasComboBox.FormattingEnabled = true;
            this.schemasComboBox.Location = new System.Drawing.Point(5, 24);
            this.schemasComboBox.Name = "schemasComboBox";
            this.schemasComboBox.Size = new System.Drawing.Size(404, 21);
            this.schemasComboBox.TabIndex = 3;
            // 
            // ValidationSchemaForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(486, 235);
            this.Controls.Add(this.schemasComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.schemaTexBox);
            this.Controls.Add(this.btnAccept);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ValidationSchemaForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Validation Schema";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnAccept_Click(object sender, System.EventArgs e)
		{
            Close();
		}
	}
}
