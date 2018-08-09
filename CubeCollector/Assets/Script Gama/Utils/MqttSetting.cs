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

		//Object's names'
		public const string GAMA_MANAGER_OBJECT_NAME = "GamaManger";
		public const string SET_TOPIC_MANAGER = "SetTopicManager";
		public const string GET_TOPIC_MANAGER = "GetTopicManager";
		public const string COLOR_TOPIC_MANAGER = "ColorTopicManager";
		public const string POSITION_TOPIC_MANAGER = "PositionTopicManager";
		public const string MONO_FREE_TOPIC_MANAGER = "MonoFreeTopicManager";
		public const string MULTIPLE_FREE_TOPIC_MANAGER = "MultipleFreeTopicManager";
		public const string REPLAY_TOPIC_MANAGER = "ReplayTopicManager";
		public const string NOTIFICATION_TOPIC_MANAGER = "NotificationTopicManager";

		// Topics to receive
		public const string MAIN_TOPIC = "Unity";
		public const string GET_TOPIC = "get";
		public const string SET_TOPIC = "set";

		public const string MONO_FREE_TOPIC = "monoFree";
		public const string MULTIPLE_FREE_TOPIC = "multipleFree";

		public const string COLOR_TOPIC = "color";
		public const string POSITION_TOPIC = "position";
		public const string MOVE_TOPIC = "move";

		public const string DEFAULT_TOPIC = "default";


		// Topics to send
		public const string REPLAY_TOPIC = "replay";
		public const string NOTIFICATION_TOPIC = "notification";



		// Message types
		public const string NOTIFY_MSG = "notify";
		public const string REPLAY_MSG = "replay";
		public const string ASK_MSG = "ask";

		// The prifix to use to get game objects script component
		public const string SCRIPT_PRIFIX = "Controller";

		// Topics' Scripts
		public const string MAIN_TOPIC_SCRIPT = "GamaManager";
		public const string MONO_FREE_TOPIC_SCRIPT = "MonoFreeTopic";
		public const string POSITION_TOPIC_SCRIPT = "PositionTopic";
		public const string COLOR_TOPIC_SCRIPT = "ColorTopic";
		public const string SET_TOPIC_SCRIPT = "SetTopic";
		public const string GET_TOPIC_SCRIPT = "GetTopic";



		public static GameObject[] allObjects = null;

		public static List<string> getTopicsInList(){
			List<string> topicsList = new List<string>();
			topicsList.Add (MAIN_TOPIC);
			topicsList.Add (MONO_FREE_TOPIC);
			topicsList.Add (MULTIPLE_FREE_TOPIC);
			topicsList.Add (POSITION_TOPIC);
			topicsList.Add (COLOR_TOPIC);
			topicsList.Add (REPLAY_TOPIC);
			topicsList.Add (DEFAULT_TOPIC);
			topicsList.Add (SET_TOPIC);
			topicsList.Add (GET_TOPIC);

			return topicsList;
		}
	}


}

