using System;
using System.Xml.Serialization;
using UnityEngine;


namespace msi.gama.metamodel.shape
{
    [System.Xml.Serialization.XmlRoot("msi.gama.metamodel.shape.GamaPoint")]
    [System.Xml.Serialization.XmlType("value", IncludeInSchema = true)]
    //[System.Xml.Serialization.XmlInclude
    public class GamaPoint
    {
        [XmlElement("x")]
        public float x { set; get; }
        [XmlElement("y")]
        public float y { set; get; }
        [XmlElement("z")]
        public float z { set; get; }

    public GamaPoint()
        {
            this.x = 0;
            this.y = 1;
            this.z = 2;
        }

    public Vector3 getVector3()
        {
        return new Vector3(this.x, this.y, this.z); 
        }
    }
}