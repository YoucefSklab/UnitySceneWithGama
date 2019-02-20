using UnityEditor;
using UnityEngine;

namespace Nextzen.Unity.Editor
{
    [CustomEditor(typeof(RegionMap))]
    public class RegionMapEditor : UnityEditor.Editor
    {
        private RegionMap map;

        private void OnEnable()
        {
            this.map = (RegionMap)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ApiKey"));
            if (GUILayout.Button("Get an API key", EditorStyles.miniButtonRight))
            {
                Application.OpenURL("https://developers.nextzen.org/");
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Style"), true);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Area"), true);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("UnitsPerMeter"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("RegionName"));

            map.GroupOptions = (SceneGroupType)EditorGUILayout.EnumFlagsField("Grouping Options", map.GroupOptions);

            // EditorGUILayout.PropertyField(serializedObject.FindProperty("GroupOptions"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("GameObjectOptions"), true);

            bool valid = map.IsValid();

            EditorConfig.SetColor(valid ?
                EditorConfig.DownloadButtonEnabledColor :
                EditorConfig.DownloadButtonDisabledColor);

            if (GUILayout.Button("Download"))
            {
                map.LogWarnings();

                if (valid)
                {
                    map.DownloadTilesAsync();
                }
                else
                {
                    map.LogErrors();
                }
            }

            if (map.HasPendingTasks())
            {
                // Go through another OnInspectorGUI cycle
                Repaint();

                if (map.FinishedRunningTasks())
                {
                    map.GenerateSceneGraph();
                }
            }

            EditorConfig.ResetColor();

            serializedObject.ApplyModifiedProperties();
        }
    }
}