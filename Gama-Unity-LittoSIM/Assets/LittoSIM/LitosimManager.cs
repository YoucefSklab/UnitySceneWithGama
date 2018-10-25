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
        
        for (int i = 0; i <4; i++)
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
            addCube2(position, Color.yellow);
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

        // contents cnt = new contents("Cube", 1, position.x, position.y);
        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Cube", 1, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message);
        gameNbr++;

        addObjectOnPanel(1, "Changer en zones constructibles", 1, -100);
    }

     public void addCube2(Vector3 position, Color color)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cube);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;

        // contents cnt = new contents("Cube", 1, position.x, position.y);
        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Cube", 1, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message);
        gameNbr++;

        addObjectOnPanel(5, "Changer en zones constructibles", 5, -500);
    }

    public void addSphere(Vector3 position, Color color)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;

        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Sphere", 2, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message);
        gameNbr++;

        addObjectOnPanel(2, "Changer en zones naturelles", 2, -200);
    }

    public void addCapsule(Vector3 position, Color color)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;

        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Capsule", 3, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message);
        gameNbr++;

        addObjectOnPanel(3, "Changer en zones 3", 3, -300);
    }

    public void addCylinder(Vector3 position, Color color)
    {
        GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        game.transform.position = position;
        game.transform.localScale = new Vector3(40, 40, 40);
        Renderer rend = game.GetComponent<Renderer>();
        rend.material.color = color;

        string message = MsgSerialization.serialization(new LittosimMessage("Unity", "GamaMainAgent", "Cylinder", 4, position.x, position.y, DateTime.Now.ToString()));
        publishMessage(message);
        gameNbr++;

        addObjectOnPanel(4, "Changer en zones 4", 4, -400);

    }





	public void gamaAddElement (object args)
	{
		object[] obj = (object[])args;
        int type = Int32.Parse ((string)obj [0]);
        string name = (string)obj [1];
        string texte = (string)obj [2];
        int delay = Int32.Parse ((string)obj [3]);
        int montant = Int32.Parse ((string)obj [4]);
        float x = float.Parse((string)obj [5]);
        float y = float.Parse((string)obj [6]);
        float z = float.Parse((string)obj [7]);

        Vector3 position = new Vector3(x,y,z);

        switch (type){
            case 1:

            break;
            case 2:

            break;
            case 3:

            break;
            case 4:

            break;
            case 5:

            break;
        }
	}



    public void publishMessage(string message)
    {
        GamaManager.client.Publish("littosim", System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        Debug.Log("-> " + message);
    }

    public void addObjectOnPanel(int type, string message, int nbr, int montant)
    {
        GameObject ActionsPanelParent = GameObject.Find("Content-Panel-Actions");
        GameObject ActionPanelChild = null;
        valider.active = true;

        switch (type)
        {
            case 1:
                createActionPaneChild(type, ActionPanelChild, ActionsPanelParent, "Changer en zones urbaines", "1", "-100");
                break;
            case 2:
                createActionPaneChild(type, ActionPanelChild, ActionsPanelParent, "Urbaine intense", "2", "-200");
                break;
            case 3:
                createActionPaneChild(type, ActionPanelChild, ActionsPanelParent, "Digue", "3", "-300");
                break;
            case 4:
                createActionPaneChild(type, ActionPanelChild, ActionsPanelParent, "Change en zones agricoles", "4", "-400");
                break;
            case 5:
                createActionPaneChild(type, ActionPanelChild, ActionsPanelParent, "Changer en zones naturelles", "5", "-500");
                break;
        }

        int value = 100 * type;
        setBudgetInitialValue(Convert.ToString(value));
        setBudgetRestantValue(Convert.ToString(value));

    }

    public void setBudgetInitialValue(string value)
    {
        GameObject BudgetInitial = GameObject.Find("BudgetInitialValue");
        BudgetInitial.GetComponent<Text>().text = value;

    }

    public void setBudgetRestantValue(string value)
    {
        GameObject BudgetRestant = GameObject.Find("BudgetRestantValue");
        BudgetRestant.GetComponent<Text>().text = value;
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

    public void createActionPaneChild(int type, GameObject panelChild, GameObject panelParent, string texte, string delay, string montant)
    {
        panelChild = Instantiate(ActionPrefab);
        panelChild.name = "Panel-Action-" + type + "-" + actionCounter;
        panelChild.transform.position = getAtActionPanelPosition();
        panelChild.transform.SetParent(panelParent.transform);
        actionsList.Add(panelChild);
        panelChild.transform.Find("Texte_type").GetComponent<Text>().text = (texte);
        panelChild.transform.Find("Texte_nombre").GetComponent<Text>().text = (delay);
        panelChild.transform.Find("Texte_montant").GetComponent<Text>().text = (montant);

        updateValderPosition();
        if (actionsList.Count >= 10)
        {
            RectTransform rt = panelParent.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, (rt.sizeDelta.y + (actionsList.Count - 10) * 80f));
        }
        actionCounter++;
    }

    public void updateValderPosition()
    {
        Vector3 newPosition = actionsList[actionsList.Count - 1].transform.position;
        Vector3 position = valider.transform.position;
        position.y = newPosition.y - 100;
        valider.transform.position = position;
    }

    public void validateActionList()
    {
        GameObject ActionsPanelParent = GameObject.Find("Content-Panel-Recap-Actions");
        foreach (var ActionPanelChild in actionsList)
        {
            string texte = ActionPanelChild.transform.Find("Texte_type").GetComponent<Text>().text;
            string delay = ActionPanelChild.transform.Find("Texte_nombre").GetComponent<Text>().text;
            createRecapActionPaneChild(1, ActionsPanelParent, texte, delay);
        }
        while (actionsList.Count > 0)
        {
            GameObject ActionPanelChild = actionsList[0];
            actionsList.Remove(ActionPanelChild);
            Destroy(ActionPanelChild);
            valider.active = false;
        }
    }

    public void createRecapActionPaneChild(int type, GameObject panelParent, string texte, string delay)
    {
        GameObject panelChild = Instantiate(RecapActionPrefab);
        panelChild.name = "Panel-Recap-Action-" + type + "-" + recapActionCounter;
        panelChild.transform.position = getAtRecapActionPanelPosition();
        panelChild.transform.SetParent(panelParent.transform);
        panelChild.transform.Find("Texte_type").GetComponent<Text>().text = (texte);
        panelChild.transform.Find("Texte_nombre").GetComponent<Text>().text = (delay);
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




}
