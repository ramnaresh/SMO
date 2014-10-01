namespace Microsoft.Samples.SqlServer
{
    partial class DatabaseDefrag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseDefrag));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.DatabasesComboBox = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.defragmentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            // 
            // DatabasesComboBox
            // 
            resources.ApplyResources(this.DatabasesComboBox, "DatabasesComboBox");
            this.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabasesComboBox.FormattingEnabled = true;
            this.DatabasesComboBox.Name = "DatabasesComboBox";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.Label1, "Label1");
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Name = "Label1";
            // 
            // defragmentButton
            // 
            resources.ApplyResources(this.defragmentButton, "defragmentButton");
            this.defragmentButton.Name = "defragmentButton";
            this.defragmentButton.UseVisualStyleBackColor = true;
            this.defragmentButton.Click += new System.EventHandler(this.defragmentButton_Click);
            // 
            // DatabaseDefrag
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.defragmentButton);
            this.Controls.Add(this.DatabasesComboBox);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "DatabaseDefrag";
            this.Load += new System.EventHandler(this.DatabaseDefrag_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DatabaseDefrag_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox DatabasesComboBox;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button defragmentButton;
    }
}

