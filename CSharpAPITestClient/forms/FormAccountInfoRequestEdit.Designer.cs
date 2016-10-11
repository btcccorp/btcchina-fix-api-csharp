namespace CSharpAPITestClient.forms
{
    partial class FormAccountInfoRequestEdit
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
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            // 
            // textBox2
            // 
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            // 
            // button1
            // 
            this.button1.Text = "Save";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormAccountInfoRequestEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(362, 174);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAccountInfoRequestEdit";
            this.Load += new System.EventHandler(this.FormAccountInfoRequestEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public int listBoxSelectedIndexNo;
    }
}
