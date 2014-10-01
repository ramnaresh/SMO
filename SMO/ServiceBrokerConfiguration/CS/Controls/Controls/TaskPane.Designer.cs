using System.Windows.Forms;

namespace Microsoft.Samples.SqlServer.Controls
{
    partial class TaskPane
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            this.TaskTabControl = new System.Windows.Forms.TabControl();
            this.ObjectsTabPage = new System.Windows.Forms.TabPage();
            this.sqlConnectionControl = new Microsoft.Samples.SqlServer.Controls.SqlConnectionControl();
            this.ConfigurationTabPage = new System.Windows.Forms.TabPage();
            this.configurationPanel = new Microsoft.Samples.SqlServer.Controls.ObjectsSplitPanel();
            this.TaskTabControl.SuspendLayout();
            this.ObjectsTabPage.SuspendLayout();
            this.ConfigurationTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // TaskTabControl
            // 
            this.TaskTabControl.Controls.Add(this.ObjectsTabPage);
            this.TaskTabControl.Controls.Add(this.ConfigurationTabPage);
            this.TaskTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TaskTabControl.Location = new System.Drawing.Point(0, 0);
            this.TaskTabControl.Name = "TaskTabControl";
            this.TaskTabControl.SelectedIndex = 0;
            this.TaskTabControl.Size = new System.Drawing.Size(391, 511);
            this.TaskTabControl.TabIndex = 0;
            this.TaskTabControl.SelectedIndexChanged += new System.EventHandler(this.TaskTabControl_SelectedIndexChanged);
            // 
            // ObjectsTabPage
            // 
            this.ObjectsTabPage.Controls.Add(this.sqlConnectionControl);
            this.ObjectsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ObjectsTabPage.Name = "ObjectsTabPage";
            this.ObjectsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ObjectsTabPage.Size = new System.Drawing.Size(383, 485);
            this.ObjectsTabPage.TabIndex = 0;
            this.ObjectsTabPage.Text = "Objects";
            this.ObjectsTabPage.UseVisualStyleBackColor = true;
            // 
            // sqlConnectionControl
            // 
            this.sqlConnectionControl.BackColor = System.Drawing.Color.Transparent;
            this.sqlConnectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlConnectionControl.Location = new System.Drawing.Point(3, 3);
            this.sqlConnectionControl.Name = "sqlConnectionControl";
            this.sqlConnectionControl.ObjectsSplitPanel = null;
            this.sqlConnectionControl.OptionsConfiguration = null;
            this.sqlConnectionControl.Size = new System.Drawing.Size(377, 479);
            this.sqlConnectionControl.TabIndex = 0;
            // 
            // ConfigurationTabPage
            // 
            this.ConfigurationTabPage.Controls.Add(this.configurationPanel);
            this.ConfigurationTabPage.Location = new System.Drawing.Point(4, 22);
            this.ConfigurationTabPage.Name = "ConfigurationTabPage";
            this.ConfigurationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConfigurationTabPage.Size = new System.Drawing.Size(383, 485);
            this.ConfigurationTabPage.TabIndex = 1;
            this.ConfigurationTabPage.Text = "Configuration";
            this.ConfigurationTabPage.UseVisualStyleBackColor = true;
            // 
            // configurationPanel
            // 
            this.configurationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationPanel.Location = new System.Drawing.Point(3, 3);
            this.configurationPanel.Name = "configurationPanel";
            this.configurationPanel.Size = new System.Drawing.Size(377, 479);
            this.configurationPanel.TabIndex = 0;
            // 
            // TaskPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TaskTabControl);
            this.Name = "TaskPane";
            this.Size = new System.Drawing.Size(391, 511);
            this.TaskTabControl.ResumeLayout(false);
            this.ObjectsTabPage.ResumeLayout(false);
            this.ConfigurationTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Samples.SqlServer.Controls.SqlConnectionControl sqlConnectionControl;
        private Microsoft.Samples.SqlServer.Controls.ObjectsSplitPanel configurationPanel;

        private System.Windows.Forms.TabControl TaskTabControl;
        private System.Windows.Forms.TabPage ObjectsTabPage;
        private System.Windows.Forms.TabPage ConfigurationTabPage;

        public SqlConnectionControl SqlConnectionControl
        {
            get
            {
                return this.sqlConnectionControl;
            }
        }

        public ObjectsSplitPanel ConfigurationPanel
        {
            get
            {
                return this.configurationPanel;
            }
        }
    }
}
