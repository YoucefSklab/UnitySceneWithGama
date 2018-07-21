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


public class MainScript : MonoBehaviour {

	private MainScript m_Instance;
	public MainScript Instance { get { return m_Instance; } }

	private string receivedMsg;

	private MqttClient client;
	private GamaMethods gama; 
	private MsgSerialization msgDes;
	private GamaMessage currentMsg;

	// Use this for initialization
	void Start () {
		gama = new GamaMethods ();
		msgDes = new MsgSerialization ();
		receivedMsg = "";

		//client = new MqttClient(IPAddress.Parse("143.185.118.233"),8080 , false , null ); 
		client = new MqttClient("localhost",1883 , false , null );

		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

		string clientId = Guid.NewGuid().ToString(); 
		client.Connect(clientId); 

		client.Subscribe(new string[] { "Unity" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 

		List<string> targets = new List<string>();
		targets.Add("Inbox1"); targets.Add("Inbox2"); targets.Add("Inbox3"); targets.Add("Inbox4");
		Debug.Log("The list is: " + Tools.listToString(targets, "|"));
	}


	void FixedUpdate ()
	{

		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

		// This is done in the if block below
		// updateReceivedMsgOnUnity (receivedMsg);

		// TODO: Review this part. Need to get correctly the received message
		if (receivedMsg != "") {
			string message =  receivedMsg;//receivedMsg.Substring (21);

			currentMsg = msgDes.msgDeserialization (message);
			string att = msgDes.getMsgAttribute (message, "unityAction");
			Debug.Log ("Got the attribute unityAction " + att);

			GameObject gameObject = gama.getGameObjectByName (currentMsg.getObjectName ());

			if(gameObject != null){
				gameObject.SendMessage (currentMsg.getAction (), int.Parse(currentMsg.getAttributeValue ()));
				gameObject.SendMessage ("setReceivedText", "Set received Text TO CHANGE");

			}else{
				Debug.Log ("No methode to call. null object!");
			}

			/*
			Dictionary<string, object> test = Tools.DictionaryFromType (currentMsg);
			Debug.Log ("All elements in dic : " +test.ToString ());
			foreach (var pair in test)
			{
				Debug.Log ("Key : -> " + pair.Key+ " Its Value -> "+ pair.Value);
			}
		*/
		}

	}



	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		receivedMsg = System.Text.Encoding.UTF8.GetString (e.Message);

		Debug.Log("Received: " +  receivedMsg );
		Debug.Log (gama.getGamaVersion ());
	}



	void OnGUI(){
		if ( GUI.Button (new Rect (20,40,180,20), "Send Mqtt message")) {
			client.Publish("Gama", System.Text.Encoding.UTF8.GetBytes("Sending from Unity3D!!! Good"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("Message sent!");

			gama.getAllSceneGameObject();
		}
	}




	public static void sendGotBoxMsg(){
		GamaReponseMessage msg = new GamaReponseMessage ("sender", "receivers", "contents", "emissionTimeStamp");
		string message = msgDes.msgSerialization (msg);
		client.Publish("Gama", System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
	}


	void Awake()
	{
		m_Instance = this;
	}

	void OnDestroy()
	{
		m_Instance = null;
	}


	void OnGui()
	{
		// common GUI code goes here
	}


	// Update is called once per frame
	void Update () {
		
	}

	public void handleMessage(GamaMessage msg){

	}
}
