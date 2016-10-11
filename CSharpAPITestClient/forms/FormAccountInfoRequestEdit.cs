using System;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormAccountInfoRequestEdit : FormAccountInfoRequest
    {
        public FormAccountInfoRequestEdit()
        {
            InitializeComponent();
        }

        private void FormAccountInfoRequestEdit_Load(object sender, EventArgs e)
        {
            textBox2.Text = DataContainer.Data[listBoxSelectedIndexNo].Name;
            textBox1.Text = ((MsgAIR)DataContainer.Data[listBoxSelectedIndexNo]).AccReqID;
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                MsgAIR msgDatAir = new MsgAIR
                {
                    Name = textBox2.Text,
                    AccReqID = textBox1.Text,
                    Account = Properties.Settings.Default.Account
                };

                DataContainer.Data[listBoxSelectedIndexNo] = msgDatAir;
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }
    }
}
