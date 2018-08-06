using System;
using UnityEngine;

namespace ummisco.gama.unity.utils
{
	public class MqttSetting
	{
		// Server parameters
		public static string SERVER_URL = "localhost";
		public static int SERVER_PORT = 1883;

		// Topics
		public const string MAIN_TOPIC = "Unity";
		public const string MONO_FREE_TOPIC = "monoFree";
		public const string POSITION_TOPIC = "position";
		public const string COLOR_TOPIC = "color";
		public const string REPLAY_TOPIC = "replay";
		public const string DEFAULT_TOPIC = "default";


		// Message types
		public const string NOTIFY_MSG = "notify";
		public const string REPLAY_MSG = "replay";
		public const string ASK_MSG = "ask";

		// The prifix to use to get game objects script component
		public const string SCRIPT_PRIFIX = "Controller";

		// Topics' Scripts
		public const string MAIN_TOPIC_SCRIPT = "MainScript";
		public const string MONO_FREE_TOPIC_SCRIPT = "MonoFreeTopic";
		public const string POSITION_TOPIC_SCRIPT = "PositionTopic";
		public const string COLOR_TOPIC_SCRIPT = "ColorTopic";


		public static GameObject[] allObjects = null;
	}


}

