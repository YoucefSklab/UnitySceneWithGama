﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;


using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using System;
using System.IO;

using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.ComponentModel;
using ummisco.gama.unity.topics;
using System.Globalization;


public class MainScript : MonoBehaviour
{


	private static MainScript m_Instance = null;

	public static MainScript Instance { get { return m_Instance; } }
	//Static instance of MainScript which allows it to be accessed by any other script.

	public string receivedMsg = "";
	public string clientId = Guid.NewGuid ().ToString ();
	public MqttClient client = new MqttClient (MqttSetting.SERVER_URL, MqttSetting.SERVER_PORT, false, null);
	public GamaMethods gama = new GamaMethods ();
	public MsgSerialization msgDes = new MsgSerialization ();
	public GamaMessage currentMsg;

	public GameObject[] allObjects = null;
	public GameObject mainGameObject = null;
	public GameObject gameObjectTarget = null;
	public object[] obj = null;
	public string objectTargetName = "";


	List<MqttMsgPublishEventArgs> msgList = new List<MqttMsgPublishEventArgs> ();


	void Awake ()
	{
		m_Instance = this;
		//Check if instance already exists 	
		//If instance already exists and it's not this:
		//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a MainScript.
		if (m_Instance == null)
			m_Instance = this;
		else if (m_Instance != this)
			Destroy (gameObject);    
		
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);

		MqttSetting.allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();

