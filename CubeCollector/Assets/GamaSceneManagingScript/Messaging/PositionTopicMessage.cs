using msi.gama.metamodel.shape;

namespace ummisco.gama.unity.messages
{

    [System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.PositionTopicMessage")]
	public class PositionTopicMessage : TopicMessage
	{



        public GamaPoint position { set; get; }

        public PositionTopicMessage()
		{

		}

		public PositionTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, GamaPoint position) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.position = position;
		}


	}

}

