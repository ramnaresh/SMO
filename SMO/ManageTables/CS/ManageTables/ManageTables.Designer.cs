namespace Microsoft.Samples.SqlServer
{
    partial class ManageTables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageTables));
            this.TablesComboBox = new System.Windows.Forms.ComboBox();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.TableNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ColumnsListView = new System.Windows.Forms.ListView();
            this.clhColumnName = new System.Windows.Forms.ColumnHeader();
            this.clhDataType = new System.Windows.Forms.ColumnHeader();
            this.clhLength = new System.Windows.Forms.ColumnHeader();
            this.clhAllowNulls = new System.Windows.Forms.ColumnHeader();
            this.clhInPrimaryKey = new System.Windows.Forms.ColumnHeader();
            this.DeleteTableButton = new System.Windows.Forms.Button();
            this.AddTableButton = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TablesComboBox
            // 
            resources.ApplyResources(this.TablesComboBox, "TablesComboBox");
            this.TablesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TablesComboBox.FormattingEnabled = true;
            this.TablesComboBox.Name = "TablesComboBox";
            this.TablesComboBox.SelectedIndexChanged += new System.EventHandler(this.Tables_SelectedIndexChangedComboBox);
            // 
            // DatabasesComboBox
            // 
            resources.ApplyResources(this.DatabasesComboBox, "DatabasesComboBox");
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.FormattingEnabled = true;
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            this.DatabasesComboBox.SelectedIndexChanged += new System.EventHandler(this.Databases_SelectedIndexChangedComboBox);
            // 
            // TableNameTextBox
            // 
            resources.ApplyResources(this.TableNameTextBox, "TableNameTextBox");
            this.TableNameTextBox.Name = "TableNameTextBox";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // ColumnsListView
            // 
            resources.ApplyResources(this.ColumnsListView, "ColumnsListView");
            this.ColumnsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhColumnName,
            this.clhDataType,
            this.clhLength,
            this.clhAllowNulls,
            this.clhInPrimaryKey});
            this.ColumnsListView.FullRowSelect = true;
            this.ColumnsListView.HideSelection = false;
            this.ColumnsListView.MultiSelect = false;
            this.ColumnsListView.Name = "ColumnsListView";
            this.ColumnsListView.View = System.Windows.Forms.View.Details;
            this.ColumnsListView.SelectedIndexChanged += new System.EventHandler(this.Columns_SelectedIndexChangedListView);
            // 
            // clhColumnName
            // 
            resources.ApplyResources(this.clhColumnName, "clhColumnName");
            // 
            // clhDataType
            // 
            resources.ApplyResources(this.clhDataType, "clhDataType");
            // 
            // clhLength
            // 
            resources.ApplyResources(this.clhLength, "clhLength");
            // 
            // clhAllowNulls
            // 
            resources.ApplyResources(this.clhAllowNulls, "clhAllowNulls");
            // 
            // clhInPrimaryKey
            // 
            resources.ApplyResources(this.clhInPrimaryKey, "clhInPrimaryKey");
            // 
            // DeleteTableButton
            // 
            resources.ApplyResources(this.DeleteTableButton, "DeleteTableButton");
            this.DeleteTableButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteTableButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.DeleteTableButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DeleteTableButton.Name = "DeleteTableButton";
            this.DeleteTableButton.UseVisualStyleBackColor = false;
            this.DeleteTableButton.Click += new System.EventHandler(this.DeleteTableButton_Click);
            // 
            // AddTableButton
            // 
            resources.ApplyResources(this.AddTableButton, "AddTableButton");
            this.AddTableButton.BackColor = System.Drawing.SystemColors.Control;
            this.AddTableButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.AddTableButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddTableButton.Name = "AddTableButton";
            this.AddTableButton.UseVisualStyleBackColor = false;
            this.AddTableButton.Click += new System.EventHandler(this.AddTableButton_Click);
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.Label3, "Label3");
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Name = "Label3";
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
            //this.Label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // ManageTables
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.TablesComboBox);
            this.Controls.Add(this.DatabasesComboBox);
            this.Controls.Add(this.TableNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ColumnsListView);
            this.Controls.Add(this.DeleteTableButton);
            this.Controls.Add(this.AddTableButton);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "ManageTables";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManageTables_FormClosed);
            this.Load += new System.EventHandler(this.ManageTables_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox TablesComboBox;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.TextBox TableNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView ColumnsListView;
        private System.Windows.Forms.ColumnHeader clhColumnName;
        private System.Windows.Forms.ColumnHeader clhDataType;
        private System.Windows.Forms.ColumnHeader clhLength;
        private System.Windows.Forms.ColumnHeader clhAllowNulls;
        private System.Windows.Forms.ColumnHeader clhInPrimaryKey;
        private System.Windows.Forms.Button DeleteTableButton;
        private System.Windows.Forms.Button AddTableButton;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label1;
    }
}

