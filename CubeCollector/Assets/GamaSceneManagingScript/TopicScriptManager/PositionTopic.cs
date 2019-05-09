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
	public class PositionTopic : Topic
	{
		public PositionTopicMessage topicMessage;

		public PositionTopic (TopicMessage currentMsg, GameObject gameObj) : base (gameObj)
		{

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

                //XmlNode[] positionNode = (XmlNode[])topicMessage.position;

                Vector3 position = topicMessage.position.getVector3(); //ConvertType.vector3FromXmlNode (positionNode, MqttSetting.GAMA_POINT);

				sendTopic (position);

			} 
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (Vector3 position)
		{
            Debug.Log(" Position to set is ----------->> "+position);
			targetGameObject.transform.position = position;
		}

		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.topicMessage = (PositionTopicMessage)obj [0];
			this.targetGameObject = (GameObject)obj [1];
		}
	}
}

