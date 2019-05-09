using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.DestroyTopicMessage")]
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

