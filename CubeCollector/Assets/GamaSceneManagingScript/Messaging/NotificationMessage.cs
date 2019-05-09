using System;

namespace ummisco.gama.unity.messages
{
	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.NotificationMessage")]
	public class NotificationMessage
	{

		public Boolean unread { get; set;}

		public string sender { get; set;}

		public string receivers { get; set;}

		public string contents { get; set;}

		public string emissionTimeStamp{ get; set;}

		public string notificationId { get; set;}

		public NotificationMessage (string sender, string receivers, string contents, string emissionTimeStamp, string notificationId)
		{
			this.unread = true;
			this.sender = sender;
			this.receivers = receivers;
			this.contents = contents;
			this.emissionTimeStamp = emissionTimeStamp;
			this.notificationId = notificationId;
		
		}

		public NotificationMessage (){
		
		}


	}
}

