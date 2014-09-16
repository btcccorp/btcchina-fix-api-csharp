// include QuickFix.dll in your project 
using System;
using System.Xml;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using System.Collections.Generic;

namespace BTCC_FIX_Client
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			BTCCFIXClientApp app = new BTCCFIXClientApp();
			string sessionFile =  "<YOUR PATH>/session_client.txt";
			SessionSettings settings = new SessionSettings(sessionFile);
			IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
			ILogFactory logFactory = new FileLogFactory(settings);
			QuickFix.Transport.SocketInitiator initiator = new QuickFix.Transport.SocketInitiator(app, storeFactory, settings, logFactory);
			initiator.Start();

			BTCCMarketDataRequest btccDataRequest = new BTCCMarketDataRequest();

			System.Threading.Thread.Sleep(5000);
			//request full snapshot
			MarketDataRequest dataRequest = btccDataRequest.marketDataFullSnapRequest("BTCCNY");
			bool ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			Console.WriteLine("SendToTarget ret={0}", ret);

			//			dataRequest = btccDataRequest.marketDataFullSnapRequest("LTCCNY");
			//			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			//			Console.WriteLine("SendToTarget ret={0}", ret);

			//			dataRequest = btccDataRequest.marketDataFullSnapRequest("LTCBTC");
			//			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			//			Console.WriteLine("SendToTarget ret={0}", ret);

			System.Threading.Thread.Sleep(15000);
			//request incremental request
			dataRequest = btccDataRequest.marketDataIncrementalRequest("BTCCNY");
			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			Console.WriteLine("SendToTarget ret={0}", ret);

			//			dataRequest = btccDataRequest.marketDataIncrementalRequest("LTCCNY");
			//			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			//			Console.WriteLine("SendToTarget ret={0}", ret);

			//			dataRequest = btccDataRequest.marketDataIncrementalRequest("LTCBTC");
			//			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			//			Console.WriteLine("SendToTarget ret={0}", ret);

			System.Threading.Thread.Sleep(40000);
			//unsubscribe incremental request
			dataRequest = btccDataRequest.unsubscribeIncrementalRequest("BTCCNY");
			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			Console.WriteLine("SendToTarget ret={0}", ret);

			//			dataRequest = btccDataRequest.unsubscribeIncrementalRequest("LTCCNY");
			//			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			//			Console.WriteLine("SendToTarget ret={0}", ret);

			//			dataRequest = btccDataRequest.unsubscribeIncrementalRequest("LTCBTC");
			//			ret = Session.SendToTarget(dataRequest, app.m_sessionID);
			//			Console.WriteLine("SendToTarget ret={0}", ret);
		}
	}
		
	class BTCCFIXClientApp : MessageCracker, IApplication
	{
		public SessionID m_sessionID { get; set; }

		#region Application Methods
		public void OnCreate(SessionID sessionID)
		{
			m_sessionID = sessionID;
		}

		public void OnLogon(SessionID sessionID)
		{
			Console.WriteLine("Logon - " + sessionID.ToString());

		}

		public void OnLogout(SessionID sessionID)
		{
			Console.WriteLine("Logout - " + sessionID.ToString());
		}

		public void FromAdmin(QuickFix.Message msg, SessionID sessionID)
		{
			Console.WriteLine("FromAdmin - " + msg.ToString() + "@" + sessionID.ToString());
		}

		public void ToAdmin(QuickFix.Message msg, SessionID sessionID)
		{
			Console.WriteLine("ToAdmin - " + msg.ToString() + "@" + sessionID.ToString());
		}

		public void ToApp(QuickFix.Message msg, SessionID sessionID) { 
			Console.WriteLine("ToApp - " + msg.ToString() + "@" + sessionID.ToString());
		}
		public void FromApp(QuickFix.Message msg, SessionID sessionID) { 
			Console.WriteLine("FromApp - " + msg.ToString() + "@" + sessionID.ToString());
			try
			{
				Crack(msg, sessionID);
			}
			catch (Exception ex)
			{
				Console.WriteLine("==Cracker exception==");
				Console.WriteLine(ex.ToString());
				Console.WriteLine(ex.StackTrace);
			}
		}
		#endregion

		public void OnMessage(MarketDataIncrementalRefresh msg, SessionID sessionID)
		{
			FIX44XMLParser parser = new FIX44XMLParser();

			Console.WriteLine("==========Header::==========");
			Console.WriteLine(parser.getFieldName(Tags.BeginString.ToString())+":: "+msg.Header.GetString(Tags.BeginString));
			Console.WriteLine(parser.getFieldName(Tags.BodyLength.ToString())+":: "+msg.Header.GetString(Tags.BodyLength));
			Console.WriteLine(parser.getFieldName(Tags.MsgType.ToString())+":: MarketDataIncrementalRefresh ("+msg.Header.GetString(Tags.MsgType)+")");
			Console.WriteLine(parser.getFieldName(Tags.MsgSeqNum.ToString())+":: "+msg.Header.GetString(Tags.MsgSeqNum));
			Console.WriteLine(parser.getFieldName(Tags.SenderCompID.ToString())+":: "+msg.Header.GetString(Tags.SenderCompID));
			Console.WriteLine(parser.getFieldName(Tags.SendingTime.ToString())+":: "+msg.Header.GetString(Tags.SendingTime));
			Console.WriteLine(parser.getFieldName(Tags.TargetCompID.ToString())+":: "+msg.Header.GetString(Tags.TargetCompID));

			Console.WriteLine("==========Body:: ==========");
			Console.WriteLine(parser.getFieldName(Tags.NoMDEntries.ToString())+":: "+msg.GetString(Tags.NoMDEntries));

			MarketDataIncrementalRefresh.NoMDEntriesGroup g0 = new MarketDataIncrementalRefresh.NoMDEntriesGroup();  
			for(int grpIndex = 1; grpIndex<= msg.GetInt(Tags.NoMDEntries); grpIndex += 1)
			{
				Console.WriteLine("---------- ----------");
				msg.GetGroup(grpIndex, g0);
				//				Console.WriteLine(parser.getFieldName(Tags.MDUpdateAction.ToString())+":: "+g0.GetString(Tags.MDUpdateAction));

				Console.WriteLine(parser.getFieldName(Tags.MDUpdateAction.ToString())+":: "+
					parser.getFieldName(Tags.MDUpdateAction.ToString(),g0.GetString(Tags.MDUpdateAction).ToString())+
					"("+g0.GetString(Tags.MDUpdateAction)+")"
				);

				Console.WriteLine(parser.getFieldName(Tags.MDEntryType.ToString())+":: "+
					parser.getFieldName(Tags.MDEntryType.ToString(),g0.GetString(Tags.MDEntryType).ToString())+
					"("+g0.GetString(Tags.MDEntryType)+")"
				);

				try{
					Console.WriteLine(parser.getFieldName(Tags.MDEntryPx.ToString())+":: "+g0.GetString(Tags.MDEntryPx));
				} catch (Exception ex){
					Console.WriteLine(parser.getFieldName(Tags.MDEntrySize.ToString())+":: "+g0.GetString(Tags.MDEntrySize));
				}

				Console.WriteLine(parser.getFieldName(Tags.MDEntryDate.ToString())+":: "+g0.GetString(Tags.MDEntryDate));
				Console.WriteLine(parser.getFieldName(Tags.MDEntryTime.ToString())+":: "+g0.GetString(Tags.MDEntryTime));
			}

			Console.WriteLine("==========Trailer:: ==========");
			Console.WriteLine(parser.getFieldName(Tags.CheckSum.ToString())+":: "+msg.Trailer.GetString(Tags.CheckSum));
		}

		public void OnMessage( MarketDataSnapshotFullRefresh msg, SessionID sessionID)
		{
			FIX44XMLParser parser = new FIX44XMLParser();

			Console.WriteLine("==========Header::==========");
			Console.WriteLine(parser.getFieldName(Tags.BeginString.ToString())+":: "+msg.Header.GetString(Tags.BeginString));
			Console.WriteLine(parser.getFieldName(Tags.BodyLength.ToString())+":: "+msg.Header.GetString(Tags.BodyLength));
			Console.WriteLine(parser.getFieldName(Tags.MsgType.ToString())+":: MarketDataSnapshotFullRefresh ("+msg.Header.GetString(Tags.MsgType)+")");
			Console.WriteLine(parser.getFieldName(Tags.MsgSeqNum.ToString())+":: "+msg.Header.GetString(Tags.MsgSeqNum));
			Console.WriteLine(parser.getFieldName(Tags.SenderCompID.ToString())+":: "+msg.Header.GetString(Tags.SenderCompID));
			Console.WriteLine(parser.getFieldName(Tags.SendingTime.ToString())+":: "+msg.Header.GetString(Tags.SendingTime));
			Console.WriteLine(parser.getFieldName(Tags.TargetCompID.ToString())+":: "+msg.Header.GetString(Tags.TargetCompID));

			Console.WriteLine("==========Body:: ==========");
			Console.WriteLine(parser.getFieldName(Tags.Symbol.ToString())+":: "+msg.GetString(Tags.Symbol));
			Console.WriteLine(parser.getFieldName(Tags.NoMDEntries.ToString())+":: "+msg.GetString(Tags.NoMDEntries));

			MarketDataSnapshotFullRefresh.NoMDEntriesGroup g0 = new MarketDataSnapshotFullRefresh.NoMDEntriesGroup();  
			for(int grpIndex = 1; grpIndex<= msg.GetInt(Tags.NoMDEntries); grpIndex += 1)
			{
				Console.WriteLine("---------- ----------");
				msg.GetGroup(grpIndex, g0);
				Console.WriteLine(parser.getFieldName(Tags.MDEntryType.ToString())+":: "+
					parser.getFieldName(Tags.MDEntryType.ToString(),g0.GetString(Tags.MDEntryType).ToString())+
					"("+g0.GetString(Tags.MDEntryType)+")"
				);

				try{
					Console.WriteLine(parser.getFieldName(Tags.MDEntryPx.ToString())+":: "+g0.GetString(Tags.MDEntryPx));
				} catch (Exception ex){
					Console.WriteLine(parser.getFieldName(Tags.MDEntrySize.ToString())+":: "+g0.GetString(Tags.MDEntrySize));
				}

				Console.WriteLine(parser.getFieldName(Tags.MDEntryDate.ToString())+":: "+g0.GetString(Tags.MDEntryDate));
				Console.WriteLine(parser.getFieldName(Tags.MDEntryTime.ToString())+":: "+g0.GetString(Tags.MDEntryTime));
			}

			Console.WriteLine("==========Trailer:: ==========");
			Console.WriteLine(parser.getFieldName(Tags.CheckSum.ToString())+":: "+msg.Trailer.GetString(Tags.CheckSum));

		}
	}

	class BTCCMarketDataRequest
	{
		public MarketDataRequest marketDataFullSnapRequest(string symbol)
		{
			MarketDataRequest tickerRequest = new MarketDataRequest();

			MarketDataRequest.NoRelatedSymGroup symbolGroup = new MarketDataRequest.NoRelatedSymGroup();
			symbolGroup.SetField(new Symbol(symbol)); 
			tickerRequest.AddGroup(symbolGroup);

			tickerRequest.Set(new MDReqID("123"));
			tickerRequest.Set(new SubscriptionRequestType('0'));
			tickerRequest.Set(new MarketDepth(0));

			addMDType(tickerRequest, '0');
			addMDType(tickerRequest, '1');
			addMDType(tickerRequest, '2');
			addMDType(tickerRequest, '3');
			addMDType(tickerRequest, '4');
			addMDType(tickerRequest, '5');
			addMDType(tickerRequest, '6');
			addMDType(tickerRequest, '7');
			addMDType(tickerRequest, '8');
			addMDType(tickerRequest, '9');
			addMDType(tickerRequest, 'A');
			addMDType(tickerRequest, 'B');
			addMDType(tickerRequest, 'C');

			return tickerRequest;
		}

		public MarketDataRequest marketDataIncrementalRequest(string symbol)
		{
			MarketDataRequest tickerRequest = new MarketDataRequest();

			MarketDataRequest.NoRelatedSymGroup symbolGroup = new MarketDataRequest.NoRelatedSymGroup();
			symbolGroup.SetField(new Symbol(symbol)); 
			tickerRequest.AddGroup(symbolGroup);

			tickerRequest.Set(new MDReqID("123"));
			tickerRequest.Set(new SubscriptionRequestType('1'));
			tickerRequest.Set(new MarketDepth(0));

			addMDType(tickerRequest, '0');
			addMDType(tickerRequest, '1');
			addMDType(tickerRequest, '2');
			addMDType(tickerRequest, '3');
			addMDType(tickerRequest, '4');
			addMDType(tickerRequest, '5');
			addMDType(tickerRequest, '6');
			addMDType(tickerRequest, '7');
			addMDType(tickerRequest, '8');
			addMDType(tickerRequest, '9');
			addMDType(tickerRequest, 'A');
			addMDType(tickerRequest, 'B');
			addMDType(tickerRequest, 'C');

			return tickerRequest;
		}

		public MarketDataRequest unsubscribeIncrementalRequest(string symbol)
		{
			MarketDataRequest tickerRequest = new MarketDataRequest();

			MarketDataRequest.NoRelatedSymGroup symbolGroup = new MarketDataRequest.NoRelatedSymGroup();
			symbolGroup.SetField(new Symbol(symbol)); 
			tickerRequest.AddGroup(symbolGroup);

			tickerRequest.Set(new MDReqID("123"));
			tickerRequest.Set(new SubscriptionRequestType('2'));
			tickerRequest.Set(new MarketDepth(0));

			addMDType(tickerRequest, '0');
			addMDType(tickerRequest, '1');
			addMDType(tickerRequest, '2');
			addMDType(tickerRequest, '3');
			addMDType(tickerRequest, '4');
			addMDType(tickerRequest, '5');
			addMDType(tickerRequest, '6');
			addMDType(tickerRequest, '7');
			addMDType(tickerRequest, '8');
			addMDType(tickerRequest, '9');
			addMDType(tickerRequest, 'A');
			addMDType(tickerRequest, 'B');
			addMDType(tickerRequest, 'C');

			return tickerRequest;
		}

		private static void addMDType(MarketDataRequest tickerRequest, char type) {
			MarketDataRequest.NoMDEntryTypesGroup g0 = new MarketDataRequest.NoMDEntryTypesGroup();
			g0.Set(new MDEntryType(type));
			tickerRequest.AddGroup(g0);
		}
	}

	class FIX44XMLParser{
		public string getFieldName(string tag){
			string field_name = "";
			XmlDocument doc = new XmlDocument();      
			doc.Load ("<YOUR PATH>/FIX44.xml");
			XmlElement rootElem = doc.DocumentElement;
			XmlNodeList field = rootElem.GetElementsByTagName ("field");
			foreach (XmlNode node in field)  
			{  
				string field_number = ((XmlElement)node).GetAttribute("number");
				field_name = ((XmlElement)node).GetAttribute("name");
				//				string field_type = ((XmlElement)node).GetAttribute("type");
				//				Console.WriteLine("field number is: "+ field_number);  
				//				Console.WriteLine("field name is: "+ field_name);  
				//				Console.WriteLine("field type is: "+ field_type);  
				if (tag.Equals (field_number)) {
					return field_name;
				}
			}
			return field_name;
		} 

		public string getFieldName(string group, string tag){
			string field_name = "";
			XmlDocument doc = new XmlDocument();      
			doc.Load ("<YOUR PATH>/FIX44.xml");
			XmlElement rootElem = doc.DocumentElement;
			XmlNodeList field = rootElem.GetElementsByTagName ("field");
			foreach (XmlNode node in field)  
			{  
				string field_number = ((XmlElement)node).GetAttribute("number");
				field_name = ((XmlElement)node).GetAttribute("name");
				//				string field_type = ((XmlElement)node).GetAttribute("type");
				//				Console.WriteLine("field number is: "+ field_number);  
				//				Console.WriteLine("field name is: "+ field_name);  
				//				Console.WriteLine("field type is: "+ field_type);  
				if (group.Equals (field_number)) {
					XmlNodeList value = ((XmlElement)node).GetElementsByTagName("value");
					foreach (XmlNode subnode in value){
						string num=((XmlElement)subnode).GetAttribute("enum");
						field_name=((XmlElement)subnode).GetAttribute("description");
						if(tag.Equals(num)){
							return field_name;
						}
					}
					return field_name;
				}
			}
			return field_name;
		} 
	}

}
