using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using CSharpAPITestClient.forms;

namespace CSharpAPITestClient
{

    #region MessageClasses

    [XmlInclude(typeof(MsgAIR))]
    [XmlInclude(typeof(MsgNOSR))]
    [XmlInclude(typeof(MsgOSR))]
    [XmlInclude(typeof(MsgBOIR))]
    [XmlInclude(typeof(MsgOCRR))]
    [XmlInclude(typeof(MsgOCR))]
    public abstract class MsgClass
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string MsgType { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
    public class MsgAIR : MsgClass
    {
        public MsgAIR()
        {
            MsgType = "U1000";
        }
        public string AccReqID { get; set; }
    }

    public class MsgNOSR : MsgClass
    {
        public MsgNOSR()
        {
            MsgType = "D";
        }

        public string ClOrdID { get; set; }
        public string OrderQty { get; set; }
        public string OrdType { get; set; }
        public string Side { get; set; }
        public string Symbol { get; set; }
        public DateTime TransactTime { get; set; }
    }

    public class MsgOSR : MsgClass
    {
        public MsgOSR()
        {
            MsgType = "H";
        }
        public string ClOrdID { get; set; }
        public string Side { get; set; }
        public string Symbol { get; set; }
        public string OrderID { get; set; }
    }

    public class MsgBOIR : MsgClass
    {
        public MsgBOIR()
        {
            MsgType = "AF2";
        }
        public string Symbol { get; set; }
        public string MassStatusReqID { get; set; }
        public string GroupNums { get; set; }
        public string OrdStatus { get; set; }
    }

    public class MsgOCRR : MsgClass
    {
        public MsgOCRR()
        {
            MsgType = "G";
        }
        public string ClOrdID { get; set; }
        public string OrderQty { get; set; }
        public string OrderID { get; set; }
    }

    public class MsgOCR : MsgClass
    {
        public MsgOCR()
        {
            MsgType = "F";
        }
        public string ClOrdID { get; set; }
        public string OrderID { get; set; }
        public string Symbol { get; set; }
        public DateTime TransactTime { get; set; }
    }


    #endregion


    public static class DataContainer
    {
        public static BindingList<MsgClass> Data = new BindingList<MsgClass>();
        public static Form1 form1;
    }

    public static class Util
    {
        public static void Save(BindingList<MsgClass> data)
        {
            XmlSerializer writer = new XmlSerializer(typeof(BindingList<MsgClass>));
            FileStream file = new FileStream("data.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            writer.Serialize(file, data);
            file.Close();
        }

        public static BindingList<MsgClass> Load()
        {
            BindingList<MsgClass> data = new BindingList<MsgClass>();
            XmlSerializer reader = new XmlSerializer(typeof(BindingList<MsgClass>));
            StreamReader file = new StreamReader("data.xml");
            data = (BindingList<MsgClass>) reader.Deserialize(file);
            file.Close();
            return data;
        }

    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(DataContainer.form1 = new Form1());

    }
        static void OnProcessExit(object sender, EventArgs e)
        {
            Util.Save(DataContainer.Data);
        }
    }
}
