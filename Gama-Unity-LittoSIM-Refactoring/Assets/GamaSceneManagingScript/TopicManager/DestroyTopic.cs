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
    public class DestroyTopic : Topic
    {

        public DestroyTopicMessage topicMessage;
        public GameObject objectToDestroy;

        public DestroyTopic(DestroyTopicMessage topicMessage, GameObject gameObj) : base(gameObj)
        {
            this.topicMessage = topicMessage;
        }

        // Use this for initialization
        public override void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {

        }

        public void ProcessTopic(object obj)
        {
            setAllProperties(obj);
            // Debug.Log ("Order received. Let's destroy the GameObject ");
            Destroy(GameObject.Find(topicMessage.objectName));
        }

        public override void setAllProperties(object args)
        {
            object[] obj = (object[])args;
            this.topicMessage = (DestroyTopicMessage)obj[0];
        }
    }
}