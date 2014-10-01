namespace Microsoft.Samples.SqlServer
{
    partial class BackupRestore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupRestore));
            this.label4 = new System.Windows.Forms.Label();
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.GetBackupFileButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.BackupFileTextBox = new System.Windows.Forms.TextBox();
            this.BackupButton = new System.Windows.Forms.Button();
            this.RestoreButton = new System.Windows.Forms.Button();
            this.ResultsTextBox = new System.Windows.Forms.TextBox();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ControlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ControlsPanel
            // 
            resources.ApplyResources(this.ControlsPanel, "ControlsPanel");
            this.ControlsPanel.Controls.Add(this.label2);
            this.ControlsPanel.Controls.Add(this.GetBackupFileButton);
            this.ControlsPanel.Controls.Add(this.label3);
            this.ControlsPanel.Controls.Add(this.DatabasesComboBox);
            this.ControlsPanel.Controls.Add(this.BackupFileTextBox);
            this.ControlsPanel.Controls.Add(this.BackupButton);
            this.ControlsPanel.Controls.Add(this.RestoreButton);
            this.ControlsPanel.Name = "ControlsPanel";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // GetBackupFileButton
            // 
            resources.ApplyResources(this.GetBackupFileButton, "GetBackupFileButton");
            this.GetBackupFileButton.Name = "GetBackupFileButton";
            this.GetBackupFileButton.Click += new System.EventHandler(this.GetBackupFileButton_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // DatabasesComboBox
            // 
            resources.ApplyResources(this.DatabasesComboBox, "DatabasesComboBox");
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.FormattingEnabled = true;
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            this.DatabasesComboBox.Sorted = true;
            // 
            // BackupFileTextBox
            // 
            resources.ApplyResources(this.BackupFileTextBox, "BackupFileTextBox");
            this.BackupFileTextBox.Name = "BackupFileTextBox";
            // 
            // BackupButton
            // 
            resources.ApplyResources(this.BackupButton, "BackupButton");
            this.BackupButton.Name = "BackupButton";
            this.BackupButton.Click += new System.EventHandler(this.BackupButton_Click);
            // 
            // RestoreButton
            // 
            resources.ApplyResources(this.RestoreButton, "RestoreButton");
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // ResultsTextBox
            // 
            resources.ApplyResources(this.ResultsTextBox, "ResultsTextBox");
            this.ResultsTextBox.Name = "ResultsTextBox";
            this.ResultsTextBox.ReadOnly = true;
            // 
            // statusBar1
            // 
            resources.ApplyResources(this.statusBar1, "statusBar1");
            this.statusBar1.Name = "statusBar1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.CheckFileExists = false;
            this.openFileDialog1.FileName = "SmoDemoBackup.bak";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            this.openFileDialog1.InitialDirectory = "C:\\";
            // 
            // BackupRestore
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.ControlsPanel);
            this.Controls.Add(this.ResultsTextBox);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.label4);
            this.Name = "BackupRestore";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BackupRestore_FormClosed);
            this.Load += new System.EventHandler(this.BackupRestore_Load);
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel ControlsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GetBackupFileButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.TextBox BackupFileTextBox;
        private System.Windows.Forms.Button BackupButton;
        private System.Windows.Forms.Button RestoreButton;
        private System.Windows.Forms.TextBox ResultsTextBox;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

