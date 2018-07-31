using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.GamaUnityMessage")]
	public class GamaMessage
	{
		public string unread { set; get; }

		public string sender { set; get; }

		public string receivers { set; get; }

		public string contents { set; get; }

		public string emissionTimeStamp { set; get; }

		public string unityAction { set; get; }

		public string unityObject { set; get; }

		public object unityAttribute { set; get; }

		public string unityValue { set; get; }

		public GamaMessage ()
		{
			
		}


		public string getContents ()
		{
			return this.contents;
		}

		public string getAction ()
		{
			return this.unityAction;
		}

		public string getObjectName ()
		{
			return this.unityObject;
		}

		public object getObjectAttribute ()
		{
			return this.unityAttribute;
		}

		public string getAttributeValue ()
		{
			return this.unityValue;
		}


	}

}

