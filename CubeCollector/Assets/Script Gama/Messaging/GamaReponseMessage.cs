using System;

namespace ummisco.gama.unity.messages
{
	[System.Xml.Serialization.XmlRoot("ummisco.gama.unity.messages.GamaReponseMessage")]
	public class GamaReponseMessage
	{
		public Boolean unread { get; set; }
		public string sender { get; set; }
		public string receivers { get; set; }
		public string contents;
		public string emissionTimeStamp;


		public GamaReponseMessage ()
		{
			
		}

		public GamaReponseMessage (string sender, string receivers, string contents, string emissionTimeStamp)
		{
			this.unread = true;
			this.sender = sender;
			this.receivers = receivers;
			this.contents = contents;
			this.emissionTimeStamp = emissionTimeStamp;
		}

		public void setSender(string sender){
			this.sender= sender;
		}

		public string getSender(){
			
			return this.sender;
		}

		public void setReceivers(string receivers){
			this.receivers = receivers;
		}

		public string getReceivers(){
			return this.receivers;
		}

		public void setContents(string contents){
			this.contents = contents;
		}

		public string getContents(){
			return this.contents;
		}

		public void setEmissionTimeStamp(string emissionTimeStamp){
			this.emissionTimeStamp = emissionTimeStamp;
		}

		public string getEmissionTimeStamp(){

			return this.emissionTimeStamp;
		}

	

	}
}

