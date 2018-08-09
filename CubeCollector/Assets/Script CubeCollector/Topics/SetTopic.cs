using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;


namespace ummisco.gama.unity.topics
{
	public class SetTopic : Topic
	{

		public SetTopic (GamaMessage currentMsg, GameObject gameObj) : base (currentMsg, gameObj)
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


		public void ProcessTopic (object obj)
		{
			setAllProperties (obj);


			if (targetGameObject != null) {

				XmlNode[] node = (XmlNode[])message.unityAttribute;
				Dictionary<object, object> dataDictionary = new Dictionary<object, object> ();

				XmlElement elt = (XmlElement)node.GetValue (1);
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

				sendTopic (targetGameObject, message.getAction (), dataDictionary);
				Debug.Log ("Method called");

			} 


		}


		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (GameObject targetGameObject, string methodName, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);
			object obj = data [keyList.ElementAt (0)];

			FieldInfo[] fieldInfoSet = targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetFields ();
	
			foreach (KeyValuePair<object, object> pair in data) {
				foreach (FieldInfo fi in fieldInfoSet) {
					if (fi.Name.Equals (pair.Key.ToString ())) {
						UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX);
						fi.SetValue (ob, (Convert.ChangeType (pair.Value, fi.FieldType)));
					}
				}
			}
		}

	}

}