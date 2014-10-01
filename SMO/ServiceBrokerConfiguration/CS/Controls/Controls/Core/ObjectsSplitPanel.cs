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
using System.Collections;
using Microsoft.SqlServer.MessageBox;
using System.Security.Permissions;

namespace Microsoft.Samples.SqlServer.Controls
{
    //Control to host EditObject and output textbox and output grid.
    //Contains a StripButton that calls IConfiguration methods such as Create and Drop
    public partial class ObjectsSplitPanel : UserControl
    {

        #region Private members and Constructor
        private ArrayList editObjects = new ArrayList();
        private int previousSplitterDistance;
        private SelectedGridItemChangedEventHandler onSelectedGridItemChanged;
        private PropertyValueChangedEventHandler onPropertyValueChanged;
        private EventHandler onImportScriptOK;
        
        const string OutputTextTableName = "OutputTextTable";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum operations
        {
            New, Show, Drop, Default
        }

        public ObjectsSplitPanel()
        {
            InitializeComponent();

        }
        #endregion

        #region UI Methods that call various IConfiguration methods.
        //Call Create method on an object instance that implements IConfiguration
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void CreateStripButton_Click(object sender, EventArgs e)
        {
            if (this.ActiveEditObject != null)
            {
                try
                {
                    this.ActiveEditObject.Configuration.Create();
                    this.outputTextBox.Text = this.ActiveEditObject.Configuration.SqlScript;

                    this.SetStripButtonState(ObjectsSplitPanel.operations.Show);
                }
                catch (Exception ex)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                    emb.Show(this);
                }
            }
        }

        //Call Alter method on an object instance that implements IConfiguration
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void AlterStripButton_Click(object sender, EventArgs e)
        {
            if (this.ActiveEditObject != null)
            {
                try
                {
                    this.ActiveEditObject.Configuration.Alter();

                    this.outputTextBox.Text = this.ActiveEditObject.Configuration.SqlScript;
                    this.outputDataGridView.AutoGenerateColumns = true;

                }
                catch (Exception ex)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                    emb.Show(this);
                }
            }
        }

        //Call Drop method on an object instance that implements IConfiguration
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,System.Windows.Forms.MessageBoxIcon)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void DropStripButton_Click(object sender, EventArgs e)
        {
            if (this.ActiveEditObject != null)
            {
                try
                {
                    if (MessageBox.Show
                    ("Drop this object?", "Drop Message",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk)
                    == DialogResult.OK)
                    {
                        this.ActiveEditObject.Configuration.Drop();
                        this.SetStripButtonState(ObjectsSplitPanel.operations.Drop);
                        this.RemoveAllObjects();
                    }

                }
                catch (Exception ex)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                    emb.Show(this);
                }
            }
        }

        //Call Export method on an object instance that implements IConfiguration
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveEditObject != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.outputTextBox.Text = this.ActiveEditObject.Configuration.Export(String.Empty);
                    DataSet ds = this.ActiveEditObject.Configuration.Query();
                    if (ds != null)
                        this.outputDataGridView.DataSource = ds.Tables[0];

                }
                catch (Exception ex)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                    emb.Show(this);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        
        //Query the object instance that implements IConfiguration
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void QueryStripButton_Click(object sender, EventArgs e)
        {
            if (this.ActiveEditObject != null)
            {
                try
                {
                    this.QueryObject(this.ActiveEditObject.Configuration);
                }
                catch (Exception ex)
                {
                    ExceptionMessageBox emb = new ExceptionMessageBox(ex);
                    emb.Show(this);
                }
            }
        }

        //Call Query method on an object instance that implements IConfiguration
        public void QueryObject(IConfiguration config)
        {
            DataSet ds = null;
            string output = "(no output)";

            if (config != null)
                ds = config.Query();

            if (ds != null)
            {
                //Tables[0] is for the DataGrid
                this.outputDataGridView.DataSource = ds.Tables[0];

                //An OutputTextTable DataTable is for the Output text box. Shows how a Query method could
                //output to multiple controls
                if (ds.Tables.Contains(OutputTextTableName))
                {
                    if (ds.Tables[OutputTextTableName].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (DataRow row in ds.Tables[OutputTextTableName].Rows)
                        {
                            sb.AppendLine(row[0].ToString());
                        }
                        output = sb.ToString();
                    }
                }
            }

            this.outputTextBox.Text = output;
        }    
        
        public void SetStripButtonState(operations operationType)
        {
            //Disable all and set default text
            this.CreateStripButton.Enabled = false;
            this.AlterStripButton.Enabled = false;
            this.DropStripButton.Enabled = false;
            this.QueryStripButton.Enabled = false;
            this.exportToolStripMenuItem.Enabled = false;

            if (operationType == operations.New)
            {
                //Enable/Disable Buttons
                CreateStripButton.Enabled = true;
            }
            else if (operationType == operations.Show)
            {
                this.AlterStripButton.Enabled = true;
                this.DropStripButton.Enabled = true;
                this.QueryStripButton.Enabled = true;
                this.exportToolStripMenuItem.Enabled = true;
            }

        }

        //Add the IConfiguration object to the panel
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public EditObject AddObject(string caption, IConfiguration configuration)
        {
            EditObject obj = this.AddObject(caption);
            obj.ObjectGrid.SelectedObject = configuration;
            obj.Configuration = configuration;
            obj.ObjectGrid.Width = obj.ObjectGrid.Width;
            return obj;
        }
        
        //Add an object to the panel
        public EditObject AddObject(string caption)
        {
            EditObject obj = new EditObject();
            
            obj.BackColor = Color.Transparent;
            obj.Caption = caption;
            obj.Dock = DockStyle.Top;
            obj.ContainerPanel = this;
           
            this.objectsContainer.Panel2.Controls.Add(obj);

            editObjects.Add(obj);

            this.ActiveEditObject = obj;

            return obj;
        }
        
        //Remove all objects from the panel. 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        public void RemoveAllObjects()
        {
            this.Tag = String.Empty;

            this.outputTextBox.Clear();
            this.outputDataGridView.DataSource = null;

            while(this.objectsContainer.Panel2.Controls.Count > 0)
            {
                this.objectsContainer.Panel2.Controls
                    .Remove(this.objectsContainer.Panel2.Controls[this.objectsContainer.Panel2.Controls.Count-1]);
            }

            EditObjectButton.Text = "Create a new object";
        }

