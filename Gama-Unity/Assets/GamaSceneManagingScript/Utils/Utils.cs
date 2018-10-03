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


    }

}

