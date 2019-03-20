using System;
using ummisco.gama.unity.messages;
using ummisco.gama.unity.utils;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ummisco.gama.unity.topics
{
    public class Topic : MonoBehaviour
    {

        protected MsgSerialization msgDes = new MsgSerialization();
        protected GamaMethods gama = new GamaMethods();

        protected GameObject targetGameObject { get; set; }

        protected MonoBehaviour[] scripts { get; set; }

        void Awake()
        {

        }

        // Use this for initialization
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public Topic(GameObject gameO)
        {
            this.targetGameObject = targetGameObject;
            this.scripts = targetGameObject.GetComponents<MonoBehaviour>();

        }

        public virtual MethodInfo[] getMethodsInfo(BindingFlags flags)
        {
            setScript();
            return targetGameObject.GetComponent(scripts[0].GetType()).GetType().GetMethods(flags);
        }

        public virtual void setAllProperties(object args)
        {
            object[] obj = (object[])args;
            this.targetGameObject = (GameObject)obj[0];
            this.scripts = targetGameObject.GetComponents<MonoBehaviour>();
        }

        public void setScript()
        {
            this.scripts = targetGameObject.GetComponents<MonoBehaviour>();
        }

        /*
		public virtual void ProcessTopic (object obj){
		
		}
*/

        //public abstract void sendTopic<T> (GameObject targetGameObject, string methodName, Dictionary<object, object> data)
        //	where T : Component;
        /*{
			setAllProperties (obj);

			if (targetGameObject != null) {

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

				sendTopic (targetGameObject, message.getAction (), dataDictionary);
				Debug.Log ("Method called");

			} 
		}

		// The method to call Game Objects methods
		//----------------------------------------
		public virtual void sendTopic (GameObject targetGameObject, string methodName, Dictionary<object, object> data)
		{

			int size = data.Count;
			List<object> keyList = new List<object> (data.Keys);

			MethodInfo methInfo = targetGameObject.GetComponent (scripts[0].GetType ()).GetType ().GetMethod (methodName);
			ParameterInfo[] parameter = methInfo.GetParameters ();
			object obj = data [keyList.ElementAt (0)];
			targetGameObject.SendMessage (methodName, Tools.convertParameter (obj, parameter [0]));
		}
 */
    }
}

