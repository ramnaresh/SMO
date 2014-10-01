namespace Microsoft.Samples.SqlServer
{
    partial class DependenciesForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DependenciesForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DependenciesTreeView = new System.Windows.Forms.TreeView();
            this.WhereUsedContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.WhereUsedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScriptTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.WhereUsedContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DependenciesTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ScriptTextBox);
            // 
            // DependenciesTreeView
            // 
            this.DependenciesTreeView.ContextMenuStrip = this.WhereUsedContextMenuStrip;
            resources.ApplyResources(this.DependenciesTreeView, "DependenciesTreeView");
            this.DependenciesTreeView.Name = "DependenciesTreeView";
            this.DependenciesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DependenciesTreeView_AfterSelect);
            // 
            // WhereUsedContextMenuStrip
            // 
            this.WhereUsedContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WhereUsedToolStripMenuItem});
            this.WhereUsedContextMenuStrip.Name = "WhereUsedContextMenuStrip";
            resources.ApplyResources(this.WhereUsedContextMenuStrip, "WhereUsedContextMenuStrip");
            // 
            // WhereUsedToolStripMenuItem
            // 
            this.WhereUsedToolStripMenuItem.Name = "WhereUsedToolStripMenuItem";
            resources.ApplyResources(this.WhereUsedToolStripMenuItem, "WhereUsedToolStripMenuItem");
            this.WhereUsedToolStripMenuItem.Click += new System.EventHandler(this.WhereUsedToolStripMenuItem_Click);
            // 
            // ScriptTextBox
            // 
            resources.ApplyResources(this.ScriptTextBox, "ScriptTextBox");
            this.ScriptTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.ScriptTextBox.Name = "ScriptTextBox";
            this.ScriptTextBox.ReadOnly = true;
            // 
            // DependenciesForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DependenciesForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.WhereUsedContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView DependenciesTreeView;
        private System.Windows.Forms.TextBox ScriptTextBox;
        private System.Windows.Forms.ContextMenuStrip WhereUsedContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem WhereUsedToolStripMenuItem;
    }
}