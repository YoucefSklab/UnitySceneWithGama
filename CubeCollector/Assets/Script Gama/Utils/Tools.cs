using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Diagnostics;

namespace ummisco.gama.unity.utils
{
	public class Tools {

		public static string listToString(List<string> inputList, string sep){
			StringBuilder builder = new StringBuilder();
			foreach (string elt in inputList) 
			{
				builder.Append(elt).Append(sep); 
			}
			string result = builder.ToString(); // Get string from StringBuilder
			return result;
		}


		public static string convertMessage(string message){
			return message.Replace("&amp;","&").Replace("&lt;","<").Replace("&gt;",">").Replace("&quot;","\"").Replace("&apos;","'");
		}


	}
}

