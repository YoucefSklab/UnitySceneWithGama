using System;
using System.Collections;
using System.Collections.Generic;
using ummisco.gama.unity.messages;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt.Messages;

public class LitosimManager : MonoBehaviour
{

    // Use this for initialization

    public int actionToDo = 0;
    void Start()
    {

    }

    public GameObject particle;
    void Update()
    {
        Vector3 mouse = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            Debug.Log("---> position: " + mouse);
        }

        /* 


        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed secondary button.");
            Debug.Log("---> position: " + mouse);
        }


        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Pressed middle click.");
            Debug.Log("---> position: " + mouse);
        }
        */



        // Left-half of the screen.
        //Rect bounds = new Rect(0, 0, Screen.width / 2, Screen.height);

        Rect bounds = new Rect(58, 843, 1320, 200);



        bounds = new Rect(58, 43, 1320, 200);

        bounds = new Rect(268, 113, 1020, 200);

        GameObject panel = GameObject.Find("Panel-Map");



        var firstPlayerHand = GameObject.Find("First Player Hand");
        var position1 = panel.transform.position;
        var position2 = panel.GetComponent<RectTransform>().rect;

        bounds = new Rect(position2.x, position2.y, position2.width, position2.height);

        if (Input.GetMouseButtonDown(0) && bounds.Contains(Input.mousePosition))
        {
            Vector3 position = Input.mousePosition;
            addIfObject(position);
        }


    }

    void OnGUI()
    {
        Rect bounds = new Rect(58, -843, 1320, 200);

        GUI.Label(bounds, "test");

        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.red);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(bounds, GUIContent.none);

    }

    public void onAddButtonClicked(int actionId)
    {

        actionToDo = actionId;

    }

    public void addIfObject(Vector3 position)
    {

        if (actionToDo == 1)
        {
            addCube(position, Color.red);
        }
        if (actionToDo == 2)
        {
            addSphere(position, Color.green);
        }
        if (actionToDo == 3)
        {
            addCapsule(position, Color.blue);
        }
        if (actionToDo == 4)
        {
            addCylinder(position, Color.magenta);
        }
        if (actionToDo == 5)
        {
            addCube(position, Color.yellow);
        }

        actionToDo = 0;
    }


    public void addCube(Vector3 position, Color color)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cube);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;

        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Cube1", 1, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message); 
    }

    public void addSphere(Vector3 position, Color color)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;

        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Cube1", 2, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message); 
    }

    public void addCapsule(Vector3 position, Color color)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;

        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Cube1", 3, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message); 
    }

    public void addCylinder(Vector3 position, Color color)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;

        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Cube1", 4, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message); 
    }

    public void publishMessage(string message)
    {        
        GamaManager.client.Publish("Created", System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
    }
}
