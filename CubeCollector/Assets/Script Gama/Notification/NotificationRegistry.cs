using System;
using UnityEngine;


namespace ummisco.gama.unity.notification
{
	public class NotificationRegistry
	{
		public NotificationRegistry ()
		{
			
		}

		public static void getCallingMethod ()
		{

			System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace ();
			// Get calling method name
			string methodLongName = stackTrace.GetFrame (1).GetMethod ().ReflectedType.ToString () + "."
			                        + stackTrace.GetFrame (1).GetMethod ().Name;

			Debug.Log ("--> Method long name is : " + methodLongName);
		}
	}
}

