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
    public partial class FormOrderStatusRequest : Form
    {
        public FormOrderStatusRequest()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormOrderStatusRequest_Load(object sender, EventArgs e)
        {
            textBox4.Text = Properties.Settings.Default.Account;
        }

        public virtual void button1_Click(object sender, EventArgs e)
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

                DataContainer.Data.Add(msgOSR);
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = Guid.NewGuid().ToString();
        }
    }
}
