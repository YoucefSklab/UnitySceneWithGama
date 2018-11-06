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
//using System.Text;
using System.IO;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using ummisco.gama.unity.notification;


public class PlayerController : MonoBehaviour
{

	public int speed;
	public Text countText;
	public Text winText;
	public Text receivedMqttMessage;
	public GameObject gamaManager;
	public GamaMethods gama = new GamaMethods ();
	private Rigidbody rb;
	public int count;

	public Shader shader;
	public Texture texture;
	public Color color;

	UnityEvent m_MyEvent;


	protected void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
		receivedMqttMessage.text = "";
		//MqttSetting.allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
		//gamaManager = gama.getGameObjectByName ("GamaManager");
		Console.WriteLine ("test");



	//	Debug.DrawLine(new Vector3(-3,0,-3), new Vector3(3,0,3), Color.green, 20, false);









		/*

		FieldInfo[] fieldInfoSet = gameObject.GetComponent<PlayerController> ().GetType ().GetFields ();

	
			foreach (FieldInfo fi in fieldInfoSet) {
			
			Debug.Log ("Name is  ----> :"+fi.Name);
			Debug.Log ("Its type is ----> :"+fi.FieldType);

			if (fi.FieldType.Equals (typeof(UnityEngine.UI.Text))) {
				Debug.Log (" ------------------------ ");
			}

				if (fi.Name.Equals (pair.Key.ToString ())) {

					Debug.Log ("Its type is ----> :"+fi.FieldType);

					UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent (scripts[0].GetType ());
					fi.SetValue (ob, (Convert.ChangeType (pair.Value, fi.FieldType)));
				}

			}

		*/


		/*

		Component[] cs = (Component[])gameObject.GetComponents (typeof(Component));
		foreach (Component c in cs) 
		{
			PropertyInfo[] propertyInfo = c.GetType().GetProperties();
			foreach (PropertyInfo p in propertyInfo) {
				Debug.Log("Property Name is : "+p.Name);
			}
		}
		*/



		/*

		string property = "localScale";

		string test = "(1.0,1.0,1.0)";


		Vector3 vect = parseVector3 (test);

		Debug.Log ("------->>   its type is : " + vect);

		Component[] cs = (Component[])gameObject.GetComponents (typeof(Component));
		foreach (Component c in cs) {
			//Debug.Log("name: "+c.name+" type: "+c.GetType() +" basetype: "+c.GetType().BaseType);

			PropertyInfo propertyInfo = c.GetType().GetProperty(property);
			if (propertyInfo != null) {
				
				Debug.Log ("------->>   Good. Property exist. Its name is : " + propertyInfo.Name + " and its value is: " + propertyInfo.GetValue ((System.Object)c, null));

				Debug.Log ("------->>   its type is : " + propertyInfo.PropertyType);





				object val = vect; //Convert.ChangeType (value, propertyInfo.PropertyType);
				System.Object obj = (System.Object)c;
				propertyInfo.SetValue(
					obj, 
					val, 
					null);
				
			} else {
				//Debug.Log ("------->>   Sorry. Property doesn't exist : "+property +" and component is "+ c.name);
			}

		



		}

		*/

	
		EventTrigger myEventTrigger = GetComponent<EventTrigger> (); //you need to have an EventTrigger component attached this gameObject
		myEventTrigger.AddListener (EventTriggerType.Move, OnMoveEvent);

	}


	public void OnMoveEvent(AxisEventData data)
	{
		Debug.Log("OnMove called.");
	}

	public void OnDragEventExample()
	{
		Debug.Log("OnMove called.");
	}

	void onClickListener (PointerEventData eventData)
	{
		Debug.Log("onClickListener called.");
	}

	public Vector3 parseVector3(string sourceString) {

		string outString;
		Vector3 outVector3;
		string[] splitString;//= new Array();

		// Trim extranious parenthesis

		outString = sourceString.Substring(1, sourceString.Length - 2);

		// Split delimted values into an array

		splitString = outString.Split("," [0]);

		// Build new Vector3 from array elements

		outVector3.x = float.Parse(splitString[0]);
		outVector3.y = float.Parse(splitString[1]);
		outVector3.z = float.Parse(splitString[2]);

		return outVector3;

	}

	void Update()
	{
		

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
		//rb.AddForce (movement * speed);
		rb.MovePosition (movement);
	}



	void OnTriggerEnter (Collider other)
	{
		MqttSetting.allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();

		gamaManager = gama.getGameObjectByName (MqttSetting.GAMA_MANAGER_OBJECT_NAME);

		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
			Debug.Log ("The game Object name is: " + gamaManager.name);

			gamaManager.SendMessage ("sendGotBoxMsg");
		}

	}


	public void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 5) {
			winText.text = "You Win!";
		}
	}

	public void setReceivedText (string text)
	{
		receivedMqttMessage.text = text;
	}


	public void setWinText (string text)
	{
		winText.text = text;
	}

	public void setSpeed (int s)
	{
		this.speed = s;
	}

	public int getSpeed ()
	{
		return this.speed;
	}

	public void changeAllAttributes (object args)
	{
		object[] obj = (object[])args;
		this.speed = Int32.Parse ((string)obj [0]);
		this.countText.text = (string)obj [1];
		this.winText.text = (string)obj [2];
 	}



}