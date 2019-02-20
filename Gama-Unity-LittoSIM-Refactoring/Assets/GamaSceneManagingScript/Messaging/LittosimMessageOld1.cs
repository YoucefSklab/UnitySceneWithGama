using System;
using System.Xml.Serialization;

namespace ummisco.gama.unity.messages
{
	[Serializable()]
    //[XmlRoot ("ummisco.gama.unity.messages.GamaMessage")]
    [XmlRoot ("msi.gama.extensions.messaging.GamaMessage")]
    [XmlInclude(typeof(contents))]
	public class LittosimMessageOld
	{
		public Boolean unread { get; set;}

		public string sender { get; set;}

		public string receivers { get; set;}
	
       // [XmlElement("ummisco.gama.unity.messages.GamaMessage.contents")]
    	public object contents { get; set; }
       	public string emissionTimeStamp{ get; set;}


		public LittosimMessageOld ()
		{
			
		}

		public LittosimMessageOld (string sender, string receivers, object contents, string emissionTimeStamp)
		{
			this.unread = true;
			this.sender = sender;
			this.receivers = receivers;
            this.contents = contents;
            this.emissionTimeStamp = emissionTimeStamp;
		}

        

		

	}
}

