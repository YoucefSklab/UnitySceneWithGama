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
using UnityEngine.Events;
using UnityEngine.EventSystems;


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


	// $$$$$$$$$$$$$$
	public GameObject testObject;
	public Shader shader;
	public Texture texture;
	public Color color;

	UnityEvent m_MyEvent;

	System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);

	protected void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
		receivedMqttMessage.text = "";
		//MqttSetting.allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
		//gamaManager = gama.getGameObjectByName ("GamaManager");



		//-------------------------------

		st = new System.Diagnostics.StackTrace(true);
		if (m_MyEvent == null)
			m_MyEvent = new UnityEvent();

		m_MyEvent.AddListener(notify);
		//m_MyEvent.AddListener(notify2);


		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener((data) => { notify(); });
		//entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
		Debug.Log("---->>> "+entry.eventID.ToString ());

		Debug.Log("---->>> Element size: "+trigger.triggers.Count);
		trigger.triggers.Add(entry);

		Debug.Log("---->>> Element size: "+trigger.triggers.Count);

		//--------------------------------

	}

	public void OnPointerDownDelegate(PointerEventData data)
	{
		Debug.Log("OnPointerDownDelegate called.");
	}
	//----------------------------------------------------------------
	/// <summary>
	/// Ceci est un test de notification. On verra ce que ça va donner!
	/// </summary>
	void Update()
	{
		//notify ();
		if (Input.anyKeyDown && m_MyEvent != null)
		{
			m_MyEvent.Invoke();

		}

	}

	void notify2()
	{
		Debug.Log("-> ---> Send notification really");
	}

	void notify()
	{
		Debug.Log("-> Send notification");
		notify2 ();
	}
	//-----------------------------------------------------------------



	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);


		//--------------------------------

		// Create a StackTrace that captures
		// filename, line number, and column
		// information for the current thread.

		for(int i =0; i< st.FrameCount; i++ )
		{
			// Note that high up the call stack, there is only
			// one stack frame.
			System.Diagnostics.StackFrame sf = st.GetFrame(i);
			Debug.Log("-> "+st.GetFrames ().Length);
			Debug.Log("High up the call stack, Method: "+ sf.GetMethod());
			Debug.Log("High up the call stack, Line Number: " + sf.GetFileLineNumber());

		}

		//--------------------------------
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

		testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		testObject.name = "TestGameObject";
		//Add Components
		testObject.AddComponent<Rigidbody>();
		testObject.AddComponent<BoxCollider>();
		color = Tools.stringToColor ("black");
		Renderer rend = testObject.GetComponent<Renderer>();
		rend.material.color = color;
	
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