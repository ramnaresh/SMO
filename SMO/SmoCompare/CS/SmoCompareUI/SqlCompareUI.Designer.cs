namespace Microsoft.Samples.SqlServer
{
    partial class SqlCompareUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlCompareUI));
            this.value1ColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.urn2ColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.serverLabel1 = new System.Windows.Forms.Label();
            this.value2ColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.propertyNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.differencesListView = new System.Windows.Forms.ListView();
            this.urn1ColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.shallowCompareButton = new System.Windows.Forms.Button();
            this.serverTextBox1 = new System.Windows.Forms.TextBox();
            this.urnCompareGroupBox = new System.Windows.Forms.GroupBox();
            this.urnTextBox1 = new System.Windows.Forms.TextBox();
            this.object1URNLabel = new System.Windows.Forms.Label();
            this.object2URNLabel = new System.Windows.Forms.Label();
            this.urnTextBox2 = new System.Windows.Forms.TextBox();
            this.objectBrowse2Button = new System.Windows.Forms.Button();
            this.objectBrowse1Button = new System.Windows.Forms.Button();
            this.serverLoginGroupBox = new System.Windows.Forms.GroupBox();
            this.serverLabel2 = new System.Windows.Forms.Label();
            this.serverTextBox2 = new System.Windows.Forms.TextBox();
            this.inDepthCompareButton = new System.Windows.Forms.Button();
            this.passwordTextBox2 = new System.Windows.Forms.TextBox();
            this.loginTextBox2 = new System.Windows.Forms.TextBox();
            this.loginLabel2 = new System.Windows.Forms.Label();
            this.passwordLabel2 = new System.Windows.Forms.Label();
            this.passwordTextBox1 = new System.Windows.Forms.TextBox();
            this.loginTextBox1 = new System.Windows.Forms.TextBox();
            this.loginLabel1 = new System.Windows.Forms.Label();
            this.passwordLabel1 = new System.Windows.Forms.Label();
            this.genScript2to1Button = new System.Windows.Forms.Button();
            this.genScript1to2Button = new System.Windows.Forms.Button();
            this.differencesLabel = new System.Windows.Forms.Label();
            this.col1 = new System.Windows.Forms.ColumnHeader();
            this.objectListView2 = new System.Windows.Forms.ListView();
            this.col2 = new System.Windows.Forms.ColumnHeader();
            this.object2OnlyLabel = new System.Windows.Forms.Label();
            this.object1OnlyLabel = new System.Windows.Forms.Label();
            this.objectListView1 = new System.Windows.Forms.ListView();
            this.urnCompareGroupBox.SuspendLayout();
            this.serverLoginGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // value1ColumnHeader
            // 
            this.value1ColumnHeader.Name = "value1ColumnHeader";
            resources.ApplyResources(this.value1ColumnHeader, "value1ColumnHeader");
            // 
            // urn2ColumnHeader
            // 
            this.urn2ColumnHeader.Name = "urn2ColumnHeader";
            resources.ApplyResources(this.urn2ColumnHeader, "urn2ColumnHeader");
            // 
            // serverLabel1
            // 
            resources.ApplyResources(this.serverLabel1, "serverLabel1");
            this.serverLabel1.Name = "serverLabel1";
            // 
            // value2ColumnHeader
            // 
            this.value2ColumnHeader.Name = "value2ColumnHeader";
            resources.ApplyResources(this.value2ColumnHeader, "value2ColumnHeader");
            // 
            // propertyNameColumnHeader
            // 
            this.propertyNameColumnHeader.Name = "propertyNameColumnHeader";
            resources.ApplyResources(this.propertyNameColumnHeader, "propertyNameColumnHeader");
            // 
            // differencesListView
            // 
            resources.ApplyResources(this.differencesListView, "differencesListView");
            this.differencesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.propertyNameColumnHeader,
            this.urn1ColumnHeader,
            this.urn2ColumnHeader,
            this.value1ColumnHeader,
            this.value2ColumnHeader});
            this.differencesListView.FullRowSelect = true;
            this.differencesListView.GridLines = true;
            this.differencesListView.Name = "differencesListView";
            this.differencesListView.View = System.Windows.Forms.View.Details;
            // 
            // urn1ColumnHeader
            // 
            resources.ApplyResources(this.urn1ColumnHeader, "urn1ColumnHeader");
            // 
            // shallowCompareButton
            // 
            resources.ApplyResources(this.shallowCompareButton, "shallowCompareButton");
            this.shallowCompareButton.Name = "shallowCompareButton";
            this.shallowCompareButton.Click += new System.EventHandler(this.shallowCompareButton_Click);
            // 
            // serverTextBox1
            // 
            resources.ApplyResources(this.serverTextBox1, "serverTextBox1");
            this.serverTextBox1.Name = "serverTextBox1";
            this.serverTextBox1.TextChanged += new System.EventHandler(this.serverTextBox1_TextChanged);
            // 
            // urnCompareGroupBox
            // 
            resources.ApplyResources(this.urnCompareGroupBox, "urnCompareGroupBox");
            this.urnCompareGroupBox.Controls.Add(this.urnTextBox1);
            this.urnCompareGroupBox.Controls.Add(this.object1URNLabel);
            this.urnCompareGroupBox.Controls.Add(this.object2URNLabel);
            this.urnCompareGroupBox.Controls.Add(this.urnTextBox2);
            this.urnCompareGroupBox.Controls.Add(this.objectBrowse2Button);
            this.urnCompareGroupBox.Controls.Add(this.objectBrowse1Button);
            this.urnCompareGroupBox.Name = "urnCompareGroupBox";
            this.urnCompareGroupBox.TabStop = false;
            // 
            // urnTextBox1
            // 
            resources.ApplyResources(this.urnTextBox1, "urnTextBox1");
            this.urnTextBox1.Name = "urnTextBox1";
            this.urnTextBox1.ReadOnly = true;
            // 
            // object1URNLabel
            // 
            resources.ApplyResources(this.object1URNLabel, "object1URNLabel");
            this.object1URNLabel.Name = "object1URNLabel";
            // 
            // object2URNLabel
            // 
            resources.ApplyResources(this.object2URNLabel, "object2URNLabel");
            this.object2URNLabel.Name = "object2URNLabel";
            // 
            // urnTextBox2
            // 
            resources.ApplyResources(this.urnTextBox2, "urnTextBox2");
            this.urnTextBox2.Name = "urnTextBox2";
            this.urnTextBox2.ReadOnly = true;
            // 
            // objectBrowse2Button
            // 
            resources.ApplyResources(this.objectBrowse2Button, "objectBrowse2Button");
            this.objectBrowse2Button.Name = "objectBrowse2Button";
            this.objectBrowse2Button.Click += new System.EventHandler(this.objectBrowse2Button_Click);
            // 
            // objectBrowse1Button
            // 
            resources.ApplyResources(this.objectBrowse1Button, "objectBrowse1Button");
            this.objectBrowse1Button.Name = "objectBrowse1Button";
            this.objectBrowse1Button.Click += new System.EventHandler(this.objectBrowse1Button_Click);
            // 
            // serverLoginGroupBox
            // 
            resources.ApplyResources(this.serverLoginGroupBox, "serverLoginGroupBox");
            this.serverLoginGroupBox.Controls.Add(this.serverLabel1);
            this.serverLoginGroupBox.Controls.Add(this.shallowCompareButton);
            this.serverLoginGroupBox.Controls.Add(this.serverTextBox1);
            this.serverLoginGroupBox.Controls.Add(this.serverLabel2);
            this.serverLoginGroupBox.Controls.Add(this.serverTextBox2);
            this.serverLoginGroupBox.Controls.Add(this.inDepthCompareButton);
            this.serverLoginGroupBox.Controls.Add(this.passwordTextBox2);
            this.serverLoginGroupBox.Controls.Add(this.loginTextBox2);
            this.serverLoginGroupBox.Controls.Add(this.loginLabel2);
            this.serverLoginGroupBox.Controls.Add(this.passwordLabel2);
            this.serverLoginGroupBox.Controls.Add(this.passwordTextBox1);
            this.serverLoginGroupBox.Controls.Add(this.loginTextBox1);
            this.serverLoginGroupBox.Controls.Add(this.loginLabel1);
            this.serverLoginGroupBox.Controls.Add(this.passwordLabel1);
            this.serverLoginGroupBox.Name = "serverLoginGroupBox";
            this.serverLoginGroupBox.TabStop = false;
            // 
            // serverLabel2
            // 
            resources.ApplyResources(this.serverLabel2, "serverLabel2");
            this.serverLabel2.Name = "serverLabel2";
            // 
            // serverTextBox2
            // 
            resources.ApplyResources(this.serverTextBox2, "serverTextBox2");
            this.serverTextBox2.Name = "serverTextBox2";
            this.serverTextBox2.TextChanged += new System.EventHandler(this.serverTextBox2_TextChanged);
            // 
            // inDepthCompareButton
            // 
            resources.ApplyResources(this.inDepthCompareButton, "inDepthCompareButton");
            this.inDepthCompareButton.Name = "inDepthCompareButton";
            this.inDepthCompareButton.Click += new System.EventHandler(this.inDepthCompareButton_Click);
            // 
            // passwordTextBox2
            // 
            resources.ApplyResources(this.passwordTextBox2, "passwordTextBox2");
            this.passwordTextBox2.Name = "passwordTextBox2";
            this.passwordTextBox2.TextChanged += new System.EventHandler(this.passwordTextBox2_TextChanged);
            // 
            // loginTextBox2
            // 
            resources.ApplyResources(this.loginTextBox2, "loginTextBox2");
            this.loginTextBox2.Name = "loginTextBox2";
            this.loginTextBox2.TextChanged += new System.EventHandler(this.loginTextBox2_TextChanged);
            // 
            // loginLabel2
            // 
            resources.ApplyResources(this.loginLabel2, "loginLabel2");
            this.loginLabel2.Name = "loginLabel2";
            // 
            // passwordLabel2
            // 
            resources.ApplyResources(this.passwordLabel2, "passwordLabel2");
            this.passwordLabel2.Name = "passwordLabel2";
            // 
            // passwordTextBox1
            // 
            resources.ApplyResources(this.passwordTextBox1, "passwordTextBox1");
            this.passwordTextBox1.Name = "passwordTextBox1";
            this.passwordTextBox1.TextChanged += new System.EventHandler(this.passwordTextBox1_TextChanged);
            // 
            // loginTextBox1
            // 
            resources.ApplyResources(this.loginTextBox1, "loginTextBox1");
            this.loginTextBox1.Name = "loginTextBox1";
            this.loginTextBox1.TextChanged += new System.EventHandler(this.loginTextBox1_TextChanged);
            // 
            // loginLabel1
            // 
            resources.ApplyResources(this.loginLabel1, "loginLabel1");
            this.loginLabel1.Name = "loginLabel1";
            // 
            // passwordLabel1
            // 
            resources.ApplyResources(this.passwordLabel1, "passwordLabel1");
            this.passwordLabel1.Name = "passwordLabel1";
            // 
            // genScript2to1Button
            // 
            resources.ApplyResources(this.genScript2to1Button, "genScript2to1Button");
            this.genScript2to1Button.Name = "genScript2to1Button";
            this.genScript2to1Button.Click += new System.EventHandler(this.genScript2to1Button_Click);
            // 
            // genScript1to2Button
            // 
            resources.ApplyResources(this.genScript1to2Button, "genScript1to2Button");
            this.genScript1to2Button.Name = "genScript1to2Button";
            this.genScript1to2Button.Click += new System.EventHandler(this.genScript1to2Button_Click);
            // 
            // differencesLabel
            // 
            resources.ApplyResources(this.differencesLabel, "differencesLabel");
            this.differencesLabel.Name = "differencesLabel";
            // 
            // col1
            // 
            this.col1.Name = "col1";
            resources.ApplyResources(this.col1, "col1");
            // 
            // objectListView2
            // 
            resources.ApplyResources(this.objectListView2, "objectListView2");
            this.objectListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col2});
            this.objectListView2.FullRowSelect = true;
            this.objectListView2.GridLines = true;
            this.objectListView2.Name = "objectListView2";
            this.objectListView2.View = System.Windows.Forms.View.Details;
            // 
            // col2
            // 
            resources.ApplyResources(this.col2, "col2");
            // 
            // object2OnlyLabel
            // 
            resources.ApplyResources(this.object2OnlyLabel, "object2OnlyLabel");
            this.object2OnlyLabel.Name = "object2OnlyLabel";
            // 
            // object1OnlyLabel
            // 
            resources.ApplyResources(this.object1OnlyLabel, "object1OnlyLabel");
            this.object1OnlyLabel.Name = "object1OnlyLabel";
            // 
            // objectListView1
            // 
            resources.ApplyResources(this.objectListView1, "objectListView1");
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1});
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.GridLines = true;
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // SqlCompareUI
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.differencesListView);
            this.Controls.Add(this.urnCompareGroupBox);
            this.Controls.Add(this.serverLoginGroupBox);
            this.Controls.Add(this.genScript2to1Button);
            this.Controls.Add(this.genScript1to2Button);
            this.Controls.Add(this.differencesLabel);
            this.Controls.Add(this.objectListView2);
            this.Controls.Add(this.object2OnlyLabel);
            this.Controls.Add(this.object1OnlyLabel);
            this.Controls.Add(this.objectListView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SqlCompareUI";
            this.urnCompareGroupBox.ResumeLayout(false);
            this.urnCompareGroupBox.PerformLayout();
            this.serverLoginGroupBox.ResumeLayout(false);
            this.serverLoginGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader value1ColumnHeader;
        private System.Windows.Forms.ColumnHeader urn2ColumnHeader;
        private System.Windows.Forms.Label serverLabel1;
        private System.Windows.Forms.ColumnHeader value2ColumnHeader;
        private System.Windows.Forms.ColumnHeader propertyNameColumnHeader;
        private System.Windows.Forms.ListView differencesListView;
        private System.Windows.Forms.ColumnHeader urn1ColumnHeader;
        private System.Windows.Forms.Button shallowCompareButton;
        private System.Windows.Forms.TextBox serverTextBox1;
        private System.Windows.Forms.GroupBox urnCompareGroupBox;
        private System.Windows.Forms.TextBox urnTextBox1;
        private System.Windows.Forms.Label object1URNLabel;
        private System.Windows.Forms.Label object2URNLabel;
        private System.Windows.Forms.TextBox urnTextBox2;
        private System.Windows.Forms.Button objectBrowse2Button;
        private System.Windows.Forms.Button objectBrowse1Button;
        private System.Windows.Forms.GroupBox serverLoginGroupBox;
        private System.Windows.Forms.Label serverLabel2;
        private System.Windows.Forms.TextBox serverTextBox2;
        private System.Windows.Forms.Button inDepthCompareButton;
        private System.Windows.Forms.TextBox passwordTextBox2;
        private System.Windows.Forms.TextBox loginTextBox2;
        private System.Windows.Forms.Label loginLabel2;
        private System.Windows.Forms.Label passwordLabel2;
        private System.Windows.Forms.TextBox passwordTextBox1;
        private System.Windows.Forms.TextBox loginTextBox1;
        private System.Windows.Forms.Label loginLabel1;
        private System.Windows.Forms.Label passwordLabel1;
        private System.Windows.Forms.Button genScript2to1Button;
        private System.Windows.Forms.Button genScript1to2Button;
        private System.Windows.Forms.Label differencesLabel;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ListView objectListView2;
        private System.Windows.Forms.ColumnHeader col2;
        private System.Windows.Forms.Label object2OnlyLabel;
        private System.Windows.Forms.Label object1OnlyLabel;
        private System.Windows.Forms.ListView objectListView1;
    }
}

