using System;
using System.Xml.Serialization;

namespace ummisco.gama.unity.messages
{
    
    [Serializable()]
    //[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.CreatedAgentMessage.contents")]
   // [XmlRoot ("ummisco.gama.unity.messages.CreatedAgentMessage.contents")]
    public class contents
    {
        public string name { get; set; }
        public int type { get; set; }

        public float x { get; set; }
        public float y { get; set; }

        public float z { get; set; }

        public contents(string name, int type, float x, float y, float z)
        {
            this.name = name;
            this.type = type;
            this.x = x;
            this.y = y;
            this.z = z;

        }

        public contents(){

        }

        public contents(string name, int type, float x, float y)
        {
            this.name = name;
            this.type = type;
            this.x = x;
            this.y = y;
            this.z = 0;
        }
    }
}