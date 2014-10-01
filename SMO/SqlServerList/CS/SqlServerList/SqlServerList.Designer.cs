namespace Microsoft.Samples.SqlServer
{
    partial class SQLServerList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLServerList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.serverListBox1 = new System.Windows.Forms.ListBox();
            this.enumAvailableSqlServersLabel = new System.Windows.Forms.Label();
            this.serverListBox2 = new System.Windows.Forms.ListBox();
            this.getDataSourcesLabel = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.serverListBox1);
            this.splitContainer1.Panel1.Controls.Add(this.enumAvailableSqlServersLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.serverListBox2);
            this.splitContainer1.Panel2.Controls.Add(this.getDataSourcesLabel);
            // 
            // serverListBox1
            // 
            resources.ApplyResources(this.serverListBox1, "serverListBox1");
            this.serverListBox1.FormattingEnabled = true;
            this.serverListBox1.Name = "serverListBox1";
            this.serverListBox1.Sorted = true;
            // 
            // enumAvailableSqlServersLabel
            // 
            resources.ApplyResources(this.enumAvailableSqlServersLabel, "enumAvailableSqlServersLabel");
            this.enumAvailableSqlServersLabel.Name = "enumAvailableSqlServersLabel";
            // 
            // serverListBox2
            // 
            resources.ApplyResources(this.serverListBox2, "serverListBox2");
            this.serverListBox2.FormattingEnabled = true;
            this.serverListBox2.Name = "serverListBox2";
            this.serverListBox2.Sorted = true;
            // 
            // getDataSourcesLabel
            // 
            resources.ApplyResources(this.getDataSourcesLabel, "getDataSourcesLabel");
            this.getDataSourcesLabel.Name = "getDataSourcesLabel";
            // 
            // SQLServerList
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer1);
            this.Name = "SQLServerList";
            this.Load += new System.EventHandler(this.SQLServerList_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox serverListBox1;
        private System.Windows.Forms.ListBox serverListBox2;
        private System.Windows.Forms.Label enumAvailableSqlServersLabel;
        private System.Windows.Forms.Label getDataSourcesLabel;
    }
}

