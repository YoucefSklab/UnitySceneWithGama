using System;

namespace ummisco.gama.unity.messages
{
	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.GamaReponseMessage")]
	public class GamaReponseMessage
	{
		public Boolean unread ;

		public string sender ;

		public string receivers ;

		public string topic ;

		public string contents ;

		public string emissionTimeStamp;


		public GamaReponseMessage ()
		{
			
		}

		public GamaReponseMessage (string sender, string receivers, string topic, string contents, string emissionTimeStamp)
		{
			this.unread = true;
			this.sender = sender;
			this.receivers = receivers;
			this.topic = topic;
			this.contents = contents;
			this.emissionTimeStamp = emissionTimeStamp;
		}


		public void setSender (string sender)
		{
			this.sender = sender;
		}

		public string getSender ()
		{
			
			return this.sender;
		}

		public void setReceivers (string receivers)
		{
			this.receivers = receivers;
		}

		public string getReceivers ()
		{
			return this.receivers;
		}

		public void setTopic (string topic)
		{
			this.topic = topic;
		}

		public string getTopic ()
		{
			return this.topic;
		}


		public void setContents (string contents)
		{
			this.contents = contents;
		}

		public string getContents ()
		{
			return this.contents;
		}

		public void setEmissionTimeStamp (string emissionTimeStamp)
		{
			this.emissionTimeStamp = emissionTimeStamp;
		}

		public string getEmissionTimeStamp ()
		{

			return this.emissionTimeStamp;
		}

	}
}

