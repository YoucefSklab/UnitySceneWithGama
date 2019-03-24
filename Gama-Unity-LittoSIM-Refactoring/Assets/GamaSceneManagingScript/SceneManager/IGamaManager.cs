using System;
using UnityEngine;
using System.Collections.Generic;

namespace ummisco.gama.unity.SceneManager
{
    public class IGamaManager
    {
        //Object's names'
        public const string GAMA_MANAGER= "GamaManager";
        public const string GAMA_MAIN_CAMERA = "MainCamera";
        public const string CSV_READER = "CSVReader";

        public const string RESOURCES_PATH = "Assets/Resources/";



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


        public const string CSVREADER = "CSVReader";

        public IGamaManager()
        {

        }

    }
}
