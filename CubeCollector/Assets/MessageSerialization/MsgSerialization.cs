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
			UnityEngine.Debug.Log ("The Message content -----------> is: " + result.contents);
			msg = result;
		}
		return msg;
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

