using System;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace ummisco.gama.unity.topics
{
	public class Topic
	{

		protected MsgSerialization msgDes = new MsgSerialization ();
		protected GamaMethods gama = new GamaMethods ();
		protected GamaMessage message;
		protected GameObject gameObject;

		public Topic (GamaMessage currentMsg, GameObject gameO)
		{
			this.message = currentMsg;
			this.gameObject = gameObject;

		}



		public MethodInfo[] getMethodsInfo(BindingFlags flags){
			return gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethods (flags);
		}


		// The method to call Game Objects methods
		//----------------------------------------
		public void sendMessageToGameObject (GameObject gameObject, string methodName, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);

			MethodInfo methInfo = gameObject.GetComponent (gameObject.name+MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethod (methodName);
			ParameterInfo[] parameter = methInfo.GetParameters ();

			switch (size) {
			case 0:
				gameObject.SendMessage (methodName);
				break;
			case 1:
				gameObject.SendMessage (methodName, convertParameter (data [keyList.ElementAt (0)], parameter [0]));
				break;

			default:
				object[] obj = new object[size + 1];
				int i = 0;
				foreach (KeyValuePair<object, object> pair in data) {
					obj [i] = pair.Value;
					i++;
				}
				gameObject.SendMessage (methodName, obj);
				break;
			}
				

		}


		public object convertParameter (object val, ParameterInfo par)
		{
			object propValue = Convert.ChangeType (val, par.ParameterType);
			return propValue;
		}




	}
}

