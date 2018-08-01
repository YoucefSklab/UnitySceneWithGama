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





	// Use this for initialization
	void Start ()
	{
		
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		client.Connect (clientId); 
		client.Subscribe (new string[] { MqttSetting.MAIN_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 
	
		/*
		List<string> targets = new List<string>();
		targets.Add("Inbox1"); targets.Add("Inbox2"); targets.Add("Inbox3"); targets.Add("Inbox4");
		Debug.Log("The list is: " + Tools.listToString(targets, "|"));
		*/
	}


	void FixedUpdate ()
	{

	//	client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 

		// TODO: Review this part. Need to get correctly the received message
		if (receivedMsg != "") {
			string message = receivedMsg;//receivedMsg.Substring (21);

			currentMsg = msgDes.msgDeserialization (message);
			string att = msgDes.getMsgAttribute (message, "unityAction");

			GameObject gameObject = gama.getGameObjectByName (currentMsg.getObjectName ());

			BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
			System.Reflection.MethodInfo[] info = gameObject.GetComponent ("PlayerController").GetType ().GetMethods (flags);
			//System.Reflection.MethodInfo[] info = gameObject.GetComponent ("PlayerController").GetType ().GetMethods ();
		

			//System.Reflection.MethodInfo[] info = gameObject.GetType ().GetMethods ();

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


		

			if (gameObject != null) {

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

				//	gameObject.SendMessage (currentMsg.getAction (), int.Parse(currentMsg.getAttributeValue ()));
				//	gameObject.SendMessage (currentMsg.getAction (), getParameterType(currentMsg.getAttributeValue ()));

				//	gameObject.SendMessage ("setReceivedText", "Set received Text TO CHANGE");

				sendMessageToGameObject (gameObject, currentMsg.getAction (), dataDictionary);

			} else {
				Debug.Log ("No methode to call. null object!");
			}


		}

	}



	void client_MqttMsgPublishReceived (object sender, MqttMsgPublishEventArgs e)
	{ 
		receivedMsg = System.Text.Encoding.UTF8.GetString (e.Message);

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
		GamaReponseMessage msg = new GamaReponseMessage ("sender", "receivers", "contents", "emissionTimeStamp");

		string message = msgDes.msgSerialization (msg);
		client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes (message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		//client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes ("Good, Another box1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
		//client.Publish ("Gama", System.Text.Encoding.UTF8.GetBytes ("Good, Another box2"));
	}


	void Awake ()
	{
		m_Instance = this;

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
		/*
		case 2:
			gameObject.SendMessage ( methodName, 	convertParameter( data[keyList.ElementAt (0)],   par [0] ),
				convertParameter( data[keyList.ElementAt (1)],   par [1] ));
			break;
		case 3:
			gameObject.SendMessage ( methodName, 	convertParameter( data[keyList.ElementAt (0)],   par [0] ),
				convertParameter( data[keyList.ElementAt (1)],   par [1] ),
				convertParameter( data[keyList.ElementAt (2)],   par [2] ));
			break;
		case 4:
			gameObject.SendMessage ( methodName, 	convertParameter( data[keyList.ElementAt (0)],   par [0] ),
				convertParameter( data[keyList.ElementAt (1)],   par [1] ),
				convertParameter( data[keyList.ElementAt (2)],   par [2] ),
				convertParameter( data[keyList.ElementAt (3)],   par [3] ));
			break;
		case 5:
			gameObject.SendMessage ( methodName, 	convertParameter( data[keyList.ElementAt (0)],   par [0] ),
				convertParameter( data[keyList.ElementAt (1)],   par [1] ),
				convertParameter( data[keyList.ElementAt (2)],   par [2] ),
				convertParameter( data[keyList.ElementAt (3)],   par [3] ),
				convertParameter( data[keyList.ElementAt (4)],   par [4] ));
			break;
		case 6:
			gameObject.SendMessage ( methodName, 	convertParameter( data[keyList.ElementAt (0)],   par [0] ),
				convertParameter( data[keyList.ElementAt (1)],   par [1] ),
				convertParameter( data[keyList.ElementAt (2)],   par [2] ),
				convertParameter( data[keyList.ElementAt (3)],   par [3] ),
				convertParameter( data[keyList.ElementAt (4)],   par [4] ),
				convertParameter( data[keyList.ElementAt (5)],   par [5] ));
			break;
		case 7:
			gameObject.SendMessage ( methodName, 	convertParameter( data[keyList.ElementAt (0)],   par [0] ),
				convertParameter( data[keyList.ElementAt (1)],   par [1] ),
				convertParameter( data[keyList.ElementAt (2)],   par [2] ),
				convertParameter( data[keyList.ElementAt (3)],   par [3] ),
				convertParameter( data[keyList.ElementAt (4)],   par [4] ),
				convertParameter( data[keyList.ElementAt (5)],   par [5] ),
				convertParameter( data[keyList.ElementAt (6)],   par [6] ));
			break;
		case 8:
			gameObject.SendMessage ( methodName, 	convertParameter( data[keyList.ElementAt (0)],   par [0] ),
				convertParameter( data[keyList.ElementAt (1)],   par [1] ),
				convertParameter( data[keyList.ElementAt (2)],   par [2] ),
				convertParameter( data[keyList.ElementAt (3)],   par [3] ),
				convertParameter( data[keyList.ElementAt (4)],   par [4] ),
				convertParameter( data[keyList.ElementAt (5)],   par [5] ),
				convertParameter( data[keyList.ElementAt (6)],   par [6] ),
				convertParameter( data[keyList.ElementAt (7)],   par [7] ));
			break;
		case 9:
			gameObject.SendMessage ( methodName, 	convertParameter( data[keyList.ElementAt (0)],   par [0] ),
				convertParameter( data[keyList.ElementAt (1)],   par [1] ),
				convertParameter( data[keyList.ElementAt (2)],   par [2] ),
				convertParameter( data[keyList.ElementAt (3)],   par [3] ),
				convertParameter( data[keyList.ElementAt (4)],   par [4] ),
				convertParameter( data[keyList.ElementAt (5)],   par [5] ),
				convertParameter( data[keyList.ElementAt (6)],   par [6] ),
				convertParameter( data[keyList.ElementAt (7)],   par [7] ),
				convertParameter( data[keyList.ElementAt (8)],   par [8] ));
			break;
	*/
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
