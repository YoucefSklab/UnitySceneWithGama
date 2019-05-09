using System;
using UnityEngine;
using System.Collections.Generic;

namespace ummisco.gama.unity.utils
{
	public class MqttSetting
	{
		// Server parameters
	    public static string SERVER_URL = "localhost";
		public static int SERVER_PORT = 1883;

		//public static string SERVER_URL = "195.221.248.15";
		//public static int SERVER_PORT = 1935;
		public static string DEFAULT_USER = "gama_demo";
		public static string DEFAULT_PASSWORD = "gama_demo";


		//public static string SERVER_URL = "iot.eclipse.org";
		//public static int SERVER_PORT = 1935;

		//Object's names'
		public const string GAMA_MANAGER_OBJECT_NAME = "GamaManager";
		public const string SET_TOPIC_MANAGER = "SetTopicManager";
		public const string GET_TOPIC_MANAGER = "GetTopicManager";
		public const string COLOR_TOPIC_MANAGER = "ColorTopicManager";
		public const string POSITION_TOPIC_MANAGER = "PositionTopicManager";
		public const string MONO_FREE_TOPIC_MANAGER = "MonoFreeTopicManager";
		public const string MULTIPLE_FREE_TOPIC_MANAGER = "MultipleFreeTopicManager";
		public const string REPLAY_TOPIC_MANAGER = "ReplayTopicManager";
		public const string NOTIFICATION_TOPIC_MANAGER = "NotificationTopicManager";
		public const string MOVE_TOPIC_MANAGER = "MoveTopicManager";
		public const string PROPERTY_TOPIC_MANAGER = "PropertyTopicManager";
		// Create Destroy game Objects
		public const string CREATE_TOPIC_MANAGER = "CreateTopicManager";
		public const string DESTROY_TOPIC_MANAGER = "DestroyTopicManager";
		public const string MAIN_TOPIC_MANAGER = "MainTopicManager";




		// Topics to receive
		public const string MAIN_TOPIC = "Unity";
		public const string GET_TOPIC = "get";
		public const string SET_TOPIC = "set";
		public const string PROPERTY_TOPIC = "property";

		public const string MONO_FREE_TOPIC = "monoFree";
		public const string MULTIPLE_FREE_TOPIC = "multipleFree";

		public const string COLOR_TOPIC = "color";
		public const string POSITION_TOPIC = "position";
		public const string MOVE_TOPIC = "move";


		// Topics to Create/ Detroy GameObjects
		public const string CREATE_TOPIC = "create";
		public const string DESTROY_TOPIC = "destroy";

		public const string DEFAULT_TOPIC = "default";





		// Topics to send
		public const string REPLAY_TOPIC = "replay";
		public const string NOTIFICATION_TOPIC = "subscribeToNotification";



		// Message types
		public const string NOTIFY_MSG = "notification";
		public const string REPLAY_MSG = "replay";
		public const string ASK_MSG = "ask";

		// The prifix to use to get game objects script component
		public const string SCRIPT_PRIFIX = "Controller";

		// Topics' Scripts
		public const string GAMA_MANAGER_SCRIPT = "GamaManager";
		public const string MONO_FREE_TOPIC_SCRIPT = "MonoFreeTopic";
		public const string MULTIPLE_FREE_TOPIC_SCRIPT = "MultipleFreeTopic";
		public const string POSITION_TOPIC_SCRIPT = "PositionTopic";
		public const string COLOR_TOPIC_SCRIPT = "ColorTopic";
		public const string SET_TOPIC_SCRIPT = "SetTopic";
		public const string GET_TOPIC_SCRIPT = "GetTopic";
		public const string MOVE_TOPIC_SCRIPT = "MoveTopic";
		public const string PROPERTY_TOPIC_SCRIPT = "PropertyTopic";
		public const string NOTIFICATION_TOPIC_SCRIPT = "NotificationTopic";
		public const string CREATE_TOPIC_SCRIPT = "CreateTopic";
		public const string DESTROY_TOPIC_SCRIPT = "DestroyTopic";
		public const string MAIN_TOPIC_SCRIPT = "MainTopic";

		public static GameObject[] allObjects = null;


		// Gama Data Types:

		public const string GAMA_POINT = "msi.gama.metamodel.shape.GamaPoint";
		public const string GAMA_RGB_COLOR = "ummisco.gama.unity.data.type.rgbColor";





		public static List<string> getTopicsInList ()
		{
			List<string> topicsList = new List<string> ();
			topicsList.Add (MAIN_TOPIC);
			topicsList.Add (MONO_FREE_TOPIC);
			topicsList.Add (MULTIPLE_FREE_TOPIC);
			topicsList.Add (POSITION_TOPIC);
			topicsList.Add (COLOR_TOPIC);
			topicsList.Add (REPLAY_TOPIC);
			topicsList.Add (DEFAULT_TOPIC);
			topicsList.Add (SET_TOPIC);
			topicsList.Add (GET_TOPIC);
			topicsList.Add (MOVE_TOPIC);
			topicsList.Add (PROPERTY_TOPIC);
			topicsList.Add (NOTIFICATION_TOPIC);
			topicsList.Add (CREATE_TOPIC);
			topicsList.Add (DESTROY_TOPIC);

			return topicsList;
		}
	}


}

