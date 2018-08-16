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


namespace ummisco.gama.unity.topics
{
	public class NotificationTopic : Topic
	{

		public NotificationTopicMessage topicMessage;
		public List<NotificationEntry> notificationsList; 

		public NotificationTopic (NotificationTopicMessage topicMessage, GameObject gameObj) : base (gameObj)
		{
			this.topicMessage = topicMessage;
		}

		// Use this for initialization
		public override void Start ()
		{
			notificationsList = new List<NotificationEntry>();
		}

		// Update is called once per frame
		public override void Update ()
		{

		}


		public void ProcessTopic (object obj)
		{
			setAllProperties (obj);



		//	new Part() {PartName="crank arm", PartId=1234}

		//	NotificationEntry notif2 = new NotificationEntry() {objectName= topicMessage.objectName, fieldType=topicMessage.fieldType, fieldName=topicMessage.fieldName, fieldValue=topicMessage.fieldValue, fieldOperator=topicMessage.fieldOperator};


			NotificationEntry notif = new NotificationEntry (topicMessage.objectName, topicMessage.fieldType, topicMessage.fieldName, topicMessage.fieldValue, topicMessage.fieldOperator);

			Debug.Log ("The field name is: " + notif.fieldName);
			Debug.Log ("The object name is: " + notif.objectName);



			notificationsList.Add (notif);
		
		//	sendTopic (targetGameObject, dataDictionary);


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

	}

}