using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class MsgSerialization {

	public void msgDeserialization (){
		string aciResponseData = "<ummisco.gama.network.common.CompositeGamaMessage>\n  <unread>true</unread>\n  <sender class=\"string\">Gama</sender>\n  <receivers class=\"string\">Unity</receivers>\n  <contents class=\"string\">&lt;string&gt; This message is sent from Gama to Unity &lt;/string&gt;</contents>\n  <emissionTimeStamp>633</emissionTimeStamp>\n</ummisco.gama.network.common.CompositeGamaMessage>";

		using(TextReader sr = new StringReader(aciResponseData))
		{
			var serializer = new System.Xml.Serialization.XmlSerializer(typeof(GamaMsg));
			GamaMsg msg =  (GamaMsg)serializer.Deserialize(sr);
			UnityEngine.Debug.Log("-------------> " +msg.unread);
		}
	}

}




[System.Xml.Serialization.XmlRoot("ummisco.gama.network.common.CompositeGamaMessage")]
public class GamaMsg
{
	public string unread;
	public string sender;
}