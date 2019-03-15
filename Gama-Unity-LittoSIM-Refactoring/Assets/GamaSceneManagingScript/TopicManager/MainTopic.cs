using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;
using System.Globalization;
using ummisco.gama.unity.AgentBehaviours;
using System.Xml.Linq;
using Nextzen.VectorData;
using ummisco.gama.unity.GamaAgent;

namespace ummisco.gama.unity.topics
{
    public class MainTopic : Topic
    {

        public GamaMessage topicMessage;
        public GameObject newObject;
        public Color objectColor;


        public MainTopic(GamaMessage topicMessage, GameObject gameObj) : base(gameObj)
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
            sendTopic();

        }


        // The method to call Game Objects methods
        //----------------------------------------
        public void sendTopic()
        {

            GameObject objectManager = getGameObjectByName(MqttSetting.GAMA_MANAGER_OBJECT_NAME, UnityEngine.Object.FindObjectsOfType<GameObject>());
            //Debug.Log("The content is: " + topicMessage.contents.ToString());
            Agent gamaAgent = UtilXml.getAgent((XmlNode[])topicMessage.contents);
            GamaManager.gamaAgentList.Add(gamaAgent);

        }

        public override void setAllProperties(object args)
        {
            object[] obj = (object[])args;
            this.topicMessage = (GamaMessage)obj[0];
        }


        public GameObject getGameObjectByName(string objectName, GameObject[] allObjects)
        {
            return GameObject.Find(objectName);
            /*
            foreach (GameObject gameObj in allObjects)
            {
                if (gameObj.activeInHierarchy)
                {
                    if (objectName.Equals(gameObj.name))
                    {
                        return gameObj;
                    }
                }
            }
            return null;
            */           
        }





    }




}