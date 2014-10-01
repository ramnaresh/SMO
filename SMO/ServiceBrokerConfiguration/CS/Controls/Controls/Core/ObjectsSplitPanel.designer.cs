using System.Windows.Forms;

namespace Microsoft.Samples.SqlServer.Controls
{
    partial class ObjectsSplitPanel
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.ToolStripItem.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectsSplitPanel));
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.objectsContainer = new System.Windows.Forms.SplitContainer();
            this.EditPanel = new System.Windows.Forms.Panel();
            this.editObjectButton = new System.Windows.Forms.Button();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.editToolStrip = new System.Windows.Forms.ToolStrip();
            this.newStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.createStripButton = new System.Windows.Forms.ToolStripButton();
            this.AlterStripButton = new System.Windows.Forms.ToolStripButton();
            this.DropStripButton = new System.Windows.Forms.ToolStripButton();
            this.queryStripButton = new System.Windows.Forms.ToolStripButton();
            this.EnableObjectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.navigationButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.ScriptStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OutputContainer = new System.Windows.Forms.SplitContainer();
            this.OutputPanel = new System.Windows.Forms.Panel();
            this.OutputButton = new System.Windows.Forms.Button();
            this.outputTabControl = new System.Windows.Forms.TabControl();
            this.TextBoxTabPage = new System.Windows.Forms.TabPage();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.DataGridTabPage = new System.Windows.Forms.TabPage();
            this.outputDataGridView = new System.Windows.Forms.DataGridView();
            this.newButtonImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.objectsContainer.Panel1.SuspendLayout();
            this.objectsContainer.SuspendLayout();
            this.EditPanel.SuspendLayout();
            this.editToolStrip.SuspendLayout();
            this.OutputContainer.Panel1.SuspendLayout();
            this.OutputContainer.Panel2.SuspendLayout();
            this.OutputContainer.SuspendLayout();
            this.OutputPanel.SuspendLayout();
            this.outputTabControl.SuspendLayout();
            this.TextBoxTabPage.SuspendLayout();
            this.DataGridTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainContainer.Panel1
            // 
            this.mainContainer.Panel1.Controls.Add(this.objectsContainer);
            this.mainContainer.Panel1MinSize = 50;
            // 
            // mainContainer.Panel2
            // 
            this.mainContainer.Panel2.AutoScroll = true;
            this.mainContainer.Panel2.Controls.Add(this.OutputContainer);
            this.mainContainer.Panel2MinSize = 23;
            this.mainContainer.Size = new System.Drawing.Size(342, 505);
            this.mainContainer.SplitterDistance = 330;
            this.mainContainer.TabIndex = 0;
            // 
            // objectsContainer
            // 
            this.objectsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectsContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.objectsContainer.IsSplitterFixed = true;
            this.objectsContainer.Location = new System.Drawing.Point(0, 0);
            this.objectsContainer.Name = "objectsContainer";
            this.objectsContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // objectsContainer.Panel1
            // 
            this.objectsContainer.Panel1.Controls.Add(this.EditPanel);
            this.objectsContainer.Panel1.Controls.Add(this.editToolStrip);
            this.objectsContainer.Panel1MinSize = 45;
            // 
            // objectsContainer.Panel2
            // 
            this.objectsContainer.Panel2.AutoScroll = true;
            this.objectsContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.objectsContainer.Panel2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.objectsContainer.Size = new System.Drawing.Size(342, 330);
            this.objectsContainer.SplitterDistance = 46;
            this.objectsContainer.SplitterWidth = 1;
            this.objectsContainer.TabIndex = 20;
            // 
            // EditPanel
            // 
            this.EditPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.EditPanel.Controls.Add(this.editObjectButton);
            this.EditPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.EditPanel.Location = new System.Drawing.Point(0, 25);
            this.EditPanel.Name = "EditPanel";
            this.EditPanel.Size = new System.Drawing.Size(342, 23);
            this.EditPanel.TabIndex = 19;
            
            // 
            // editObjectButton
            // 
            this.editObjectButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editObjectButton.BackColor = System.Drawing.Color.Transparent;
            this.editObjectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.editObjectButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editObjectButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.editObjectButton.FlatAppearance.BorderSize = 0;
            this.editObjectButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.editObjectButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.editObjectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editObjectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editObjectButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editObjectButton.ImageKey = "Minus.bmp";
            this.editObjectButton.ImageList = this.ImageList;
            this.editObjectButton.Location = new System.Drawing.Point(0, 0);
            this.editObjectButton.Margin = new System.Windows.Forms.Padding(1);
            this.editObjectButton.Name = "editObjectButton";
            this.editObjectButton.Size = new System.Drawing.Size(341, 23);
            this.editObjectButton.TabIndex = 1;
            this.editObjectButton.Text = "Create a new object";
            this.editObjectButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.editObjectButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.editObjectButton.UseVisualStyleBackColor = false;
            this.editObjectButton.Click += new System.EventHandler(this.EditObjectButton_Click);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.White;
            this.ImageList.Images.SetKeyName(0, "Plus.bmp");
            this.ImageList.Images.SetKeyName(1, "Minus.bmp");
            // 
            // editToolStrip
            // 
            this.editToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.editToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.editToolStrip.Enabled = false;
            this.editToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.editToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStripButton,
            this.createStripButton,
            this.AlterStripButton,
            this.DropStripButton,
            this.queryStripButton,
            this.EnableObjectButton,
            this.toolStripSeparator4,
            this.navigationButton,
            this.ScriptStripButton});
            this.editToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.editToolStrip.Location = new System.Drawing.Point(0, 0);
            this.editToolStrip.Name = "editToolStrip";
            this.editToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.editToolStrip.Size = new System.Drawing.Size(342, 25);
            this.editToolStrip.Stretch = true;
            this.editToolStrip.TabIndex = 18;
            this.editToolStrip.Text = "toolStrip1";
            // 
            // newStripButton
            // 
            this.newStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NewStripButton.Image")));
            this.newStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newStripButton.Name = "newStripButton";
            this.newStripButton.Size = new System.Drawing.Size(41, 22);
            this.newStripButton.Text = "New";
            // 
            // CreateStripButton
            // 
            this.createStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.createStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CreateStripButton.Image")));
            this.createStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createStripButton.Name = "createStripButton";
            this.createStripButton.Size = new System.Drawing.Size(44, 22);
            this.createStripButton.Text = "Create";
            this.createStripButton.Click += new System.EventHandler(this.CreateStripButton_Click);
            // 
            // AlterStripButton
            // 
            this.AlterStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AlterStripButton.Image = ((System.Drawing.Image)(resources.GetObject("AlterStripButton.Image")));
            this.AlterStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AlterStripButton.Name = "AlterStripButton";
            this.AlterStripButton.Size = new System.Drawing.Size(34, 22);
            this.AlterStripButton.Text = "Alter";
            this.AlterStripButton.Click += new System.EventHandler(this.AlterStripButton_Click);
            // 
            // DropStripButton
            // 
            this.DropStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DropStripButton.Image = ((System.Drawing.Image)(resources.GetObject("DropStripButton.Image")));
            this.DropStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DropStripButton.Name = "DropStripButton";
            this.DropStripButton.Size = new System.Drawing.Size(34, 22);
            this.DropStripButton.Text = "Drop";
            this.DropStripButton.Click += new System.EventHandler(this.DropStripButton_Click);
            // 
            // QueryStripButton
            // 
            this.queryStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.queryStripButton.Image = ((System.Drawing.Image)(resources.GetObject("QueryStripButton.Image")));
            this.queryStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.queryStripButton.Name = "queryStripButton";
            this.queryStripButton.Size = new System.Drawing.Size(41, 22);
            this.queryStripButton.Text = "Query";
            this.queryStripButton.Click += new System.EventHandler(this.QueryStripButton_Click);
            // 
            // EnableObjectButton
            // 
            this.EnableObjectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EnableObjectButton.Enabled = false;
            this.EnableObjectButton.Image = ((System.Drawing.Image)(resources.GetObject("EnableObjectButton.Image")));
            this.EnableObjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EnableObjectButton.Name = "EnableObjectButton";
            this.EnableObjectButton.Size = new System.Drawing.Size(43, 22);
            this.EnableObjectButton.Text = "Enable";
            this.EnableObjectButton.Visible = false;
            this.EnableObjectButton.Click += new System.EventHandler(this.EnableObjectButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // NavigationButton
            // 
            this.navigationButton.Name = "navigationButton";
            this.navigationButton.Size = new System.Drawing.Size(71, 22);
            this.navigationButton.Text = "Navigation";
            this.navigationButton.Visible = false;
            // 
            // ScriptStripButton
            // 
            this.ScriptStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ScriptStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem});
            this.ScriptStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ScriptStripButton.Image")));
            this.ScriptStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScriptStripButton.Name = "ScriptStripButton";
            this.ScriptStripButton.Size = new System.Drawing.Size(47, 17);
            this.ScriptStripButton.Text = "Script";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Visible = false;
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // OutputContainer
            // 
            this.OutputContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.OutputContainer.IsSplitterFixed = true;
            this.OutputContainer.Location = new System.Drawing.Point(0, 0);
            this.OutputContainer.Name = "OutputContainer";
            this.OutputContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // OutputContainer.Panel1
            // 
            this.OutputContainer.Panel1.Controls.Add(this.OutputPanel);
            // 
            // OutputContainer.Panel2
            // 
            this.OutputContainer.Panel2.Controls.Add(this.outputTabControl);
            this.OutputContainer.Size = new System.Drawing.Size(342, 171);
            this.OutputContainer.SplitterDistance = 25;
            this.OutputContainer.SplitterWidth = 1;
            this.OutputContainer.TabIndex = 0;
            // 
            // OutputPanel
            // 
            this.OutputPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.OutputPanel.Controls.Add(this.OutputButton);
            this.OutputPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OutputPanel.Location = new System.Drawing.Point(0, 0);
            this.OutputPanel.Name = "OutputPanel";
            this.OutputPanel.Size = new System.Drawing.Size(342, 23);
            this.OutputPanel.TabIndex = 1;
            // 
            // OutputButton
            // 
            this.OutputButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputButton.BackColor = System.Drawing.Color.Transparent;
            this.OutputButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OutputButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OutputButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.OutputButton.FlatAppearance.BorderSize = 0;
            this.OutputButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.OutputButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.OutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OutputButton.ImageKey = "Minus.bmp";
            this.OutputButton.Location = new System.Drawing.Point(0, 0);
            this.OutputButton.Margin = new System.Windows.Forms.Padding(1);
            this.OutputButton.Name = "OutputButton";
            this.OutputButton.Size = new System.Drawing.Size(341, 23);
            this.OutputButton.TabIndex = 1;
            this.OutputButton.Text = "Output";
            this.OutputButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.OutputButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.OutputButton.UseVisualStyleBackColor = false;
            
            // 
            // outputTabControl
            // 
            this.outputTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTabControl.Controls.Add(this.TextBoxTabPage);
            this.outputTabControl.Controls.Add(this.DataGridTabPage);
            this.outputTabControl.Location = new System.Drawing.Point(3, 1);
            this.outputTabControl.Multiline = true;
            this.outputTabControl.Name = "outputTabControl";
            this.outputTabControl.SelectedIndex = 0;
            this.outputTabControl.Size = new System.Drawing.Size(339, 144);
            this.outputTabControl.TabIndex = 0;
            // 
            // TextBoxTabPage
            // 
            this.TextBoxTabPage.Controls.Add(this.outputTextBox);
            this.TextBoxTabPage.Location = new System.Drawing.Point(4, 22);
            this.TextBoxTabPage.Name = "TextBoxTabPage";
            this.TextBoxTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TextBoxTabPage.Size = new System.Drawing.Size(331, 118);
            this.TextBoxTabPage.TabIndex = 0;
            this.TextBoxTabPage.Text = "Text";
            this.TextBoxTabPage.UseVisualStyleBackColor = true;
            // 
            // outputTextbox
            // 
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextbox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(325, 112);
            this.outputTextBox.TabIndex = 0;
            // 
            // DataGridTabPage
            // 
            this.DataGridTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.DataGridTabPage.Controls.Add(this.outputDataGridView);
            this.DataGridTabPage.Location = new System.Drawing.Point(4, 22);
            this.DataGridTabPage.Name = "DataGridTabPage";
            this.DataGridTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DataGridTabPage.Size = new System.Drawing.Size(331, 118);
            this.DataGridTabPage.TabIndex = 1;
            this.DataGridTabPage.Text = "Grid";
            // 
            // outputDataGridView
            // 
            this.outputDataGridView.AllowUserToAddRows = false;
            this.outputDataGridView.AllowUserToDeleteRows = false;
            this.outputDataGridView.AllowUserToResizeRows = false;
            this.outputDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.outputDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.outputDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.outputDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.outputDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.outputDataGridView.Location = new System.Drawing.Point(3, 3);
            this.outputDataGridView.Name = "outputDataGridView";
            this.outputDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.outputDataGridView.RowHeadersVisible = false;
            this.outputDataGridView.Size = new System.Drawing.Size(325, 112);
            this.outputDataGridView.TabIndex = 0;
            // 
            // NewButtonImageList
            // 
            this.newButtonImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.newButtonImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.newButtonImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ObjectsSplitPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainContainer);
            this.Name = "ObjectsSplitPanel";
            this.Size = new System.Drawing.Size(342, 505);
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel2.ResumeLayout(false);
            this.mainContainer.ResumeLayout(false);
            this.objectsContainer.Panel1.ResumeLayout(false);
            this.objectsContainer.Panel1.PerformLayout();
            this.objectsContainer.ResumeLayout(false);
            this.EditPanel.ResumeLayout(false);
            this.editToolStrip.ResumeLayout(false);
            this.editToolStrip.PerformLayout();
            this.OutputContainer.Panel1.ResumeLayout(false);
            this.OutputContainer.Panel2.ResumeLayout(false);
            this.OutputContainer.ResumeLayout(false);
            this.OutputPanel.ResumeLayout(false);
            this.outputTabControl.ResumeLayout(false);
            this.TextBoxTabPage.ResumeLayout(false);
            this.TextBoxTabPage.PerformLayout();
            this.DataGridTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
       
        private System.Windows.Forms.SplitContainer mainContainer;
        private System.Windows.Forms.ImageList newButtonImageList;
        private System.Windows.Forms.ToolStripDropDownButton navigationButton;
        private System.Windows.Forms.SplitContainer objectsContainer;

        private System.Windows.Forms.ToolStrip editToolStrip;
        private System.Windows.Forms.TabControl outputTabControl;
        private System.Windows.Forms.DataGridView outputDataGridView;
        private System.Windows.Forms.TextBox outputTextBox;

        private System.Windows.Forms.ToolStripButton AlterStripButton;
        private System.Windows.Forms.ToolStripButton createStripButton;

        private System.Windows.Forms.ToolStripButton DropStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        
        private System.Windows.Forms.Panel OutputPanel;
        private System.Windows.Forms.Button OutputButton;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.Button editObjectButton;
        private System.Windows.Forms.Panel EditPanel;
        private System.Windows.Forms.SplitContainer OutputContainer;
        private System.Windows.Forms.ToolStripDropDownButton newStripButton;
        private System.Windows.Forms.ToolStripButton EnableObjectButton;
        
        private System.Windows.Forms.ToolStripDropDownButton ScriptStripButton;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        
        private System.Windows.Forms.TabPage TextBoxTabPage;
        private System.Windows.Forms.TabPage DataGridTabPage;
        private System.Windows.Forms.ToolStripButton queryStripButton;

        public ToolStripButton CreateStripButton
        {
            get
            {
                return this.createStripButton;
            }
        }

        //EditObjectButton
        public Button EditObjectButton
        {
            get
            {
                return this.editObjectButton;
            }
        }

        public SplitContainer MainContainer
        {
            get
            {
                return this.mainContainer;
            }
        }

        public ImageList NewButtonImageList
        {
            get
            {
                return this.newButtonImageList;
            }
        }

        public ToolStripDropDownButton NavigationButton
        {
            get
            {
                return this.navigationButton;
            }
        }

        public SplitContainer ObjectsContainer
        {
            get
            {
                return this.objectsContainer;
            }
        }

        public ToolStrip EditToolStrip
        {
            get
            {
                return this.editToolStrip;
            }
        }

        public TabControl OutputTabControl
        {
            get
            {
                return this.outputTabControl;
            }
        }
        public DataGridView OutputDataGridView
        {
            get
            {
                return this.outputDataGridView;
            }
        }

        public TextBox OutputTextBox
        {
            get
            {
                return this.outputTextBox;
            }
        }
        public ToolStripDropDownButton NewStripButton
        {
            get
            {
                return this.newStripButton;
            }
        }
        public ToolStripButton QueryStripButton
        {
            get
            {
                return this.queryStripButton;
            }
        }
    }
}
