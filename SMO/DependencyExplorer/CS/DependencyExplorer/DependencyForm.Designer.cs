namespace Microsoft.Samples.SqlServer
{
    partial class DependencyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DependencyForm));
            this.ValueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.NameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DependenciesTreeView = new System.Windows.Forms.TreeView();
            this.PropertiesListView = new System.Windows.Forms.ListView();
            this.WhereUsedMenuItem = new System.Windows.Forms.MenuItem();
            this.NodeContextMenu = new System.Windows.Forms.ContextMenu();
            this.DependenciesMenuItem = new System.Windows.Forms.MenuItem();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ValueColumnHeader
            // 
            resources.ApplyResources(this.ValueColumnHeader, "ValueColumnHeader");
            // 
            // NameColumnHeader
            // 
            resources.ApplyResources(this.NameColumnHeader, "NameColumnHeader");
            // 
            // SplitContainer1
            // 
            resources.ApplyResources(this.SplitContainer1, "SplitContainer1");
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.DependenciesTreeView);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.PropertiesListView);
            // 
            // DependenciesTreeView
            // 
            resources.ApplyResources(this.DependenciesTreeView, "DependenciesTreeView");
            this.DependenciesTreeView.Name = "DependenciesTreeView";
            this.DependenciesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DependenciesTreeView_AfterSelect);
            // 
            // PropertiesListView
            // 
            this.PropertiesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumnHeader,
            this.ValueColumnHeader});
            resources.ApplyResources(this.PropertiesListView, "PropertiesListView");
            this.PropertiesListView.FullRowSelect = true;
            this.PropertiesListView.Name = "PropertiesListView";
            this.PropertiesListView.View = System.Windows.Forms.View.Details;
            // 
            // WhereUsedMenuItem
            // 
            this.WhereUsedMenuItem.Index = 0;
            resources.ApplyResources(this.WhereUsedMenuItem, "WhereUsedMenuItem");
            this.WhereUsedMenuItem.Click += new System.EventHandler(this.WhereUsedMenuItem_Click);
            // 
            // NodeContextMenu
            // 
            this.NodeContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.WhereUsedMenuItem,
            this.DependenciesMenuItem});
            // 
            // DependenciesMenuItem
            // 
            this.DependenciesMenuItem.Index = 1;
            resources.ApplyResources(this.DependenciesMenuItem, "DependenciesMenuItem");
            this.DependenciesMenuItem.Click += new System.EventHandler(this.DependenciesMenuItem_Click);
            // 
            // DependencyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainer1);
            this.Name = "DependencyForm";
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ValueColumnHeader;
        private System.Windows.Forms.ColumnHeader NameColumnHeader;
        private System.Windows.Forms.SplitContainer SplitContainer1;
        private System.Windows.Forms.TreeView DependenciesTreeView;
        private System.Windows.Forms.ListView PropertiesListView;
        private System.Windows.Forms.MenuItem WhereUsedMenuItem;
        private System.Windows.Forms.ContextMenu NodeContextMenu;
        private System.Windows.Forms.MenuItem DependenciesMenuItem;
    }
}

