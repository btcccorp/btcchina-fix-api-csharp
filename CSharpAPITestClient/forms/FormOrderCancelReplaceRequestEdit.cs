using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormOrderCancelReplaceRequestEdit : CSharpAPITestClient.forms.FormOrderCancelReplaceRequest
    {
        public FormOrderCancelReplaceRequestEdit()
        {
            InitializeComponent();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox5.Text) &&

             !String.IsNullOrEmpty(textBox6.Text))
            {
                MsgOCRR msgOCRR = new MsgOCRR
                {
                    Name = textBox5.Text,
                    Account = Properties.Settings.Default.Account,
                    ClOrdID = textBox1.Text,
                    OrderID = textBox6.Text,
                    OrderQty = textBox2.Text
                };

                DataContainer.Data[listBoxSelectedIndexNo] = msgOCRR;
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }

        private void FormOrderCancelReplaceRequestEdit_Load(object sender, EventArgs e)
        {
            textBox5.Text = ((MsgOCRR) DataContainer.Data[listBoxSelectedIndexNo]).Name;
            textBox1.Text = ((MsgOCRR) DataContainer.Data[listBoxSelectedIndexNo]).ClOrdID;
            textBox2.Text = ((MsgOCRR) DataContainer.Data[listBoxSelectedIndexNo]).OrderQty;
            textBox6.Text = ((MsgOCRR) DataContainer.Data[listBoxSelectedIndexNo]).OrderID;
        }
    }
}
