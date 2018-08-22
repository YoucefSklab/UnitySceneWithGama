using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.GetTopicMessage")]
	public class GetTopicMessage : TopicMessage
	{

		public string attribute { set; get; }

		public GetTopicMessage()
		{

		}

		public GetTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string attribute) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.attribute = attribute;
		}


	}

}

