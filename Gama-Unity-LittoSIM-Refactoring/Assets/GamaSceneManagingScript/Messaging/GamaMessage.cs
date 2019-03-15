using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

    [System.Xml.Serialization.XmlRoot("msi.gama.extensions.messaging.GamaMessage")]
    public class GamaMessage
    {
        public Boolean unread { set; get; }

        public string sender { set; get; }

        public string receivers { set; get; }

        public object contents { set; get; }

        public string emissionTimeStamp { set; get; }

        public GamaMessage()
        {

        }

        public GamaMessage(string sender, string receivers, object contents, string emissionTimeStamp)
        {
            this.unread = true;
            this.sender = sender;
            this.receivers = receivers;
            this.contents = contents;
            this.emissionTimeStamp = emissionTimeStamp;
        }


        public object getContents()
        {
            return this.contents;
        }

    }

}

