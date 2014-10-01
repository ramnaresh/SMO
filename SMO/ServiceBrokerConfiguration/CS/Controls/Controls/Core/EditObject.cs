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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Samples.SqlServer;

namespace Microsoft.Samples.SqlServer.Controls
{
    //Container control for PropertyGrid to edit an object that implements IConfiguration
    public partial class EditObject : UserControl
    {
        private bool m_LoadObjectOnExpand = false;

        public EditObject()
        {
            this.Initialize();
        }

        public EditObject(IConfiguration configuration)
        {
            this.Initialize();
            this.m_Configuration = configuration;
        }

        private void Initialize()
        {
            InitializeComponent();
        }

        public bool LoadObjectOnExpand
        {
            get
            {
                return m_LoadObjectOnExpand;
            }
            set
            {
                m_LoadObjectOnExpand = value;
            }
        }


        private bool m_ButtonVisible;
        public bool ButtonVisible
        {
            get
            {
                return m_ButtonVisible;
            }
            set
            {
                m_ButtonVisible = value;
                this.editObjectContainer.Panel1Collapsed = !m_ButtonVisible;
            }
        }

        private IConfiguration m_Configuration;
        [Browsable(false)]
        public IConfiguration Configuration
        {
            get
            {
                return m_Configuration;
            }
            set
            {
                m_Configuration = value;
            }
        }

        private string m_Caption;
        [Browsable(true)]
        public string Caption
        {
            get
            {
                return m_Caption;
            }
            set
            {
                m_Caption = value;
                ExpandCollapseButton.Text = m_Caption;
            }
        }

        private bool m_Expanded;
        [Browsable(true)]
        public bool Expanded
        {
            get
            {
                return m_Expanded;
            }
            set
            {
                m_Expanded = value;
            }
        }

        public int CollapsedHeight
        {
            get
            {
                return ExpandCollapseButton.Height + 2;
            }
        }

        //Expand the ContainerPanel
        public void Expand(int height)
        {
            if (this.ContainerPanel != null)
            {
                if (this.ContainerPanel.SingleVisibleObject)
                {
                    this.ContainerPanel.CollapseAll();
                }
            }
            ExpandCollapseButton.ImageKey = "Minus.bmp";

            ObjectGrid.Height = height ;
            ObjectGrid.Width = GridPanel.Width ;
            this.Height = height;

            this.Expanded = true;
            ExpandCollapseButton.Tag = "1";
            this.ScrollIntoView();

        }

        private ObjectsSplitPanel m_ContainerPanel;
        public ObjectsSplitPanel ContainerPanel
        {
            get
            {
                return m_ContainerPanel;
            }
            set
            {
                m_ContainerPanel = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void ScrollIntoView()
        {
            if (this.ContainerPanel != null)
            {
                SplitterPanel panel = (SplitterPanel)this.Parent;
                panel.ScrollControlIntoView(this);
            }
        }

        public void Collapse()
        {
            ExpandCollapseButton.Tag = "0";
            this.Height = this.CollapsedHeight;

            ExpandCollapseButton.ImageKey = "Plus.bmp";
          
            this.Expanded = false;
        }

        private void ExpandCollapseButton_MouseEnter(object sender, EventArgs e)
        {
            ExpandCollapseButton.ForeColor = Color.Blue;
        }

        private void ExpandCollapseButton_MouseLeave(object sender, EventArgs e)
        {
            ExpandCollapseButton.ForeColor = Color.Black;
        }

        private void ExpandCollapseButton_MouseDown(object sender, MouseEventArgs e)
        {
            ExpandCollapseButton.ForeColor = Color.Red;
        }

        private void ExpandCollapseButton_MouseUp(object sender, MouseEventArgs e)
        {
            ExpandCollapseButton.ForeColor = Color.Blue;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditButton.ImageKey = "PanelDropDown_Down.bmp";

            Rectangle rect = EditButton.RectangleToScreen(EditButton.ClientRectangle);
            EditMenu.Show(rect.X - (EditMenu.Width - EditButton.Width) - 4, rect.Y + (EditButton.Height - 6));
        }
        
        private void HideEditButton()
        {
            EditButton.ImageKey = String.Empty;
        }
        private void ShowEditButton()
        {
            EditButton.ImageKey = "PanelDropDown.bmp";
        }

        private void EditMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.ShowEditButton();
        }

        private void EditObject_Enter(object sender, EventArgs e)
        {
            this.ShowEditButton();
            this.ScrollIntoView();
            if (this.ContainerPanel != null)
            {
                this.ContainerPanel.ActiveEditObject = this;
            }
           
        }

        private void EditObject_Leave(object sender, EventArgs e)
        {
            this.HideEditButton();
        }

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ContainerPanel != null)
                this.ContainerPanel.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ContainerPanel != null)
            {
                //Ignore SingleObjectView
                bool check = this.ContainerPanel.SingleVisibleObject;
                this.ContainerPanel.SingleVisibleObject = false;
                this.ContainerPanel.ExpandAll();
                this.ContainerPanel.SingleVisibleObject = check;
            }
        }

        private void SingleView_Click(object sender, EventArgs e)
        {
            SingleView.Checked = !SingleView.Checked;
            if (this.ContainerPanel != null)
                this.ContainerPanel.SingleVisibleObject = SingleView.Checked;
        }

        private void ObjectGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            this.ContainerPanel.OnSelectedGridItemChanged(e);
        }

        private void ObjectGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.ContainerPanel.OnPropertyValueChanged(e);
        }

        private void ObjectGrid_SizeChanged(object sender, EventArgs e)
        {
            this.Height = ObjectGrid.Height + 30;
            GridPanel.Height = ObjectGrid.Height;
        }  
    }
}
