using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;


namespace ummisco.gama.unity.topics
{
	public class PropertyTopic : Topic
	{

		public PropertyTopicMessage topicMessage;

		public PropertyTopic (PropertyTopicMessage topicMessage, GameObject gameObj) : base (gameObj)
		{
			this.topicMessage = topicMessage;
		}

		// Use this for initialization
		public override void Start ()
		{

		}

		// Update is called once per frame
		public override void Update ()
		{

		}


		public void ProcessTopic (object obj)
		{
			setAllProperties (obj);


			if (targetGameObject != null) {

				string property = topicMessage.property;
				string value = topicMessage.value;

				sendTopic (targetGameObject, property, value);
			} 


		}


		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (GameObject targetGameObject, string property, string value)
		{

			Component[] cs = (Component[])targetGameObject.GetComponents (typeof(Component));
			foreach (Component c in cs) {
				//Debug.Log("name: "+c.name+" type: "+c.GetType() +" basetype: "+c.GetType().BaseType);
				PropertyInfo propertyInfo = c.GetType().GetProperty(property);
				if (propertyInfo != null) {
					//Debug.Log ("------->>   Good. Property exist. Its name is : " + propertyInfo.Name + " and its value is: " + propertyInfo.GetValue ((System.Object)c, null));
					object val =  Convert.ChangeType (value, propertyInfo.PropertyType);
					System.Object obj = (System.Object)c;
					propertyInfo.SetValue(
						obj, 
						val, 
						null);
				} else {
					//Debug.Log ("------->>   Sorry. Property doesn't exist : "+property +" and component is "+ c.name);
				}
				
				/*
				foreach (PropertyInfo fi in c.GetType().GetProperties ()) {
						System.Object obj = (System.Object)c;
					if (fi.Name.Equals ("isKinematic")) {
						Debug.Log("------->>   Good. I have got it. ");
						object val = (Boolean)true;
						fi.SetValue(
							obj, 
							val, 
							null);
					}

					//Debug.Log("fi name: "+fi.Name+" type: "+fi.PropertyType);
				}
				*/

			}

		}

		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.topicMessage = (PropertyTopicMessage)obj [0];
			this.targetGameObject = (GameObject)obj [1];
		}

	}

}