using System;
using UnityEngine;

namespace ummisco.gama.unity
{
	public class GamaMethods
	{

		private string gamaVersion = "1.8";
			
		public GamaMethods ()
		{
			
		}


		public string getGamaVersion(){
			return gamaVersion;
		}


		public void getAllSceneGameObject(){
			GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;

			foreach(GameObject gameO in allObjects)
				if (gameO.activeInHierarchy)
					//print(thisObject+" is an active object") ;
				//print("test");
					Debug.Log(gameO.name);

		}

	}
}

