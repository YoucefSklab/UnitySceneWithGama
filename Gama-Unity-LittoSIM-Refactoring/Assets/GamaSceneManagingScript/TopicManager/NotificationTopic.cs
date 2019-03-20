using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using ummisco.gama.unity.SceneManager;
using ummisco.gama.unity.GamaConcepts;
using System.Reflection;
using System.Linq;
using System;
using System.Xml;
using ummisco.gama.unity.notification;
using System.Xml.Linq;

namespace ummisco.gama.unity.topics
{
    public class NotificationTopic : Topic
    {
        public NotificationTopicMessage topicMessage;

        public NotificationTopic(NotificationTopicMessage topicMessage, GameObject gameObj) : base(gameObj)
        {
            this.topicMessage = topicMessage;
        }

        // Use this for initialization
        public override void Start()
        {
            NotificationRegistry.notificationsList = new List<NotificationEntry>();

        }

        // Update is called once per frame
        public override void Update()
        {
            if (NotificationRegistry.notificationsList.Count >= 1)
            {
                foreach (NotificationEntry el in NotificationRegistry.notificationsList)
                {
                    if (!el.notify && !el.isSent)
                    {
                        switch (el.fieldType)
                        {
                            case IGamaConcept.ITEM_FIELD:
                                Debug.Log("Check for field notification");
                                if (isFieldNotification(el))
                                {
                                    Debug.Log("------->>  Yes you have to send  a field notification");
                                    el.notify = true;
                                }
                                else
                                {

                                    //Debug.Log ("------->>  Sorry, No need to send notification");
                                }
                                break;
                            case IGamaConcept.ITEM_PROPERTY:

                                Debug.Log("Check for propety notification with id: " + el.notificationId + " and name : " + el.fieldName);

                                if (isPropertyNotification(el))
                                {
                                    Debug.Log("------->>  Yes you have to send a property notification");
                                    el.notify = true;
                                }
                                else
                                {

                                    //Debug.Log ("------->>  Sorry, No need to send notification");
                                }

                                //Debug.Log ("Property notification");
                                break;
                            default:

                                break;
                        }
                    }
                    else
                    {
                        if (!el.isSent)
                            Debug.Log("Notification " + el.notificationId + " should have been sent!");
                    }
                }
            }
        }

        public void ProcessTopic(object obj)
        {
            setAllProperties(obj);

            NotificationEntry notif = new NotificationEntry(topicMessage.notificationId, topicMessage.objectName, topicMessage.fieldType, topicMessage.fieldName, topicMessage.fieldValue, topicMessage.fieldOperator, topicMessage.sender);
            NotificationRegistry.addToList(notif);

        }

        public Boolean isFieldNotification(NotificationEntry entry)
        {
            GameObject targetGameObject = GameObject.Find(entry.objectName);
            scripts = targetGameObject.GetComponents<MonoBehaviour>();
            // Debug.Log ("-------------->>>> The Operator" + scripts[0].GetType ());
            FieldInfo[] fieldInfoGet = targetGameObject.GetComponent(scripts[0].GetType()).GetType().GetFields();

            foreach (FieldInfo fi in fieldInfoGet)
            {
                if (fi.Name.Equals(entry.fieldName))
                {
                    UnityEngine.Component ob = (UnityEngine.Component)targetGameObject.GetComponent(scripts[0].GetType());
                    object target = fi.GetValue(ob);

                    Debug.Log("-------------->>>> The field name: " + entry.fieldName);
                    //Debug.Log ("-------------->>>> The field type: " + entry.fieldType);
                    //Debug.Log ("-------------->>>> The Operator: " + entry.fieldOperator);
                    //Debug.Log ("-------------->>>> The current field value: " + fi.GetValue (ob));
                    //Debug.Log ("-------------->>>> The field introduced value: " + entry.fieldValue);

                    XmlNode[] node = (XmlNode[])entry.fieldValue;
                    foreach (XmlNode n in node)
                    {
                        if (n is XmlText)
                        {

                            switch (fi.FieldType.ToString())
                            {
                                case IDataType.UNITY_INT:
                                    return Compare<System.Int32>(entry.fieldOperator, (System.Int32)
                                        Convert.ChangeType(fi.GetValue(ob), fi.FieldType),
                                        (System.Int32)Convert.ChangeType(n.Value, fi.FieldType));
                                case IDataType.UNITY_DOUBLE:
                                    return Compare<System.Double>(entry.fieldOperator, (System.Double)
                                        Convert.ChangeType(fi.GetValue(ob), fi.FieldType),
                                        (System.Double)Convert.ChangeType(n.Value, fi.FieldType));
                                case IDataType.UNITY_BOOLEAN:
                                    return Compare<System.Boolean>(entry.fieldOperator, (System.Boolean)
                                        Convert.ChangeType(fi.GetValue(ob), fi.FieldType),
                                        (System.Boolean)Convert.ChangeType(n.Value, fi.FieldType));
                                case IDataType.UNITY_STRING:
                                    return Compare<System.String>(entry.fieldOperator, (System.String)
                                        Convert.ChangeType(fi.GetValue(ob), fi.FieldType),
                                        (System.String)Convert.ChangeType(n.Value, fi.FieldType));
                                case IDataType.UNITY_CHAR:
                                    return Compare<System.Char>(entry.fieldOperator, (System.Char)
                                        Convert.ChangeType(fi.GetValue(ob), fi.FieldType),
                                        (System.Char)Convert.ChangeType(n.Value, fi.FieldType));

                                default:

                                    break;

                            }
                        }
                    }
                }
            }
            return false;
        }