		GameObject[] all = UnityEngine.Object.FindObjectsOfType<GameObject> ();
		foreach (GameObject gO in allObjects) {
			if (gO.activeInHierarchy) {
				if (gO.name.Equals ("MainGameObject")) {
					mainGameObject = gO;
				}
			}					
		}


	}


	// Use this for initialization
	void Start ()
	{

		//client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		client.Connect (clientId); 
		client.Subscribe (new string[] { MqttSetting.MAIN_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.NOTIFY_MSG }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe (new string[] { MqttSetting.MONO_FREE_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe (new string[] { MqttSetting.POSITION_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe (new string[] { MqttSetting.COLOR_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.GET_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.SET_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

	}


	void FixedUpdate ()
	{

		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

		//Debug.Log ("------------------------------> Trait another message for: " + gameObject.name);

		if (msgList.Count > 0) {

			MqttMsgPublishEventArgs e = msgList [0];
			if (!MqttSetting.getTopicsInList ().Contains (e.Topic)) {
				Debug.Log ("-> The Topic doesn't exist in the defined list. Please check!");
				return;
			}
		
			receivedMsg = System.Text.Encoding.UTF8.GetString (e.Message);
			GamaMessage currentMsg = msgDes.msgDeserialization (receivedMsg);

			switch (e.Topic) {
			case MqttSetting.MAIN_TOPIC:
				Debug.Log ("-> Message to deal with as topic: " + MqttSetting.MAIN_TOPIC);

				break;
			case MqttSetting.MONO_FREE_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Message to deal with as topic: " + MqttSetting.MONO_FREE_TOPIC);
				objectTargetName = currentMsg.getObjectName ();
				allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
				gameObjectTarget = null;

				foreach (GameObject gameO in allObjects) {
					if (gameO.activeInHierarchy) {
						if (objectTargetName.Equals (gameO.name)) {
							gameObjectTarget = gameO;
						}
					}					
				}
				obj = new object[]{ currentMsg, gameObjectTarget };

				gameObject.GetComponent (MqttSetting.MONO_FREE_TOPIC_SCRIPT).SendMessage ("ProcessMonoFreeTopic", obj);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.POSITION_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Message to deal with as topic: " + MqttSetting.POSITION_TOPIC);
				objectTargetName = currentMsg.getObjectName ();
				allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
				gameObjectTarget = null;

				foreach (GameObject gameO in allObjects) {
					if (gameO.activeInHierarchy) {
						if (objectTargetName.Equals (gameO.name)) {
							gameObjectTarget = gameO;
						}
					}					
				}

				obj = new object[]{ currentMsg, gameObjectTarget };

				gameObject.GetComponent (MqttSetting.POSITION_TOPIC_SCRIPT).SendMessage ("ProcessPositionTopic", obj);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.COLOR_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Message to deal with as topic: " + MqttSetting.COLOR_TOPIC);
				objectTargetName = currentMsg.getObjectName ();
				allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
				gameObjectTarget = null;

				foreach (GameObject gameO in allObjects) {
					if (gameO.activeInHierarchy) {
						if (objectTargetName.Equals (gameO.name)) {
							gameObjectTarget = gameO;
						}
					}					
				}

				obj = new object[]{ currentMsg, gameObjectTarget };

				// gameObject is the current gameObject to which this script is attached
				gameObject.GetComponent (MqttSetting.COLOR_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
		
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.GET_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Message to deal with as topic: " + MqttSetting.GET_TOPIC);
				objectTargetName = currentMsg.getObjectName ();
				allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
				gameObjectTarget = null;

				foreach (GameObject gameO in allObjects) {
					if (gameO.activeInHierarchy) {
						if (objectTargetName.Equals (gameO.name)) {
							gameObjectTarget = gameO;
						}
					}					
				}

				string value = null;
				obj = new object[]{ currentMsg, gameObjectTarget, value };

				gameObject.GetComponent (MqttSetting.GET_TOPIC_SCRIPT).SendMessage ("ProcessGetTopic", obj);

				value = (string)obj [2];
				sendReplay (clientId, "GamaAgent", MqttSetting.REPLAY_TOPIC, value);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.SET_TOPIC:
				//------------------------------------------------------------------------------

				Debug.Log ("-> Message to deal with as topic: " + MqttSetting.SET_TOPIC);
				objectTargetName = currentMsg.getObjectName ();
				allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
				gameObjectTarget = null;

				foreach (GameObject gameO in allObjects) {
					if (gameO.activeInHierarchy) {
						if (objectTargetName.Equals (gameO.name)) {
							gameObjectTarget = gameO;
						}
					}					
				}

				obj = new object[]{ currentMsg, gameObjectTarget };

				gameObject.GetComponent (MqttSetting.SET_TOPIC_SCRIPT).SendMessage ("ProcessSetTopic", obj);
				//------------------------------------------------------------------------------
				break;
			default:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Message to deal with as topic: " + MqttSetting.DEFAULT_TOPIC);
				//------------------------------------------------------------------------------
				break;
			}

			msgList.Remove (e);
		}


	}



	void client_MqttMsgPublishReceived (object sender, MqttMsgPublishEventArgs e)
	{ 
		msgList.Add (e);
		receivedMsg = System.Text.Encoding.UTF8.GetString (e.Message);
		//Debug.Log (">  New Message received on topic : " + e.Topic);
		//Debug.Log (">  msgList count : " + msgList.Count);
	}



	void OnGUI ()
	{
		if (GUI.Button (new Rect (20, 40, 180, 20), "Send Mqtt message")) {
			client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes ("Sending from Unity3D!!! Good"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log ("Message sent! test");

			gama.getAllSceneGameObject ();

		}
	}


	public void tester ()
	{
		client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes ("Good, Bug fixed -> Sending from Unity3D!!! Good"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
	}


	public void sendGotBoxMsg ()
	{
		GamaReponseMessage msg = new GamaReponseMessage (clientId, "GamaAgent", MqttSetting.NOTIFY_MSG, "Got a Box notification", DateTime.Now.ToString ());

		string message = msgDes.msgSerialization (msg);
		client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes (message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		//client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes ("Good, Another box2"));
	}

	public void sendReplay (string sender, string receiver, string topic, string replayMsg)
	{
		GamaReponseMessage msg = new GamaReponseMessage (sender, receiver, topic, replayMsg, DateTime.Now.ToString ());
		string message = msgDes.msgSerialization (msg);
		client.Publish (topic, System.Text.Encoding.UTF8.GetBytes (message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
	}

	void OnDestroy ()
	{
		m_Instance = null;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	public void handleMessage (GamaMessage msg)
	{

	}


	// The method to call Game Objects methods
	//----------------------------------------
	public void sendMessageToGameObject (GameObject gameObject, string methodName, Dictionary<object, object> data)
	{

		int size = data.Count;
		List<object> keyList = new List<object> (data.Keys);

		System.Reflection.MethodInfo info = gameObject.GetComponent ("PlayerController").GetType ().GetMethod (methodName);
		ParameterInfo[] par = info.GetParameters ();


		for (int j = 0; j < par.Length; j++) {
			System.Reflection.ParameterInfo par1 = par [j];

			Debug.Log ("->>>>>>>>>>>>>>--> parametre Name >>=>>=>>=  " + par1.Name);
			Debug.Log ("->>>>>>>>>>>>>>--> parametre Type >>=>>=>>=  " + par1.ParameterType);

		}

		switch (size) {
		case 0:
			gameObject.SendMessage (methodName);
			break;
		case 1:
			gameObject.SendMessage (methodName, convertParameter (data [keyList.ElementAt (0)], par [0]));
			break;
		
		default:
			object[] obj = new object[size + 1];
			int i = 0;
			foreach (KeyValuePair<object, object> pair in data) {
				obj [i] = pair.Value;
				i++;
			}
			gameObject.SendMessage (methodName, obj);
			break;
		}

	}

	public object convertParameter (object val, ParameterInfo par)
	{
		//TypeConverter typeConverter = TypeDescriptor.GetConverter(par.ParameterType);
		//object propValue = typeConverter.ConvertFromString(val);
		object propValue = Convert.ChangeType (val, par.ParameterType);

		return propValue;
	}

	public GameObject getGameObjectByName (string objectName)
	{
		foreach (GameObject gameO in MqttSetting.allObjects) {
			if (gameO.activeInHierarchy) {
				if (objectName.Equals (gameO.name)) {
					return gameO;
				}
			}					
		}
		return null;
	}
}
