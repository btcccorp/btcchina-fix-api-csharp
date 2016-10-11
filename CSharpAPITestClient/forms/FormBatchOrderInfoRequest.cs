using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormBatchOrderInfoRequest : Form
    {
        public FormBatchOrderInfoRequest()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormBatchOrderInfoRequest_Load(object sender, EventArgs e)
        {
            textBox4.Text = Properties.Settings.Default.Account;
        }

        public virtual void button1_Click(object sender, EventArgs e)
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

                DataContainer.Data.Add(msgBOIR);
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }
    }
}
