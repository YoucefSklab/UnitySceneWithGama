﻿using System;
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
    public static int gameNbr = 0;

    public GameObject ActionPrefab;
    public GameObject RecapActionPrefab;
    public GameObject MessagePrefab;

    public List<GameObject> actionsList = new List<GameObject>();
    public List<GameObject> recapActionsList = new List<GameObject>();
    public List<GameObject> messagesList = new List<GameObject>();

    public Vector3 initialPosition = new Vector3(1976.2f, 1159.5f, 0.0f);
    public Vector3 lastPosition = new Vector3(1976.2f, 1159.5f, 0.0f);

    public Vector3 initialRecapPosition = new Vector3(2020.0f, -135.3f, 0.0f);
    public Vector3 lastRecapPosition = new Vector3(2020.0f, -135.3f, 0.0f);

    public Vector3 initialMessagePosition;
    public Vector3 lastMessagePosition;

    public int elementCounter = 0;
    public int actionCounter = 0;
    public int recapActionCounter = 0;
    public int messageCounter = 0;

    public GameObject valider;

    void Start()
    {
        //lastPosition 


        initialRecapPosition = new Vector3(2020.0f, -135.3f, 0.0f);
        lastRecapPosition = new Vector3(2020.0f, -135.3f, 0.0f);
        Destroy(GameObject.Find("Panel-Recap-Action-1"));

        // initialPosition = GameObject.Find("Panel-Action-1").transform.position;
        // lastPosition = initialPosition;
        Destroy(GameObject.Find("Panel-Action-1"));
        Debug.Log("The position is : " + GameObject.Find("Panel-Message-1").transform.position);
        Destroy(GameObject.Find("Panel-Message-1"));

        initialMessagePosition = new Vector3(-836.3f, -136.2f, 0.0f);
        lastMessagePosition = initialMessagePosition;

        valider.active = false;

        GameObject panelParent = GameObject.Find("Panel-Messages");
        //GameObject panelParent = GameObject.Find("Panel-Messages");

        for (int i = 0; i < 4; i++)
        {
            createMessagePaneChild(i, panelParent, "Ceci est le message N : " + i);
        }
    }

    void Awak()
    {


    }

    void Update()
    {

        if (GameObject.Find("Panel-Message-0-0"))
        {
            Debug.Log("--+-> " + GameObject.Find("Panel-Message-0-0").transform.position);
            //   GameObject.Find("Panel-Message-0-0").transform.position = initialMessagePosition;
        }

        Vector3 mouse = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            Debug.Log("---> position: " + mouse);
        }

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
            position.z = -21;
            sendGamaMessage(position);
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

    public void sendGamaMessage(Vector3 position)
    {
        string message = "";
        switch (actionToDo)
        {
            case 1:
                message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", 1, position.x, position.y, DateTime.Now.ToString()));
                publishMessage(message);
                break;
            case 2:
                message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", 2, position.x, position.y, DateTime.Now.ToString()));
                publishMessage(message);
                break;
            case 3:
                message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", 3, position.x, position.y, DateTime.Now.ToString()));
                publishMessage(message);
                break;
            case 4:
                message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", 4, position.x, position.y, DateTime.Now.ToString()));
                publishMessage(message);
                break;
            case 5:
                message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", 5, position.x, position.y, DateTime.Now.ToString()));
                publishMessage(message);
                break;
        }
        actionToDo = 0;
        gameNbr++;
    }



    public void addCube(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cube);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);
    }

    public void addSphere(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);
    }

    public void addCapsule(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);
    }

    public void addCylinder(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);

    }

    public void addCube2(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cube);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);
    }





    public void gamaAddElement(object args)
    {
        object[] obj = (object[])args;
        int type = Int32.Parse((string)obj[0]);
        string name = (string)obj[1];
        string texte = (string)obj[2];
        int delay = Int32.Parse((string)obj[3]);
        int montant = Int32.Parse((string)obj[4]);
        float x = float.Parse((string)obj[5]);
        float y = float.Parse((string)obj[6]);
        float z = float.Parse((string)obj[7]);

        Vector3 position = new Vector3(x, y, z);

        switch (type)
        {
            case 1:
                addCube(position, Color.red, type, name, texte, delay, montant);
                break;
            case 2:
                addSphere(position, Color.red, type, name, texte, delay, montant);
                break;
            case 3:
                addCapsule(position, Color.red, type, name, texte, delay, montant);
                break;
            case 4:
                addCylinder(position, Color.red, type, name, texte, delay, montant);
                break;
            case 5:
                addCube2(position, Color.red, type, name, texte, delay, montant);
                break;
        }

        elementCounter++;
    }



    public void gamaAddValidElement(object args)
    {
        object[] obj = (object[])args;
        int type = Int32.Parse((string)obj[0]);
        string name = (string)obj[1];
        string texte = (string)obj[2];
        int delay = Int32.Parse((string)obj[3]);

        GameObject panelParent = GameObject.Find("Content-Panel-Recap-Actions");
        createRecapActionPaneChild(type, name, panelParent, texte, delay);
    }



    public void publishMessage(string message)
    {
        GamaManager.client.Publish("littosim", System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        Debug.Log("-> " + message);
    }

    public void addObjectOnPanel(int type, string name, string texte, int delay, int montant)
    {
        GameObject ActionsPanelParent = GameObject.Find("Content-Panel-Actions");
        valider.active = true;
        
        switch (type)
        {
            case 1:
                createActionPaneChild(type, name, ActionsPanelParent, texte, delay.ToString(), montant.ToString());
                break;
            case 2:
                createActionPaneChild(type, name, ActionsPanelParent, texte, delay.ToString(), montant.ToString());
                break;
            case 3:
                createActionPaneChild(type, name, ActionsPanelParent, texte, delay.ToString(), montant.ToString());
                break;
            case 4:
                createActionPaneChild(type, name, ActionsPanelParent, texte, delay.ToString(), montant.ToString());
                break;
            case 5:
                createActionPaneChild(type, name, ActionsPanelParent, texte, delay.ToString(), montant.ToString());
                break;
        }

       

    }
   
    public Vector3 getAtActionPanelPosition()
    {
        if (actionsList.Count > 0)
        {
            lastPosition = (actionsList[actionsList.Count - 1]).transform.position;
            lastPosition.y = lastPosition.y - 100;
            return lastPosition;
        }
        else
        {
            //initialPosition.y = initialPosition.y - 100;
            return initialPosition;
        }
    }

    public Vector3 getAtRecapActionPanelPosition()
    {
        if (recapActionsList.Count > 0)
        {
            lastRecapPosition = (recapActionsList[(recapActionsList.Count - 1)]).transform.position;
            lastRecapPosition.y = lastRecapPosition.y - 85;
            return lastRecapPosition;
        }
        else
        {
            return initialRecapPosition;
        }
    }

    public Vector3 getAtMessagePanelPosition()
    {
        if (messagesList.Count > 0)
        {
            lastMessagePosition = (messagesList[(messagesList.Count - 1)]).transform.position;
            lastMessagePosition.y = lastMessagePosition.y - 85;
            return lastMessagePosition;
        }
        else
        {
            return initialMessagePosition;
        }
    }

    public void createActionPaneChild(int type, string name, GameObject panelParent, string texte, string delay, string montant)
    {

        GameObject panelChild = Instantiate(ActionPrefab);
        panelChild.name = name;
        panelChild.transform.position = getAtActionPanelPosition();
        panelChild.transform.SetParent(panelParent.transform);
        actionsList.Add(panelChild);
        panelChild.transform.Find("Texte_type").GetComponent<Text>().text = (texte);
        panelChild.transform.Find("Texte_nombre").GetComponent<Text>().text = (delay);
        panelChild.transform.Find("Texte_montant").GetComponent<Text>().text = (montant);

        updateValiderPosition();

        if (actionsList.Count >= 10)
        {
            RectTransform rt = panelParent.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, (rt.sizeDelta.y + (actionsList.Count - 10) * 80f));
        }
        actionCounter++;
    }

    public void updateValiderPosition()
    {
        Vector3 newPosition = actionsList[actionsList.Count - 1].transform.position;
        Vector3 position = valider.transform.position;
        position.y = newPosition.y - 100;
        valider.transform.position = position;
    }

    public void validateActionList()
    {
        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", 100, 0, 0, DateTime.Now.ToString()));
        publishMessage(message);
    }

    public void destroyElement(string name)
    {
        if (GameObject.Find(name))
        {
            GameObject obj = GameObject.Find(name);
            actionsList.Remove(obj);
            Destroy(obj);
            if (actionsList.Count == 0)
            {
                valider.active = false;
            }
        }
    }

    public void createRecapActionPaneChild(int type, string name, GameObject panelParent, string texte, int delay)
    {
        GameObject panelChild = Instantiate(RecapActionPrefab);
        panelChild.name = name;
        panelChild.transform.position = getAtRecapActionPanelPosition();
        panelChild.transform.SetParent(panelParent.transform);
        panelChild.transform.Find("Texte_type").GetComponent<Text>().text = (texte);
        panelChild.transform.Find("Texte_nombre").GetComponent<Text>().text = (delay.ToString());
        recapActionsList.Add(panelChild);

        if (recapActionsList.Count >= 5)
        {
            RectTransform rt = panelParent.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, (rt.sizeDelta.y + ((recapActionsList.Count - 5) * 85f)));
        }
        recapActionCounter++;
    }

    public void createMessagePaneChild(int type, GameObject panelParent, string texte)
    {
        GameObject panelChild = Instantiate(MessagePrefab);
        panelChild.name = "Panel-Message-" + type + "-" + messageCounter;
        panelChild.transform.position = getAtMessagePanelPosition();
        panelChild.transform.SetParent(panelParent.transform);
        panelChild.transform.Find("Texte_type").GetComponent<Text>().text = (texte);
        messagesList.Add(panelChild);

        if (messagesList.Count >= 5)
        {
            RectTransform rt = panelParent.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, (rt.sizeDelta.y + ((messagesList.Count - 5) * 85f)));
        }
        messageCounter++;
    }


    public void setInitialBudget(object args)
    {
        object[] obj = (object[])args;
        string value = (string)obj[0];
        GameObject.Find("BudgetInitialValue").GetComponent<Text>().text = value;
    }

    public void setRemainingBudget(object args)
    {
        object[] obj = (object[])args;
        string value = (string)obj[0];
        GameObject.Find("BudgetRestantValue").GetComponent<Text>().text = value;
    }




}
