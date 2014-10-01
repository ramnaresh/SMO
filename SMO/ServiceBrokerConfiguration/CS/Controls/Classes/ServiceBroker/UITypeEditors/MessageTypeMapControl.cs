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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Globalization;

namespace Microsoft.Samples.SqlServer
{
    public class MessageTypeMapControl : System.Windows.Forms.UserControl
    {
        private ListViewItem lvItem;
		private System.ComponentModel.Container components = null;
        
        private ComboBox ListViewComboBox;
        private Panel panel1;
        private Label label1;
        private ListView CustomListView;
		
		public MessageTypeMapControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

            // Create column headers for the data.
            ColumnHeader columnheader;
            columnheader = new ColumnHeader();
            columnheader.Text = "MessageType";
            columnheader.Width = 310;
            CustomListView.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Width = CustomListView.Width - 340;
            columnheader.Text = String.Format("MessageSource");
            CustomListView.Columns.Add(columnheader);
		}

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageTypeMapControl));
            this.CustomListView = new System.Windows.Forms.ListView();
            this.ListViewComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CustomListView
            // 
            this.CustomListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.CustomListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CustomListView.CheckBoxes = true;
            resources.ApplyResources(this.CustomListView, "CustomListView");
            this.CustomListView.FullRowSelect = true;
            this.CustomListView.Name = "CustomListView";
            this.CustomListView.View = System.Windows.Forms.View.Details;
            this.CustomListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.myListView1_MouseUp);
            this.CustomListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.myListView1_ColumnWidthChanging);
            this.CustomListView.MouseCaptureChanged += new System.EventHandler(this.myListView1_MouseCaptureChanged);
            // 
            // ListViewComboBox
            // 
            this.ListViewComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.ListViewComboBox, "ListViewComboBox");
            this.ListViewComboBox.FormattingEnabled = true;
            this.ListViewComboBox.Items.AddRange(new object[] {
            resources.GetString("ListViewComboBox.Items"),
            resources.GetString("ListViewComboBox.Items1"),
            resources.GetString("ListViewComboBox.Items2")});
            this.ListViewComboBox.Name = "ListViewComboBox";
            this.ListViewComboBox.Leave += new System.EventHandler(this.cbListViewCombo_Leave);
            this.ListViewComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbListViewCombo_KeyPress);
            this.ListViewComboBox.SelectedValueChanged += new System.EventHandler(this.cbListViewCombo_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // MessageTypeMapControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ListViewComboBox);
            this.Controls.Add(this.CustomListView);
            resources.ApplyResources(this, "$this");
            this.Name = "MessageTypeMapControl";
            this.Load += new System.EventHandler(this.MessageTypeMapControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        public ComboBox MessageSourceComboBox
        {
            get
            {
                return this.ListViewComboBox;
            }
        }

        public ListView MessageMapListView
        {
            get
            {
                return this.CustomListView;
            }
        }

        private void cbListViewCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            lvItem.SubItems[1].Text = this.ListViewComboBox.Text;

            // Hide the ComboBox.
            this.ListViewComboBox.Visible = false;
        }

        private void cbListViewCombo_Leave(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            if(this.ListViewComboBox.SelectedItem != null)
                lvItem.SubItems[1].Text = this.ListViewComboBox.Text;

            // Hide the ComboBox.
            this.ListViewComboBox.Visible = false;
        }

        private void cbListViewCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the user presses ESC.
            switch (e.KeyChar)
            {
                case (char)(int)Keys.Escape:
                    {
                        // Reset the original text value, and then hide the ComboBox.
                        this.ListViewComboBox.Text = lvItem.SubItems[1].Text;
                        this.ListViewComboBox.Visible = false;
                        break;
                    }

                case (char)(int)Keys.Enter:
                    {
                        // Hide the ComboBox.
                        this.ListViewComboBox.Visible = false;
                        break;
                    }
            }

        }
        
        private void myListView1_MouseUp(object sender, MouseEventArgs e)
        {
            // Get the item on the row that is clicked.
            lvItem = this.CustomListView.GetItemAt(e.X, e.Y);

            // Make sure that an item is clicked.
            if (lvItem != null)
            {
                // Get the bounds of the item that is clicked.
                Rectangle ClickedItem = lvItem.Bounds;

                // Verify that the column is completely scrolled off to the left.
                if ((ClickedItem.Left + this.CustomListView.Columns[0].Width) < 0)
                {
                    // If the cell is out of view to the left, do nothing.
                    return;
                }

                 // Verify that the column is partially scrolled off to the left.
                else if (ClickedItem.Left < 0)
                {
                    // Determine if column extends beyond right side of ListView.
                    if ((ClickedItem.Left + this.CustomListView.Columns[0].Width) > this.CustomListView.Width)
                    {
                        // Set width of column to match width of ListView.
                        ClickedItem.Width = this.CustomListView.Width;
                        ClickedItem.X = 0;
                    }
                    else
                    {
                        // Right side of cell is in view.
                        ClickedItem.Width = this.CustomListView.Columns[0].Width + ClickedItem.Left;
                        ClickedItem.X = 2;
                    }
                }
                else if (this.CustomListView.Columns[0].Width > this.CustomListView.Width)
                {
                    ClickedItem.Width = this.CustomListView.Width;
                }
                else
                {
                    ClickedItem.Width = this.CustomListView.Columns[0].Width;
                    ClickedItem.X = 2;
                }

                // Adjust the top to account for the location of the ListView.
                ClickedItem.Y += this.CustomListView.Top;
                ClickedItem.X += this.CustomListView.Left;

                // Assign calculated bounds to the ComboBox.
                this.ListViewComboBox.Bounds = ClickedItem;

                // Set text for ComboBox to match the item that is clicked.
                this.ListViewComboBox.SelectedItem = lvItem.SubItems[1].Text;

                this.ListViewComboBox.Left = this.CustomListView.Columns[0].Width + 4;
                this.ListViewComboBox.Width = this.CustomListView.Columns[1].Width;
                this.ListViewComboBox.BringToFront();
                this.ListViewComboBox.Visible = true;
                this.ListViewComboBox.Focus();
            }

        }

        private void myListView1_MouseCaptureChanged(object sender, EventArgs e)
        {
            ListViewComboBox.Visible = false;
        }

        private void myListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            ListViewComboBox.Visible = false;
        }

        private void MessageTypeMapControl_Load(object sender, EventArgs e)
        {
            ListViewComboBox.Visible = false;
        }
        
	}
}