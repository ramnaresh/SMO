namespace Microsoft.Samples.SqlServer
{
    partial class SqlService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlService));
            this.TimeoutLabel = new System.Windows.Forms.Label();
            this.TimeoutUpDown = new System.Windows.Forms.NumericUpDown();
            this.ResumeButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.clhName = new System.Windows.Forms.ColumnHeader();
            this.sbrStatus = new System.Windows.Forms.StatusBar();
            this.clhService = new System.Windows.Forms.ColumnHeader();
            this.ServicesListView = new System.Windows.Forms.ListView();
            this.clhStatus = new System.Windows.Forms.ColumnHeader();
            this.clhStartupType = new System.Windows.Forms.ColumnHeader();
            this.clhLogOnAs = new System.Windows.Forms.ColumnHeader();
            this.clhState = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // TimeoutLabel
            // 
            resources.ApplyResources(this.TimeoutLabel, "TimeoutLabel");
            this.TimeoutLabel.Name = "TimeoutLabel";
            // 
            // TimeoutUpDown
            // 
            resources.ApplyResources(this.TimeoutUpDown, "TimeoutUpDown");
            this.TimeoutUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.TimeoutUpDown.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.TimeoutUpDown.Name = "TimeoutUpDown";
            this.TimeoutUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // ResumeButton
            // 
            resources.ApplyResources(this.ResumeButton, "ResumeButton");
            this.ResumeButton.Name = "ResumeButton";
            this.ResumeButton.Click += new System.EventHandler(this.ResumeButton_Click);
            // 
            // PauseButton
            // 
            resources.ApplyResources(this.PauseButton, "PauseButton");
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // StopButton
            // 
            resources.ApplyResources(this.StopButton, "StopButton");
            this.StopButton.Name = "StopButton";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StartButton
            // 
            resources.ApplyResources(this.StartButton, "StartButton");
            this.StartButton.Name = "StartButton";
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.RefreshButton, "RefreshButton");
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // clhName
            // 
            resources.ApplyResources(this.clhName, "clhName");
            // 
            // sbrStatus
            // 
            resources.ApplyResources(this.sbrStatus, "sbrStatus");
            this.sbrStatus.Name = "sbrStatus";
            // 
            // clhService
            // 
            resources.ApplyResources(this.clhService, "clhService");
            // 
            // ServicesListView
            // 
            resources.ApplyResources(this.ServicesListView, "ServicesListView");
            this.ServicesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhService,
            this.clhName,
            this.clhStatus,
            this.clhStartupType,
            this.clhLogOnAs,
            this.clhState});
            this.ServicesListView.FullRowSelect = true;
            this.ServicesListView.HideSelection = false;
            this.ServicesListView.MultiSelect = false;
            this.ServicesListView.Name = "ServicesListView";
            this.ServicesListView.UseCompatibleStateImageBehavior = false;
            this.ServicesListView.View = System.Windows.Forms.View.Details;
            this.ServicesListView.SelectedIndexChanged += new System.EventHandler(this.Services_SelectedIndexChangedListView);
            // 
            // clhStatus
            // 
            resources.ApplyResources(this.clhStatus, "clhStatus");
            // 
            // clhStartupType
            // 
            resources.ApplyResources(this.clhStartupType, "clhStartupType");
            // 
            // clhLogOnAs
            // 
            resources.ApplyResources(this.clhLogOnAs, "clhLogOnAs");
            // 
            // clhState
            // 
            resources.ApplyResources(this.clhState, "clhState");
            // 
            // SqlService
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.TimeoutLabel);
            this.Controls.Add(this.TimeoutUpDown);
            this.Controls.Add(this.ResumeButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.sbrStatus);
            this.Controls.Add(this.ServicesListView);
            this.Controls.Add(this.label2);
            this.Name = "SqlService";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SqlService_FormClosed);
            this.Load += new System.EventHandler(this.SqlService_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TimeoutLabel;
        private System.Windows.Forms.NumericUpDown TimeoutUpDown;
        private System.Windows.Forms.Button ResumeButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader clhName;
        private System.Windows.Forms.StatusBar sbrStatus;
        private System.Windows.Forms.ColumnHeader clhService;
        private System.Windows.Forms.ListView ServicesListView;
        private System.Windows.Forms.ColumnHeader clhStatus;
        private System.Windows.Forms.ColumnHeader clhStartupType;
        private System.Windows.Forms.ColumnHeader clhLogOnAs;
        private System.Windows.Forms.ColumnHeader clhState;
    }
}

