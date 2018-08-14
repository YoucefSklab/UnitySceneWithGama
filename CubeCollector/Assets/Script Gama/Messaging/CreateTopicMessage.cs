using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.CreateTopicMessage")]
	public class CreateTopicMessage : TopicMessage
	{


		public string type { set; get; }
		public string color { set; get; }
		public string position { set; get; }

		public CreateTopicMessage()
		{

		}

		public CreateTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string type, string color, string position) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.type = type;
			this.color = color;
			this.position = position;
		}


	}

}

