using System.Collections;
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


public class MainScript : MonoBehaviour
{

	private MainScript m_Instance;

	public MainScript Instance { get { return m_Instance; } }

	public string receivedMsg = "";
	public string clientId = Guid.NewGuid ().ToString ();
	public MqttClient client = new MqttClient (MqttSetting.SERVER_URL, MqttSetting.SERVER_PORT, false, null);
	public GamaMethods gama = new GamaMethods ();
	public MsgSerialization msgDes = new MsgSerialization ();
	public GamaMessage currentMsg;

	public GameObject[] allObjects;
	public GameObject currentGameObject = null; 



	void Awake ()
	{
		m_Instance = this;
		MqttSetting.allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();

	}


	// Use this for initialization
	void Start ()
	{

		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		client.Connect (clientId); 
		client.Subscribe (new string[] { MqttSetting.MAIN_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe (new string[] { MqttSetting.NOTIFY_MSG}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe (new string[] { MqttSetting.SPEED_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe (new string[] { MqttSetting.POSITION_TOPIC}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
		client.Subscribe (new string[] { MqttSetting.COLOR_TOPIC}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 


		/*
		List<string> targets = new List<string>();
		targets.Add("Inbox1"); targets.Add("Inbox2"); targets.Add("Inbox3"); targets.Add("Inbox4");
		Debug.Log("The list is: " + Tools.listToString(targets, "|"));
		*/
	}


	void FixedUpdate ()
	{
		/*
		//	client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

		// TODO: Review this part. Need to get correctly the received message
		if (receivedMsg != "") {
			string message = receivedMsg;

			currentMsg = msgDes.msgDeserialization (message);
		//	string att = msgDes.getMsgAttribute (message, "unityAction");

			currentGameObject = gama.getGameObjectByName (currentMsg.getObjectName ());

			BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
			System.Reflection.MethodInfo[] info = currentGameObject.GetComponent (currentGameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethods (flags);
			//System.Reflection.MethodInfo[] info = currentGameObject.GetComponent ("PlayerController").GetType ().GetMethods ();
		


			//System.Reflection.MethodInfo[] info = currentGameObject.GetType ().GetMethods ();
			Debug.Log ("->>>>>>>>>>>>>>--> " + info.ToString ());

			for (int i = 0; i < info.Length; i++) {
				System.Reflection.MethodInfo info1 = info [i];

				Debug.Log ("->>>>>>>>>>>>>>--> Name >>=>>=>>=  " + info1.Name);

				ParameterInfo[] par = info1.GetParameters ();

				for (int j = 0; j < par.Length; j++) {
					System.Reflection.ParameterInfo par1 = par [j];

					Debug.Log ("->>>>>>>>>>>>>>--> parametre Name >>=>>=>>=  " + par1.Name);
					Debug.Log ("->>>>>>>>>>>>>>--> parametre Type>>=>>=>>=  " + par1.ParameterType);

				}
			}


		

			if (currentGameObject != null) {

				XmlNode[] node = (XmlNode[])currentMsg.unityAttribute;

				Dictionary<object, object> dataDictionary = new Dictionary<object, object> ();

				for (int i = 1; i < node.Length; i++) {
					XmlElement elt = (XmlElement)node.GetValue (i);
					XmlNodeList list = elt.ChildNodes;

					object atr = "";
					object vl = "";

					foreach (XmlElement item in list) {
						

						if (item.Name.Equals ("attribute")) {
							atr = item.InnerText;
							Debug.Log ("======+>  attribute is : " + atr);

						}
						if (item.Name.Equals ("value")) {
							vl = item.InnerText;
							Debug.Log ("======+>  value is : " + vl);
						}


					}
					dataDictionary.Add (atr, vl);
				}

				// Loop over pairs with foreach.
				Debug.Log ("====== =====================    ALL THE VALUES ARE ================");
				foreach (KeyValuePair<object, object> pair in dataDictionary) {
					Debug.Log (pair.Key + "  +++++  " + pair.Value);
				}
			


				Debug.Log ("---->>>>   Good, There is a methode to call! all numbers are: " + dataDictionary.Count);

				//	currentGameObject.SendMessage (currentMsg.getAction (), int.Parse(currentMsg.getAttributeValue ()));
				//	currentGameObject.SendMessage (currentMsg.getAction (), getParameterType(currentMsg.getAttributeValue ()));

				//	currentGameObject.SendMessage ("setReceivedText", "Set received Text TO CHANGE");

				sendMessageToGameObject (currentGameObject, currentMsg.getAction (), dataDictionary);

			} else {
				Debug.Log ("No methode to call. null object!");
			}


		}
		*/

	}



	void client_MqttMsgPublishReceived (object sender, MqttMsgPublishEventArgs e)
	{ 
		receivedMsg = System.Text.Encoding.UTF8.GetString (e.Message);

		switch (e.Topic) {
		case MqttSetting.MAIN_TOPIC:
			Debug.Log ("-> Message to deal with as topic: 1 ----> " + MqttSetting.MAIN_TOPIC);
			GamaMessage currentMsg = msgDes.msgDeserialization (receivedMsg);
			Debug.Log ("-> Message to deal with as topic: 2 ----> " + MqttSetting.MAIN_TOPIC);
			GameObject gameObject = gama.getGameObjectByName (currentMsg.getObjectName ());
			//SpeedTopic dealTopic = new SpeedTopic (currentMsg, (GameObject) gama.getGameObjectByName(currentMsg.getObjectName ()) );
			//SpeedTopic1 dealTopic =  new SpeedTopic1 (gameObject);
			SpeedTopic1 dealTopic = new SpeedTopic1 (currentMsg, (GameObject) gama.getGameObjectByName(currentMsg.getObjectName ()) );
			Debug.Log ("-> Message to deal with as topic: 3 ----> " + MqttSetting.MAIN_TOPIC);
			dealTopic.ProcessToMessage ();
			Debug.Log ("-> Message to deal with as topic: 4 ----> " + MqttSetting.MAIN_TOPIC);
			break;
		case MqttSetting.SPEED_TOPIC:
			Debug.Log ("-> Message to deal with as topic: "+MqttSetting.SPEED_TOPIC);
			break;
		case MqttSetting.POSITION_TOPIC:
			Debug.Log ("-> Message to deal with as topic: "+MqttSetting.POSITION_TOPIC);
			break;
		case MqttSetting.COLOR_TOPIC:
			Debug.Log ("-> Message to deal with as topic: "+MqttSetting.COLOR_TOPIC);
			break;
		default:
			break;
		}

		Debug.Log ("Received: " + receivedMsg);
		Debug.Log (gama.getGamaVersion ());
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
		Debug.Log ("Tres Birn ce test ");


	}


	public void sendGotBoxMsg ()
	{
		GamaReponseMessage msg = new GamaReponseMessage (clientId, "GamaAgent", MqttSetting.NOTIFY_MSG, "Got a Box notification", DateTime.Now.ToString ());

		string message = msgDes.msgSerialization (msg);
		client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes (message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		//client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes ("Good, Another box2"));
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
			Debug.Log ("->>>>>>>>>>>>>>--> parametre Type>>=>>=>>=  " + par1.ParameterType);

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
}
