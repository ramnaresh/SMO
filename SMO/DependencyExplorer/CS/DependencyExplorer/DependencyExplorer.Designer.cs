namespace Microsoft.Samples.SqlServer
{
    partial class DependencyExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DependencyExplorer));
            this.MainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.ConnectMenuItem = new System.Windows.Forms.MenuItem();
            this.MenuItem4 = new System.Windows.Forms.MenuItem();
            this.MenuItem5 = new System.Windows.Forms.MenuItem();
            this.MenuItem2 = new System.Windows.Forms.MenuItem();
            this.WithoutDependenciesMenuItem = new System.Windows.Forms.MenuItem();
            this.WithDependenciesMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowDependenciesmenuItem = new System.Windows.Forms.MenuItem();
            this.MenuItem3 = new System.Windows.Forms.MenuItem();
            this.ShowDropDdlmenuItem = new System.Windows.Forms.MenuItem();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.TableListView = new System.Windows.Forms.ListView();
            this.Label1 = new System.Windows.Forms.Label();
            this.StatusBar = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MainMenu1
            // 
            this.MainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem1,
            this.MenuItem2,
            this.MenuItem3});
            // 
            // MenuItem1
            // 
            this.MenuItem1.Index = 0;
            this.MenuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ConnectMenuItem,
            this.MenuItem4,
            this.MenuItem5});
            resources.ApplyResources(this.MenuItem1, "MenuItem1");
            // 
            // ConnectMenuItem
            // 
            this.ConnectMenuItem.Index = 0;
            resources.ApplyResources(this.ConnectMenuItem, "ConnectMenuItem");
            this.ConnectMenuItem.Click += new System.EventHandler(this.ConnectMenuItem_Click);
            // 
            // MenuItem4
            // 
            this.MenuItem4.Index = 1;
            resources.ApplyResources(this.MenuItem4, "MenuItem4");
            // 
            // MenuItem5
            // 
            this.MenuItem5.Index = 2;
            resources.ApplyResources(this.MenuItem5, "MenuItem5");
            this.MenuItem5.Click += new System.EventHandler(this.MenuItem5_Click);
            // 
            // MenuItem2
            // 
            this.MenuItem2.Index = 1;
            this.MenuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.WithoutDependenciesMenuItem,
            this.WithDependenciesMenuItem,
            this.ShowDependenciesmenuItem});
            resources.ApplyResources(this.MenuItem2, "MenuItem2");
            // 
            // WithoutDependenciesMenuItem
            // 
            this.WithoutDependenciesMenuItem.Index = 0;
            resources.ApplyResources(this.WithoutDependenciesMenuItem, "WithoutDependenciesMenuItem");
            this.WithoutDependenciesMenuItem.Click += new System.EventHandler(this.WithoutDependenciesMenuItem_Click);
            // 
            // WithDependenciesMenuItem
            // 
            this.WithDependenciesMenuItem.Index = 1;
            resources.ApplyResources(this.WithDependenciesMenuItem, "WithDependenciesMenuItem");
            this.WithDependenciesMenuItem.Click += new System.EventHandler(this.WithDependenciesMenuItem_Click);
            // 
            // ShowDependenciesmenuItem
            // 
            this.ShowDependenciesmenuItem.Index = 2;
            resources.ApplyResources(this.ShowDependenciesmenuItem, "ShowDependenciesmenuItem");
            this.ShowDependenciesmenuItem.Click += new System.EventHandler(this.ShowDependenciesmenuItem_Click);
            // 
            // MenuItem3
            // 
            this.MenuItem3.Index = 2;
            this.MenuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ShowDropDdlmenuItem});
            resources.ApplyResources(this.MenuItem3, "MenuItem3");
            // 
            // ShowDropDdlmenuItem
            // 
            this.ShowDropDdlmenuItem.Index = 0;
            resources.ApplyResources(this.ShowDropDdlmenuItem, "ShowDropDdlmenuItem");
            this.ShowDropDdlmenuItem.Click += new System.EventHandler(this.ShowDropDdlmenuItem_Click);
            // 
            // DatabasesComboBox
            // 
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.DatabasesComboBox, "DatabasesComboBox");
            this.DatabasesComboBox.FormattingEnabled = true;
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            this.DatabasesComboBox.SelectedIndexChanged += new System.EventHandler(this.DatabasesComboBox_SelectedIndexChanged);
            // 
            // TableListView
            // 
            resources.ApplyResources(this.TableListView, "TableListView");
            this.TableListView.HideSelection = false;
            this.TableListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("TableListView.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("TableListView.Items1")))});
            this.TableListView.MultiSelect = false;
            this.TableListView.Name = "TableListView";
            this.TableListView.UseCompatibleStateImageBehavior = false;
            // 
            // Label1
            // 
            resources.ApplyResources(this.Label1, "Label1");
            this.Label1.Name = "Label1";
            // 
            // StatusBar
            // 
            resources.ApplyResources(this.StatusBar, "StatusBar");
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.ReadOnly = true;
            // 
            // DependencyExplorer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableListView);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.DatabasesComboBox);
            this.Menu = this.MainMenu1;
            this.Name = "DependencyExplorer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DependencyExplorer_FormClosed);
            this.Load += new System.EventHandler(this.DependencyExplorer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu MainMenu1;
        private System.Windows.Forms.MenuItem MenuItem1;
        private System.Windows.Forms.MenuItem ConnectMenuItem;
        private System.Windows.Forms.MenuItem MenuItem4;
        private System.Windows.Forms.MenuItem MenuItem5;
        private System.Windows.Forms.MenuItem MenuItem2;
        private System.Windows.Forms.MenuItem WithoutDependenciesMenuItem;
        private System.Windows.Forms.MenuItem WithDependenciesMenuItem;
        private System.Windows.Forms.MenuItem ShowDependenciesmenuItem;
        private System.Windows.Forms.MenuItem MenuItem3;
        private System.Windows.Forms.MenuItem ShowDropDdlmenuItem;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.ListView TableListView;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox StatusBar;
    }
}