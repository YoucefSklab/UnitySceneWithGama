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
using ummisco.gama.unity.GamaConcepts;
using ummisco.gama.unity.SceneManager;

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
            //Debug.Log ("Order received. Let's create the object ");
            setAllProperties(obj);
            sendTopic();
        }

        // The method to call Game Objects methods
        //----------------------------------------
        public void sendTopic()
        {

            GameObject objectManager = GameObject.Find(IGamaManager.GAMA_MANAGER);
            switch (topicMessage.type)
            {
                case IGamaConcept.CAPSULE:
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    break;
                case IGamaConcept.CUBE:
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    break;
                case IGamaConcept.CYLINDER:
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    break;
                case IGamaConcept.PLANE:
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    break;
                case IGamaConcept.QUAD:
                    newObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    break;
                case IGamaConcept.SPHERE:
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

            // Set the position to the new GameObject
            //---------------------------------------
            XmlNode[] positionNode = (XmlNode[])topicMessage.position;
            Vector3 movement = ConvertType.vector3FromXmlNode(positionNode, IGamaConcept.GAMA_POINT_CLASS);
            newObject.transform.position = movement;


            XmlNode[] colorNode = (XmlNode[])topicMessage.color;
            objectColor = ConvertType.rgbColorFromXmlNode(colorNode, IGamaConcept.GAMA_RGB_COLOR_CLASS);

            objectColor.a = 1.0f;

            Renderer rend = newObject.GetComponent<Renderer>();
            rend.material.color = objectColor;

            objectManager.SendMessage("addObjectToList", newObject);
            

        }

        public override void setAllProperties(object args)
        {
            object[] obj = (object[])args;
            this.topicMessage = (CreateTopicMessage)obj[0];
        }
    }
}