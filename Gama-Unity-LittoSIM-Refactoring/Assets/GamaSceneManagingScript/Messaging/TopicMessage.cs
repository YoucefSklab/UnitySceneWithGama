using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

    //[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.GamaUnityMessage")]
    public class TopicMessage
    {
        public string unread { set; get; }

        public string sender { set; get; }

        public string receivers { set; get; }

        public string contents { set; get; }

        public string objectName { set; get; }

        public string emissionTimeStamp { set; get; }

        public TopicMessage()
        {

        }

        public TopicMessage(string unread, string sender, string receivers, string contents, string objectName, string emissionTimeStamp)
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

