﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;


namespace ummisco.gama.unity.topics
{
	public class MoveTopic : Topic
	{


		public Rigidbody rb;
		public float inverseMoveTime;
		public float moveTime = 10000.1f;
		public MoveTopicMessage topicMessage;

		public MoveTopic (TopicMessage currentMsg, GameObject gameObj) : base (gameObj)
		{

		}

		// Use this for initialization
		public override void Start ()
		{
			inverseMoveTime = 1f / moveTime;

		}

		// Update is called once per frame
		public override void Update ()
		{

		}

		public void ProcessTopic (object obj)
		{
			setAllProperties (obj);

			rb = targetGameObject.GetComponent<Rigidbody> ();

			if (targetGameObject != null) {

				XmlNode[] node = (XmlNode[])topicMessage.position;
				Dictionary<object, object> dataDictionary = new Dictionary<object, object> ();

				for (int i = 1; i < node.Length; i++) {
					XmlElement elt = (XmlElement)node.GetValue (i);
					XmlNodeList list = elt.ChildNodes;

					object atr = "";
					object vl = "";

					foreach (XmlElement item in list) {
						if (item.Name.Equals ("attribute")) {
							atr = item.InnerText;
						}
						if (item.Name.Equals ("value")) {
							vl = item.InnerText;
						}
					}
					dataDictionary.Add (atr, vl);
				}

				sendTopic (dataDictionary, topicMessage.speed);
			} 
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (Dictionary<object, object> data, int speed)
		{
			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);
			float x, y, z; 

			x = float.Parse ((string)data [keyList.ElementAt (0)], CultureInfo.InvariantCulture.NumberFormat);
			y = float.Parse ((string)data [keyList.ElementAt (1)], CultureInfo.InvariantCulture.NumberFormat);
			z = float.Parse ((string)data [keyList.ElementAt (2)], CultureInfo.InvariantCulture.NumberFormat);
		
			//	Debug.Log ("Move to  (X=" + x + ",Y=" + y + ",Z=" + z + ") position!");

			Debug.Log ("Move to  (X=" + x + ",Y=" + y + ",Z=" + z + ") with speed "+speed );

			moveToPosition ((float)x, (float)y, (float)z, speed);
		}


		public void moveToPosition (float xDir, float yDir, float zDir, int speed)
		{

			//Store start position to move from, based on objects current transform position.
			Vector3 start = targetGameObject.transform.position;

			// Calculate end position based on the direction parameters passed in when calling Move.
			Vector3 end = start + new Vector3 (xDir, yDir, zDir);

			Vector3 movement = new Vector3 (xDir, yDir, zDir);

			rb.AddForce (movement * speed);

			//StartCoroutine (SmoothMovement (end, speed));
		}

		//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
		protected IEnumerator SmoothMovement (Vector3 end, int speed)
		{
			//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
			//Square magnitude is used instead of magnitude because it's computationally cheaper.
			float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			Debug.Log ("Before Moving and it remains " + sqrRemainingDistance.ToString ());
			Debug.Log ("inverseMoveTime is " + inverseMoveTime);

		
			inverseMoveTime = 100f;

			//While that distance is greater than a very small amount (Epsilon, almost zero):
			while (sqrRemainingDistance > float.Epsilon) {
				//Find a new position proportionally closer to the end, based on the moveTime
				//Vector3 newPostion = Vector3.MoveTowards (rb.position, end, inverseMoveTime * Time.deltaTime);
				Vector3 newPostion = Vector3.MoveTowards (rb.position, end, speed * Time.deltaTime);

				Debug.Log ("New position is " + newPostion.ToString ());

				//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
				rb.MovePosition (newPostion);

				//Recalculate the remaining distance after moving.
				sqrRemainingDistance = (transform.position - end).sqrMagnitude;
				Debug.Log ("Stillll Moving and it remains " + sqrRemainingDistance.ToString ());
				//Return and loop until sqrRemainingDistance is close enough to zero to end the function
				yield return null;
			}


			Debug.Log ("Good! end distination is reached");
		}

		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.topicMessage = (MoveTopicMessage)obj [0];
			this.targetGameObject = (GameObject)obj [1];
		}

	}
}

