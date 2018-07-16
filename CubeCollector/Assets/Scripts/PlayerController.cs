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

		displayReceivedMsg (receivedMsg);




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
		Debug.Log("Good... Done!");
		Debug.Log (gama.getGamaVersion ());

		Debug.Log ("Type is: "+e.GetType ());
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







	void displayReceivedMsg(string msg){
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