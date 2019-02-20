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
			GameObject[] allObjects = MqttSetting.allObjects;
			/* 
			foreach (GameObject gameO in allObjects) {
				if (gameO.activeInHierarchy) {
					Debug.Log (gameO.name);
				}					
			}
			*/
			return allObjects;
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

