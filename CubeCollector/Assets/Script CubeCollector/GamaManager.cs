using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using ummisco.gama.unity.notification;

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


public class GamaManager : MonoBehaviour
{


	private static GamaManager m_Instance = null;

	public static GamaManager Instance { get { return m_Instance; } }
	//Static instance of GamaManager which allows it to be accessed by any other script.

	public string receivedMsg = "";
	public string clientId = Guid.NewGuid ().ToString ();
	public MqttClient client = new MqttClient (MqttSetting.SERVER_URL, MqttSetting.SERVER_PORT, false, null);
	public GamaMethods gama = new GamaMethods ();
	public MsgSerialization msgDes = new MsgSerialization ();
	public TopicMessage currentMsg;

	public GameObject[] allObjects = null;
	public GameObject gamaManager = null;
	public GameObject targetGameObject = null;
	public GameObject topicGameObject = null;
	public object[] obj = null;

	public Boolean notificationSent = false;

	public List<GameObject> objectsList = new List<GameObject> ();



	List<MqttMsgPublishEventArgs> msgList = new List<MqttMsgPublishEventArgs> ();


	void Awake ()
	{
		m_Instance = this;
		//Check if instance already exists 	
		//If instance already exists and it's not this:
		//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a gamaManager.
		if (m_Instance == null)
			m_Instance = this;
		else if (m_Instance != this)
			Destroy (gameObject);    
		
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameObject);

		MqttSetting.allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
		allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();

		gamaManager = getGameObjectByName (MqttSetting.GAMA_MANAGER_OBJECT_NAME);

