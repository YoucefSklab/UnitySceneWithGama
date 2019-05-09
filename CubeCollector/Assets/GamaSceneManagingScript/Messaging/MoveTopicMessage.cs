using msi.gama.metamodel.shape;

namespace ummisco.gama.unity.messages
{

    [System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.MoveTopicMessage")]
	public class MoveTopicMessage : TopicMessage
	{



        public GamaPoint position { set; get; }
        public float speed { set; get; }
		public bool smoothMove { set; get; }

		public MoveTopicMessage()
		{

		}

		public MoveTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, GamaPoint position, float speed, bool smoothMove) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.position = position;
			this.speed = speed;
			this.smoothMove = smoothMove;
		}

	}

}

