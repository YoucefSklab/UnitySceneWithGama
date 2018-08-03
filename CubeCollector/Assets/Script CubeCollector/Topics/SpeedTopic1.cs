using System;
using ummisco.gama.unity.messages;
using UnityEngine;
using System.Reflection;
using ummisco.gama.unity.utils;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ummisco.gama.unity.topics
{
	public class SpeedTopic1 
	{
		protected MsgSerialization msgDes = new MsgSerialization ();
		protected GamaMethods gama = new GamaMethods ();
		protected GamaMessage message;
		protected GameObject gameObject;

		public SpeedTopic1 (GamaMessage currentMsg, GameObject gameO)
		{
			this.message = currentMsg;
			this.gameObject = gameO;
			Debug.Log ("->>>>>>>>>>>>>>--> --->>>  inside SpeedTopic");
		}



		public MethodInfo[] getMethodsInfo(BindingFlags flags){
			return gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethods (flags);
		}

		public void ProcessToMessage ()
		{
			Debug.Log ("->>>>>>>>>>>>>>--> --->>>  this is from speed Topic");



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

				foreach (KeyValuePair<object, object> pair in dataDictionary) {
					//	Debug.Log (pair.Key + "  +++++  " + pair.Value);
				}

				sendMessageToGameObject (gameObject, message.getAction (), dataDictionary);

			} 
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public new void sendMessageToGameObject (GameObject gameObject, string methodName, Dictionary<object, object> data)
		{
			
			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);

			MethodInfo methInfo = gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethod (methodName);
			ParameterInfo[] parameter = methInfo.GetParameters ();
			object obj = data [keyList.ElementAt (0)];
			//object obj2 = base.convertParameter (obj, parameter [0]);
			gameObject.SendMessage (methodName, convertParameter(obj, parameter [0]));


				Debug.Log ("Method called");
		}


		public object convertParameter (object val, ParameterInfo par)
		{
			object propValue = Convert.ChangeType (val, par.ParameterType);
			return propValue;
		}

	


	}
}

