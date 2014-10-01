namespace Microsoft.Samples.SqlServer
{
    partial class ManageDatabaseUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDatabaseUsers));
            this.LoginsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DeleteUserButton = new System.Windows.Forms.Button();
            this.clhUserName = new System.Windows.Forms.ColumnHeader();
            this.AddUserButton = new System.Windows.Forms.Button();
            this.UsersListView = new System.Windows.Forms.ListView();
            this.clhLoginName = new System.Windows.Forms.ColumnHeader();
            this.clhUserID = new System.Windows.Forms.ColumnHeader();
            this.DatabasesLabel = new System.Windows.Forms.Label();
            this.UsersLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoginsComboBox
            // 
            resources.ApplyResources(this.LoginsComboBox, "LoginsComboBox");
            this.LoginsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LoginsComboBox.DropDownWidth = 250;
            this.LoginsComboBox.FormattingEnabled = true;
            this.LoginsComboBox.Name = "LoginsComboBox";
            this.LoginsComboBox.SelectedIndexChanged += new System.EventHandler(this.Logins_SelectedIndexChangedComboBox);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // DatabasesComboBox
            // 
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.DatabasesComboBox, "DatabasesComboBox");
            this.DatabasesComboBox.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            this.DatabasesComboBox.SelectedIndexChanged += new System.EventHandler(this.Databases_SelectedIndexChangedComboBox);
            // 
            // UserNameTextBox
            // 
            resources.ApplyResources(this.UserNameTextBox, "UserNameTextBox");
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.TextChanged += new System.EventHandler(this.UserName_TextChangedTextBox);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // DeleteUserButton
            // 
            resources.ApplyResources(this.DeleteUserButton, "DeleteUserButton");
            this.DeleteUserButton.Name = "DeleteUserButton";
            this.DeleteUserButton.Click += new System.EventHandler(this.DeleteUserButton_Click);
            // 
            // clhUserName
            // 
            resources.ApplyResources(this.clhUserName, "clhUserName");
            // 
            // AddUserButton
            // 
            resources.ApplyResources(this.AddUserButton, "AddUserButton");
            this.AddUserButton.Name = "AddUserButton";
            this.AddUserButton.Click += new System.EventHandler(this.AddUserButton_Click);
            // 
            // UsersListView
            // 
            resources.ApplyResources(this.UsersListView, "UsersListView");
            this.UsersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhUserName,
            this.clhLoginName,
            this.clhUserID});
            this.UsersListView.FullRowSelect = true;
            this.UsersListView.HideSelection = false;
            this.UsersListView.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.UsersListView.MultiSelect = false;
            this.UsersListView.Name = "UsersListView";
            this.UsersListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.UsersListView.View = System.Windows.Forms.View.Details;
            this.UsersListView.SelectedIndexChanged += new System.EventHandler(this.Users_SelectedIndexChangedListView);
            // 
            // clhLoginName
            // 
            resources.ApplyResources(this.clhLoginName, "clhLoginName");
            // 
            // clhUserID
            // 
            resources.ApplyResources(this.clhUserID, "clhUserID");
            // 
            // DatabasesLabel
            // 
            this.DatabasesLabel.BackColor = System.Drawing.SystemColors.Control;
            this.DatabasesLabel.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.DatabasesLabel, "DatabasesLabel");
            this.DatabasesLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DatabasesLabel.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.DatabasesLabel.Name = "DatabasesLabel";
            // 
            // UsersLabel
            // 
            resources.ApplyResources(this.UsersLabel, "UsersLabel");
            this.UsersLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.UsersLabel.Name = "UsersLabel";
            // 
            // ManageDatabaseUsers
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.LoginsComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DatabasesComboBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DeleteUserButton);
            this.Controls.Add(this.AddUserButton);
            this.Controls.Add(this.UsersListView);
            this.Controls.Add(this.DatabasesLabel);
            this.Controls.Add(this.UsersLabel);
            this.Name = "ManageDatabaseUsers";
            this.Load += new System.EventHandler(this.ManageDatabaseUsers_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManageDatabaseUsers_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox LoginsComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DeleteUserButton;
        private System.Windows.Forms.ColumnHeader clhUserName;
        private System.Windows.Forms.Button AddUserButton;
        private System.Windows.Forms.ListView UsersListView;
        private System.Windows.Forms.ColumnHeader clhLoginName;
        private System.Windows.Forms.ColumnHeader clhUserID;
        private System.Windows.Forms.Label DatabasesLabel;
        private System.Windows.Forms.Label UsersLabel;
    }
}

