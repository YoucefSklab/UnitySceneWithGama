using System;
using UnityEngine;
using UnityEditor;


namespace ummisco.gama.unity.notification
{
	public class NotificationEntry
	{

		public string objectName { get; set;}
		public string fieldType { get; set;}
		public string fieldName { get; set;}
		public string fieldValue { get; set;}
		public string fieldOperator { get; set;}

		public NotificationEntry (string objectName, string fieldType, string fieldName, string fieldValue, string fieldOperator)
		{
			this.objectName = fieldType;
			this.fieldType = fieldType;
			this.fieldName = fieldName;
			this.fieldValue = fieldValue;
			this.fieldOperator = fieldOperator;
		}

		public NotificationEntry()
		{
		
		}

	}
}

