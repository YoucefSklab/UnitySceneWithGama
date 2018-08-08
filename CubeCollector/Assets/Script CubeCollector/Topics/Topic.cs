using System;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ummisco.gama.unity.topics
{
	public class Topic : MonoBehaviour
	{

		protected MsgSerialization msgDes = new MsgSerialization ();
		protected GamaMethods gama = new GamaMethods ();

		protected GamaMessage message { get ; set; }

		protected new GameObject gameObject { get ; set; }


		void Awake ()
		{

		}

		// Use this for initialization
		public virtual void Start ()
		{

		}

		// Update is called once per frame
		public virtual void Update ()
		{

		}

	

		public Topic (GamaMessage currentMsg, GameObject gameO)
		{
			this.message = currentMsg;
			this.gameObject = gameObject;
		}



		public virtual MethodInfo[] getMethodsInfo (BindingFlags flags)
		{
			return gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethods (flags);
		}




		public virtual void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.message = (GamaMessage)obj [0];
			this.gameObject = (GameObject)obj [1];
		}



		public virtual void ProcessTopic (object obj)
		{
			setAllProperties (obj);

			if (gameObject != null) {

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

				sendTopic (gameObject, message.getAction (), dataDictionary);
				Debug.Log ("Method called");

			} 
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public virtual void sendTopic (GameObject gameObject, string methodName, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);

			MethodInfo methInfo = gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethod (methodName);
			ParameterInfo[] parameter = methInfo.GetParameters ();
			object obj = data [keyList.ElementAt (0)];
			gameObject.SendMessage (methodName, Tools.convertParameter (obj, parameter [0]));
		}



	}
}

