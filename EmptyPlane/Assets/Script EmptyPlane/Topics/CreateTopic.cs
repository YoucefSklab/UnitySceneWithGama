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


namespace ummisco.gama.unity.topics
{
	public class CreateTopic : Topic
	{

		public CreateTopicMessage topicMessage;
		public GameObject newObject;
		public Color objectColor;


		public CreateTopic (CreateTopicMessage topicMessage, GameObject gameObj) : base (gameObj)
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

			Debug.Log ("Order received. Let's create the object ");

			string type = topicMessage.type;
			string color = topicMessage.color;
			object position = topicMessage.position;

			sendTopic (topicMessage.objectName, type, color, position);

		}


		// The method to call Game Objects methods
		//----------------------------------------
		public void sendTopic (string objectName, string type, string color, object position)
		{

			GameObject objectManager = getGameObjectByName (MqttSetting.GAMA_MANAGER_OBJECT_NAME, UnityEngine.Object.FindObjectsOfType<GameObject> ());


			switch (type) {
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
				Debug.Log ("Object's type not specified. So, Object with type Sphere will be created as default object! ");
				newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				break;
			
			}


			// Set the name of the game object
			//--------------------------------
			newObject.name = objectName;

		
			// Set the position to the new GameObject
			//---------------------------------------
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

			int size = dataDictionary.Count;
			List<object> keyList = new List<object> (dataDictionary.Keys);
			float x, y, z;

			x = float.Parse ((string)dataDictionary [keyList.ElementAt (0)], CultureInfo.InvariantCulture.NumberFormat);
			y = float.Parse ((string)dataDictionary [keyList.ElementAt (1)], CultureInfo.InvariantCulture.NumberFormat);
			z = float.Parse ((string)dataDictionary [keyList.ElementAt (2)], CultureInfo.InvariantCulture.NumberFormat);

			Debug.Log ("----->>>>    X,Y,Z  " + x + "," + y + "," + z);

			Vector3 movement = new Vector3 (x, y, z);
			newObject.transform.position = movement;


			//Add Components to the Game Object
			//---------------------------------
			// newObject.AddComponent<Rigidbody>();
			//testObject.AddComponent<MeshFilter>(); // already added by creation
			//testObject.AddComponent<MeshRenderer>(); // already added when created
			//newObject.AddComponent<BoxCollider>();

			objectColor = Tools.stringToColor (color);
			Renderer rend = newObject.GetComponent<Renderer>();
			//rend.material = new Material(Shader.Find("Player"));
			//Renderer rend = GetComponent<Renderer>();
			//rend.material = new Material(shader);
			//rend.material.mainTexture = texture;
			rend.material.color = objectColor;



			Debug.Log ("Object poision not set. This has to be completed!");

			objectManager.SendMessage ("addObjectToList", newObject);


		}

		public override void setAllProperties (object args)
		{
			object[] obj = (object[])args;
			this.topicMessage = (CreateTopicMessage)obj [0];
		}


		public GameObject getGameObjectByName (string objectName, GameObject[] allObjects)
		{
			foreach (GameObject gameObj in allObjects) {
				if (gameObj.activeInHierarchy) {
					if (objectName.Equals (gameObj.name)) {
						return gameObj;
					}
				}					
			}
			return null;
		}

	}




}