        public Boolean isPropertyNotification(NotificationEntry entry)
        {
            GameObject targetGameObject = GameObject.Find(entry.objectName);
            Component[] cs = (Component[])targetGameObject.GetComponents(typeof(Component));

            Debug.Log("Check in isPropertyNotification for propety notification");
            if (entry.fieldName.Equals(IGamaConcept.ITEM_POSITION)) //TODO: to review this and make it work with  all king of properties 
            {
                XmlNode[] node = (XmlNode[])entry.fieldValue;
                Vector3 vect = ConvertType.vector3FromXmlNode(node, IGamaConcept.GAMA_POINT_CLASS);

                Vector3 propValue = (Vector3)targetGameObject.transform.position;
                Debug.Log("Return - > " + CompareVector3(entry.fieldOperator, vect, propValue) + "v1 = " + vect + " v2 = " + propValue);
                return CompareVector3(entry.fieldOperator, vect, propValue);
            }

            // Do not delete
            /* 
                foreach (Component c in cs) {
                    Debug.Log ("+++ >> name: " + c.name + " type: " + c.GetType () + " basetype: " + c.GetType ().BaseType);
                    PropertyInfo propertyInfo = c.GetType ().GetProperty (entry.fieldName);

                    PropertyInfo[] propertyI = c.GetType ().GetProperties ();

                    foreach (PropertyInfo p in propertyI) {
                        Debug.Log ("+++ Property name is : " + p.Name);

                    }
                    if (propertyInfo != null) {
                        System.Object obj = (System.Object)c;
                        Debug.Log ("Name +++++++++++  >>>> " + propertyInfo.Name);
                        if (propertyInfo.PropertyType.Equals (typeof(Vector3))) {

                            XmlNode[] node = (XmlNode[])entry.fieldValue;

                            Vector3 vect = ConvertType.vector3FromXmlNode (node, MqttSetting.GAMA_POINT);

                            Debug.Log ("Check for -++++++++++++++++>   " + vect);

                            Vector3 propValue = (Vector3)propertyInfo.GetValue (obj, new object[] { 0 });

                            return CompareVector3 (entry.fieldOperator, vect, propValue);
                        } else {

                            //propetyValue = Convert.ChangeType (entry.fieldValue, propertyInfo.PropertyType);

                        }

                    } else {
                        //Debug.Log ("------->>   Sorry. Property doesn't exist : "+property +" and component is "+ c.name);
                    }
                }
            */
            return false;
        }

        public static bool Compare<T>(string op, T x, T y) where T : IComparable
        {
            switch (op)
            {
                case "==":
                    return x.CompareTo(y) == 0;
                case "!=":
                    return x.CompareTo(y) != 0;
                case ">":
                    return x.CompareTo(y) > 0;
                case ">=":
                    return x.CompareTo(y) >= 0;
                case "<":
                    return x.CompareTo(y) < 0;
                case "<=":
                    return x.CompareTo(y) <= 0;
                default:
                    return false;
            }
        }

        public static bool CompareVector3(string op, Vector3 v1, Vector3 v2)
        {
            Debug.Log("Check in CompareVector3 for propety notification");
            switch (op)
            {
                case "==":
                    return v1.x.CompareTo(v2.x) == 0;
                case "!=":
                    return v1.x.CompareTo(v2.x) != 0;
                case ">":
                    return v1.x.CompareTo(v2.x) > 0;
                case ">=":
                    return v1.x.CompareTo(v2.x) >= 0;
                case "<":
                    return v1.x.CompareTo(v2.x) < 0;
                case "<=":
                    return v1.x.CompareTo(v2.x) <= 0;
                default:
                    return false;
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
                        fi.SetValue(ob, (Convert.ChangeType(pair.Value, fi.FieldType)));
                    }
                }
            }
        }

        public override void setAllProperties(object args)
        {
            object[] obj = (object[])args;
            this.topicMessage = (NotificationTopicMessage)obj[0];

        }
    }
}