using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;



public class ColorTopic : MonoBehaviour
{


	protected MsgSerialization msgDes = new MsgSerialization ();
	protected GamaMethods gama = new GamaMethods ();
	protected GamaMessage message;
	protected GameObject gameObject;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}


	public MethodInfo[] getMethodsInfo (BindingFlags flags)
	{
		return gameObject.GetComponent (gameObject.name + MqttSetting.SCRIPT_PRIFIX).GetType ().GetMethods (flags);
	}

	public void ProcessColorTopic ()
	{
		Debug.Log ("->>>>>>>>>>>>>>--> --->>>  this is from color Topic");

		if (gameObject != null) {

			XmlNode[] node = (XmlNode[])message.unityAttribute;
			Dictionary<object, object> dataDictionary = new Dictionary<object, object> ();

			XmlElement elt = (XmlElement)node.GetValue (1);
			XmlNodeList list = elt.ChildNodes;

			object atr = "";
			object vl = "";

			foreach (XmlElement item in list) {
				if (item.Name.Equals ("attribute")) {
					atr = item.InnerText;
				}
				if (item.Name.Equals ("value")) {
					vl = item.InnerText;
				}
			}
			dataDictionary.Add (atr, vl);

			sendColorTopic (gameObject, message.getAction (), dataDictionary);
			Debug.Log ("Method called");

		} 
	}

	// The method to call Game Objects methods
	//----------------------------------------
	public new void sendColorTopic (GameObject gameObject, string methodName, Dictionary<object, object> data)
	{

		int size = data.Count;
		List<object> keyList = new List<object> (data.Keys);
		object obj = data [keyList.ElementAt (0)];
		gameObject.GetComponent<Renderer>().material.color = ToColor ((string) obj);

	}


	public static Color ToColor(string c)
	{

		string color="red";
		Debug.Log ("Color is "+color);
		return (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
	}


	public object convertParameter (object val, ParameterInfo par)
	{
		object propValue = Convert.ChangeType (val, par.ParameterType);
		return propValue;
	}


	public void setAllPropertiesColorTopic (object args)
	{
		object[] obj = (object[])args;
		this.message = (GamaMessage)obj [0];
		this.gameObject = (GameObject)obj [1];
	}


	public GameObject getGameObjectByName (string objectName)
	{
		foreach (GameObject gameO in MqttSetting.allObjects) {
			if (gameO.activeInHierarchy) {
				if (objectName.Equals (gameO.name)) {
					return gameO;
				}
			}					
		}
		return null;
	}


}

