namespace Microsoft.Samples.SqlServer
{
    partial class LoadRegAssembly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadRegAssembly));
            this.clhCreateDate = new System.Windows.Forms.ColumnHeader();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.DropAssemblyButton = new System.Windows.Forms.Button();
            this.AddAssemblyButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clhAssemblyVersion = new System.Windows.Forms.ColumnHeader();
            this.AssembliesListView = new System.Windows.Forms.ListView();
            this.clhAssemblyName = new System.Windows.Forms.ColumnHeader();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.AssemblyFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clhCreateDate
            // 
            resources.ApplyResources(this.clhCreateDate, "clhCreateDate");
            // 
            // DatabasesComboBox
            // 
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.DatabasesComboBox, "DatabasesComboBox");
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            this.DatabasesComboBox.SelectedIndexChanged += new System.EventHandler(this.Databases_SelectedIndexChangedComboBox);
            // 
            // DropAssemblyButton
            // 
            resources.ApplyResources(this.DropAssemblyButton, "DropAssemblyButton");
            this.DropAssemblyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.DropAssemblyButton.Name = "DropAssemblyButton";
            this.DropAssemblyButton.Click += new System.EventHandler(this.DropAssemblyButton_Click);
            // 
            // AddAssemblyButton
            // 
            resources.ApplyResources(this.AddAssemblyButton, "AddAssemblyButton");
            this.AddAssemblyButton.Margin = new System.Windows.Forms.Padding(1, 0, 3, 3);
            this.AddAssemblyButton.Name = "AddAssemblyButton";
            this.AddAssemblyButton.Click += new System.EventHandler(this.AddAssemblyButton_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label3.Name = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Name = "label2";
            // 
            // clhAssemblyVersion
            // 
            resources.ApplyResources(this.clhAssemblyVersion, "clhAssemblyVersion");
            // 
            // AssembliesListView
            // 
            resources.ApplyResources(this.AssembliesListView, "AssembliesListView");
            this.AssembliesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhAssemblyName,
            this.clhAssemblyVersion,
            this.clhCreateDate});
            this.AssembliesListView.FullRowSelect = true;
            this.AssembliesListView.HideSelection = false;
            this.AssembliesListView.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.AssembliesListView.MultiSelect = false;
            this.AssembliesListView.Name = "AssembliesListView";
            this.AssembliesListView.View = System.Windows.Forms.View.Details;
            this.AssembliesListView.SelectedIndexChanged += new System.EventHandler(this.Assemblies_SelectedIndexChangedListView);
            // 
            // clhAssemblyName
            // 
            resources.ApplyResources(this.clhAssemblyName, "clhAssemblyName");
            // 
            // openFileDialog1
            // 
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // AssemblyFileTextBox
            // 
            resources.ApplyResources(this.AssemblyFileTextBox, "AssemblyFileTextBox");
            this.AssemblyFileTextBox.Name = "AssemblyFileTextBox";
            this.AssemblyFileTextBox.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // LoadRegAssembly
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.DatabasesComboBox);
            this.Controls.Add(this.DropAssemblyButton);
            this.Controls.Add(this.AddAssemblyButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AssembliesListView);
            this.Controls.Add(this.AssemblyFileTextBox);
            this.Controls.Add(this.label1);
            this.Name = "LoadRegAssembly";
            this.Load += new System.EventHandler(this.LoadRegAssembly_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoadRegAssembly_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader clhCreateDate;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.Button DropAssemblyButton;
        private System.Windows.Forms.Button AddAssemblyButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader clhAssemblyVersion;
        private System.Windows.Forms.ListView AssembliesListView;
        private System.Windows.Forms.ColumnHeader clhAssemblyName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox AssemblyFileTextBox;
        private System.Windows.Forms.Label label1;
    }
}

