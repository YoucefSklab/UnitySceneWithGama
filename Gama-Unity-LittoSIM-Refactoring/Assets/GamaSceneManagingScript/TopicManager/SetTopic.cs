using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;
using UnityEngine.UI;
using ummisco.gama.unity.GamaConcepts;

namespace ummisco.gama.unity.topics
{
    public class SetTopic : Topic
    {
        public SetTopicMessage topicMessage;
        public object setObject;

        public SetTopic(SetTopicMessage topicMessage, GameObject gameObj) : base(gameObj)
        {
            this.topicMessage = topicMessage;
        }

        // Use this for initialization
        public override void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {

        }

        public void ProcessTopic(object obj)
        {
            setAllProperties(obj);

            if (targetGameObject != null)
            {

                XmlNode[] node = (XmlNode[])topicMessage.attributes;
                Dictionary<object, object> dataDictionary = new Dictionary<object, object>();

                XmlElement elt = (XmlElement)node.GetValue(1);
                XmlNodeList list = elt.ChildNodes;

                object atr = "";
                object vl = "";

                foreach (XmlElement item in list)
                {
                    if (item.Name.Equals(IGamaConcept.ITEM_ATTRIBUTE))
                    {
                        atr = item.InnerText;
                    }
                    if (item.Name.Equals(IGamaConcept.ITEM_VALUE))
                    {
                        vl = item.InnerText;
                    }
                }
                dataDictionary.Add(atr, vl);

                sendTopic(targetGameObject, dataDictionary);
            }
        }

        // The method to call Game Objects methods
        //----------------------------------------
        public void sendTopic(GameObject targetGameObject, Dictionary<object, object> data)
        {

            int size = data.Count;

            List<object> keyList = new List<object>(data.Keys);
            object obj = data[keyList.ElementAt(0)];

            FieldInfo[] fieldInfoSet = targetGameObject.GetComponent(scripts[0].GetType()).GetType().GetFields();

            foreach (KeyValuePair<object, object> pair in data)
            {
                foreach (FieldInfo fi in fieldInfoSet)
                {
                    if (fi.Name.Equals(pair.Key.ToString()))
                    {
                        UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent(scripts[0].GetType());

                        if (fi.FieldType.Equals(typeof(UnityEngine.UI.Text)))
                        {

                            Component[] cs = (Component[])targetGameObject.GetComponents(typeof(Component));
                            foreach (Component c in cs)
                            {
                                if (c.name.Equals(pair.Key.ToString()))
                                {
                                    Text txt = targetGameObject.GetComponent<Text>();

                                    txt.text = "Score : ";
                                }
                            }

                            //	Debug.Log ("try to get the fields lis  " + fi.GetType ().GetField ("text").ToString ());
                            FieldInfo[] fieldInfoSet2 = fi.FieldType.GetFields();
                            Debug.Log("Its fieldInfoSet2 is 1 ----> : " + fieldInfoSet2.ToList().ToString());
                            foreach (FieldInfo fi2 in fieldInfoSet2)
                            {
                                Debug.Log("Its Name is 1 ----> : " + fi2.Name);
                                //Debug.Log ("Its Value is 1 ----> : " + fi2.GetValue ());
                            }

                            //fi.FieldType.GetFields ();

                            //fi.SetValue (ob, setObject);

                        }
                        else
                        {
                            //TODO: need to complete this list
                            Debug.Log("Its Name is ----> : " + fi.Name + " and type is :" + fi.FieldType.ToString());
                            switch (fi.FieldType.ToString())
                            {

                                case IDataType.UNITY_INT:
                                    Debug.Log("Its type is ----> :" + fi.FieldType);
                                    break;
                                case IDataType.UNITY_DOUBLE:
                                    Debug.Log("Its type is ----> :" + fi.FieldType);
                                    fi.SetValue(ob, (System.Double)pair.Value);
                                    break;
                                case IDataType.UNITY_SINGLE:
                                    Debug.Log("Its type is ----> :" + fi.FieldType);
                                    fi.SetValue(ob, Convert.ToSingle(pair.Value));
                                    break;
                                case IDataType.UNITY_BOOLEAN:
                                    Debug.Log("Its type is ----> :" + fi.FieldType);
                                    fi.SetValue(ob, Convert.ChangeType(pair.Value, fi.FieldType));
                                    break;
                                case IDataType.UNITY_STRING:
                                    Debug.Log("Its type is ----> :" + fi.FieldType);
                                    fi.SetValue(ob, (System.String)pair.Value);
                                    break;
                                case IDataType.UNITY_CHAR:
                                    Debug.Log("Its type is ----> :" + fi.FieldType);
                                    fi.SetValue(ob, (System.Char)pair.Value);
                                    break;

                                default:

                                    break;

                            }
                            //fi.SetValue (ob, (Convert.ChangeType (pair.Value, fi.FieldType)));
                        }
                        //	fi.SetValue (ob, (Convert.ChangeType (pair.Value, fi.FieldType)));
                    }
                }
            }
        }

        public override void setAllProperties(object args)
        {
            object[] obj = (object[])args;
            this.topicMessage = (SetTopicMessage)obj[0];
            this.targetGameObject = (GameObject)obj[1];
            this.scripts = targetGameObject.GetComponents<MonoBehaviour>();
        }
    }
}