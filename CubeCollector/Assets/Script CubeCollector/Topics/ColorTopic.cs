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
	public abstract class ColorTopic : Topic
	{

		private static ColorTopic m_Instance = null;

		public static ColorTopic Instance { get { return m_Instance; } }

		void Awake ()
		{
			if (m_Instance == null) m_Instance = this;
		}



		public ColorTopic (GamaMessage currentMsg, GameObject gameObj) : base (currentMsg, gameObj)
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

			} 
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (GameObject targetGameObject, string methodName, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);
			object obj = data [keyList.ElementAt (0)];
			targetGameObject.GetComponent<Renderer> ().material.color = Tools.stringToColor ((string)obj);

		}

	}

}