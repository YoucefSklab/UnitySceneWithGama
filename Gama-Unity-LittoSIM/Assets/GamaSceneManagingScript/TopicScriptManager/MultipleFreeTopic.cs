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
	public class MultipleFreeTopic : Topic
	{
		public MultipleFreeTopicMessage topicMessage;
		public object[] obj;

		public MultipleFreeTopic (MultipleFreeTopicMessage topicMessage, GameObject gameObj) : base (gameObj)
		{
			this.topicMessage = topicMessage;
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

			//Debug.Log (" --->>>  this is from multiple free Topic");

			setAllProperties (obj);

			BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
			MethodInfo[] info = getMethodsInfo (flags);

			for (int i = 0; i < info.Length; i++) {
				MethodInfo infoItem = info [i];
				ParameterInfo[] par = infoItem.GetParameters ();
				for (int j = 0; j < par.Length; j++) {
					//ParameterInfo parInfo = par [j];
					//Debug.Log ("->>>>>>>>>>>>>>--> parametre Name >>=>>=>>=  " + parInfo.Name);
					//Debug.Log ("->>>>>>>>>>>>>>--> parametre Type>>=>>=>>=  " + parInfo.ParameterType);
				}
			}

			if (targetGameObject != null) {

				XmlNode[] node = (XmlNode[])topicMessage.attributes;
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

				foreach (KeyValuePair<object, object> pair in dataDictionary) {
					//	Debug.Log (pair.Key + "  +++++  " + pair.Value);
				}

				sendTopic (targetGameObject, (string)topicMessage.methodName, dataDictionary);

			} 
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (GameObject targetGameObject, string methodName, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);

			MethodInfo methInfo = targetGameObject.GetComponent (scripts[0].GetType ()).GetType ().GetMethod (methodName);
			ParameterInfo[] parameter = methInfo.GetParameters ();

			int nbr = 0;
			obj  = new object[keyList.Count];
			foreach (KeyValuePair<object, object> pair in data) {
				obj [nbr] = (string)pair.Value;
				nbr++;
				//	Debug.Log (pair.Key + "  +++++  " + pair.Value);
			}

			//= data [keyList.ElementAt (0)];
			targetGameObject.SendMessage (methodName, obj);

			Debug.Log ("Method called");
		}


		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.topicMessage = (MultipleFreeTopicMessage)obj [0];
			this.targetGameObject = (GameObject)obj [1];
		}

	}

}

