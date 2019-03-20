using System;
using System.Collections.Generic;
using ummisco.gama.unity.GamaConcepts;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot (IGamaConcept.GAMA_DESTROY_TOPIC_MESSAGE_CLASS)]
	public class DestroyTopicMessage : TopicMessage
	{

		public DestroyTopicMessage()
		{

		}
		public DestroyTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{

		}
	}

}

