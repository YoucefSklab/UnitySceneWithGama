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
			//	gameObject.SendMessage (currentMsg.getAction (), int.Parse(currentMsg.getAttributeValue ()));
			//	gameObject.SendMessage ("setReceivedText", "Set received Text TO CHANGE");
			//	Debug.Log ("---> " + currentMsg.getObjectAttribute());


				//List<ItemAttributes> list = currentMsg.getObjectAttribute();
				//	ItemAttributes list = msgDes.msgDeserialization (currentMsg.getObjectAttribute());

				//Debug.Log ("---> elements are: "+list.Count);
				Debug.Log ("---> type is : "+currentMsg.unityAttribute.GetType ());
				Debug.Log ("---> Value is : "+currentMsg.unityAttribute);
				Debug.Log ("---> Value is : "+currentMsg.unityAttribute);

				XmlNode[] node  = (XmlNode[]) currentMsg.unityAttribute;

				Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

				for (int i = 1; i < node.Length; i++)
				{
					XmlElement elt = (XmlElement) node.GetValue (i);
					XmlNodeList list = elt.ChildNodes;

					string atr ="";
					string vl = "";

					foreach (XmlElement item in list)
					{
						

						if (item.Name.Equals ("attribute")) {
							atr = item.InnerText;
							Debug.Log ("======+>  attribute is : "+atr);

						}
						if (item.Name.Equals ("value")) {
							vl = item.InnerText;
							Debug.Log ("======+>  value is : "+vl);
						}


					}
					dataDictionary.Add(atr, vl);
				}

				// Loop over pairs with foreach.
				Debug.Log ("====== =====================    ALL THE VALUES ARE ================");
				foreach (KeyValuePair<string, string> pair in dataDictionary)
				{
					Debug.Log (pair.Key + "  +++++  "+ pair.Value);
				}
			


				/*


				Debug.Log ("---> node Value is : "+node.GetValue(1));

				XmlElement elt = (XmlElement) node.GetValue (1);
				Debug.Log ("---> xml element Value is : "+elt.OuterXml);
				string test = elt.GetAttribute ("attributeU");

				Debug.Log (">>>>>>>>>>>>>>>>>>> Its lenth is:  "+test.Length);
				Debug.Log (">>>>>>>>>>>>>>>>>>> xml element Attribute is : "+elt.GetAttribute ("class"));
				Debug.Log (">>>>>>>>>>>>>>>>>>> xml element Value is : "+elt.GetAttribute ("valueU"));
				Debug.Log ("---> elt.InnerText : "+elt.InnerText);



				elt = (XmlElement) node.GetValue (2);
				Debug.Log ("---> xml element Value is : "+elt.OuterXml);
				Debug.Log ("---> elt.InnerText : "+elt.InnerText);
				object obj = (object)elt.GetAttribute ("valueU");
				Debug.Log (">>>>>>>>>>>>>>>>>>> xml element Attribute is : "+elt.GetAttribute ("attributeU"));
				Debug.Log (">>>>>>>>>>>>>>>>>>> xml element Value is : "+elt.GetAttribute ("valueU"));
				Debug.Log ("=========>>>> xml element Value is : "+obj.ToString());
			
				XmlNodeList list = elt.ChildNodes;
				foreach (XmlElement item in list)
				{
					Debug.Log ("========P=>>>> xml element Value is : "+item.InnerText);
					Debug.Log ("========P=>>>> item.Name : "+item.Name);

				}
			*/

			
				/*
				MemoryStream stm = new MemoryStream();
				StreamWriter stw = new StreamWriter(stm);
				stw.Write(node.OuterXml);
				stw.Flush();
				stm.Position = 0;

				//XmlSerializer ser = new XmlSerializer(typeof(T));
				//T result = (ser.Deserialize(stm) as T);
				*/





			/*	string data = node.ToString ();

			
				Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

				foreach (XElement element in doc.Descendants().Where(p => p.HasElements == false)) {
					int keyInt = 0;
					string keyName = element.Name.LocalName;

					while (dataDictionary.ContainsKey(keyName)) {
						keyName = element.Name.LocalName + "_" + keyInt++;
					}

					dataDictionary.Add(keyName, element.Value);
				}



				// Loop over pairs with foreach.
				foreach (KeyValuePair<string, string> pair in dataDictionary)
				{
					Debug.Log (pair.Key + " ----  "+ pair.Value);
				}


				*/
			

				//Debug.Log ("---> Size is : "+currentMsg.unityAttribute.Count);
				Debug.Log ("---> Action is : "+currentMsg.getAction ());
			//	Debug.Log ("---> elements in list attribute : "+list.attribute);
				/*
				for (int i=0; i<list.Count;i++)
				{
					ItemAttributes item = list[i];
					Debug.Log ("\t---> Attribute" + item.attribute);
					Debug.Log ("\t---> Value" + item.value);

				}
				*/

				Debug.Log ("Good, There is a methode to call!");
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




	public void sendGotBoxMsg(){
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
