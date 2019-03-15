using System;
using System.Collections.Generic;

namespace ummisco.gama.unity.messages
{

    [System.Xml.Serialization.XmlRoot("ummisco.gama.unity.messages.ColorTopicMessage")]
    public class ColorTopicMessage : TopicMessage
    {
        public int red { set; get; }
        public int green { set; get; }
        public int blue { set; get; }

        public ColorTopicMessage()
        {

        }

        public ColorTopicMessage(string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, int red, int green, int blue) : base(unread, sender, receivers, contents, objectName, emissionTimeStamp)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }


    }

}