#endregion
        
        #region Other User Interface Members
        private void EditObjectButton_Click(object sender, EventArgs e)
        {
            objectsContainer.Panel2Collapsed = !objectsContainer.Panel2Collapsed;
            if (objectsContainer.Panel2Collapsed)
            {
                EditObjectButton.ImageKey = "Plus.bmp";
                previousSplitterDistance = mainContainer.SplitterDistance;
                mainContainer.SplitterDistance = 0;
            }
            else
            {
                EditObjectButton.ImageKey = "Minus.bmp";

                mainContainer.SplitterDistance = previousSplitterDistance;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public event EventHandler ImportScriptOK
        {
            add
            {
                onImportScriptOK += value;
            }
            remove
            {
                onImportScriptOK -= value;
            }
        }

        public event SelectedGridItemChangedEventHandler SelectedGridItemChanged
        {
            add
            {
                onSelectedGridItemChanged += value;
            }
            remove
            {
                onSelectedGridItemChanged -= value;
            }
        }

        protected internal virtual void OnSelectedGridItemChanged(SelectedGridItemChangedEventArgs e)
        {
            if (onSelectedGridItemChanged != null)
            {
                onSelectedGridItemChanged(m_ActiveEditObject, e);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event PropertyValueChangedEventHandler PropertyValueChanged
        {
            add
            {
                onPropertyValueChanged += value;
            }
            remove
            {
                onPropertyValueChanged -= value;
            }
        }

        protected internal virtual void OnPropertyValueChanged(PropertyValueChangedEventArgs e)
        {
            if (onPropertyValueChanged != null)
            {
                onPropertyValueChanged(m_ActiveEditObject, e);
            }
        }

        private EditObject m_ActiveEditObject;
        internal EditObject ActiveEditObject
        {
            get
            {
                return m_ActiveEditObject;
            }
            set
            {
                m_ActiveEditObject = value;
            }
        }

        private bool m_SingleVisibleObject = true;
        [Browsable(true), DefaultValue(true)]
        public bool SingleVisibleObject
        {
            get
            {
                return m_SingleVisibleObject;
            }
            set
            {
                m_SingleVisibleObject = value;
            }
        }

        public void CollapseAll()
        {
            foreach (EditObject obj in this.editObjects)
            {
                obj.Collapse();
            }
        }

        public void ExpandAll()
        {
            foreach (EditObject obj in this.editObjects)
            {
                obj.Expand(200);
            }
        }

        public void HideObject(int index)
        {
            Control ctrl = this.objectsContainer.Panel2.Controls[index];
            ctrl.Visible = false;
        }

        public Control ShowObject(int index)
        {
            Control ctrl = this.objectsContainer.Panel2.Controls[index];
            ctrl.Visible = true;

            return ctrl;
        }

        private void EnableObjectButton_Click(object sender, EventArgs e)
        {
            EnableObjectButton.Checked = !EnableObjectButton.Checked;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Not implemented for this sample.
        }
#endregion         
    }
}
