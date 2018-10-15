using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Nextzen.VectorData;
using ummisco.gama.unity.GamaAgent;
using System.Linq;

namespace ummisco.gama.unity.utils
{
    public class Utils
    {

        public static Material getMaterialByName(string matName)
        {
            foreach (var mater in (Material[])Resources.FindObjectsOfTypeAll(typeof(Material)))
            {
                if (mater.name.Equals(matName))
                {
                    return mater;
                }
            }
            return null;
        }

        public static Color getColorFromGamaColor(GamaColor color)
        {
            Color newColor = new Color();

            var bigint = (int) color.value;
            var r = (bigint >> 16) & 255;
            var g = (bigint >> 8) & 255;
            var b = bigint & 255;

            newColor.r = r;
            newColor.b = b;
            newColor.g = g;

            return newColor;
        }


    }

}

