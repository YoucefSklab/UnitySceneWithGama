using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;
using ummisco.gama.unity.notification;
using UnityEditor.VersionControl;


namespace ummisco.gama.unity.topics
{
	public class NotificationTopic : Topic
	{

		public NotificationTopicMessage topicMessage;


		public NotificationTopic (NotificationTopicMessage topicMessage, GameObject gameObj) : base (gameObj)
		{
			this.topicMessage = topicMessage;
		}

		// Use this for initialization
		public override void Start ()
		{
			NotificationRegistry.notificationsList = new List<NotificationEntry> ();
		}

		// Update is called once per frame
		public override void Update ()
		{
			if (NotificationRegistry.notificationsList.Count >= 1) {




				foreach (NotificationEntry el in NotificationRegistry.notificationsList) {

					switch (el.fieldType) {

					case "field":
						//Debug.Log ("Field notification");
						if (isNotification (el)) {
							//Debug.Log ("------->>  Yes you have to send notification");
							el.notify = true;
						} else {
							//Debug.Log ("------->>  Sorry, No need to send notification");
						}
						break;
					case "property":

						//Debug.Log ("Property notification");
						break;
					default:
					
						break;
					}

				//	Debug.Log ("Game Object: " + el.objectName);
				//	Debug.Log ("Field Name: " + el.fieldName);
				//	Debug.Log ("Field Type: " + el.fieldType);
				//	Debug.Log ("FIeld Value: " + el.fieldValue);
				//	Debug.Log ("Field Operator: " + el.fieldOperator);
				}


			}
		}




		public void ProcessTopic (object obj)
		{
			setAllProperties (obj);

			NotificationEntry notif = new NotificationEntry (topicMessage.notificationId, topicMessage.objectName, topicMessage.fieldType, topicMessage.fieldName, topicMessage.fieldValue, topicMessage.fieldOperator, topicMessage.sender);
			NotificationRegistry.addToList(notif);
		
		}

		public Boolean isNotification (NotificationEntry entry)
		{

			GameObject targetGameObject = getGameObjectByName (entry.objectName);

			FieldInfo[] fieldInfoGet = targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetFields ();



			foreach (FieldInfo fi in fieldInfoGet) {
				if (fi.Name.Equals (entry.fieldName)) {
					UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX);
					object target = fi.GetValue (ob);
				
				//	Debug.Log ("-------------->>>> The Operator" + entry.fieldOperator);
				//	Debug.Log ("-------------->>>> The field value" + fi.GetValue (ob));
				//	Debug.Log ("-------------->>>> The field introduced value " + entry.fieldValue);

					switch (fi.FieldType.ToString ()) {
					case "System.Int32":
						return Compare<System.Int32>(entry.fieldOperator, (System.Int32) 
							Convert.ChangeType (fi.GetValue (ob), fi.FieldType), 
							(System.Int32)Convert.ChangeType (entry.fieldValue, fi.FieldType));
						break;
					default:

						break;

					}



				

				}
			}

		

			return false;
		}


		public Boolean isNotification2 (NotificationEntry entry)
		{

			GameObject targetGameObject = getGameObjectByName (entry.objectName);

			FieldInfo[] fieldInfoGet = targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetFields ();



			foreach (FieldInfo fi in fieldInfoGet) {
				if (fi.Name.Equals (entry.fieldName)) {
					UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX);
					object target = fi.GetValue (ob);
					//return IsValueEqual (entry.fieldValue, target, entry.fieldOperator);
					//return Compare(entry.fieldOperator, (fi.GetType ()) target, (object) entry.fieldValue);

					//return Compare<int>(entry.fieldOperator, (int) Convert.ChangeType (entry.fieldValue, int), (int) Convert.ChangeType (entry.fieldValue, int)); 
					//, (fi.GetType ()) target, (object) entry.fieldValue);

					//	object propValue = Convert.ChangeType (val, par.ParameterType);
					//msgReplay = fi.GetValue (ob).ToString ();
					return Compare<int> (entry.fieldOperator, 12, 12); 

				}
			}



			return false;
		}





		public static bool Compare<T> (string op, T x, T y) where T:IComparable
		{
			switch (op) {
			case "==":
				return x.CompareTo (y) == 0;
			case "!=":
				return x.CompareTo (y) != 0;
			case ">":
				return x.CompareTo (y) > 0;
			case ">=":
				return x.CompareTo (y) >= 0;
			case "<":
				return x.CompareTo (y) < 0;
			case "<=":
				return x.CompareTo (y) <= 0;
			default:
				return false;
			}
		}







		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (GameObject targetGameObject, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);
			object obj = data [keyList.ElementAt (0)];

			FieldInfo[] fieldInfoSet = targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetFields ();

			foreach (KeyValuePair<object, object> pair in data) {
				foreach (FieldInfo fi in fieldInfoSet) {
					if (fi.Name.Equals (pair.Key.ToString ())) {
						UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent (targetGameObject.name + MqttSetting.SCRIPT_PRIFIX);
						fi.SetValue (ob, (Convert.ChangeType (pair.Value, fi.FieldType)));
					}
				}
			}
		}

		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.topicMessage = (NotificationTopicMessage)obj [0];
		}


		public GameObject getGameObjectByName (string objectName)
		{
			
			GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();

			foreach (GameObject gameO in allObjects) {
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