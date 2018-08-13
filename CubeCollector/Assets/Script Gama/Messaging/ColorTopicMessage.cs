using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.ColorTopicMessage")]
	public class ColorTopicMessage : TopicMessage
	{



		public string color { set; get; }

		public ColorTopicMessage()
		{

		}

		public ColorTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string color) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.color = color;
		}


	}

}

