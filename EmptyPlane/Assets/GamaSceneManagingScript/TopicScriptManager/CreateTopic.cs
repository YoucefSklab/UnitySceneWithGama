﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;
using System.Globalization;
using ummisco.gama.unity.Behaviour;


namespace ummisco.gama.unity.topics
{
    public class CreateTopic : Topic
    {

        public CreateTopicMessage topicMessage;
        public GameObject newObject;
        public Color objectColor;


        public CreateTopic(CreateTopicMessage topicMessage, GameObject gameObj) : base(gameObj)
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

            //Debug.Log ("Order received. Let's create the object ");



            sendTopic();

        }


        // The method to call Game Objects methods
        //----------------------------------------
        public void sendTopic()
        {

            GameObject objectManager = getGameObjectByName(MqttSetting.GAMA_MANAGER_OBJECT_NAME, UnityEngine.Object.FindObjectsOfType<GameObject>());


            switch (topicMessage.type)
            {
                case "Capsule":
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    break;
                case "Cube":
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    break;
                case "Cylinder":
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    break;
                case "Plane":
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    break;
                case "Quad":
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    break;
                case "Sphere":
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;
                default:
                    Debug.Log("Object's type not specified. So, Object with type Sphere will be created as default object! ");
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    break;

            }


            // Set the name of the game object
            //--------------------------------
            newObject.name = topicMessage.objectName;
            newObject.AddComponent<AgentBehaviour>();

            Debug.Log(newObject + " is created and AgentBehaviour attached - ---------- ");
            // Set the position to the new GameObject
            //---------------------------------------
            XmlNode[] positionNode = (XmlNode[])topicMessage.position;
            Vector3 movement = ConvertType.vector3FromXmlNode(positionNode, MqttSetting.GAMA_POINT);
            newObject.transform.position = movement;


            XmlNode[] colorNode = (XmlNode[])topicMessage.color;
            objectColor = ConvertType.rgbColorFromXmlNode(colorNode, MqttSetting.GAMA_RGB_COLOR);

            objectColor.a = 1.0f;

            Renderer rend = newObject.GetComponent<Renderer>();
            rend.material.color = objectColor;


            Debug.Log("The color is " + objectColor.ToString());

            objectManager.SendMessage("addObjectToList", newObject);


        }

        public override void setAllProperties(object args)
        {
            object[] obj = (object[])args;
            this.topicMessage = (CreateTopicMessage)obj[0];
        }


        public GameObject getGameObjectByName(string objectName, GameObject[] allObjects)
        {
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
        }

    }




}