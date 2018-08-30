using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AssemblyCSharp
{
	public class EventTriggerClass : EventTrigger
	{
		public void setSpeed(PointerEventData data)
		{
			Debug.Log("setSpeed called.");
		}

		public override void OnCancel(BaseEventData data)
		{
			Debug.Log("OnCancel called.");
		}



	}
}

