using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.SetTopicMessage")]
	public class SetTopicMessage : TopicMessage
	{

		public object attributes { set; get; }

		public SetTopicMessage()
		{
		
		}

		public SetTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, object attributes) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.attributes = attributes;
		}

	}

}

