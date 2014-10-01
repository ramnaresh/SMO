using System.Windows.Forms;
namespace Microsoft.Samples.SqlServer.Controls
{
    partial class EditObject
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.ToolStripItem.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditObject));
            this.ObjectImageList = new System.Windows.Forms.ImageList(this.components);
            this.expandCollapseButton = new System.Windows.Forms.Button();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.objectGrid = new System.Windows.Forms.PropertyGrid();
            this.editButton = new System.Windows.Forms.Button();
            this.EditMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openExistingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SingleView = new System.Windows.Forms.ToolStripMenuItem();
            this.editObjectContainer = new System.Windows.Forms.SplitContainer();
            this.GridPanel.SuspendLayout();
            this.EditMenu.SuspendLayout();
            this.editObjectContainer.Panel1.SuspendLayout();
            this.editObjectContainer.Panel2.SuspendLayout();
            this.editObjectContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ObjectImageList
            // 
            this.ObjectImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ObjectImageList.ImageStream")));
            this.ObjectImageList.TransparentColor = System.Drawing.Color.White;
            this.ObjectImageList.Images.SetKeyName(0, "Plus.bmp");
            this.ObjectImageList.Images.SetKeyName(1, "Minus.bmp");
            this.ObjectImageList.Images.SetKeyName(2, "Save.bmp");
            this.ObjectImageList.Images.SetKeyName(3, "PanelDropDown_Down.bmp");
            this.ObjectImageList.Images.SetKeyName(4, "PanelDropDown.bmp");
            // 
            // ExpandCollapseButton
            // 
            this.expandCollapseButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.expandCollapseButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.expandCollapseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.expandCollapseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.expandCollapseButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.expandCollapseButton.FlatAppearance.BorderSize = 0;
            this.expandCollapseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.expandCollapseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.expandCollapseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expandCollapseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expandCollapseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.expandCollapseButton.ImageKey = "(none)";
            this.expandCollapseButton.ImageList = this.ObjectImageList;
            this.expandCollapseButton.Location = new System.Drawing.Point(1, 1);
            this.expandCollapseButton.Margin = new System.Windows.Forms.Padding(1);
            this.expandCollapseButton.Name = "expandCollapseButton";
            this.expandCollapseButton.Size = new System.Drawing.Size(375, 23);
            this.expandCollapseButton.TabIndex = 0;
            this.expandCollapseButton.Text = "Button";
            this.expandCollapseButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.expandCollapseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.expandCollapseButton.UseVisualStyleBackColor = false;
            this.expandCollapseButton.MouseLeave += new System.EventHandler(this.ExpandCollapseButton_MouseLeave);
            this.expandCollapseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ExpandCollapseButton_MouseDown);
            this.expandCollapseButton.MouseEnter += new System.EventHandler(this.ExpandCollapseButton_MouseEnter);
            this.expandCollapseButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ExpandCollapseButton_MouseUp);
            // 
            // GridPanel
            // 
            this.GridPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.GridPanel.Controls.Add(this.objectGrid);
            this.GridPanel.Location = new System.Drawing.Point(1, 1);
            this.GridPanel.Margin = new System.Windows.Forms.Padding(1);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(398, 95);
            this.GridPanel.TabIndex = 9;
            // 
            // ObjectGrid
            // 
            this.objectGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectGrid.BackColor = System.Drawing.Color.White;
            this.objectGrid.HelpVisible = false;
            this.objectGrid.Location = new System.Drawing.Point(1, -1);
            this.objectGrid.Margin = new System.Windows.Forms.Padding(1);
            this.objectGrid.Name = "objectGrid";
            this.objectGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.objectGrid.Size = new System.Drawing.Size(396, 100);
            this.objectGrid.TabIndex = 7;
            this.objectGrid.ToolbarVisible = false;
            this.objectGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.ObjectGrid_SelectedGridItemChanged);
            this.objectGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ObjectGrid_PropertyValueChanged);
            this.objectGrid.SizeChanged += new System.EventHandler(this.ObjectGrid_SizeChanged);
            // 
            // EditButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.editButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.editButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.editButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editButton.ImageKey = "(none)";
            this.editButton.ImageList = this.ObjectImageList;
            this.editButton.Location = new System.Drawing.Point(376, 1);
            this.editButton.Margin = new System.Windows.Forms.Padding(1);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(23, 23);
            this.editButton.TabIndex = 0;
            this.editButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // EditMenu
            // 
            this.EditMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openExistingToolStripMenuItem,
            this.toolStripMenuItem2,
            this.expandToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.SingleView});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(163, 148);
            this.EditMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.EditMenu_Closed);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // openExistingToolStripMenuItem
            // 
            this.openExistingToolStripMenuItem.Name = "openExistingToolStripMenuItem";
            this.openExistingToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openExistingToolStripMenuItem.Text = "Open Existing ...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            this.expandToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.expandToolStripMenuItem.Text = "Collapse All";
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // SingleView
            // 
            this.SingleView.Checked = true;
            this.SingleView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SingleView.Name = "SingleView";
            this.SingleView.Size = new System.Drawing.Size(162, 22);
            this.SingleView.Text = "Single Object View";
            this.SingleView.Click += new System.EventHandler(this.SingleView_Click);
            // 
            // editObjectContainer
            // 
            this.editObjectContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editObjectContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.editObjectContainer.Location = new System.Drawing.Point(2, 0);
            this.editObjectContainer.Name = "editObjectContainer";
            this.editObjectContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // editObjectContainer.Panel1
            // 
            this.editObjectContainer.Panel1.Controls.Add(this.expandCollapseButton);
            this.editObjectContainer.Panel1.Controls.Add(this.editButton);
            // 
            // editObjectContainer.Panel2
            // 
            this.editObjectContainer.Panel2.Controls.Add(this.GridPanel);
            this.editObjectContainer.Size = new System.Drawing.Size(400, 124);
            this.editObjectContainer.SplitterDistance = 25;
            this.editObjectContainer.SplitterWidth = 1;
            this.editObjectContainer.TabIndex = 8;
            // 
            // EditObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.editObjectContainer);
            this.Name = "EditObject";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.Size = new System.Drawing.Size(402, 125);
            this.Enter += new System.EventHandler(this.EditObject_Enter);
            this.Leave += new System.EventHandler(this.EditObject_Leave);
            this.GridPanel.ResumeLayout(false);
            this.EditMenu.ResumeLayout(false);
            this.editObjectContainer.Panel1.ResumeLayout(false);
            this.editObjectContainer.Panel2.ResumeLayout(false);
            this.editObjectContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList ObjectImageList;
        private System.Windows.Forms.Button expandCollapseButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.PropertyGrid objectGrid;
        
        private System.Windows.Forms.ContextMenuStrip EditMenu;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openExistingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SingleView;
        private System.Windows.Forms.SplitContainer editObjectContainer;

        //ExpandCollapseButton
        public Button ExpandCollapseButton
        {
            get
            {
                return this.expandCollapseButton;
            }
        }

        //EditButton
        public Button EditButton
        {
            get
            {
                return this.editButton;
            }
        }

        //ObjectGrid
        public PropertyGrid ObjectGrid
        {
            get
            {
                return this.objectGrid;
            }
        }
    }
}
