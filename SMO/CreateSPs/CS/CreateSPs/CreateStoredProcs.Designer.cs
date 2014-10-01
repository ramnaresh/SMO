namespace Microsoft.Samples.SqlServer
{
    partial class CreateStoredProcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateStoredProcs));
            this.DropOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.PrefixTextBox = new System.Windows.Forms.TextBox();
            this.PrefixLabel = new System.Windows.Forms.Label();
            this.ConnectedPanel = new System.Windows.Forms.Panel();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.ScriptTextBox = new System.Windows.Forms.TextBox();
            this.sbrStatus = new System.Windows.Forms.StatusBar();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.ConnectedPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DropOnlyCheckBox
            // 
            resources.ApplyResources(this.DropOnlyCheckBox, "DropOnlyCheckBox");
            this.DropOnlyCheckBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.DropOnlyCheckBox.Name = "DropOnlyCheckBox";
            // 
            // PrefixTextBox
            // 
            resources.ApplyResources(this.PrefixTextBox, "PrefixTextBox");
            this.PrefixTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.PrefixTextBox.Name = "PrefixTextBox";
            // 
            // PrefixLabel
            // 
            resources.ApplyResources(this.PrefixLabel, "PrefixLabel");
            this.PrefixLabel.Name = "PrefixLabel";
            // 
            // ConnectedPanel
            // 
            resources.ApplyResources(this.ConnectedPanel, "ConnectedPanel");
            this.ConnectedPanel.Controls.Add(this.DropOnlyCheckBox);
            this.ConnectedPanel.Controls.Add(this.PrefixTextBox);
            this.ConnectedPanel.Controls.Add(this.PrefixLabel);
            this.ConnectedPanel.Controls.Add(this.DatabasesComboBox);
            this.ConnectedPanel.Controls.Add(this.DatabaseLabel);
            this.ConnectedPanel.Name = "ConnectedPanel";
            // 
            // DatabasesComboBox
            // 
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.DatabasesComboBox, "DatabasesComboBox");
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            // 
            // DatabaseLabel
            // 
            resources.ApplyResources(this.DatabaseLabel, "DatabaseLabel");
            this.DatabaseLabel.Name = "DatabaseLabel";
            // 
            // ScriptTextBox
            // 
            resources.ApplyResources(this.ScriptTextBox, "ScriptTextBox");
            this.ScriptTextBox.Name = "ScriptTextBox";
            // 
            // sbrStatus
            // 
            resources.ApplyResources(this.sbrStatus, "sbrStatus");
            this.sbrStatus.Name = "sbrStatus";
            // 
            // ExecuteButton
            // 
            resources.ApplyResources(this.ExecuteButton, "ExecuteButton");
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // CreateStoredProcs
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.ConnectedPanel);
            this.Controls.Add(this.ScriptTextBox);
            this.Controls.Add(this.sbrStatus);
            this.Controls.Add(this.ExecuteButton);
            this.Name = "CreateStoredProcs";
            this.Load += new System.EventHandler(this.CreateStoredProcs_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateStoredProcs_FormClosed);
            this.ConnectedPanel.ResumeLayout(false);
            this.ConnectedPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox DropOnlyCheckBox;
        private System.Windows.Forms.TextBox PrefixTextBox;
        private System.Windows.Forms.Label PrefixLabel;
        private System.Windows.Forms.Panel ConnectedPanel;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.TextBox ScriptTextBox;
        private System.Windows.Forms.StatusBar sbrStatus;
        private System.Windows.Forms.Button ExecuteButton;
    }
}

