namespace Microsoft.Samples.SqlServer
{
    partial class SqlObjectBrowser
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
        /// The contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlObjectBrowser));
            this.cancelCommandButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.objectBrowserTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // cancelCommandButton
            // 
            resources.ApplyResources(this.cancelCommandButton, "cancelCommandButton");
            this.cancelCommandButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelCommandButton.Name = "cancelCommandButton";
            this.cancelCommandButton.Click += new System.EventHandler(this.cancelCommandButton_Click);
            // 
            // selectButton
            // 
            resources.ApplyResources(this.selectButton, "selectButton");
            this.selectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectButton.Name = "selectButton";
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // objectBrowserTreeView
            // 
            resources.ApplyResources(this.objectBrowserTreeView, "objectBrowserTreeView");
            this.objectBrowserTreeView.Name = "objectBrowserTreeView";
            this.objectBrowserTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.objectBrowserTreeView_AfterExpand);
            this.objectBrowserTreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.objectBrowserTreeView_AfterCollapse);
            // 
            // SqlObjectBrowser
            // 
            this.AcceptButton = this.selectButton;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cancelCommandButton;
            this.Controls.Add(this.cancelCommandButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.objectBrowserTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SqlObjectBrowser";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SqlObjectBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelCommandButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.TreeView objectBrowserTreeView;
    }
}