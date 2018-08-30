using System.Collections;
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

			XmlNode[] positionNode = (XmlNode[])topicMessage.position;
			Vector3 movement = ConvertType.vector3FromXmlNode (positionNode, MqttSetting.GAMA_POINT);

			sendTopic (movement);
	
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (Vector3 movement )
		{

			if (topicMessage.smoothMove) {
				smoothMoveToPosition (movement, topicMessage.speed);
			} else {
				freeMoveToPosition (movement, topicMessage.speed);
			}


		}


		public void freeMoveToPosition (Vector3 position, int speed)
		{
			rb.AddForce (position * speed);
		}

		public void smoothMoveToPosition (Vector3 position, int speed)
		{

			//Store start position to move from, based on objects current transform position.
			Vector3 start = targetGameObject.transform.position;

			// Calculate end position based on the direction parameters passed in when calling Move.
			Vector3 end = start + position;

			StartCoroutine (SmoothMovement (end, speed));
		}

		//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
		protected IEnumerator SmoothMovement (Vector3 end, int speed)
		{
			//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
			//Square magnitude is used instead of magnitude because it's computationally cheaper.
			float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			inverseMoveTime = 100f;

			//While that distance is greater than a very small amount (Epsilon, almost 
			while (sqrRemainingDistance > 0.1f) {
				//Find a new position proportionally closer to the end, based on the moveTime
				//Vector3 newPostion = Vector3.MoveTowards (rb.position, end, inverseMoveTime * Time.deltaTime);
				Vector3 newPostion = Vector3.MoveTowards (rb.position, end, speed * Time.deltaTime);


				//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
				rb.MovePosition (newPostion);

				//Recalculate the remaining distance after moving.
				sqrRemainingDistance = (transform.position - end).sqrMagnitude;

				//Return and loop until sqrRemainingDistance is close enough to zero to end the function
				yield return null;
			}
			Debug.Log ("Position Set! ");
			rb.velocity = Vector3.zero;
			rb.transform.position = end;
			//rb.angularVelocity = Vector3.zero;
			//rb.Sleep ();


		}



		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.topicMessage = (MoveTopicMessage)obj [0];
			this.targetGameObject = (GameObject)obj [1];
		}

	}
}

