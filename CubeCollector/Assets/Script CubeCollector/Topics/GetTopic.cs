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
	public class GetTopic : MonoBehaviour
	{


		protected MsgSerialization msgDes = new MsgSerialization ();
		protected GamaMethods gama = new GamaMethods ();
		protected GamaMessage message;
		protected GameObject gameObject;
		protected string valueIs = "";

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}


		public MethodInfo[] getMethodsInfo (BindingFlags flags)
		{
			return gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethods (flags);
		}

		public void ProcessGetTopic (object[] obj)
		{
			setAllPropertiesGetTopic (obj);


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

				obj [2] = getValueToSend (gameObject, message.getAction (), dataDictionary);
				Debug.Log ("Method called and returned -> " + obj [2]);

			}

		}

		// The method to call Game Objects methods
		//----------------------------------------
		public string getValueToSend (GameObject gameObject, string methodName, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);
			object obj = data [keyList.ElementAt (0)];

			FieldInfo[] fieldInfoGet = gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetFields ();

			string msgReplay = "";

			foreach (KeyValuePair<object, object> pair in data) {
				foreach (FieldInfo fi in fieldInfoGet) {
					if (fi.Name.Equals (pair.Key.ToString ())) {
						UnityEngine.Component ob = (UnityEngine.Component)gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX);
						msgReplay = fi.GetValue (ob).ToString ();
					}
				}
			}
			Debug.Log ("To return this -> " + msgReplay);
			return msgReplay;
		}





		public object convertParameter (object val, ParameterInfo par)
		{
			object propValue = Convert.ChangeType (val, par.ParameterType);
			return propValue;
		}


		public void setAllPropertiesGetTopic (object args)
		{
			object[] obj = (object[])args;
			this.message = (GamaMessage)obj [0];
			this.gameObject = (GameObject)obj [1];
			this.valueIs = (string)obj [2];
		}


		public GameObject getGameObjectByName (string objectName)
		{
			foreach (GameObject gameO in MqttSetting.allObjects) {
				if (gameO.activeInHierarchy) {
					if (objectName.Equals (gameO.name)) {
						return gameO;
					}
				}					
			}
			return null;
		}


	}
}

