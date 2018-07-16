using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class MsgSerialization {

	public GamaMsg msgDeserialization (string aciResponseData){
		GamaMsg msg;
		using(TextReader sr = new StringReader(aciResponseData))
		{
			var serializer = new System.Xml.Serialization.XmlSerializer(typeof(GamaMsg));
			GamaMsg result =  (GamaMsg)serializer.Deserialize(sr);
			msg = result;
		}
		return msg;
	}


	public string getMsgAttribute(string aciResponseData, string att){
		
		using(TextReader sr = new StringReader(aciResponseData))
		{
			var serializer = new System.Xml.Serialization.XmlSerializer(typeof(GamaMsg));
			GamaMsg msg =  (GamaMsg)serializer.Deserialize(sr);

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




[System.Xml.Serialization.XmlRoot("ummisco.gama.network.common.CompositeGamaMessage")]
public class GamaMsg
{
	public string unread;
	public string sender;
	public string receivers;
	public string contents;
	public string emissionTimeStamp;
}

