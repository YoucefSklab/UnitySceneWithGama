using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.MonoActionTopicMessage")]
	public class MonoFreeTopicMessage : TopicMessage
	{


		public string attribute { set; get; }

		public string methodName { set; get; }

		public MonoFreeTopicMessage()
		{

		}

		public MonoFreeTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string attribute, string methodeName) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.attribute = attribute;
			this.methodName = methodeName;
		}

		


	}

}

