using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class SceneLoader : MonoBehaviour
	{
		public GameObject mainScript;          // MainScript prefab to instantiate.

		void Awake ()
		{
			//Check if a MainScript has already been assigned to static variable MainScript.Instance or if it's still null
			if (MainScript.Instance == null)
				//Instantiate mainScript prefab
				Instantiate(mainScript);
		}
	}
}

