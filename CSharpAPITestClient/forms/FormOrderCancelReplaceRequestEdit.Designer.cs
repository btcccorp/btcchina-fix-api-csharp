namespace CSharpAPITestClient.forms
{
    partial class FormOrderCancelReplaceRequestEdit
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
            // button2
            // 
            this.button2.Enabled = false;
            // 
            // button1
            // 
            this.button1.Text = "Save";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormOrderCancelReplaceRequestEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(440, 284);
            this.Name = "FormOrderCancelReplaceRequestEdit";
            this.Load += new System.EventHandler(this.FormOrderCancelReplaceRequestEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public int listBoxSelectedIndexNo;
    }
}
