namespace Microsoft.Samples.SqlServer
{
    partial class ServerInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerInfo));
            this.ValueColumnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.PropertyColumnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.ConnectionListView = new System.Windows.Forms.ListView();
            this.ServerListView = new System.Windows.Forms.ListView();
            this.PropertyColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.ValueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ValueColumnHeader2
            // 
            this.ValueColumnHeader2.Name = "ValueColumnHeader2";
            resources.ApplyResources(this.ValueColumnHeader2, "ValueColumnHeader2");
            // 
            // PropertyColumnHeader2
            // 
            this.PropertyColumnHeader2.Name = "PropertyColumnHeader2";
            resources.ApplyResources(this.PropertyColumnHeader2, "PropertyColumnHeader2");
            // 
            // ConnectionListView
            // 
            this.ConnectionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PropertyColumnHeader2,
            this.ValueColumnHeader2});
            resources.ApplyResources(this.ConnectionListView, "ConnectionListView");
            this.ConnectionListView.FullRowSelect = true;
            this.ConnectionListView.Name = "ConnectionListView";
            this.ConnectionListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ConnectionListView.View = System.Windows.Forms.View.Details;
            // 
            // ServerListView
            // 
            this.ServerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PropertyColumnHeader,
            this.ValueColumnHeader});
            resources.ApplyResources(this.ServerListView, "ServerListView");
            this.ServerListView.FullRowSelect = true;
            this.ServerListView.MultiSelect = false;
            this.ServerListView.Name = "ServerListView";
            this.ServerListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ServerListView.View = System.Windows.Forms.View.Details;
            // 
            // PropertyColumnHeader
            // 
            resources.ApplyResources(this.PropertyColumnHeader, "PropertyColumnHeader");
            // 
            // ValueColumnHeader
            // 
            resources.ApplyResources(this.ValueColumnHeader, "ValueColumnHeader");
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ServerListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ConnectionListView);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.RefreshButton);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // RefreshButton
            // 
            resources.ApplyResources(this.RefreshButton, "RefreshButton");
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ServerInfo
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "ServerInfo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerInfo_FormClosed);
            this.Load += new System.EventHandler(this.ServerInfo_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ValueColumnHeader2;
        private System.Windows.Forms.ColumnHeader PropertyColumnHeader2;
        private System.Windows.Forms.ListView ConnectionListView;
        private System.Windows.Forms.ListView ServerListView;
        private System.Windows.Forms.ColumnHeader PropertyColumnHeader;
        private System.Windows.Forms.ColumnHeader ValueColumnHeader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

