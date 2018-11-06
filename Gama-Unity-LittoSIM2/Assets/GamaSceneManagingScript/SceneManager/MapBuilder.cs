using Nextzen;

using UnityEditor;
using UnityEngine;

// For Android Build
public class MapBuilder 
{

}
/* 

[CustomEditor(typeof(RegionMap))]
public class MapBuilder : UnityEditor.Editor
{

    public RegionMap map;


    public MapBuilder()
    {
        
    }

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
         Debug.Log("This is from MapBuilder");   
        this.map = (RegionMap)target;
    }

    public void buildMap()
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


        if (GUILayout.Button("Download"))
        {
            map.LogWarnings();
            Debug.Log("Download Button is pressed");
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
        serializedObject.ApplyModifiedProperties();
    }

}

*/