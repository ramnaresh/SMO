namespace Microsoft.Samples.SqlServer
{
    partial class ManageDatabases
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDatabases));
            this.clhSize = new System.Windows.Forms.ColumnHeader();
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.ResultsLabel = new System.Windows.Forms.Label();
            this.clhCreateDate = new System.Windows.Forms.ColumnHeader();
            this.ShowSqlStatementsCheckBox = new System.Windows.Forms.CheckBox();
            this.clhDatabaseName = new System.Windows.Forms.ColumnHeader();
            this.EventLogTextBox = new System.Windows.Forms.TextBox();
            this.ShowServerMessagesCheckBox = new System.Windows.Forms.CheckBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.clhCompatibilityLevel = new System.Windows.Forms.ColumnHeader();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.NewDatabaseTextBox = new System.Windows.Forms.TextBox();
            this.NewDatabaseLabel = new System.Windows.Forms.Label();
            this.DatabasesListView = new System.Windows.Forms.ListView();
            this.clhSpaceAvailable = new System.Windows.Forms.ColumnHeader();
            this.sbrStatus = new System.Windows.Forms.StatusBar();
            this.SuspendLayout();
            // 
            // clhSize
            // 
            resources.ApplyResources(this.clhSize, "clhSize");
            // 
            // DisplayLabel
            // 
            resources.ApplyResources(this.DisplayLabel, "DisplayLabel");
            this.DisplayLabel.Name = "DisplayLabel";
            // 
            // ResultsLabel
            // 
            resources.ApplyResources(this.ResultsLabel, "ResultsLabel");
            this.ResultsLabel.Name = "ResultsLabel";
            // 
            // clhCreateDate
            // 
            resources.ApplyResources(this.clhCreateDate, "clhCreateDate");
            // 
            // ShowSqlStatementsCheckBox
            // 
            resources.ApplyResources(this.ShowSqlStatementsCheckBox, "ShowSqlStatementsCheckBox");
            this.ShowSqlStatementsCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.ShowSqlStatementsCheckBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ShowSqlStatementsCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ShowSqlStatementsCheckBox.Name = "ShowSqlStatementsCheckBox";
            this.ShowSqlStatementsCheckBox.UseVisualStyleBackColor = false;
            this.ShowSqlStatementsCheckBox.CheckedChanged += new System.EventHandler(this.ShowSqlStatements_CheckedChangedCheckBox);
            // 
            // clhDatabaseName
            // 
            resources.ApplyResources(this.clhDatabaseName, "clhDatabaseName");
            // 
            // EventLogTextBox
            // 
            resources.ApplyResources(this.EventLogTextBox, "EventLogTextBox");
            this.EventLogTextBox.Name = "EventLogTextBox";
            this.EventLogTextBox.ReadOnly = true;
            // 
            // ShowServerMessagesCheckBox
            // 
            resources.ApplyResources(this.ShowServerMessagesCheckBox, "ShowServerMessagesCheckBox");
            this.ShowServerMessagesCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.ShowServerMessagesCheckBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ShowServerMessagesCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ShowServerMessagesCheckBox.Name = "ShowServerMessagesCheckBox";
            this.ShowServerMessagesCheckBox.UseVisualStyleBackColor = false;
            this.ShowServerMessagesCheckBox.CheckedChanged += new System.EventHandler(this.ShowServerMessages_CheckedChangedCheckBox);
            // 
            // CreateButton
            // 
            resources.ApplyResources(this.CreateButton, "CreateButton");
            this.CreateButton.BackColor = System.Drawing.SystemColors.Control;
            this.CreateButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.CreateButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.UseVisualStyleBackColor = false;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // DeleteButton
            // 
            resources.ApplyResources(this.DeleteButton, "DeleteButton");
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.DeleteButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // clhCompatibilityLevel
            // 
            resources.ApplyResources(this.clhCompatibilityLevel, "clhCompatibilityLevel");
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.BackColor = System.Drawing.SystemColors.Control;
            this.DatabaseLabel.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.DatabaseLabel, "DatabaseLabel");
            this.DatabaseLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DatabaseLabel.Name = "DatabaseLabel";
            // 
            // NewDatabaseTextBox
            // 
            resources.ApplyResources(this.NewDatabaseTextBox, "NewDatabaseTextBox");
            this.NewDatabaseTextBox.Name = "NewDatabaseTextBox";
            this.NewDatabaseTextBox.TextChanged += new System.EventHandler(this.NewDatabaseTextBox_TextChanged);
            // 
            // NewDatabaseLabel
            // 
            resources.ApplyResources(this.NewDatabaseLabel, "NewDatabaseLabel");
            this.NewDatabaseLabel.Name = "NewDatabaseLabel";
            // 
            // DatabasesListView
            // 
            resources.ApplyResources(this.DatabasesListView, "DatabasesListView");
            this.DatabasesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhDatabaseName,
            this.clhCreateDate,
            this.clhSize,
            this.clhSpaceAvailable,
            this.clhCompatibilityLevel});
            this.DatabasesListView.FullRowSelect = true;
            this.DatabasesListView.HideSelection = false;
            this.DatabasesListView.MultiSelect = false;
            this.DatabasesListView.Name = "DatabasesListView";
            this.DatabasesListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.DatabasesListView.UseCompatibleStateImageBehavior = false;
            this.DatabasesListView.View = System.Windows.Forms.View.Details;
            this.DatabasesListView.SelectedIndexChanged += new System.EventHandler(this.Databases_SelectedIndexChangedListView);
            // 
            // clhSpaceAvailable
            // 
            resources.ApplyResources(this.clhSpaceAvailable, "clhSpaceAvailable");
            // 
            // sbrStatus
            // 
            resources.ApplyResources(this.sbrStatus, "sbrStatus");
            this.sbrStatus.Name = "sbrStatus";
            // 
            // ManageDatabases
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.ResultsLabel);
            this.Controls.Add(this.ShowSqlStatementsCheckBox);
            this.Controls.Add(this.EventLogTextBox);
            this.Controls.Add(this.ShowServerMessagesCheckBox);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.DatabaseLabel);
            this.Controls.Add(this.NewDatabaseTextBox);
            this.Controls.Add(this.NewDatabaseLabel);
            this.Controls.Add(this.DatabasesListView);
            this.Controls.Add(this.sbrStatus);
            this.Name = "ManageDatabases";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManageDatabases_FormClosed);
            this.Load += new System.EventHandler(this.ManageDatabases_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader clhSize;
        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Label ResultsLabel;
        private System.Windows.Forms.ColumnHeader clhCreateDate;
        private System.Windows.Forms.CheckBox ShowSqlStatementsCheckBox;
        private System.Windows.Forms.ColumnHeader clhDatabaseName;
        private System.Windows.Forms.TextBox EventLogTextBox;
        private System.Windows.Forms.CheckBox ShowServerMessagesCheckBox;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.ColumnHeader clhCompatibilityLevel;
        private System.Windows.Forms.Label DatabaseLabel;
        private System.Windows.Forms.TextBox NewDatabaseTextBox;
        private System.Windows.Forms.Label NewDatabaseLabel;
        private System.Windows.Forms.ListView DatabasesListView;
        private System.Windows.Forms.StatusBar sbrStatus;
        private System.Windows.Forms.ColumnHeader clhSpaceAvailable;
    }
}

