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
    public partial class FormOrderCancelReplaceRequest : Form
    {
        public FormOrderCancelReplaceRequest()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = Guid.NewGuid().ToString();
        }

        private void FormOrderCancelReplaceRequest_Load(object sender, EventArgs e)
        {
            textBox4.Text = Properties.Settings.Default.Account;
        }

        public virtual void button1_Click(object sender, EventArgs e)
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
                    OrderQty = textBox2.Text,
                };

                DataContainer.Data.Add(msgOCRR);
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }
    }
}
