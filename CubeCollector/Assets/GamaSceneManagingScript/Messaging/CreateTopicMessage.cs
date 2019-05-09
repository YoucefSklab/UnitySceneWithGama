using System;
using System.Collections.Generic;
using System.Xml.Linq;
using msi.gama.metamodel.shape;
using ummisco.gama.unity.datatype;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.CreateTopicMessage")]
	public class CreateTopicMessage : TopicMessage
	{


		public string type { set; get; }
		public GamaRGBColor color { set; get; }
		public GamaPoint position { set; get; }

		public CreateTopicMessage()
		{

		}

		public CreateTopicMessage (string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string type, GamaRGBColor color, GamaPoint position) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.type = type;
			this.color = color;
			this.position = position;
		}




    }

}

