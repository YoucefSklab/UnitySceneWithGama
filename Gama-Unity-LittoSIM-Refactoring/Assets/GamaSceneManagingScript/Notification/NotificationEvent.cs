using UnityEngine.EventSystems;


namespace ummisco.gama.unity.notification
{
	public static class NotificationEvent
	{
		
		public static void AddListener (this EventTrigger trigger, EventTriggerType eventType, System.Action<AxisEventData> listener)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry ();
			entry.eventID = eventType;
			entry.callback.AddListener (data => listener.Invoke ((AxisEventData)data));
			trigger.triggers.Add (entry);
		}
	}

}

