using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.NotificationTopicMessage")]
	public class NotificationTopicMessage : TopicMessage
	{


		//public string purpose { get; set;}
		public string fieldType { get; set;}
		public string fieldName { get; set;}
		public string fieldValue { get; set;}
		public string fieldOperator { get; set;}

		public NotificationTopicMessage()
		{

		}

		public NotificationTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName,
			string fieldType, string fieldName, string fieldValue, string fieldOperator) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			//this.purpose = purpose;
			this.fieldType = fieldType;
			this.fieldName = fieldName;
			this.fieldValue = fieldValue;
			this.fieldOperator = fieldOperator;
		}

	}

}

