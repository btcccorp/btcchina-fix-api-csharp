using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CSharpAPITestClient.coms
{
    static class Constants
    {
        public const string FIX_XML_PATH = "BTCC-FIX44.xml";
    }

    class Fix44XmlParser
	{
		public static string getFieldName(string tag)
		{
			string field_name = "";
			XmlDocument doc = new XmlDocument();
			doc.Load(Constants.FIX_XML_PATH);
			XmlElement rootElem = doc.DocumentElement;
			XmlNodeList field = rootElem.GetElementsByTagName("field");
			foreach (XmlNode node in field)
			{
				string field_number = ((XmlElement)node).GetAttribute("number");
				field_name = ((XmlElement)node).GetAttribute("name");
				//				string field_type = ((XmlElement)node).GetAttribute("type");
				//				Console.WriteLine("field number is: "+ field_number);  
				//				Console.WriteLine("field name is: "+ field_name);  
				//				Console.WriteLine("field type is: "+ field_type);  
				if (tag.Equals(field_number))
				{
					return field_name;
				}
			}
			return field_name;
		}

		public static string getFieldName(string group, string tag)
		{
			string field_name = "";
			XmlDocument doc = new XmlDocument();
			doc.Load(Constants.FIX_XML_PATH);
			XmlElement rootElem = doc.DocumentElement;
			XmlNodeList field = rootElem.GetElementsByTagName("field");
			foreach (XmlNode node in field)
			{
				string field_number = ((XmlElement)node).GetAttribute("number");
				field_name = ((XmlElement)node).GetAttribute("name");
				//				string field_type = ((XmlElement)node).GetAttribute("type");
				//				Console.WriteLine("field number is: "+ field_number);  
				//				Console.WriteLine("field name is: "+ field_name);  
				//				Console.WriteLine("field type is: "+ field_type);  
				if (group.Equals(field_number))
				{
					XmlNodeList value = ((XmlElement)node).GetElementsByTagName("value");
					foreach (XmlNode subnode in value)
					{
						string num = ((XmlElement)subnode).GetAttribute("enum");
						field_name = ((XmlElement)subnode).GetAttribute("description");
						if (tag.Equals(num))
						{
							return field_name;
						}
					}
					return field_name;
				}
			}
			return field_name;
		}

        public static bool isTagAGroup(string group)
        {
            string field_name = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(Constants.FIX_XML_PATH);
            XmlElement rootElem = doc.DocumentElement;
            XmlNodeList field = rootElem.GetElementsByTagName("field");
            foreach (XmlNode node in field)
            {
                string field_number = ((XmlElement)node).GetAttribute("number");
                field_name = ((XmlElement)node).GetAttribute("name");
                //				string field_type = ((XmlElement)node).GetAttribute("type");
                //				Console.WriteLine("field number is: "+ field_number);  
                //				Console.WriteLine("field name is: "+ field_name);  
                //				Console.WriteLine("field type is: "+ field_type);  
                if (group.Equals(field_number))
                {
                    XmlNodeList value = ((XmlElement)node).GetElementsByTagName("value");

                    if (value.Count != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

	    public static string getFieldValueName(string group, string tag)
	    {
	        if (isTagAGroup(group))
	        {
	            return getFieldName(group, tag);
	        }
	        else
	        {
	            return tag;
	        }
	    }
    }
}
