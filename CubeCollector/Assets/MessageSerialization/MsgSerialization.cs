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



	public void texting()
	{
		string aciResponseData = "<ummisco.gama.network.common.CompositeGamaMessage>\n  <unread>true</unread>\n  <sender class=\"string\">Gama</sender>\n  <receivers class=\"string\">Unity</receivers>\n  <contents class=\"string\">&lt;string&gt; This message is sent from Gama to Unity &lt;/string&gt;</contents>\n  <emissionTimeStamp>633</emissionTimeStamp>\n</ummisco.gama.network.common.CompositeGamaMessage>";
		using(TextReader sr = new StringReader(aciResponseData))
		{
			var serializer = new System.Xml.Serialization.XmlSerializer(typeof(MyClass));
			MyClass response =  (MyClass)serializer.Deserialize(sr);
			UnityEngine.Debug.Log ("The Message unread is: "+response.unread);
			UnityEngine.Debug.Log ("The Message sender is: "+response.sender);
			UnityEngine.Debug.Log ("The Message receivers is: "+response.receivers);
			UnityEngine.Debug.Log ("The Message content is: "+response.contents);
			UnityEngine.Debug.Log ("The Message emissionTimeStamp is: "+response.emissionTimeStamp);
		}
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

[System.Xml.Serialization.XmlRoot("ummisco.gama.network.common.CompositeGamaMessage")]
public class MyClass
{
	public string unread;
	public string sender;
	public string receivers;
	public string contents;
	public string emissionTimeStamp;
}
