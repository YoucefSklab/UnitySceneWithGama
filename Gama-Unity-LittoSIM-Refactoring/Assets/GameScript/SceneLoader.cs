using System;
using UnityEngine;
using ummisco.gama.unity.SceneManager;

namespace AssemblyCSharp
{
	public class SceneLoader : MonoBehaviour
	{
		public GameObject gamaManager;          // gamaManager prefab to instantiate.

		void Awake ()
		{
			//Check if a gamaManager has already been assigned to static variable gamaManager.Instance or if it's still null
			if (GamaManager.Instance == null)
				//Instantiate gamaManager prefab
				Instantiate(gamaManager);
		}
	}
}

