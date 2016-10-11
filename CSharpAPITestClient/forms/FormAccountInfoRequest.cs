using System;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormAccountInfoRequest : Form
    {
        public FormAccountInfoRequest()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAccountInfoRequest_Load(object sender, EventArgs e)
        {
            textBox4.Text = Properties.Settings.Default.Account;
        }

        public virtual void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                MsgAIR msgDatAir = new MsgAIR
                {
                    Name = textBox2.Text,
                    AccReqID = textBox1.Text,
                    Account = Properties.Settings.Default.Account
                };

                DataContainer.Data.Add(msgDatAir);
                this.Close();
            }
            else
            {
                MessageBox.Show("Required fields not specified", "Warning", MessageBoxButtons.OK);
            }
        }
    }
}
