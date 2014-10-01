namespace Microsoft.Samples.SqlServer
{
    partial class ScriptTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptTable));
            this.DependenciesCheckBox = new System.Windows.Forms.CheckBox();
            this.TablesComboBox = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.ScriptTextBox = new System.Windows.Forms.RichTextBox();
            this.ScriptDropCheckBox = new System.Windows.Forms.CheckBox();
            this.sbrStatus = new System.Windows.Forms.StatusBar();
            this.ScriptTableButton = new System.Windows.Forms.Button();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DependenciesCheckBox
            // 
            resources.ApplyResources(this.DependenciesCheckBox, "DependenciesCheckBox");
            this.DependenciesCheckBox.Name = "DependenciesCheckBox";
            // 
            // TablesComboBox
            // 
            resources.ApplyResources(this.TablesComboBox, "TablesComboBox");
            this.TablesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TablesComboBox.FormattingEnabled = true;
            this.TablesComboBox.Name = "TablesComboBox";
            // 
            // Label2
            // 
            resources.ApplyResources(this.Label2, "Label2");
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Name = "Label2";
            // 
            // DatabasesComboBox
            // 
            resources.ApplyResources(this.DatabasesComboBox, "DatabasesComboBox");
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.FormattingEnabled = true;
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            this.DatabasesComboBox.SelectedIndexChanged += new System.EventHandler(this.Databases_SelectedIndexChangedComboBox);
            // 
            // ScriptTextBox
            // 
            resources.ApplyResources(this.ScriptTextBox, "ScriptTextBox");
            this.ScriptTextBox.Name = "ScriptTextBox";
            // 
            // ScriptDropCheckBox
            // 
            resources.ApplyResources(this.ScriptDropCheckBox, "ScriptDropCheckBox");
            this.ScriptDropCheckBox.Name = "ScriptDropCheckBox";
            // 
            // sbrStatus
            // 
            resources.ApplyResources(this.sbrStatus, "sbrStatus");
            this.sbrStatus.Name = "sbrStatus";
            // 
            // ScriptTableButton
            // 
            resources.ApplyResources(this.ScriptTableButton, "ScriptTableButton");
            this.ScriptTableButton.BackColor = System.Drawing.SystemColors.Control;
            this.ScriptTableButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.ScriptTableButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ScriptTableButton.Name = "ScriptTableButton";
            this.ScriptTableButton.UseVisualStyleBackColor = false;
            this.ScriptTableButton.Click += new System.EventHandler(this.ScriptTableButton_Click);
            // 
            // DatabaseLabel
            // 
            resources.ApplyResources(this.DatabaseLabel, "DatabaseLabel");
            this.DatabaseLabel.BackColor = System.Drawing.SystemColors.Control;
            this.DatabaseLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.DatabaseLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DatabaseLabel.Name = "DatabaseLabel";
            // 
            // ScriptTable
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.DependenciesCheckBox);
            this.Controls.Add(this.TablesComboBox);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.DatabasesComboBox);
            this.Controls.Add(this.ScriptTextBox);
            this.Controls.Add(this.ScriptDropCheckBox);
            this.Controls.Add(this.sbrStatus);
            this.Controls.Add(this.ScriptTableButton);
            this.Controls.Add(this.DatabaseLabel);
            this.Name = "ScriptTable";
            this.Load += new System.EventHandler(this.ScriptTable_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScriptTable_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox DependenciesCheckBox;
        private System.Windows.Forms.ComboBox TablesComboBox;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.RichTextBox ScriptTextBox;
        private System.Windows.Forms.CheckBox ScriptDropCheckBox;
        private System.Windows.Forms.StatusBar sbrStatus;
        private System.Windows.Forms.Button ScriptTableButton;
        private System.Windows.Forms.Label DatabaseLabel;
    }
}

