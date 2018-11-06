using System;
using System.Collections;
using System.Collections.Generic;
using ummisco.gama.unity.messages;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt.Messages;

public class LittosimManager : MonoBehaviour
{

    // Use this for initialization

    public int actionToDo = 0;
    public static int gameNbr = 0;

    public string LITTOSSIM_TOPIC = "littosim";

    public GameObject ActionPrefab;
    public GameObject RecapActionPrefab;
    public GameObject MessagePrefab;

    public List<GameObject> actionsList = new List<GameObject>();
    public List<GameObject> recapActionsList = new List<GameObject>();
    public List<GameObject> messagesList = new List<GameObject>();

    public Vector3 initialPosition = new Vector3(918.0f, 131.0f, 0.0f);
    public Vector3 lastPosition = new Vector3(918.0f, 131.0f, 0.0f);

    public Vector3 initialRecapPosition = new Vector3(958.8f, -4.9f, 0.0f);
    public Vector3 lastRecapPosition = new Vector3(958.8f, -4.9f, 0.0f);

    public Vector3 createAtPosition;

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


        initialRecapPosition = GameObject.Find("Panel-Recap-Action-1").transform.position;
        lastRecapPosition = initialRecapPosition;
        //Destroy(GameObject.Find("Panel-Recap-Action-1"));
        //Debug.Log("-> The recap position is : " + GameObject.Find("Panel-Recap-Action-1").transform.position);

        // initialPosition = GameObject.Find("Panel-Action-1").transform.position;
        // lastPosition = initialPosition;
        initialPosition = GameObject.Find("Panel-Action-1").transform.position;
        lastRecapPosition = initialPosition;
        //Destroy(GameObject.Find("Panel-Action-1"));
        Debug.Log("-> The action position is : " + GameObject.Find("Panel-Action-1").transform.position);


        //Destroy(GameObject.Find("Panel-Message-1"));
        //Debug.Log("-> The message position is : " + GameObject.Find("Panel-Message-1").transform.position);

        initialMessagePosition = GameObject.Find("Panel-Message-1").transform.position;
        lastMessagePosition = initialMessagePosition;

        valider.active = false;
    }


    void Awak()
    {


    }

    public void createNewElement()
    {
        if (actionToDo != 0)
        {
            createAtPosition = Input.mousePosition;
            createAtPosition.z = -21;
            sendGamaMessage(createAtPosition);
            Debug.Log("Create new one at : " + createAtPosition);
        }
        createAtPosition = Input.mousePosition;
         Debug.Log("Create new one at --->  : "+createAtPosition);
    }

    void Update()
    {
        createAtPosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (bj is GameObject)
            {
                if (bj.tag.Equals("DeleteActionButton"))
                {
                    Debug.Log("Goooood");
                    sendDeleteAction(bj.transform.parent.name);
                }
                /* 
                if (bj.tag.Equals("MapPanel"))
                {
                    Debug.Log(" -- position clicked 3 : " + createAtPosition);
                    createAtPosition.z = -21;
                    sendGamaMessage(createAtPosition);
                    Debug.Log("Create new one at : " + createAtPosition);
                }
                */


            }

        }
        /* 
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

                }
        */

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

        // --------
        if (GUI.Button(new Rect(150, 1, 150, 20), "Send Mqtt message!"))
        {
            string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", 1, 0, 0, DateTime.Now.ToString()));
            message = "ceci est un test de message MQTT de unity vers Gama";
            string topic = "li";
            int msgId = GamaManager.client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Debug.Log("msgId is: " + msgId + " -> " + message);
            Debug.Log("Message sent to topic: " + topic);
        }

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
        game.transform.localScale = new Vector3(10, 10, 10);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);
    }

    public void addSphere(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        game.transform.position = position;
        game.transform.localScale = new Vector3(10, 10, 10);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);
    }

    public void addCapsule(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        game.transform.position = position;
        game.transform.localScale = new Vector3(10, 10, 10);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);
    }

    public void addCylinder(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        game.transform.position = position;
        game.transform.localScale = new Vector3(10, 10, 10);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;
        addObjectOnPanel(type, name, texte, delay, montant);

    }

    public void addCube2(Vector3 position, Color color, int type, string name, string texte, int delay, int montant)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cube);
        game.transform.position = position;
        game.transform.localScale = new Vector3(10, 10, 10);
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


    public void deleteActionFromList(object args)
    {
        object[] obj = (object[])args;
        string name = (string)obj[0];

        if (actionsList.Count > 0)
        {
            GameObject gameObj = GameObject.Find(name);

            actionsList.Remove(gameObj);

            foreach (var gameOb in actionsList)
            {
                if (gameOb.name.Equals(name))
                {
                    actionsList.Remove(gameOb);
                    break;
                }
            }

            Destroy(gameObj);
            lastPosition = initialPosition;
            foreach (var gameObject in actionsList)
            {
                gameObject.transform.position = lastPosition;
                lastPosition.y = lastPosition.y - 85f;
            }
        }
        Debug.Log("Here to delete the action from the list the action " + name);

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
        int msgId = GamaManager.client.Publish(LITTOSSIM_TOPIC, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        Debug.Log("msgId is: " + msgId + " -> " + message);
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
            lastMessagePosition.y = lastMessagePosition.y - 20;
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
        panelChild.transform.Find("Close-button").transform.name = "close_" + name;

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

    public void sendDeleteAction(string name)
    {
        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", 101, name, 0, 0, DateTime.Now.ToString()));
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

    public void newInfoMessage(object args)
    {
        object[] obj = (object[])args;
        int type = Int32.Parse((string)obj[0]);
        string name = (string)obj[1];
        string texte = (string)obj[2];

        GameObject panelParent = GameObject.Find("Panel-Messages");
        GameObject panelChild = Instantiate(MessagePrefab);
        panelChild.name = name;
        panelChild.transform.position = getAtMessagePanelPosition();
        panelChild.transform.SetParent(panelParent.transform);
        panelChild.transform.Find("Texte_type").GetComponent<Text>().text = (texte);
        messagesList.Add(panelChild);

        if (messagesList.Count > 5)
        {
            GameObject gameObj = messagesList[0];
            messagesList.RemoveAt(0);
            Destroy(gameObj);
            lastMessagePosition = initialMessagePosition;
            foreach (var gameObject in messagesList)
            {
                gameObject.transform.position = lastMessagePosition;
                lastMessagePosition.y = lastMessagePosition.y - 20f;
            }
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
