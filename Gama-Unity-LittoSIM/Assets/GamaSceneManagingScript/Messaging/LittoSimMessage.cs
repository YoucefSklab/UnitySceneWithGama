using System;

namespace ummisco.gama.unity.messages
{
	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.LittosimMeesage")]
	public class LittosimMessage
	{
		public Boolean unread { get; set;}

		public string sender { get; set;}

		public string receivers { get; set;}

	
		public string name { get; set; }
        public int type { get; set; }

        public float x { get; set; }
        public float y { get; set; }

		public string emissionTimeStamp{ get; set;}


		public LittosimMessage ()
		{
			
		}

		public LittosimMessage (string sender, string receivers, string name, int type, float x, float y, string emissionTimeStamp)
		{
			this.unread = true;
			this.sender = sender;
			this.receivers = receivers;
            this.name = name;
            this.type = type;
            this.x = x;
            this.y = y;			
			this.emissionTimeStamp = emissionTimeStamp;
		}

	}
}

