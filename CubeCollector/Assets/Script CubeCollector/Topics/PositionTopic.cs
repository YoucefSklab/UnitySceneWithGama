using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;



public class PositionTopic : MonoBehaviour
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

	public void ProcessPositionTopic (object obj)
	{
		setAllPropertiesPositionTopic (obj);

		if (gameObject != null) {

			XmlNode[] node = (XmlNode[])message.unityAttribute;
			Dictionary<object, object> dataDictionary = new Dictionary<object, object> ();

			for (int i = 1; i < node.Length; i++) {
				XmlElement elt = (XmlElement)node.GetValue (i);
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
			}
			sendPositionTopic (gameObject, message.getAction (), dataDictionary);

		} 
	}

	// The method to call Game Objects methods
	//----------------------------------------
	public new void sendPositionTopic (GameObject gameObject, string methodName, Dictionary<object, object> data)
	{
		int size = data.Count;
		List<object> keyList = new List<object> (data.Keys);
		float x, y, z;

		x = float.Parse ((string)data [keyList.ElementAt (0)], CultureInfo.InvariantCulture.NumberFormat);
		y = float.Parse ((string)data [keyList.ElementAt (1)], CultureInfo.InvariantCulture.NumberFormat);
		z = float.Parse ((string)data [keyList.ElementAt (2)], CultureInfo.InvariantCulture.NumberFormat);
		Debug.Log ("----->>>>    X,Y,Z  " + x + "," + y + "," + z);
//		x = float.Parse(Convert.ToDouble((string)data [keyList.ElementAt (0)]));     
//		y = float.Parse((string)data [keyList.ElementAt (1)]);     
//		z = float.Parse((string)data [keyList.ElementAt (2)]);     

		Transform tr = gameObject.GetComponent<Transform> ();
		Vector3 movement = new Vector3 (x, y, z);
		//tr.position = movement;
		gameObject.transform.position = movement;


		Debug.Log ("position applied ");
	}


	public object convertParameterPosition (object val, ParameterInfo par)
	{
		object propValue = Convert.ChangeType (val, par.ParameterType);
		return propValue;
	}


	public void setAllPropertiesPositionTopic (object args)
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

