using System;
using System.Xml.Serialization;

namespace ummisco.gama.unity.messages
{
	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.ItemAttributes")]
	//[System.Xml.Serialization.XmlRoot("ItemAttributes")]

	public class ItemAttributes
	{
		//[XmlAttribute]
		public string attribute{ set; get; }
		//[XmlAttribute]
		public string value{ set; get; }


		public ItemAttributes ()
		{
			
		}

	}
}

