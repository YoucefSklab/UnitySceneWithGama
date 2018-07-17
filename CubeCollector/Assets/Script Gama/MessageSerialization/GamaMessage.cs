using System;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot("ummisco.gama.network.common.CompositeGamaMessage")]
	public class GamaMessage
	{
		public string unread;
		public string sender;
		public string receivers;
		public string contents;
		public string emissionTimeStamp;
		public string action;

		public GamaMessage ()
		{
			
		}
	}

}

