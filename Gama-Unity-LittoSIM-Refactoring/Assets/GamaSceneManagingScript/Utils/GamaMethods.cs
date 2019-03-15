using System;
using UnityEngine;

namespace ummisco.gama.unity.utils
{
	public class GamaMethods
	{

		private string gamaVersion = "1.8";

		public GamaMethods ()
		{
			
		}


		public string getGamaVersion ()
		{
			return gamaVersion;
		}


		public GameObject[] getAllSceneGameObject ()
		{
			return MqttSetting.allObjects;
		}

		public GameObject getGameObjectByName (string objectName)
		{
            return GameObject.Find(objectName);
		}

	}
}

