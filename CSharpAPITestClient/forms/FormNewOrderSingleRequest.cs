using System;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormNewOrderSingleRequest : Form
    {
        public FormNewOrderSingleRequest()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormNewOrderSingleRequest_Load(object sender, EventArgs e)
        {
            textBox4.Text = Properties.Settings.Default.Account;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = Guid.NewGuid().ToString();
        }

        public virtual void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox5.Text) && 
                
                comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1 && comboBox3.SelectedIndex > -1)
            {
                MsgNOSR msgNOSR = new MsgNOSR
                {
                    Name = textBox5.Text,
                    Account = Properties.Settings.Default.Account,
                    ClOrdID = textBox1.Text,
                    OrderQty = textBox2.Text,
                    OrdType = comboBox1.SelectedItem.ToString(),
                    Side = comboBox2.SelectedItem.ToString(),
                    Symbol = comboBox3.SelectedItem.ToString(),
                    TransactTime = DateTime.Now
                };

                DataContainer.Data.Add(msgNOSR);
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }
    }
}
