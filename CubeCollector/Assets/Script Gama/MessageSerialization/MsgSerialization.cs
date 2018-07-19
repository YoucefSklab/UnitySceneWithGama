﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Xml;


namespace ummisco.gama.unity.messages
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


		public string msgSerialization (GamaReponseMessage msgResponseData){

			string msg = "";

	


			XmlSerializer serializer = new XmlSerializer(msgResponseData.GetType ());
			using (StringWriter writer = new StringWriter())
			{
				serializer.Serialize(writer, msgResponseData);
				return writer.ToString();
			}

			return msg;
		}

	}

}