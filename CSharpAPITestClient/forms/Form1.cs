using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CSharpAPITestClient.coms;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using Application = System.Windows.Forms.Application;
using Settings = CSharpAPITestClient.Properties.Settings;

namespace CSharpAPITestClient.forms
{
    public partial class Form1 : Form
    {
        private int _listBoxSelectedIndexNo;
        private Connection conn;
        public QuickFix.Transport.SocketInitiator initiator;

        public void AppendSenderBox(string value)
        {
            if (!value.Contains(Environment.NewLine))
            {
                value = value + "\r\n";
            }
            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(new Action<string>(AppendSenderBox), new object[] {value});
                }
                catch (ObjectDisposedException ex)
                {
                    
                }
                return;
            }
            textBox1.Text += value;

            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        public void AppendReceiverBox(string value)
        {
            if (!value.Contains(Environment.NewLine))
            {
                value = value + "\r\n";
            }
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendReceiverBox), new object[] { value });
                return;
            }
            textBox2.Text += value;

            textBox2.SelectionStart = textBox1.Text.Length;
            textBox2.ScrollToCaret();
        }
        public Form1()
        {
            InitializeComponent();
            conn = new Connection();

            SessionSettings settings = new SessionSettings("session_client.txt");
            IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
            ILogFactory logFactory = new FileLogFactory(settings);
            initiator = new QuickFix.Transport.SocketInitiator(conn, storeFactory, settings, logFactory);
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.btcc.com/apidocs/pro-exchange-trade-fix-api");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(600,400);
            try
            {
                DataContainer.Data = Util.Load();
            }
            catch
            {
            }

            listBox1.DataSource = DataContainer.Data;
            listBox1.DisplayMember = "Name";
            listBox1.SelectedIndex = -1;

            if (string.IsNullOrEmpty(Properties.Settings.Default.ACCESS_KEY) ||
                string.IsNullOrEmpty(Properties.Settings.Default.SECRET_KEY) ||
                string.IsNullOrEmpty(Properties.Settings.Default.Account))
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataContainer.form1.Close();
        }

        private void request0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAccountInfoRequest formAIR = new FormAccountInfoRequest();
            formAIR.ShowDialog();
        }

        private void request1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewOrderSingleRequest formNOSR = new FormNewOrderSingleRequest();
            formNOSR.ShowDialog();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfig formC = new FormConfig();
            formC.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataContainer.Data.RemoveAt(listBox1.SelectedIndex);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _listBoxSelectedIndexNo = listBox1.SelectedIndex;
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.Save(DataContainer.Data);
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            switch (DataContainer.Data[listBox1.SelectedIndex].MsgType)
            {
                case "U1000":
                    FormAccountInfoRequestEdit formAIREdit = new FormAccountInfoRequestEdit
                    {
                        listBoxSelectedIndexNo = listBox1.SelectedIndex
                    };
                    
                    formAIREdit.ShowDialog();
                    break;
                case "D":
                    FormNewSingleOrderRequestEdit formNSOREdit = new FormNewSingleOrderRequestEdit
                    {
                        listBoxSelectedIndexNo = listBox1.SelectedIndex
                    };

                    formNSOREdit.ShowDialog();
                    break;
                case "H":
                    FormOrderStatusRequestEdit formOSREdit = new FormOrderStatusRequestEdit
                    {
                        listBoxSelectedIndexNo = listBox1.SelectedIndex
                    };

                    formOSREdit.ShowDialog();
                    break;
                case "AF2":
                    FormBatchOrderInfoRequestEdit formBOIREdit = new FormBatchOrderInfoRequestEdit
                    {
                        listBoxSelectedIndexNo = listBox1.SelectedIndex
                    };

                    formBOIREdit.ShowDialog();
                    break;
                case "G":
                    FormOrderCancelReplaceRequestEdit formOCRREdit = new FormOrderCancelReplaceRequestEdit
                    {
                        listBoxSelectedIndexNo = listBox1.SelectedIndex
                    };
                    
                    formOCRREdit.ShowDialog();
                    break;
                case "F":
                    FormOrderCancelRequestEdit formOCREdit = new FormOrderCancelRequestEdit
                    {
                        listBoxSelectedIndexNo = listBox1.SelectedIndex
                    };
                    
                    formOCREdit.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem.Enabled = !String.IsNullOrEmpty(Properties.Settings.Default.Account) && !String.IsNullOrEmpty(Properties.Settings.Default.ACCESS_KEY) && !String.IsNullOrEmpty(Properties.Settings.Default.SECRET_KEY);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.textBoxDescription.Text =
                @"This is a simple GUI program designed to help our FIX API users/clients to test FIX connection and simple FIX requests/responses. A FIX message have compulsory and optional fields. Only compulsory fields are tested in this program.";
            about.ShowDialog();
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(@"Are you sure to delete all saved data ??", @"Confirm Reset!!",
                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                DataContainer.Data.Clear();
            }
            else
            {
                
            }
        }

        private void request2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOrderStatusRequest formOSR = new FormOrderStatusRequest();
            formOSR.ShowDialog();
        }

        private void batchOrderInfoRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBatchOrderInfoRequest formBOIR = new FormBatchOrderInfoRequest();
            formBOIR.ShowDialog();
        }

        private void orderCancelReplaceRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOrderCancelReplaceRequest formOCRR = new FormOrderCancelReplaceRequest();
            formOCRR.ShowDialog();
        }

        private void orderCancelRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOrderCancelRequest formOCR = new FormOrderCancelRequest();
            formOCR.ShowDialog();
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listBox1.IndexFromPoint(e.X, e.Y) > -1)
                {
                    listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
                }
                else
                {
                    listBox1.SelectedIndex = -1;
                    button1.Enabled = false;
                }
                
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (listBox1.IndexFromPoint(e.X, e.Y) == -1)
                {
                    listBox1.SelectedIndex = -1;
                    button1.Enabled = false;
                }
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            button1.Enabled = listBox1.SelectedIndex > -1;
            _listBoxSelectedIndexNo = listBox1.SelectedIndex;
        }

        private void SendSelectedRequest()
        {
            switch (DataContainer.Data[_listBoxSelectedIndexNo].MsgType)
            {
                case "U1000":
                    MsgAIR requestData = (MsgAIR) DataContainer.Data[_listBoxSelectedIndexNo];
                    AccountInfoRequest request = new AccountInfoRequest();

                    request.Set(new Account(Properties.Settings.Default.Account));
                    request.Set(new AccReqID(requestData.AccReqID));
                    //testforJack
                    //request.Set(new SubscriptionRequestType('1'));

                    if (!initiator.IsStopped)
                    {
                        bool ret = Session.SendToTarget(request, conn.m_sessionID);
                    }
                    else
                    {
                        MessageBox.Show(@"Connection is not established!!", @"Warning", MessageBoxButtons.OK);
                    }
                    break;
                case "D":
                    MsgNOSR requestData1 = (MsgNOSR) DataContainer.Data[_listBoxSelectedIndexNo];
                    NewOrderSingle request1 = new NewOrderSingle();

                    request1.Set(new Account(Properties.Settings.Default.Account));
                    request1.Set(new ClOrdID(requestData1.ClOrdID));
                    request1.Set(new OrderQty(decimal.Parse(requestData1.OrderQty)));
                    request1.Set(new OrdType(char.Parse(requestData1.OrdType)));
                    request1.Set(new Side(char.Parse(requestData1.Side)));
                    request1.Set(new Symbol(requestData1.Symbol));
                    request1.Set(new TransactTime(DateTime.Now));

                    if (!initiator.IsStopped)
                    {
                        bool ret = Session.SendToTarget(request1, conn.m_sessionID);
                    }
                    else
                    {
                        MessageBox.Show(@"Connection is not established!!", @"Warning", MessageBoxButtons.OK);
                    }
                    break;
                case "H":
                    MsgOSR requestData2 = (MsgOSR) DataContainer.Data[_listBoxSelectedIndexNo];
                    OrderStatusRequest request2 = new OrderStatusRequest();

                    request2.Set(new Account(Properties.Settings.Default.Account));
                    request2.Set(new ClOrdID(requestData2.ClOrdID));
                    request2.Set(new Side(char.Parse(requestData2.Side)));
                    request2.Set(new Symbol(requestData2.Symbol));
                    request2.Set(new OrderID(requestData2.OrderID));

                    if (!initiator.IsStopped)
                    {
                        bool ret = Session.SendToTarget(request2, conn.m_sessionID);
                    }
                    else
                    {
                        MessageBox.Show(@"Connection is not established!!", @"Warning", MessageBoxButtons.OK);
                    }
                    break;
                case "AF2":
                    MsgBOIR requestData3 = (MsgBOIR) DataContainer.Data[_listBoxSelectedIndexNo];
                    OrderMassStatusRequest request3 = new OrderMassStatusRequest();

                    request3.Set(new Account(Properties.Settings.Default.Account));
                    request3.Set(new Symbol(requestData3.Symbol));
                    request3.Set(new MassStatusReqID(requestData3.MassStatusReqID));
                    //request3.Set(new NoStatuses(int.Parse(requestData3.GroupNums)));

                    if (!initiator.IsStopped)
                    {
                        bool ret = Session.SendToTarget(request3, conn.m_sessionID);
                    }
                    else
                    {
                        MessageBox.Show(@"Connection is not established!!", @"Warning", MessageBoxButtons.OK);
                    }
                    break;
                case "G":
                    MsgOCRR requestData4 = (MsgOCRR) DataContainer.Data[_listBoxSelectedIndexNo];
                    OrderCancelReplaceRequest request4 = new OrderCancelReplaceRequest();

                    request4.Set(new Account(Properties.Settings.Default.Account));
                    request4.Set(new ClOrdID(requestData4.ClOrdID));
                    request4.Set(new OrderQty(decimal.Parse(requestData4.OrderQty)));
                    request4.Set(new OrderID(requestData4.OrderID));

                    if (!initiator.IsStopped)
                    {
                        bool ret = Session.SendToTarget(request4, conn.m_sessionID);
                    }
                    else
                    {
                        MessageBox.Show(@"Connection is not established!!", @"Warning", MessageBoxButtons.OK);
                    }
                    break;
                case "F":
                    MsgOCR requestData5 = (MsgOCR) DataContainer.Data[_listBoxSelectedIndexNo];
                    OrderCancelRequest request5 = new OrderCancelRequest();

                    request5.Set(new Account(Properties.Settings.Default.Account));
                    request5.Set(new ClOrdID(requestData5.ClOrdID));
                    request5.Set(new OrderID(requestData5.OrderID));
                    request5.Set(new Symbol(requestData5.Symbol));
                    request5.Set(new TransactTime(DateTime.Now));

                    if (!initiator.IsStopped)
                    {
                        bool ret = Session.SendToTarget(request5, conn.m_sessionID);
                    }
                    else
                    {
                        MessageBox.Show(@"Connection is not established!!", @"Warning", MessageBoxButtons.OK);
                    }
                    break;
                default:
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Properties.Settings.Default.Account = SignEngine.GetAccountString(Properties.Settings.Default.ACCESS_KEY,
                    Properties.Settings.Default.SECRET_KEY);
            Settings.Default.Save();
            SendSelectedRequest();
            listBox1.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            initiator.Start();
            button2.Enabled = false;
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!initiator.IsStopped)
            {
                initiator.Stop();
                textBox1.AppendText(@"Connection stopped manually!!!" + "\r\n");
                textBox2.AppendText(@"Connection stopped mannally!!!" + "\r\n");
                button2.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
            else
            {
                textBox1.AppendText(@"Connection is not running!!!" + "\r\n");
                textBox2.AppendText(@"Connection is not running!!!" + "\r\n");
                Cursor.Current = Cursors.Default;
            }
        }

        private void form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            initiator.Stop();
            Application.Exit();
        }
    }
}
