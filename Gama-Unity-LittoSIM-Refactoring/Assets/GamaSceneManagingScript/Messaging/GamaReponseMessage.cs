using System;

namespace ummisco.gama.unity.messages
{
	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.GamaReponseMessage")]
	public class GamaReponseMessage
	{
		public Boolean unread { get; set;}

		public string sender { get; set;}

		public string receivers { get; set;}

	
		public string contents { get; set;}

		public string emissionTimeStamp{ get; set;}


		public GamaReponseMessage ()
		{
			
		}

		public GamaReponseMessage (string sender, string receivers, string contents, string emissionTimeStamp)
		{
			this.unread = true;
			this.sender = sender;
			this.receivers = receivers;
			this.contents = contents;
			this.emissionTimeStamp = emissionTimeStamp;
		}

	}
}

