using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using ummisco.gama.unity;

using System;
using System.Text;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text receivedMqttMessage;


	private Rigidbody rb;
	private int  count;
	private string receivedMsg;

	private MqttClient client;
	private GamaMethods gama; 
	private Tools tools;
	private MsgSerialization msgDes;
	private GamaMsg currentMsg;



	void Start ()
	{
		tools = new Tools ();
		msgDes = new MsgSerialization ();
		rb = GetComponent<Rigidbody>();
		count = 0;
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
		Debug.Log("The list is: " + tools.listToString(targets, "|"));

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
			string mes = "<ummisco.gama.network.common.CompositeGamaMessage>\n  <unread>true</unread>\n  <sender class=\"string\">Gama</sender>\n  <receivers class=\"string\">Unity</receivers>\n  <contents class=\"string\">&lt;string&gt; This message is sent from Gama to Unity &lt;/string&gt;</contents>\n  <emissionTimeStamp>633</emissionTimeStamp>\n</ummisco.gama.network.common.CompositeGamaMessage>";
			int nbr = receivedMsg.Length - mes.Length -1;
			string message = receivedMsg.Substring (nbr);

			/*
			Debug.Log ("Longueur is: " + nbr);
			Debug.Log ("Intial Message is: " + receivedMsg);
			Debug.Log ("New Message is: " + message);
			Debug.Log ("Needed transformation is: " + message);

			Debug.Log ("lenth of receivedMsg is: " + receivedMsg.Length);
			Debug.Log ("lenth of message is: " + message.Length);
			Debug.Log ("lenth of messageNew is: " + message.Length);
			*/

			currentMsg = msgDes.msgDeserialization (message);
			Debug.Log ("The Message unread is: " + currentMsg.unread);
			Debug.Log ("The Message sender is: " + currentMsg.sender);
			Debug.Log ("The Message receivers is: " + currentMsg.receivers);
			Debug.Log ("The Message content is: " + currentMsg.contents);
			Debug.Log ("The Message emissionTimeStamp is: " + currentMsg.emissionTimeStamp);
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







	void updateReceivedMsgOnUnity(string msg){
		receivedMqttMessage.text = msg;
	}

	void sendGotBoxMsg(){
		client.Publish("Gama", System.Text.Encoding.UTF8.GetBytes("Great! I have got a box! Total Boxes is: "+count), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
	}


	void setSpeed(float s){
		this.speed = s;
	}

	public float getSpeed(){
		return this.speed;
	}

}