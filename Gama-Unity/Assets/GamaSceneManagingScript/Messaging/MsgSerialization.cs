using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Xml;
using System;


namespace ummisco.gama.unity.messages
{
	public class MsgSerialization
	{

		public MsgSerialization ()
		{
		
		}

		public TopicMessage msgDeserialization (string aciResponseData)
		{
			TopicMessage msg;
			using (TextReader sr = new StringReader (aciResponseData)) {
				var serializer = new System.Xml.Serialization.XmlSerializer (typeof(TopicMessage));
				TopicMessage result = (TopicMessage)serializer.Deserialize (sr);
				msg = result;
			}
			return msg;
		}


		public SetTopicMessage desSetTopicMsg (string aciResponseData)
		{
			SetTopicMessage msg;
			using (TextReader sr = new StringReader (aciResponseData)) {
				var serializer = new System.Xml.Serialization.XmlSerializer (typeof(SetTopicMessage));
				SetTopicMessage result = (SetTopicMessage)serializer.Deserialize (sr);
				msg = result;
			}
			return msg;
		}

		public object deserialization (string aciResponseData, object classDeserialization)
		{
			object msg;
			using (TextReader sr = new StringReader (aciResponseData)) {
				var serializer = new System.Xml.Serialization.XmlSerializer (classDeserialization.GetType ());
				object result = (object)serializer.Deserialize (sr);
				msg = result;
			}
			return msg;
		}

		public ItemAttributes msgItemDeserialization (string aciResponseData)
		{
			ItemAttributes msg;
			using (TextReader sr = new StringReader (aciResponseData)) {
				var serializer = new System.Xml.Serialization.XmlSerializer (typeof(ItemAttributes));
				ItemAttributes result = (ItemAttributes)serializer.Deserialize (sr);
				msg = result;
			}
			return msg;
		}

		public string getMsgAttribute (string aciResponseData, string att)
		{
			using (TextReader sr = new StringReader (aciResponseData)) {
				var serializer = new System.Xml.Serialization.XmlSerializer (typeof(TopicMessage));
				TopicMessage msg = (TopicMessage)serializer.Deserialize (sr);
				switch (att) {
				case "unread":
					return msg.unread;
				case "sender":
					return msg.sender;
				case "receivers":
					return msg.receivers;
				case "contents":
					return msg.contents;
				case "emissionTimeStamp":
					return msg.emissionTimeStamp;
				default:
					return null;
				}
			}
		}


		public string msgSerialization (GamaReponseMessage msgResponseData)
		{
			XmlSerializer serializer = new XmlSerializer (msgResponseData.GetType ());
			var settings = new XmlWriterSettings ();
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;

			using (var stream = new StringWriter ())
			using (var writer = XmlWriter.Create (stream, settings))
 {			//using (StringWriter writer = new StringWriter ()) 

				// removes namespace
				var xmlns = new XmlSerializerNamespaces ();
				xmlns.Add (string.Empty, string.Empty);


				serializer.Serialize (writer, msgResponseData, xmlns);
	

				return stream.ToString ();
				//return writer.ToString ();
			}














		}


		public string serialization (object msgResponseData)
		{
			
			XmlSerializer serializer = new XmlSerializer (msgResponseData.GetType ());

			using (StringWriter writer = new StringWriter ()) {

				serializer.Serialize (writer, msgResponseData);
			
				return writer.ToString ();
			}
		}

		public string serializationPlainXml (object msgResponseData)
		{

			XmlSerializer serializer = new XmlSerializer (msgResponseData.GetType ());
			var settings = new XmlWriterSettings ();
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;

			using (var stream = new StringWriter ())
			using (var writer = XmlWriter.Create (stream, settings))
 {			//using (StringWriter writer = new StringWriter ()) 

				// removes namespace
				var xmlns = new XmlSerializerNamespaces ();
				xmlns.Add (string.Empty, string.Empty);


				serializer.Serialize (writer, msgResponseData, xmlns);
				return stream.ToString ();

				//return writer.ToString ();
			}
		}



	}

}