using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormBatchOrderInfoRequestEdit : CSharpAPITestClient.forms.FormBatchOrderInfoRequest
    {
        public FormBatchOrderInfoRequestEdit()
        {
            InitializeComponent();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox5.Text) &&

             comboBox1.SelectedIndex > -1 && comboBox3.SelectedIndex > -1)
            {
                MsgBOIR msgBOIR = new MsgBOIR
                {
                    Name = textBox5.Text,
                    Account = Properties.Settings.Default.Account,
                    MassStatusReqID = textBox1.Text,
                    GroupNums = textBox2.Text,
                    Symbol = comboBox3.SelectedItem.ToString(),
                    OrdStatus = comboBox1.SelectedItem.ToString()
                };

                DataContainer.Data[listBoxSelectedIndexNo] = msgBOIR;
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }

        private void FormBatchOrderInfoRequestEdit_Load(object sender, EventArgs e)
        {
            textBox5.Text = ((MsgBOIR) DataContainer.Data[listBoxSelectedIndexNo]).Name;
            textBox1.Text = ((MsgBOIR) DataContainer.Data[listBoxSelectedIndexNo]).MassStatusReqID;
            textBox2.Text = ((MsgBOIR) DataContainer.Data[listBoxSelectedIndexNo]).GroupNums;
            comboBox3.SelectedItem = ((MsgBOIR) DataContainer.Data[listBoxSelectedIndexNo]).Symbol;
            comboBox1.SelectedItem = ((MsgBOIR) DataContainer.Data[listBoxSelectedIndexNo]).OrdStatus;
        }
    }
}
