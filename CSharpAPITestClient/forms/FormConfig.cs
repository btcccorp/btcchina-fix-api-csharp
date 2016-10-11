using System;
using System.Windows.Forms;
using CSharpAPITestClient.coms;
using CSharpAPITestClient.Properties;

namespace CSharpAPITestClient.forms
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
            {
                Settings.Default.Account = SignEngine.GetAccountString(Settings.Default.ACCESS_KEY,
                    Settings.Default.SECRET_KEY);
                Settings.Default.Save();
                this.Close();
            }
            else
            {
                MessageBox.Show(@"Required fields not specified", @"Warning", MessageBoxButtons.OK);
            }
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {

        }
    }
}
