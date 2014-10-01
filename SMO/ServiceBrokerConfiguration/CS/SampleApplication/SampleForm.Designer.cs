namespace Microsoft.Samples.SqlServer
{
    partial class TestForm
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            this.taskPane1 = new Microsoft.Samples.SqlServer.Controls.TaskPane();
            this.SuspendLayout();
            // 
            // taskPane1
            // 
            this.taskPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskPane1.Location = new System.Drawing.Point(0, 0);
            this.taskPane1.Name = "taskPane1";
            this.taskPane1.Size = new System.Drawing.Size(513, 552);
            this.taskPane1.TabIndex = 0;
            this.taskPane1.Load += new System.EventHandler(this.taskPane1_Load);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 552);
            this.Controls.Add(this.taskPane1);
            this.Name = "TestForm";
            this.Text = "Configuration Sample Form";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Samples.SqlServer.Controls.TaskPane taskPane1;
    }
}

