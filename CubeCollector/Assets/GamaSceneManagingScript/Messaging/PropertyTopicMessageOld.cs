using System;
using System.Collections.Generic;
using msi.gama.metamodel.shape;
using ummisco.gama.unity.datatype;
using UnityEngine;

namespace ummisco.gama.unity.messages
{

	[System.Xml.Serialization.XmlRoot ("ummisco.gama.unity.messages.PropertyTopicMessage")]
	public class PropertyTopicMessageOld : TopicMessage
	{


		public string property { set; get; }
		public string valueType { set; get; }
		public object value { set; get; }

		public PropertyTopicMessageOld()
		{

		}

		public PropertyTopicMessageOld(string unread, string sender, string receivers, string contents, string emissionTimeStamp, string objectName, string property, string valueType,  object value) : base (unread, sender, receivers, contents, objectName, emissionTimeStamp)
		{
			this.property = property;
			this.valueType = valueType;
			this.value = value;
		}

        public Vector3 GetValueAsVector3()
        {
            Vector3 vector = (Vector3) value;

            return vector;
        }

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

