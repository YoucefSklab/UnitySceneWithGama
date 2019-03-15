using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

    [System.Xml.Serialization.XmlRoot("ummisco.gama.unity.messages.PositionTopicMessage")]
    public class PositionTopicMessage : TopicMessage
    {

        public object position { set; get; }

        public PositionTopicMessage()
        {

        }

        public PositionTopicMessage(string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, object position) : base(unread, sender, receivers, contents, objectName, emissionTimeStamp)
        {
            this.position = position;
        }


    }

}

