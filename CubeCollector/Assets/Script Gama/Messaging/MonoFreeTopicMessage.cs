using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.MonoFreeTopicMessage")]
	public class MonoFreeTopicMessage : TopicMessage
	{


		public object attributes { set; get; }

		public object methodName { set; get; }

		public MonoFreeTopicMessage()
		{

		}

		public MonoFreeTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, object attributes, object methodeName) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.attributes = attributes;
			this.methodName = methodeName;
		}


	}

}

