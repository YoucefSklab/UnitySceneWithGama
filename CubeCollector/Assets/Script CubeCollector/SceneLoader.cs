using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class SceneLoader : MonoBehaviour
	{
		public GameObject gamaManager;          // gamaManager prefab to instantiate.

		void Awake ()
		{
			//Check if a gamaManager has already been assigned to static variable gamaManager.Instance or if it's still null
			if (GamaManger.Instance == null)
				//Instantiate gamaManager prefab
				Instantiate(gamaManager);
		}
	}
}

