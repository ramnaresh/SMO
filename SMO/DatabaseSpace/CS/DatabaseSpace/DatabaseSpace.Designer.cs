namespace Microsoft.Samples.SqlServer
{
    partial class DatabaseSpace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseSpace));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DatabaseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatabaseSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpaceAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogSpaceSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogSpaceUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DatabaseName,
            this.DatabaseSize,
            this.SpaceAvailable,
            this.LogSpaceSize,
            this.LogSpaceUsed});
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // DatabaseName
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DatabaseName.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.DatabaseName, "DatabaseName");
            this.DatabaseName.Name = "DatabaseName";
            this.DatabaseName.ReadOnly = true;
            // 
            // DatabaseSize
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.DatabaseSize.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.DatabaseSize, "DatabaseSize");
            this.DatabaseSize.Name = "DatabaseSize";
            this.DatabaseSize.ReadOnly = true;
            // 
            // SpaceAvailable
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.SpaceAvailable.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.SpaceAvailable, "SpaceAvailable");
            this.SpaceAvailable.Name = "SpaceAvailable";
            this.SpaceAvailable.ReadOnly = true;
            // 
            // LogSpaceSize
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.LogSpaceSize.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.LogSpaceSize, "LogSpaceSize");
            this.LogSpaceSize.Name = "LogSpaceSize";
            this.LogSpaceSize.ReadOnly = true;
            // 
            // LogSpaceUsed
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            this.LogSpaceUsed.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.LogSpaceUsed, "LogSpaceUsed");
            this.LogSpaceUsed.Name = "LogSpaceUsed";
            this.LogSpaceUsed.ReadOnly = true;
            // 
            // DatabaseSpace
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "DatabaseSpace";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatabaseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatabaseSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpaceAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogSpaceSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogSpaceUsed;
    }
}

