using System;

namespace ummisco.gama.unity.messages
{
	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.ReplayMessage")]
	public class ReplayMessage
	{
		public Boolean unread { get; set;}

		public string sender { get; set;}

		public string receivers { get; set;}

		public string contents { get; set;}

		public string fieldName { get; set;}

		public string fieldValue { get; set;}

		public string emissionTimeStamp{ get; set;}


		public ReplayMessage ()
		{

		}

		public ReplayMessage (string sender, string receivers, string contents, string fieldName, string fieldValue, string emissionTimeStamp)
		{
			this.unread = true;
			this.sender = sender;
			this.receivers = receivers;
			this.contents = contents;
			this.fieldName = fieldName;
			this.fieldValue = fieldValue;
			this.emissionTimeStamp = emissionTimeStamp;
		}

	}
}

