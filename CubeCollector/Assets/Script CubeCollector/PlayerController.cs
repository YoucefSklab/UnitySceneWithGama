using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;

using System;
using System.Text;
using System.IO;
using System.Reflection;
using NUnit.Framework.Internal;

public class PlayerController : MonoBehaviour {

	public int speed;
	public Text countText;
	public Text winText;
	public Text receivedMqttMessage;


	private Rigidbody rb;
	private int  count;
	private string receivedMsg;

	private MqttClient client;
	private GamaMethods gama; 
	private MsgSerialization msgDes;
	private GamaMessage currentMsg;



	void Start ()
	{


		msgDes = new MsgSerialization ();
		count = 0;
		rb = GetComponent<Rigidbody>();
		receivedMsg = "";
		SetCountText ();
		winText.text = "";
		receivedMqttMessage.text = "";

		gama = new GamaMethods ();
		// MQTT client Initialization 
		// --------------------------

		// create client instance 
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
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);

		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

		updateReceivedMsgOnUnity (receivedMsg);

		// TODO: Review this part. Need to get correctly the received message
		if (receivedMsg != "") {
			string message =  receivedMsg;//receivedMsg.Substring (21);

			currentMsg = msgDes.msgDeserialization (message);
			string att = msgDes.getMsgAttribute (message, "unityAction");
			Debug.Log ("Got the attribute unityAction " + att);


			GameObject gameObject = gama.getGameObjectByName (currentMsg.getObjectName ());

			if(gameObject != null){
				Debug.Log ("try to call ");
				gameObject.SendMessage (currentMsg.getAction (), int.Parse(currentMsg.getAttributeValue ()));
				Debug.Log ("Methode callded with success ");

			}else{
				Debug.Log ("Not called ");

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

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
			sendGotBoxMsg();

		}

	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();//+" - "+gama.getGamaVersion();
		if (count >= 5)
		{
			winText.text = "You Win!";
		}
	}



	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		receivedMsg = System.Text.Encoding.UTF8.GetString (e.Message);

		//receivedMsg = Tools.convertMessage (receivedMsg);

		Debug.Log("Received: " +  receivedMsg );


		Debug.Log ("Deserialisation is Done");
		Debug.Log("Good... Done!");
		Debug.Log (gama.getGamaVersion ());
	}

	void OnGUI(){
		if ( GUI.Button (new Rect (20,40,180,20), "Send Mqtt message")) {
			Debug.Log("sending... SKLAB");

			client.Publish("Gama", System.Text.Encoding.UTF8.GetBytes("Sending from Unity3D!!! Good"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

			Debug.Log("Message sent!");
			Debug.Log (gama.getGamaVersion ());
			gama.getAllSceneGameObject();
		}
	}




	public void handleMessage(GamaMessage msg){
		
	}



	void updateReceivedMsgOnUnity(string msg){
		receivedMqttMessage.text = msg;
	}

	void sendGotBoxMsg(){
		GamaReponseMessage msg = new GamaReponseMessage ("sender", "receivers", "contents", "emissionTimeStamp");
		string message = msgDes.msgSerialization (msg);
		//client.Publish("Gama", System.Text.Encoding.UTF8.GetBytes("Great! I have got a box! Total Boxes is: "+count), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		client.Publish("Gama", System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
	}




	void setSpeed(int s){
		this.speed = s;
	}

	public float getSpeed(){
		return this.speed;
	}

}