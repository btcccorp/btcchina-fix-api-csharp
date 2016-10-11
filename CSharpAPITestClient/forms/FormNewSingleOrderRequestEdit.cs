using System;
using System.Windows.Forms;

namespace CSharpAPITestClient.forms
{
    public partial class FormNewSingleOrderRequestEdit : FormNewOrderSingleRequest
    {
        public FormNewSingleOrderRequestEdit()
        {
            InitializeComponent();
        }

        private void FormNewSingleOrderRequestEdit_Load(object sender, EventArgs e)
        {
            textBox5.Text = ((MsgNOSR) DataContainer.Data[listBoxSelectedIndexNo]).Name;
            textBox1.Text = ((MsgNOSR) DataContainer.Data[listBoxSelectedIndexNo]).ClOrdID;
            textBox2.Text = ((MsgNOSR) DataContainer.Data[listBoxSelectedIndexNo]).OrderQty;
            comboBox1.SelectedItem = ((MsgNOSR) DataContainer.Data[listBoxSelectedIndexNo]).OrdType;
            comboBox2.SelectedItem = ((MsgNOSR) DataContainer.Data[listBoxSelectedIndexNo]).Side;
            comboBox3.SelectedItem = ((MsgNOSR) DataContainer.Data[listBoxSelectedIndexNo]).Symbol;
        }

        public override void button1_Click(object sender, EventArgs e)
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

                DataContainer.Data[listBoxSelectedIndexNo] = msgNOSR;
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }
    }
}
