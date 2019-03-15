using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.MoveTopicMessage")]
	public class MoveTopicMessage : TopicMessage
	{

		public object position { set; get; }
		public float speed { set; get; }
		public bool smoothMove { set; get; }

		public MoveTopicMessage()
		{

		}

		public MoveTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, object position, float speed, bool smoothMove) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.position = position;
			this.speed = speed;
			this.smoothMove = smoothMove;
		}


	}

}

