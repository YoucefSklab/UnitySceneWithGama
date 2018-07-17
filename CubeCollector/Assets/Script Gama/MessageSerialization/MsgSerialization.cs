using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;


namespace ummisco.gama.unity
{
		

	public class MsgSerialization {

		public GamaMessage msgDeserialization (string aciResponseData){
			GamaMessage msg;
			using(TextReader sr = new StringReader(aciResponseData))
			{
				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(GamaMessage));
				GamaMessage result =  (GamaMessage)serializer.Deserialize(sr);
				msg = result;
			}
			return msg;
		}


		public string getMsgAttribute(string aciResponseData, string att){
			
			using(TextReader sr = new StringReader(aciResponseData))
			{
				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(GamaMessage));
				GamaMessage msg =  (GamaMessage)serializer.Deserialize(sr);

				switch (att)
				{
				case "unread":
					return msg.unread;
					break;
				case "sender":
					return msg.sender;
					break;
				case "receivers":
					return msg.receivers;
					break;
				case "contents":
					return msg.contents;
					break;
				case "emissionTimeStamp":
					return msg.emissionTimeStamp;
					break;
				default:
					return null;
					break;
				}
			}
			return null;
		}
	}

}