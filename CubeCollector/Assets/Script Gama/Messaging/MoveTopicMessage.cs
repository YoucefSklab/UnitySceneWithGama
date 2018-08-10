using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.MoveTopicMessage")]
	public class MoveTopicMessage : TopicMessage
	{



		public object attributes { set; get; }

		public MoveTopicMessage()
		{

		}

		public MoveTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, object attributes) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.attributes = attributes;
		}


	}

}

