using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Diagnostics;


public class Tools {

	public string listToString(List<string> inputList, string sep){
		StringBuilder builder = new StringBuilder();
		foreach (string elt in inputList) 
		{
			builder.Append(elt).Append(sep); 
		}
		string result = builder.ToString(); // Get string from StringBuilder
		return result;
	}








}
