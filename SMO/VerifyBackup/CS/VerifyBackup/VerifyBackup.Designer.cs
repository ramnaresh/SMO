namespace Microsoft.Samples.SqlServer
{
    partial class VerifyBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifyBackup));
            this.BackupDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.DeviceTypeLabel = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.VerifyButton = new System.Windows.Forms.Button();
            this.BackupContentsListView = new System.Windows.Forms.ListView();
            this.ReadHeaderButton = new System.Windows.Forms.Button();
            this.Frame1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackupDeviceComboBox
            // 
            this.BackupDeviceComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.BackupDeviceComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.BackupDeviceComboBox, "BackupDeviceComboBox");
            this.BackupDeviceComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.BackupDeviceComboBox.FormattingEnabled = true;
            this.BackupDeviceComboBox.Name = "BackupDeviceComboBox";
            this.BackupDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.BackupDeviceComboBox_SelectedIndexChanged);
            // 
            // DeviceTypeLabel
            // 
            resources.ApplyResources(this.DeviceTypeLabel, "DeviceTypeLabel");
            this.DeviceTypeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.DeviceTypeLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.DeviceTypeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DeviceTypeLabel.Name = "DeviceTypeLabel";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.Label2, "Label2");
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Name = "Label2";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.Label1, "Label1");
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Name = "Label1";
            // 
            // LocationLabel
            // 
            resources.ApplyResources(this.LocationLabel, "LocationLabel");
            this.LocationLabel.AutoEllipsis = true;
            this.LocationLabel.BackColor = System.Drawing.SystemColors.Control;
            this.LocationLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.LocationLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LocationLabel.Name = "LocationLabel";
            // 
            // Frame1
            // 
            resources.ApplyResources(this.Frame1, "Frame1");
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.BackupDeviceComboBox);
            this.Frame1.Controls.Add(this.DeviceTypeLabel);
            this.Frame1.Controls.Add(this.Label2);
            this.Frame1.Controls.Add(this.Label1);
            this.Frame1.Controls.Add(this.LocationLabel);
            this.Frame1.Controls.Add(this.Label7);
            this.Frame1.Controls.Add(this.StatusLabel);
            this.Frame1.Controls.Add(this.Label5);
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Name = "Frame1";
            this.Frame1.TabStop = false;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.SystemColors.Control;
            this.Label7.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.Label7, "Label7");
            this.Label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label7.Name = "Label7";
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.StatusLabel.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.StatusLabel, "StatusLabel");
            this.StatusLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StatusLabel.Name = "StatusLabel";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.Label5, "Label5");
            this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label5.Name = "Label5";
            // 
            // VerifyButton
            // 
            resources.ApplyResources(this.VerifyButton, "VerifyButton");
            this.VerifyButton.BackColor = System.Drawing.SystemColors.Control;
            this.VerifyButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.VerifyButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.VerifyButton.Name = "VerifyButton";
            this.VerifyButton.UseVisualStyleBackColor = false;
            this.VerifyButton.Click += new System.EventHandler(this.VerifyButton_Click);
            // 
            // BackupContentsListView
            // 
            resources.ApplyResources(this.BackupContentsListView, "BackupContentsListView");
            this.BackupContentsListView.FullRowSelect = true;
            this.BackupContentsListView.HideSelection = false;
            this.BackupContentsListView.MultiSelect = false;
            this.BackupContentsListView.Name = "BackupContentsListView";
            this.BackupContentsListView.View = System.Windows.Forms.View.Details;
            this.BackupContentsListView.SelectedIndexChanged += new System.EventHandler(this.BackupContentsListView_SelectedIndexChanged);
            // 
            // ReadHeaderButton
            // 
            resources.ApplyResources(this.ReadHeaderButton, "ReadHeaderButton");
            this.ReadHeaderButton.Name = "ReadHeaderButton";
            this.ReadHeaderButton.Click += new System.EventHandler(this.ReadHeaderButton_Click);
            // 
            // VerifyBackup
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.VerifyButton);
            this.Controls.Add(this.BackupContentsListView);
            this.Controls.Add(this.ReadHeaderButton);
            this.Name = "VerifyBackup";
            this.Load += new System.EventHandler(this.VerifyBackup_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VerifyBackup_FormClosed);
            this.Frame1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox BackupDeviceComboBox;
        private System.Windows.Forms.Label DeviceTypeLabel;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.GroupBox Frame1;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Button VerifyButton;
        private System.Windows.Forms.ListView BackupContentsListView;
        private System.Windows.Forms.Button ReadHeaderButton;
    }
}

