using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using msi.gama.metamodel.shape;
using ummisco.gama.unity.datatype;
using UnityEngine;

namespace ummisco.gama.unity.messages
{
    [XmlInclude(typeof(GamaPoint))]
    [System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.PropertyTopicMessage")]
    [Serializable]
    public class PropertyTopicMessage : TopicMessage
	{

        [XmlElement("property")]
        public string property { set; get; }
        [XmlElement("valueType")]
        public string valueType { set; get; }
        [XmlElement("value")]
        public object value { set; get; }

		public PropertyTopicMessage()
		{

		}

		public PropertyTopicMessage(string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string property, string valueType, GamaPoint value) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.property = property;
			this.valueType = valueType;
			this.value = value;
		}
/*
        public Vector3 GetValueAsVector3()
        {
            Vector3 vector = (Vector3) value.getVector3();

            return vector;
        }
        */
        public GamaPoint GetValueAsGamaPoint()
        {
            GamaPoint vector = (GamaPoint)value;

            return vector;
        }

        public string GetValueAsString()
        {
            string stringValue = (string) value;

            return stringValue;
        }




    }

}

