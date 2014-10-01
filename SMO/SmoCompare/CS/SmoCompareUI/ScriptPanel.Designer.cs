namespace Microsoft.Samples.SqlServer
{
    partial class ScriptPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptPanel));
            this.runButton = new System.Windows.Forms.Button();
            this.panelTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // runButton
            // 
            resources.ApplyResources(this.runButton, "runButton");
            this.runButton.Name = "runButton";
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // panelTextBox
            // 
            resources.ApplyResources(this.panelTextBox, "panelTextBox");
            this.panelTextBox.Name = "panelTextBox";
            this.panelTextBox.ReadOnly = true;
            // 
            // ScriptPanel
            // 
            this.AcceptButton = this.runButton;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.panelTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ScriptPanel";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox panelTextBox;
    }
}