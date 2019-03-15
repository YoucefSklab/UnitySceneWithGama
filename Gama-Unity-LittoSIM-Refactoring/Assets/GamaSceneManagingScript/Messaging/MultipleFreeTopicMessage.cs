using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.PluralActionTopicMessage")]
	public class MultipleFreeTopicMessage : TopicMessage
	{

		public object attributes { set; get; }

		public string methodName { set; get; }

		public MultipleFreeTopicMessage()
		{

		}

		public MultipleFreeTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, object attributes, string methodeName) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.attributes = attributes;
			this.methodName = methodeName;
		}

	}

}

