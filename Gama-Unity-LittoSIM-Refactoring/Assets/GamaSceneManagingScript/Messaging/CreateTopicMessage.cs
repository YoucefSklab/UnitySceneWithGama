using System;
using System.Collections.Generic;
using ummisco.gama.unity.GamaConcepts;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot (IGamaConcept.GAMA_CREATE_TOPIC_MESSAGE_CLASS)]
	public class CreateTopicMessage : TopicMessage
	{


		public string type { set; get; }
		public object color { set; get; }
		public object position { set; get; }

		public CreateTopicMessage()
		{

		}

		public CreateTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string type, object color, object position) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.type = type;
			this.color = color;
			this.position = position;
		}


	}

}

