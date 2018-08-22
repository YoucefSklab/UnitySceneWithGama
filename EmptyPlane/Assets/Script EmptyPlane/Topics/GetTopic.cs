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
		public GetTopicMessage topicMessage;

		public GetTopic (TopicMessage currentMsg, GameObject gameObj) : base (gameObj)
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
				string attribute  = topicMessage.attribute;
				obj [2] = (object) getValueToSend (targetGameObject, attribute);

				Debug.Log ("Method called and returned -> " + obj [2]);

			}

		}

		// The method to call Game Objects methods
		//----------------------------------------
		public string getValueToSend (GameObject targetGameObject, string attibute)
		{

			FieldInfo[] fieldInfoGet = targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetFields ();
			string msgReplay = "";
		
			foreach (FieldInfo fi in fieldInfoGet) {
				if (fi.Name.Equals (attibute)) {
						UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX);
						msgReplay = fi.GetValue (ob).ToString ();
					}
				}
		
			Debug.Log ("To return this -> " + msgReplay);
			return msgReplay;
		}



		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.topicMessage = (GetTopicMessage)obj [0];
			this.targetGameObject = (GameObject)obj [1];
			this.valueIs = (string)obj [2];
		}

	}
}

