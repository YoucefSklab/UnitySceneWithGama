using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ummisco.gama.unity.messages
{

	//[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.GamaUnityMessage")]
	public class TopicMessage
	{
        [XmlElement("unread")]
        public string unread { set; get; }
        [XmlElement("sender")]
        public string sender { set; get; }
        [XmlElement("receivers")]
        public string receivers { set; get; }
        [XmlElement("contents")]
        public string contents { set; get; }
        [XmlElement("objectName")]
        public string objectName { set; get; }
        [XmlElement("emissionTimeStamp")]
        public string emissionTimeStamp { set; get; }





		public TopicMessage ()
		{

		}

		public TopicMessage (string unread, string sender, string receivers, string contents, string objectName, string emissionTimeStamp)
		{
			this.unread = unread;
			this.sender = sender;
			this.receivers = receivers;
			this.contents = contents;
			this.objectName = objectName;
			this.emissionTimeStamp = emissionTimeStamp;
		}

	}

}

