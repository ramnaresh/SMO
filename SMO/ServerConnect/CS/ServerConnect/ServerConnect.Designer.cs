namespace Microsoft.Samples.SqlServer
{
    partial class ServerConnect
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerConnect));
            this.SecurityPanel = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.WindowsAuthenticationRadioButton = new System.Windows.Forms.RadioButton();
            this.SecondsLabel = new System.Windows.Forms.Label();
            this.DisplayEventsCheckBox = new System.Windows.Forms.CheckBox();
            this.ServerNamesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AuthenticationLabel = new System.Windows.Forms.Label();
            this.TimeoutUpDown = new System.Windows.Forms.NumericUpDown();
            this.ConnectTimeoutLabel = new System.Windows.Forms.Label();
            this.CancelCommandButton = new System.Windows.Forms.Button();
            this.ConnectCommandButton = new System.Windows.Forms.Button();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.ServerNameLabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SecurityPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // SecurityPanel
            // 
            this.SecurityPanel.Controls.Add(this.radioButton2);
            this.SecurityPanel.Controls.Add(this.WindowsAuthenticationRadioButton);
            resources.ApplyResources(this.SecurityPanel, "SecurityPanel");
            this.SecurityPanel.Name = "SecurityPanel";
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            // 
            // WindowsAuthenticationRadioButton
            // 
            this.WindowsAuthenticationRadioButton.Checked = true;
            resources.ApplyResources(this.WindowsAuthenticationRadioButton, "WindowsAuthenticationRadioButton");
            this.WindowsAuthenticationRadioButton.Name = "WindowsAuthenticationRadioButton";
            this.WindowsAuthenticationRadioButton.TabStop = true;
            this.WindowsAuthenticationRadioButton.CheckedChanged += new System.EventHandler(this.WindowsAuthenticationRadioButton_CheckedChanged);
            // 
            // SecondsLabel
            // 
            resources.ApplyResources(this.SecondsLabel, "SecondsLabel");
            this.SecondsLabel.Name = "SecondsLabel";
            // 
            // DisplayEventsCheckBox
            // 
            resources.ApplyResources(this.DisplayEventsCheckBox, "DisplayEventsCheckBox");
            this.DisplayEventsCheckBox.Name = "DisplayEventsCheckBox";
            // 
            // ServerNamesComboBox
            // 
            this.ServerNamesComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.ServerNamesComboBox, "ServerNamesComboBox");
            this.ServerNamesComboBox.Name = "ServerNamesComboBox";
            this.ServerNamesComboBox.Sorted = true;
            this.toolTip1.SetToolTip(this.ServerNamesComboBox, resources.GetString("ServerNamesComboBox.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // AuthenticationLabel
            // 
            resources.ApplyResources(this.AuthenticationLabel, "AuthenticationLabel");
            this.AuthenticationLabel.Name = "AuthenticationLabel";
            // 
            // TimeoutUpDown
            // 
            resources.ApplyResources(this.TimeoutUpDown, "TimeoutUpDown");
            this.TimeoutUpDown.Name = "TimeoutUpDown";
            this.toolTip1.SetToolTip(this.TimeoutUpDown, resources.GetString("TimeoutUpDown.ToolTip"));
            this.TimeoutUpDown.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // ConnectTimeoutLabel
            // 
            resources.ApplyResources(this.ConnectTimeoutLabel, "ConnectTimeoutLabel");
            this.ConnectTimeoutLabel.Name = "ConnectTimeoutLabel";
            // 
            // CancelCommandButton
            // 
            this.CancelCommandButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.CancelCommandButton, "CancelCommandButton");
            this.CancelCommandButton.Name = "CancelCommandButton";
            this.toolTip1.SetToolTip(this.CancelCommandButton, resources.GetString("CancelCommandButton.ToolTip"));
            this.CancelCommandButton.Click += new System.EventHandler(this.CancelCommandButton_Click);
            // 
            // ConnectCommandButton
            // 
            this.ConnectCommandButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.ConnectCommandButton, "ConnectCommandButton");
            this.ConnectCommandButton.Name = "ConnectCommandButton";
            this.toolTip1.SetToolTip(this.ConnectCommandButton, resources.GetString("ConnectCommandButton.ToolTip"));
            this.ConnectCommandButton.Click += new System.EventHandler(this.ConnectCommandButton_Click);
            // 
            // PasswordTextBox
            // 
            resources.ApplyResources(this.PasswordTextBox, "PasswordTextBox");
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.toolTip1.SetToolTip(this.PasswordTextBox, resources.GetString("PasswordTextBox.ToolTip"));
            // 
            // UserNameTextBox
            // 
            resources.ApplyResources(this.UserNameTextBox, "UserNameTextBox");
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.toolTip1.SetToolTip(this.UserNameTextBox, resources.GetString("UserNameTextBox.ToolTip"));
            // 
            // PasswordLabel
            // 
            resources.ApplyResources(this.PasswordLabel, "PasswordLabel");
            this.PasswordLabel.Name = "PasswordLabel";
            // 
            // UserNameLabel
            // 
            resources.ApplyResources(this.UserNameLabel, "UserNameLabel");
            this.UserNameLabel.Name = "UserNameLabel";
            // 
            // ServerNameLabel
            // 
            resources.ApplyResources(this.ServerNameLabel, "ServerNameLabel");
            this.ServerNameLabel.Name = "ServerNameLabel";
            // 
            // ServerConnect
            // 
            this.AcceptButton = this.ConnectCommandButton;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.CancelCommandButton;
            this.Controls.Add(this.SecondsLabel);
            this.Controls.Add(this.DisplayEventsCheckBox);
            this.Controls.Add(this.ServerNamesComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AuthenticationLabel);
            this.Controls.Add(this.TimeoutUpDown);
            this.Controls.Add(this.ConnectTimeoutLabel);
            this.Controls.Add(this.CancelCommandButton);
            this.Controls.Add(this.ConnectCommandButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.ServerNameLabel);
            this.Controls.Add(this.SecurityPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerConnect";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerConnect_FormClosing);
            this.Load += new System.EventHandler(this.ServerConnect_Load);
            this.SecurityPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel SecurityPanel;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton WindowsAuthenticationRadioButton;
        private System.Windows.Forms.Label SecondsLabel;
        private System.Windows.Forms.CheckBox DisplayEventsCheckBox;
        private System.Windows.Forms.ComboBox ServerNamesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label AuthenticationLabel;
        private System.Windows.Forms.NumericUpDown TimeoutUpDown;
        private System.Windows.Forms.Label ConnectTimeoutLabel;
        private System.Windows.Forms.Button CancelCommandButton;
        private System.Windows.Forms.Button ConnectCommandButton;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label ServerNameLabel;
    }
}