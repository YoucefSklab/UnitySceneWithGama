using System;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot("ummisco.gama.unity.messages.GamaUnityMessage")]
	public class GamaMessage
	{
		public string unread;
		public string sender;
		public string receivers;
		public string contents;
		public string emissionTimeStamp;
		public string unityAction;
		public string unityObject;
		public string unityAttribute;
		public string unityValue;

		public GamaMessage ()
		{
			
		}
	}

}

