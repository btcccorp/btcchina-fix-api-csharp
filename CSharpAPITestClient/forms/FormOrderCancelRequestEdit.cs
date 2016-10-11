using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormOrderCancelRequestEdit : CSharpAPITestClient.forms.FormOrderCancelRequest
    {
        public FormOrderCancelRequestEdit()
        {
            InitializeComponent();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox5.Text) &&

            comboBox3.SelectedIndex > -1)
            {
                MsgOCR msgOCR = new MsgOCR
                {
                    Name = textBox5.Text,
                    Account = Properties.Settings.Default.Account,
                    ClOrdID = textBox1.Text,
                    OrderID = textBox2.Text,
                    Symbol = comboBox3.SelectedItem.ToString(),
                    TransactTime = DateTime.Now
                };

                DataContainer.Data[listBoxSelectedIndexNo] = msgOCR;
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }

        private void FormOrderCancelRequestEdit_Load(object sender, EventArgs e)
        {
            textBox1.Text = ((MsgOCR) DataContainer.Data[listBoxSelectedIndexNo]).ClOrdID;
            textBox2.Text = ((MsgOCR) DataContainer.Data[listBoxSelectedIndexNo]).OrderID;
            textBox5.Text = ((MsgOCR) DataContainer.Data[listBoxSelectedIndexNo]).Name;
            comboBox3.SelectedItem = ((MsgOCR) DataContainer.Data[listBoxSelectedIndexNo]).Symbol;
        }
    }
}
