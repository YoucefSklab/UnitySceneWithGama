using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;


namespace ummisco.gama.unity.topics
{
	public class PositionTopic : Topic
	{

		public PositionTopic (GamaMessage currentMsg, GameObject gameObj) : base (currentMsg, gameObj)
		{

		}

		// Use this for initialization
		public override void Start ()
		{

		}

		// Update is called once per frame
		public override void Update ()
		{

		}



		public void ProcessPositionTopic (object obj)
		{
			setAllProperties (obj);

			if (gameObject != null) {

				XmlNode[] node = (XmlNode[])message.unityAttribute;
				Dictionary<object, object> dataDictionary = new Dictionary<object, object> ();

				for (int i = 1; i < node.Length; i++) {
					XmlElement elt = (XmlElement)node.GetValue (i);
					XmlNodeList list = elt.ChildNodes;

					object atr = "";
					object vl = "";

					foreach (XmlElement item in list) {
						if (item.Name.Equals ("attribute")) {
							atr = item.InnerText;
						}
						if (item.Name.Equals ("value")) {
							vl = item.InnerText;
						}
					}
					dataDictionary.Add (atr, vl);
				}
				sendTopic (gameObject, message.getAction (), dataDictionary);

			} 
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public override void sendTopic (GameObject gameObject, string methodName, Dictionary<object, object> data)
		{
			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);
			float x, y, z;

			x = float.Parse ((string)data [keyList.ElementAt (0)], CultureInfo.InvariantCulture.NumberFormat);
			y = float.Parse ((string)data [keyList.ElementAt (1)], CultureInfo.InvariantCulture.NumberFormat);
			z = float.Parse ((string)data [keyList.ElementAt (2)], CultureInfo.InvariantCulture.NumberFormat);
			Debug.Log ("----->>>>    X,Y,Z  " + x + "," + y + "," + z);
    
			Vector3 movement = new Vector3 (x, y, z);
			gameObject.transform.position = movement;


			Debug.Log ("position applied ");
		}








	}
}

