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
    public partial class FormOrderCancelRequest : Form
    {
        public FormOrderCancelRequest()
        {
            InitializeComponent();
        }

        private void FormOrderCancelRequest_Load(object sender, EventArgs e)
        {
            textBox4.Text = Properties.Settings.Default.Account;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = Guid.NewGuid().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public virtual void button1_Click(object sender, EventArgs e)
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

                DataContainer.Data.Add(msgOCR);
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }
    }
}
