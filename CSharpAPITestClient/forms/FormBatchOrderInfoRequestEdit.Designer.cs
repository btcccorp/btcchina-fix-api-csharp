namespace CSharpAPITestClient.forms
{
    partial class FormBatchOrderInfoRequestEdit
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
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(76, 234);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            // 
            // button1
            // 
            this.button1.Text = "Save";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormBatchOrderInfoRequestEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(416, 300);
            this.Name = "FormBatchOrderInfoRequestEdit";
            this.Load += new System.EventHandler(this.FormBatchOrderInfoRequestEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public int listBoxSelectedIndexNo;
    }
}
