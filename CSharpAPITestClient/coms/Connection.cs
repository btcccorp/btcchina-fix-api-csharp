using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickFix;
using QuickFix.FIX44;
using CSharpAPITestClient.forms;

namespace CSharpAPITestClient.coms
{
    class Connection : MessageCracker, IApplication
    {
        //public ManualResetEvent Started = new ManualResetEvent(false);
        
        public enum MessageDisplayer
        {
            Sender,
            Receiver
        }
        private void MessageDisplay(QuickFix.Message msg, SessionID sessionID, MessageDisplayer msgDisplayer)
        {
            if (msgDisplayer == MessageDisplayer.Sender)
            {
                DataContainer.form1.AppendSenderBox($@"=================={msg.GetType()}==================");
                DataContainer.form1.AppendSenderBox("@" + sessionID.ToString());
                var msgLst = msg.ToList();
                var msgHeaderLst = msg.Header.ToList();

                foreach (var item in msgHeaderLst)
                {
                    DataContainer.form1.AppendSenderBox(Fix44XmlParser.getFieldName(item.Key.ToString()) + @": " + Fix44XmlParser.getFieldValueName(item.Key.ToString(), item.Value.ToString()));
                }
                foreach (var item in msgLst)
                {
                    DataContainer.form1.AppendSenderBox(Fix44XmlParser.getFieldName(item.Key.ToString()) + @": " + Fix44XmlParser.getFieldValueName(item.Key.ToString(), item.Value.ToString()));
                }
            }
            else if (msgDisplayer == MessageDisplayer.Receiver)
            {
                DataContainer.form1.AppendReceiverBox($@"=================={msg.GetType()}==================");
                DataContainer.form1.AppendReceiverBox("@" + sessionID.ToString());
                var msgLst = msg.ToList();
                var msgHeaderLst = msg.Header.ToList();

                foreach (var item in msgHeaderLst)
                {
                    DataContainer.form1.AppendReceiverBox(Fix44XmlParser.getFieldName(item.Key.ToString()) + @": " + Fix44XmlParser.getFieldValueName(item.Key.ToString(), item.Value.ToString()));
                }
                foreach (var item in msgLst)
                {
                    DataContainer.form1.AppendReceiverBox(Fix44XmlParser.getFieldName(item.Key.ToString()) + @": " + Fix44XmlParser.getFieldValueName(item.Key.ToString(), item.Value.ToString()));
                }
            }
        }
        public SessionID m_sessionID { get; set; }

        #region Application Methods
        public void OnCreate(SessionID sessionID)
        {
            m_sessionID = sessionID;
        }

        public void OnLogon(SessionID sessionID)
        {
            //Started.Set();
        }

        public void OnLogout(SessionID sessionID)
        {

        }

        public void FromAdmin(QuickFix.Message msg, SessionID sessionID)
        {
            DataContainer.form1.AppendReceiverBox(@"FromAdmin() invoked!!!!");
            MessageDisplay(msg, sessionID, MessageDisplayer.Receiver);
        }

        public void ToAdmin(QuickFix.Message msg, SessionID sessionID)
        {
            MessageDisplay(msg, sessionID, MessageDisplayer.Sender);
        }

        public void ToApp(QuickFix.Message msg, SessionID sessionID)
        {
            MessageDisplay(msg, sessionID, MessageDisplayer.Sender);
        }

        public void FromApp(QuickFix.Message msg, SessionID sessionID)
        {
            //MessageDisplay(msg, sessionID, MessageDisplayer.Receiver);
            DataContainer.form1.AppendReceiverBox(@"FromApp() invoked!!!!");
            try
            {
                Crack(msg, sessionID);

            }
            catch (Exception ex)
            {
                DataContainer.form1.AppendReceiverBox(@"==Cracker exception==");
                DataContainer.form1.AppendReceiverBox(ex.ToString());
                DataContainer.form1.AppendReceiverBox(ex.StackTrace);
            }
        }

        #endregion

        #region Message Responses

        public void OnMessage(BusinessMessageReject msg, SessionID sessionID)
        {
            MessageDisplay(msg, sessionID, MessageDisplayer.Receiver);
        }

        public void OnMessage(NewOrderSingle msg, SessionID sessionID)
        {
            MessageDisplay(msg, sessionID, MessageDisplayer.Sender);
        }

        public void OnMessage(OrderCancelReplaceRequest msg, SessionID sessionID)
        {
            MessageDisplay(msg, sessionID, MessageDisplayer.Sender);
        }
        public void OnMessage(OrderCancelRequest msg, SessionID sessionID)
        {
            MessageDisplay(msg, sessionID, MessageDisplayer.Sender);
        }
        public void OnMessage(AccountInfoResponse msg, SessionID sessionID)
        {
            MessageDisplay(msg, sessionID, MessageDisplayer.Receiver);
        }
        public void OnMessage(ExecutionReport msg, SessionID sessionID)
        {
            MessageDisplay(msg, sessionID, MessageDisplayer.Receiver);
        }


        #endregion
    }
}
