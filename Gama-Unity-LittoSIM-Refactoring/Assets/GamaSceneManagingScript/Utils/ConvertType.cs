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

namespace ummisco.gama.unity.utils
{
	public class ConvertType
	{

		public static string listToString (List<string> inputList, string sep)
		{
			StringBuilder builder = new StringBuilder ();
			foreach (string elt in inputList) {
				builder.Append (elt).Append (sep); 
			}
			string result = builder.ToString (); // Get string from StringBuilder
			return result;
		}



		public static Dictionary<string, object> DictionaryFromType (object atype)
		{

			if (atype == null)
				return new Dictionary<string, object> ();
			Type t = atype.GetType ();
			PropertyInfo[] props = t.GetProperties ();
			Dictionary<string, object> dict = new Dictionary<string, object> ();
			foreach (PropertyInfo prp in props) {
				object value = prp.GetValue (atype, new object[]{ 0 });

				dict.Add (prp.Name, value);
			}
			return dict;
		}

		public static object convertParameter (object val, ParameterInfo par)
		{
			object propValue = Convert.ChangeType (val, par.ParameterType);
			return propValue;
		}

		public static Color stringToColor (string color)
		{
			return (Color)typeof(Color).GetProperty (color.ToLowerInvariant ()).GetValue (null, null);
		}





		public static Vector3 vector3FromXmlNode (XmlNode[] node, string fieldName)
		{
			float X = 0;
			float Y = 0;
			float Z = 0;
			Boolean itExist = false;

			foreach (XmlNode n in node) {
				if (n.Value == fieldName) {
					itExist = true;
				}

				if (n.Name == "x") {
					X = float.Parse (n.InnerText); // convert the strings to float and apply to the Y variable.
				}
				if (n.Name == "y") {
					Y = float.Parse (n.InnerText); // convert the strings to float and apply to the Y variable.
				}
				if (n.Name == "z") {
					Z = float.Parse (n.InnerText); // convert the strings to float and apply to the Y variable.
				}

			}

			if (itExist) {
				return new Vector3 (X, Y, Z);
			} else {
				return new Vector3 (0, 0, 0);
			}
		}



		public static Point pointFromXmlElement (XmlNodeList listPoints)
		{
			float X = 0;
			float Y = 0;
			float Z = 0;
		

		 	foreach (XmlElement corItem in listPoints) {
				if (corItem.Name == "x") {
					X = float.Parse (corItem.InnerText); // convert the strings to float and apply to the Y variable.
				}
				if (corItem.Name == "y") {
					Y = float.Parse (corItem.InnerText); // convert the strings to float and apply to the Y variable.
				}
				if (corItem.Name == "z") {
					Z = float.Parse (corItem.InnerText); // convert the strings to float and apply to the Y variable.
				}
                                   
            }

			return new Point (X, Y, Z);
		}




		public static object valueFromXmlNode (XmlNode[] node, string fieldName)
		{
			float X = 0;
			float Y = 0;
			float Z = 0;
			Boolean itExist = false;

	

			foreach (XmlNode n in node) {


			

				if (n.Value == fieldName) {
					itExist = true;
				}

				if (n.Name == "x") {
					X = float.Parse (n.InnerText); // convert the strings to float and apply to the Y variable.
				}
				if (n.Name == "y") {
					Y = float.Parse (n.InnerText); // convert the strings to float and apply to the Y variable.
				}
				if (n.Name == "z") {
					Z = float.Parse (n.InnerText); // convert the strings to float and apply to the Y variable.
				}

			}

			if (itExist) {
				return new Vector3 (X, Y, Z);
			} else {
				return new Vector3 (0, 0, 0);
			}
		}


		public static Color rgbColorFromXmlNode (XmlNode[] node, string fieldName)
		{
			int red = 0;
			int green = 0;
			int blue = 0;
			Boolean itExist = false;

			foreach (XmlNode n in node) {
				if (n.Value == fieldName) {
					itExist = true;
				}
				if (n.Name == "red") {
					red = Int32.Parse (n.InnerText);
				}
				if (n.Name == "green") {
					green = Int32.Parse (n.InnerText); 
				}
				if (n.Name == "blue") {
					blue = Int32.Parse (n.InnerText); 
				}

			}

			if (itExist) {
				return new Color(red,green,blue);
			} else {
				return new Color(0,0,0);
			}
		}


	}
}

