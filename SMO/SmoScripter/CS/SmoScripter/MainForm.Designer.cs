namespace Microsoft.Samples.SqlServer
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.serverVersionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.speedToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SqlServerTreeView = new System.Windows.Forms.TreeView();
            this.ListView = new System.Windows.Forms.ListView();
            this.ListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptwithDependenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dependenciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ListViewContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverVersionToolStripStatusLabel,
            this.speedToolStripStatusLabel});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // serverVersionToolStripStatusLabel
            // 
            this.serverVersionToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.serverVersionToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.serverVersionToolStripStatusLabel.Name = "serverVersionToolStripStatusLabel";
            resources.ApplyResources(this.serverVersionToolStripStatusLabel, "serverVersionToolStripStatusLabel");
            // 
            // speedToolStripStatusLabel
            // 
            this.speedToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.speedToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.speedToolStripStatusLabel.Name = "speedToolStripStatusLabel";
            resources.ApplyResources(this.speedToolStripStatusLabel, "speedToolStripStatusLabel");
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SqlServerTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ListView);
            // 
            // SqlServerTreeView
            // 
            resources.ApplyResources(this.SqlServerTreeView, "SqlServerTreeView");
            this.SqlServerTreeView.Name = "SqlServerTreeView";
            this.SqlServerTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SqlServerTreeView_AfterSelect);
            this.SqlServerTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.SqlServerTreeView_AfterExpand);
            // 
            // ListView
            // 
            this.ListView.ContextMenuStrip = this.ListViewContextMenuStrip;
            resources.ApplyResources(this.ListView, "ListView");
            this.ListView.FullRowSelect = true;
            this.ListView.GridLines = true;
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.Details;
            this.ListView.DoubleClick += new System.EventHandler(this.ListView_DoubleClick);
            // 
            // ListViewContextMenuStrip
            // 
            this.ListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptToolStripMenuItem,
            this.scriptwithDependenciesToolStripMenuItem,
            this.dependenciesToolStripMenuItem});
            this.ListViewContextMenuStrip.Name = "contextMenuStrip1";
            resources.ApplyResources(this.ListViewContextMenuStrip, "ListViewContextMenuStrip");
            this.ListViewContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ListViewContextMenuStrip_Opening);
            // 
            // scriptToolStripMenuItem
            // 
            this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
            resources.ApplyResources(this.scriptToolStripMenuItem, "scriptToolStripMenuItem");
            this.scriptToolStripMenuItem.Click += new System.EventHandler(this.scriptToolStripMenuItem_Click);
            // 
            // scriptwithDependenciesToolStripMenuItem
            // 
            this.scriptwithDependenciesToolStripMenuItem.Name = "scriptwithDependenciesToolStripMenuItem";
            resources.ApplyResources(this.scriptwithDependenciesToolStripMenuItem, "scriptwithDependenciesToolStripMenuItem");
            this.scriptwithDependenciesToolStripMenuItem.Click += new System.EventHandler(this.scriptwithDependenciesToolStripMenuItem_Click);
            // 
            // dependenciesToolStripMenuItem
            // 
            this.dependenciesToolStripMenuItem.Name = "dependenciesToolStripMenuItem";
            resources.ApplyResources(this.dependenciesToolStripMenuItem, "dependenciesToolStripMenuItem");
            this.dependenciesToolStripMenuItem.Click += new System.EventHandler(this.dependenciesToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ListViewContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel serverVersionToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel speedToolStripStatusLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView ListView;
        private System.Windows.Forms.ContextMenuStrip ListViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptwithDependenciesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dependenciesToolStripMenuItem;
        private System.Windows.Forms.TreeView SqlServerTreeView;
    }
}

