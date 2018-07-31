using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine;
using System.Reflection;

namespace ummisco.gama.unity.utils
{
	public class Tools
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

		// TO DELETE
		public static string convertMessage (string message)
		{
			return message.Replace ("&amp;", "&").Replace ("&lt;", "<").Replace ("&gt;", ">").Replace ("&quot;", "\"").Replace ("&apos;", "'");
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

	}

}

