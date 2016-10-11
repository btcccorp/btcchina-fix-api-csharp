using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormOrderStatusRequestEdit : CSharpAPITestClient.forms.FormOrderStatusRequest
    {
        public FormOrderStatusRequestEdit()
        {
            InitializeComponent();
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && comboBox2.SelectedIndex > -1 && !String.IsNullOrEmpty(textBox5.Text) &&

              comboBox3.SelectedIndex > -1 && !String.IsNullOrEmpty(textBox6.Text))
            {
                MsgOSR msgOSR = new MsgOSR
                {
                    Name = textBox5.Text,
                    Account = Properties.Settings.Default.Account,
                    ClOrdID = textBox1.Text,
                    Side = comboBox2.SelectedItem.ToString(),
                    Symbol = comboBox3.SelectedItem.ToString(),
                    OrderID = textBox6.Text
                };

                DataContainer.Data[listBoxSelectedIndexNo] = msgOSR;
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }

        private void FormOrderStatusRequestEdit_Load(object sender, EventArgs e)
        {
            textBox5.Text = ((MsgOSR) DataContainer.Data[listBoxSelectedIndexNo]).Name;
            textBox1.Text = ((MsgOSR) DataContainer.Data[listBoxSelectedIndexNo]).ClOrdID;
            comboBox2.SelectedItem = ((MsgOSR) DataContainer.Data[listBoxSelectedIndexNo]).Side;
            comboBox3.SelectedItem = ((MsgOSR) DataContainer.Data[listBoxSelectedIndexNo]).Symbol;
            textBox6.Text = ((MsgOSR)DataContainer.Data[listBoxSelectedIndexNo]).OrderID;
        }
    }
}
