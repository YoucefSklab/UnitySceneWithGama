﻿using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.PropertyTopicMessage")]
	public class PropertyTopicMessage : TopicMessage
	{


		public string property { set; get; }
		public string valueType { set; get; }
		public object value { set; get; }

		public PropertyTopicMessage()
		{

		}

		public PropertyTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string property, string valueType,  object value) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.property = property;
			this.valueType = valueType;
			this.value = value;
		}

	}

}

