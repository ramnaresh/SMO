using System.Windows.Forms;

namespace Microsoft.Samples.SqlServer.Controls
{
    partial class SqlConnectionControl
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.ToolStripItem.set_Text(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlConnectionControl));
            this.ServerConnectPanel = new System.Windows.Forms.Panel();
            this.ServerLabel = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.connectionToolStrip = new System.Windows.Forms.ToolStrip();
            this.newStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.refreshStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.historyButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.optionsButton = new System.Windows.Forms.ToolStripButton();
            this.ConnectionContainer = new System.Windows.Forms.SplitContainer();
            this.CancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UserNameComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ObjectGrid = new System.Windows.Forms.PropertyGrid();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.objectsTreeView = new System.Windows.Forms.TreeView();
            this.ConnectionImageList = new System.Windows.Forms.ImageList(this.components);
            this.ToolbarPanel = new System.Windows.Forms.Panel();
            this.ServerConnectPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.connectionToolStrip.SuspendLayout();
            this.ConnectionContainer.Panel1.SuspendLayout();
            this.ConnectionContainer.Panel2.SuspendLayout();
            this.ConnectionContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ToolbarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerConnectPanel
            // 
            this.ServerConnectPanel.Controls.Add(this.ServerLabel);
            this.ServerConnectPanel.Controls.Add(this.pictureBox1);
            this.ServerConnectPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ServerConnectPanel.Location = new System.Drawing.Point(0, 0);
            this.ServerConnectPanel.Name = "ServerConnectPanel";
            this.ServerConnectPanel.Size = new System.Drawing.Size(286, 25);
            this.ServerConnectPanel.TabIndex = 31;
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(29, 6);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(84, 13);
            this.ServerLabel.TabIndex = 28;
            this.ServerLabel.TabStop = true;
            this.ServerLabel.Text = "(Not connected)";
            this.ServerLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ServerLabel_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Microsoft.Samples.SqlServer.Properties.Resources.Server;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(5, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 17);
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // connectionToolStrip
            // 
            this.connectionToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.connectionToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.connectionToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.connectionToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newStripButton,
            this.refreshStripButton,
            this.toolStripSeparator4,
            this.historyButton,
            this.optionsButton});
            this.connectionToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.connectionToolStrip.Location = new System.Drawing.Point(0, 0);
            this.connectionToolStrip.Name = "connectionToolStrip";
            this.connectionToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.connectionToolStrip.Size = new System.Drawing.Size(286, 23);
            this.connectionToolStrip.Stretch = true;
            this.connectionToolStrip.TabIndex = 27;
            this.connectionToolStrip.Text = "toolStrip1";
            // 
            // newStripButton
            // 
            this.newStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NewStripButton.Image")));
            this.newStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newStripButton.Name = "newStripButton";
            this.newStripButton.Size = new System.Drawing.Size(41, 20);
            this.newStripButton.Text = "New";
            // 
            // refreshStripButton
            // 
            this.refreshStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.refreshStripButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshStripButton.Image")));
            this.refreshStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshStripButton.Name = "refreshStripButton";
            this.refreshStripButton.Size = new System.Drawing.Size(49, 20);
            this.refreshStripButton.Text = "Refresh";
            this.refreshStripButton.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
            // 
            // historyButton
            // 
            this.historyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.historyButton.Image = ((System.Drawing.Image)(resources.GetObject("HistoryButton.Image")));
            this.historyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.historyButton.Name = "historyButton";
            this.historyButton.Size = new System.Drawing.Size(54, 20);
            this.historyButton.Text = "History";
            this.historyButton.Visible = false;
            // 
            // optionsButton
            // 
            this.optionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.optionsButton.Enabled = false;
            this.optionsButton.Image = ((System.Drawing.Image)(resources.GetObject("OptionsButton.Image")));
            this.optionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(63, 20);
            this.optionsButton.Text = "Options ...";
            // 
            // ConnectionContainer
            // 
            this.ConnectionContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectionContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.ConnectionContainer.Location = new System.Drawing.Point(0, 51);
            this.ConnectionContainer.Name = "ConnectionContainer";
            this.ConnectionContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ConnectionContainer.Panel1
            // 
            this.ConnectionContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.ConnectionContainer.Panel1.Controls.Add(this.CancelButton);
            this.ConnectionContainer.Panel1.Controls.Add(this.panel1);
            this.ConnectionContainer.Panel1.Controls.Add(this.connectButton);
            this.ConnectionContainer.Panel1.Controls.Add(this.label3);
            this.ConnectionContainer.Panel1.Controls.Add(this.ObjectGrid);
            // 
            // ConnectionContainer.Panel2
            // 
            this.ConnectionContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.ConnectionContainer.Panel2.Controls.Add(this.DatabasesComboBox);
            this.ConnectionContainer.Panel2.Controls.Add(this.label4);
            this.ConnectionContainer.Panel2.Controls.Add(this.objectsTreeView);
            this.ConnectionContainer.Size = new System.Drawing.Size(286, 438);
            this.ConnectionContainer.SplitterDistance = 166;
            this.ConnectionContainer.TabIndex = 33;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Enabled = false;
            this.CancelButton.Location = new System.Drawing.Point(207, 138);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.UserNameComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PasswordTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(7, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 51);
            this.panel1.TabIndex = 9;
            // 
            // UserNameComboBox
            // 
            this.UserNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.UserNameComboBox.FormattingEnabled = true;
            this.UserNameComboBox.Location = new System.Drawing.Point(88, 0);
            this.UserNameComboBox.Name = "UserNameComboBox";
            this.UserNameComboBox.Size = new System.Drawing.Size(181, 21);
            this.UserNameComboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordTextBox.Location = new System.Drawing.Point(88, 25);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(181, 20);
            this.PasswordTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password:";
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.Location = new System.Drawing.Point(124, 138);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(77, 23);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select the server name";
            // 
            // ObjectGrid
            // 
            this.ObjectGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectGrid.BackColor = System.Drawing.Color.White;
            this.ObjectGrid.HelpVisible = false;
            this.ObjectGrid.Location = new System.Drawing.Point(6, 24);
            this.ObjectGrid.Margin = new System.Windows.Forms.Padding(1);
            this.ObjectGrid.Name = "ObjectGrid";
            this.ObjectGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.ObjectGrid.Size = new System.Drawing.Size(277, 110);
            this.ObjectGrid.TabIndex = 8;
            this.ObjectGrid.ToolbarVisible = false;
            // 
            // DatabasesComboBox
            // 
            this.DatabasesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.FormattingEnabled = true;
            this.DatabasesComboBox.Location = new System.Drawing.Point(7, 16);
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            this.DatabasesComboBox.Size = new System.Drawing.Size(277, 21);
            this.DatabasesComboBox.TabIndex = 32;
            this.DatabasesComboBox.SelectedIndexChanged += new System.EventHandler(this.DatabasesComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Database Name:";
            // 
            // objectsTreeView
            // 
            this.objectsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectsTreeView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.objectsTreeView.FullRowSelect = true;
            this.objectsTreeView.HideSelection = false;
            this.objectsTreeView.ImageIndex = 0;
            this.objectsTreeView.ImageList = this.ConnectionImageList;
            this.objectsTreeView.Location = new System.Drawing.Point(7, 38);
            this.objectsTreeView.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.objectsTreeView.Name = "objectsTreeView";
            this.objectsTreeView.SelectedImageIndex = 0;
            this.objectsTreeView.ShowNodeToolTips = true;
            this.objectsTreeView.Size = new System.Drawing.Size(277, 227);
            this.objectsTreeView.TabIndex = 30;
            // 
            // ConnectionImageList
            // 
            this.ConnectionImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ConnectionImageList.ImageStream")));
            this.ConnectionImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ConnectionImageList.Images.SetKeyName(0, "Folder.bmp");
            this.ConnectionImageList.Images.SetKeyName(1, "Microsoft.Samples.SqlServer.ServiceContractConfiguration");
            this.ConnectionImageList.Images.SetKeyName(2, "Microsoft.Samples.SqlServer.MessageTypeConfiguration");
            this.ConnectionImageList.Images.SetKeyName(3, "Microsoft.Samples.SqlServer.ServiceQueueConfiguration");
            this.ConnectionImageList.Images.SetKeyName(4, "Microsoft.Samples.SqlServer.BrokerServiceConfiguration");
            this.ConnectionImageList.Images.SetKeyName(5, "Connect");
            this.ConnectionImageList.Images.SetKeyName(6, "Drop");
            this.ConnectionImageList.Images.SetKeyName(7, "DownArrow.bmp");
            this.ConnectionImageList.Images.SetKeyName(8, "Certificate");
            this.ConnectionImageList.Images.SetKeyName(9, "Microsoft.Samples.SqlServer.RemoteServiceBindingConfiguration");
            this.ConnectionImageList.Images.SetKeyName(10, "Microsoft.Samples.SqlServer.EndpointConfiguration");
            this.ConnectionImageList.Images.SetKeyName(11, "MasterKey");
            this.ConnectionImageList.Images.SetKeyName(12, "SmallServer.bmp");
            this.ConnectionImageList.Images.SetKeyName(13, "ServiceCertificate");
            this.ConnectionImageList.Images.SetKeyName(14, "EndpointUser.bmp");
            this.ConnectionImageList.Images.SetKeyName(15, "NotEndpointUser.bmp");
            this.ConnectionImageList.Images.SetKeyName(16, "NotEndpoint.bmp");
            this.ConnectionImageList.Images.SetKeyName(17, "EndpointCertificate.bmp");
            this.ConnectionImageList.Images.SetKeyName(18, "Server.bmp");
            this.ConnectionImageList.Images.SetKeyName(19, "Database.bmp");
            this.ConnectionImageList.Images.SetKeyName(20, "XmlMap.bmp");
            this.ConnectionImageList.Images.SetKeyName(21, "error.bmp");
            // 
            // ToolbarPanel
            // 
            this.ToolbarPanel.Controls.Add(this.connectionToolStrip);
            this.ToolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolbarPanel.Location = new System.Drawing.Point(0, 25);
            this.ToolbarPanel.Name = "ToolbarPanel";
            this.ToolbarPanel.Size = new System.Drawing.Size(286, 23);
            this.ToolbarPanel.TabIndex = 11;
            // 
            // SqlConnectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ToolbarPanel);
            this.Controls.Add(this.ConnectionContainer);
            this.Controls.Add(this.ServerConnectPanel);
            this.Name = "SqlConnectionControl";
            this.Size = new System.Drawing.Size(286, 489);
            this.Load += new System.EventHandler(this.SqlConnectionControl_Load);
            this.ServerConnectPanel.ResumeLayout(false);
            this.ServerConnectPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.connectionToolStrip.ResumeLayout(false);
            this.connectionToolStrip.PerformLayout();
            this.ConnectionContainer.Panel1.ResumeLayout(false);
            this.ConnectionContainer.Panel1.PerformLayout();
            this.ConnectionContainer.Panel2.ResumeLayout(false);
            this.ConnectionContainer.Panel2.PerformLayout();
            this.ConnectionContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ToolbarPanel.ResumeLayout(false);
            this.ToolbarPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip connectionToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton historyButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TreeView objectsTreeView;
        private System.Windows.Forms.ToolStripButton optionsButton;
        private System.Windows.Forms.ToolStripButton refreshStripButton;

        private System.Windows.Forms.PropertyGrid ObjectGrid;
        private System.Windows.Forms.Panel ServerConnectPanel;       
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SplitContainer ConnectionContainer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel ServerLabel;
        private System.Windows.Forms.ComboBox UserNameComboBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList ConnectionImageList;
        
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.Label label4;
        
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Panel ToolbarPanel;
        private System.Windows.Forms.ToolStripDropDownButton newStripButton;


        public ToolStrip ConnectionToolStrip
        {
            get
            {
                return this.connectionToolStrip;
            }
        }

        public ToolStripDropDownButton HistoryButton
        {
            get
            {
                return this.historyButton;
            }
        }
        public Button ConnectButton
        {
            get
            {
                return this.connectButton;
            }
        }

        public TreeView ObjectsTreeView
        {
            get
            {
                return this.objectsTreeView;
            }
        }

        public ToolStripButton OptionsButton
        {
            get
            {
                return this.optionsButton;
            }
        }

        public ToolStripButton RefreshStripButton
        {
            get
            {
                return this.refreshStripButton;
            }
        }

        public ToolStripDropDownButton NewStripButton
        {
            get
            {
                return this.newStripButton;
            }
        }
    }
}
