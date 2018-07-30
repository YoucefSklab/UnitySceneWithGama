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

	public MainScript mainScript;


	private Rigidbody rb;
	private int  count;


	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
		receivedMqttMessage.text = "";
		mainScript = new MainScript ();

	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
	}


	public void UpdatePosition (float moveHorizontal, float moveVertical)
	{
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
			mainScript.sendGotBoxMsg();
		}

	}


	public void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 5)
		{
			winText.text = "You Win!";
		}
	}

	public void setReceivedText (string text)
	{
		receivedMqttMessage.text = text;

	}

	public void setSpeed(int s){
		this.speed = s;
	}

	public int getSpeed(){
		return this.speed;
	}


	public void changeAllAttributes(object args){
		object[] obj = (object[])args;
		this.speed = (int)obj[0];
		this.countText.text = (string)obj[1];
		this.winText.text = (string)obj[2];
	}





}