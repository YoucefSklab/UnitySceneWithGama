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
	public class GetTopic : Topic
	{

		protected string valueIs = "";

		public GetTopic (GamaMessage currentMsg, GameObject gameObj) : base (currentMsg, gameObj)
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
	
		public void ProcessTopic (object[] obj)
		{
			this.setAllProperties (obj);


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

				obj [2] = getValueToSend (targetGameObject, message.getAction (), dataDictionary);
				Debug.Log ("Method called and returned -> " + obj [2]);

			}

		}

		// The method to call Game Objects methods
		//----------------------------------------
		public string getValueToSend (GameObject targetGameObject, string methodName, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);
			object obj = data [keyList.ElementAt (0)];

			FieldInfo[] fieldInfoGet = targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetFields ();

			string msgReplay = "";

			foreach (KeyValuePair<object, object> pair in data) {
				foreach (FieldInfo fi in fieldInfoGet) {
					if (fi.Name.Equals (pair.Key.ToString ())) {
						UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX);
						msgReplay = fi.GetValue (ob).ToString ();
					}
				}
			}
			Debug.Log ("To return this -> " + msgReplay);
			return msgReplay;
		}



		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.message = (GamaMessage)obj [0];
			this.targetGameObject = (GameObject)obj [1];
			this.valueIs = (string)obj [2];
		}

	}
}