		notificationSent = false;

	}


	// Use this for initialization
	void Start ()
	{
		// To put only in start bloc
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;


		client.Connect (clientId); 
		client.Subscribe (new string[] { MqttSetting.MAIN_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.MONO_FREE_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe (new string[] { MqttSetting.MULTIPLE_FREE_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.POSITION_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe (new string[] { MqttSetting.COLOR_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.GET_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.SET_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.MOVE_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.PROPERTY_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.CREATE_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.NOTIFICATION_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

		NotificationRegistry.getCallingMethod ();
	}


	void FixedUpdate ()
	{

		//Debug.Log ("-> The number of all created gameObjects is : "+ objectsList.Count);

		if (msgList.Count > 0) {

			MqttMsgPublishEventArgs e = msgList [0];
			if (!MqttSetting.getTopicsInList ().Contains (e.Topic)) {
				Debug.Log ("-> The Topic '" + e.Topic + "' doesn't exist in the defined list. Please check!");
				return;
			}
		
			receivedMsg = System.Text.Encoding.UTF8.GetString (e.Message);
			//Debug.Log ("-> Received Message is : " + receivedMsg);
			allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();

			switch (e.Topic) {
			case MqttSetting.MAIN_TOPIC:
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.MAIN_TOPIC);

				topicGameObject = gameObject; 

				break;
			case MqttSetting.MONO_FREE_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.MONO_FREE_TOPIC);

				MonoFreeTopicMessage monoFreeTopicMessage = (MonoFreeTopicMessage)msgDes.deserialization (receivedMsg, new MonoFreeTopicMessage ());
				targetGameObject = getGameObjectByName (monoFreeTopicMessage.objectName);
				obj = new object[]{ monoFreeTopicMessage, targetGameObject };

				topicGameObject = getGameObjectByName (MqttSetting.MONO_FREE_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.MONO_FREE_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.MULTIPLE_FREE_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.MULTIPLE_FREE_TOPIC);

				MultipleFreeTopicMessage multipleFreetopicMessage = (MultipleFreeTopicMessage)msgDes.deserialization (receivedMsg, new MultipleFreeTopicMessage ());
				targetGameObject = getGameObjectByName (multipleFreetopicMessage.objectName);
				obj = new object[]{ multipleFreetopicMessage, targetGameObject };

				topicGameObject = getGameObjectByName (MqttSetting.MULTIPLE_FREE_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.MULTIPLE_FREE_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.POSITION_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.POSITION_TOPIC);

				PositionTopicMessage positionTopicMessage = (PositionTopicMessage)msgDes.deserialization (receivedMsg, new PositionTopicMessage ());

				Debug.Log ("-> Topic Timestamp is : " + positionTopicMessage.emissionTimeStamp);

				targetGameObject = getGameObjectByName (positionTopicMessage.objectName);
				obj = new object[]{ positionTopicMessage, targetGameObject };

				topicGameObject = getGameObjectByName (MqttSetting.POSITION_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.POSITION_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.MOVE_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.MOVE_TOPIC);

				MoveTopicMessage moveTopicMessage = (MoveTopicMessage)msgDes.deserialization (receivedMsg, new MoveTopicMessage ());
				targetGameObject = getGameObjectByName (moveTopicMessage.objectName);
				obj = new object[]{ moveTopicMessage, targetGameObject };

				topicGameObject = getGameObjectByName (MqttSetting.MOVE_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.MOVE_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.COLOR_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.COLOR_TOPIC);

				ColorTopicMessage colorTopicMessage = (ColorTopicMessage)msgDes.deserialization (receivedMsg, new ColorTopicMessage ());
				targetGameObject = getGameObjectByName (colorTopicMessage.objectName);
				obj = new object[]{ colorTopicMessage, targetGameObject };

				topicGameObject = getGameObjectByName (MqttSetting.COLOR_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.COLOR_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
		
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.GET_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.GET_TOPIC);
				string value = null;

				GetTopicMessage getTopicMessage = (GetTopicMessage)msgDes.deserialization (receivedMsg, new GetTopicMessage ());
				targetGameObject = getGameObjectByName (getTopicMessage.objectName);


				topicGameObject = getGameObjectByName (MqttSetting.GET_TOPIC_MANAGER);
				obj = new object[]{ getTopicMessage, targetGameObject, value };
			
				topicGameObject.GetComponent (MqttSetting.GET_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
				value = (string)obj [2];
				sendReplay (clientId, "GamaAgent", value);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.SET_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.SET_TOPIC);

				SetTopicMessage setTopicMessage = (SetTopicMessage)msgDes.deserialization (receivedMsg, new SetTopicMessage ());
				targetGameObject = getGameObjectByName (setTopicMessage.objectName);
				obj = new object[]{ setTopicMessage, targetGameObject };

				topicGameObject = getGameObjectByName (MqttSetting.SET_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.SET_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.PROPERTY_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.PROPERTY_TOPIC);

				PropertyTopicMessage propertyTopicMessage = (PropertyTopicMessage)msgDes.deserialization (receivedMsg, new PropertyTopicMessage ());
				targetGameObject = getGameObjectByName (propertyTopicMessage.objectName);
				obj = new object[]{ propertyTopicMessage, targetGameObject };

				topicGameObject = getGameObjectByName (MqttSetting.PROPERTY_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.PROPERTY_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
				//------------------------------------------------------------------------------
				break;

			case MqttSetting.CREATE_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.CREATE_TOPIC);

				CreateTopicMessage createTopicMessage = (CreateTopicMessage)msgDes.deserialization (receivedMsg, new CreateTopicMessage ());
				obj = new object[]{ createTopicMessage };

				topicGameObject = getGameObjectByName (MqttSetting.CREATE_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.CREATE_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);
				//------------------------------------------------------------------------------
				break;
			case MqttSetting.NOTIFICATION_TOPIC:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.NOTIFICATION_TOPIC);

				NotificationTopicMessage notificationTopicMessage = (NotificationTopicMessage)msgDes.deserialization (receivedMsg, new NotificationTopicMessage ());
				obj = new object[]{ notificationTopicMessage };

				topicGameObject = getGameObjectByName (MqttSetting.NOTIFICATION_TOPIC_MANAGER);

				topicGameObject.GetComponent (MqttSetting.NOTIFICATION_TOPIC_SCRIPT).SendMessage ("ProcessTopic", obj);

				//------------------------------------------------------------------------------
				break;
			default:
				//------------------------------------------------------------------------------
				Debug.Log ("-> Topic to deal with is : " + MqttSetting.DEFAULT_TOPIC);
				//------------------------------------------------------------------------------
				break;
			}

			msgList.Remove (e);
		}

		checkForNotifications ();
	
	}



	void client_MqttMsgPublishReceived (object sender, MqttMsgPublishEventArgs e)
	{ 
		msgList.Add (e);
		receivedMsg = System.Text.Encoding.UTF8.GetString (e.Message);
		Debug.Log (">  New Message received on topic : " + e.Topic);
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

		GamaReponseMessage msg = new GamaReponseMessage (clientId, "GamaAgent", "Got a Box notification", DateTime.Now.ToString ());

		string message = msgDes.msgSerialization (msg);
		client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes (message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		//client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes ("Good, Another box2"));
	}

	// à revoir en utilisant publishMessage
	public void sendReplay (string sender, string receiver, string replayMsg)
	{
		GamaReponseMessage msg = new GamaReponseMessage (sender, receiver, replayMsg, DateTime.Now.ToString ());
		string message = msgDes.msgSerialization (msg);

		publishMessage (message, MqttSetting.REPLAY_TOPIC);
	}

	void OnDestroy ()
	{
		m_Instance = null;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	public void handleMessage (TopicMessage msg)
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
		object propValue = Convert.ChangeType (val, par.ParameterType);
		return propValue;
	}

	public GameObject getGameObjectByName (string objectName)
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


	public void addObjectToList (GameObject obj)
	{
		objectsList.Add (obj);
	}

	public void removeObjectFromList (GameObject obj)
	{
		objectsList.Remove (obj);
	}


	public void checkForNotifications ()
	{
		if (NotificationRegistry.notificationsList.Count >= 1) {
			foreach (NotificationEntry el in NotificationRegistry.notificationsList) {
				if (!notificationSent) // TODO Implement a mecanism of notification frequency! 
				if (el.notify) {
					string msg = getReplayNotificationMessage (el);
					publishMessage (msg, MqttSetting.NOTIFY_MSG);
					el.notify = false;
					notificationSent = true;
				}
			}
		}	
	}

	public string getReplayNotificationMessage (NotificationEntry el)
	{
		NotificationMessage msg = new NotificationMessage ("Unity", el.agentId, "Contents Not set", DateTime.Now.ToString (), el.notificationId);
		string message = msgDes.serializationPlainXml (msg);
		return message;
	}

	public void publishMessage (string message, string topic)
	{
		client.Publish (topic, System.Text.Encoding.UTF8.GetBytes (message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
	}
}
