using System;
using UnityEngine;

namespace ummisco.gama.unity.datatype
{
    public class GamaRGBColor
    {
        public int red { set; get; }
        public int green { set; get; }
        public int blue { set; get; }

    public GamaRGBColor()
        {
            this.red = 0;
            this.green = 1;
            this.blue = 2;
        }

    public Color getRGBColor()
        {
        return new Color(this.red,this.green,this.blue);
        }
    }
}