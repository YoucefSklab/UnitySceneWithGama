using System;
using System.Collections;
using System.Collections.Generic;
using ummisco.gama.unity.messages;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt.Messages;
using ummisco.gama.unity.SceneManager;
using ummisco.gama.unity.littosim.ActionPrefab;

namespace ummisco.gama.unity.littosim
{
    public class LittosimManager : MonoBehaviour
    {
        // Use this for initialization
        public static int actionToDo = 0;
        public static int gameNbr = 0;

        public GameObject ActionPanelPrefab;
        public GameObject ActionRecapPanelPrefab;
        public GameObject MessagePanelPrefab;
        public GameObject ButtonActionPrefab;
        public GameObject UA;
        public GameObject Def_Cote;

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
        public GameObject valider_text;
        public GameObject valider_montant;

        public float lineHeight = 80f;
        public float zCoordinate = 60;

        public Canvas uiCanvas;
        private GameObject uiManager;
        private GameObject main_canvas;


        void Start()
        {
            initialRecapPosition = new Vector3(2020.0f, -135.3f, 0.0f);
            lastRecapPosition = new Vector3(2020.0f, -135.3f, 0.0f);

            uiManager = GameObject.Find(IUILittoSim.UI_MANAGER_GAMEOBJECT);
            main_canvas = GameObject.Find(IUILittoSim.MAIN_CANVAS);

      //      Debug.Log(" The position is: " + GameObject.Find("Button_Action_Prefab").transform.position);

            /*

            IActionButton.action_nbr = 10;

            for (int i = 1; i <=10; i++)
            {
                if (i != 2) { 
                GameObject obj = Instantiate(ButtonActionPrefab);
                obj.name = "Action"+i;
                Vector3 position = IActionButton.GetPosition(i);
                obj.GetComponent<Button_Action_Prefab>().SetUp("actionName"+i, i, "help"+i, "I_naturel", "UA", IActionButton.GetPosition(i));
                obj.transform.SetParent(main_canvas.transform);
                }
            }
            */





            //obj.GetComponent<Button_Action_Prefab>().code = 20;


            //obj = Instantiate(ButtonActionPrefab);
            //obj.name = "Action2";
            //obj.GetComponent<ActionManager>().action = new Action("actionName2", 21, 23, 80f, "DEF_COTE", "This is help", "This is icon");


            // initialPosition = GameObject.Find("IUILittoSim.ACTION_PANEL_PREFAB").transform.position;
            // lastPosition = initialPosition;

//            Debug.Log("The hight of " + IUILittoSim.MESSAGE_PANEL_PREFAB + " is : " + GameObject.Find(IUILittoSim.MESSAGE_PANEL_PREFAB).GetComponent<RectTransform>().rect.height);

            initialMessagePosition = new Vector3(-836.3f, -136.2f, 0.0f);
            lastMessagePosition = initialMessagePosition;

            Destroy(GameObject.Find(IUILittoSim.ACTION_PANEL_PREFAB));
            Destroy(GameObject.Find(IUILittoSim.MESSAGE_PANEL_PREFAB));
            Destroy(GameObject.Find(IUILittoSim.ACTION_RECAP_PANEL_PREFAB));

            deactivateValider();
        }

        void Awak()
        {

        }

        void FixedUpdate()
        {
            /*
            if (GameObject.Find(IUILittoSim.MESSAGES_PANEL))
            {
              //  Debug.Log("--+-> " + GameObject.Find(IUILittoSim.MESSAGES_PANEL).transform.position);
                //   GameObject.Find(IUILittoSim.MESSAGE_PANEL).transform.position = initialMessagePosition;
            }
            */

            Debug.Log("The active panel is " + uiManager.GetComponent<UIManager>().getActivePanel());

            Vector3 mouse = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                GameObject bj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
                if (bj is GameObject)
                    if (bj.tag.Equals("DeleteActionButton"))
                    {
                        Debug.Log("Goooood");
                        Debug.Log("Goooood--------->  to delete is : " + bj.name);
                        sendDeleteAction(bj.transform.parent.name);
                    }
                // Debug.Log("Goooood--------->  Selected is : "+bj.name);
            }

            // Left-half of the screen.
            //Rect bounds = new Rect(0, 0, Screen.width / 2, Screen.height);

            /*
             Rect bounds = new Rect(58, 843, 1320, 200);
             bounds = new Rect(58, 43, 1320, 200);
             bounds = new Rect(268, 113, 1020, 200);
                         
             GameObject panel = GameObject.Find("Panel-Map");
             var firstPlayerHand = GameObject.Find("First Player Hand");
             var position1 = panel.transform.position;
             var position2 = panel.GetComponent<RectTransform>().rect;
             bounds = new Rect(position2.x, position2.y, position2.width, position2.height);
             */
        }

        public void createNewElement()
        {
            Vector3 position = Input.mousePosition;
            Debug.Log("Mouse position is : " + position);
            position = uiManager.GetComponent<UIManager>().worldToUISpace(uiCanvas, position);
            position.z = -80;
            sendGamaMessage(position);

            // To delete

            GameObject panelChild = Instantiate(UA);
            panelChild.name = "UA"+position.x+"_"+position.y;
            panelChild.transform.position = position;
            GameObject panelParent = GameObject.Find(IUILittoSim.ACTION_LIST_RECAP_PANEL);
            panelChild.transform.SetParent(panelParent.transform);
 
            // to delete
            /*
            GameObject game = GameObject.CreatePrimitive(PrimitiveType.Cube);
            game.transform.position = position;
            game.transform.localScale = new Vector3(30, 30, 30);
            Renderer rend = game.GetComponent<Renderer>();
            rend.material.color = Color.red;
            */
            Debug.Log("Final created position is :" + position);

        }

        public void createNewElementOld()
        {
            Vector3 position = Input.mousePosition;
            Vector3 panelPosition = position;
            Debug.Log("Mouse position is : " + position);
            GameObject Object = GameObject.Find("Object");
            Vector3 objectPosition = Object.transform.position;
            Vector3 objectLocalPosition = Object.transform.localPosition;

            Debug.Log("Object Position is : " + Object.transform.position);
            Debug.Log("Object local Position is : " + Object.transform.localPosition);
            Debug.Log("Object transform local Position is : " + Object.transform.TransformPoint(objectLocalPosition.x, objectLocalPosition.y, objectLocalPosition.z));


            GameObject Canvas_Actions = GameObject.Find(IUILittoSim.MAIN_CANVAS);
            //GameObject Canvas_Actions = GameObject.Find("IUILittoSim.MAP_PANEL");
            RectTransform Canvas = Canvas_Actions.GetComponent<RectTransform>();
            Camera cam = GamaManager.MainCamera.GetComponent<Camera>();

            Debug.Log("1 Panel cam position is: " + cam.WorldToViewportPoint(position));
            Debug.Log("2 Panel cam position is: " + cam.WorldToScreenPoint(position));
            Debug.Log("3 Panel cam position is: " + cam.ScreenToWorldPoint(position));
            Debug.Log("4 Panel cam position is: " + cam.ScreenToViewportPoint(position));

            Debug.Log("5 Object cam position is: " + cam.WorldToViewportPoint(objectPosition));
            Debug.Log("6 Object cam position is: " + cam.WorldToScreenPoint(objectPosition));
            Debug.Log("7 Object cam position is: " + cam.ScreenToWorldPoint(objectPosition));
            Debug.Log("8 Object cam position is: " + cam.ScreenToViewportPoint(objectPosition));

            panelPosition = uiManager.GetComponent<UIManager>().worldToUISpace(uiCanvas, position);

            Debug.Log("WorldToCanvasPosition position is: " + panelPosition);

            Vector3[] localCorners = new Vector3[4];
            Vector3[] worldCorners = new Vector3[4];
            Canvas.GetLocalCorners(localCorners);
            Canvas.GetWorldCorners(worldCorners);
            Rect newRect = new Rect(localCorners[0], localCorners[2] - localCorners[0]);

            Object.transform.localPosition = localCorners[2];

            Debug.Log("newRect Before : 0 = " + localCorners[0] + " 1 = " + localCorners[1] + " 2 = " + localCorners[2] + " 3 = " + localCorners[3]);
            Debug.Log("newRect After : 0 = " + cam.WorldToScreenPoint(localCorners[0]) + " 1 = " + cam.WorldToScreenPoint(localCorners[1]) + " 2 = " + cam.WorldToScreenPoint(localCorners[2]) + " 3 = " + cam.WorldToScreenPoint(localCorners[3]));

            Debug.Log("Panel position is: " + panelPosition);

            //game.transform.position  = position - cam.WorldToScreenPoint(corners[0]);
            //Debug.Log("-----> "+newRect.Contains(Input.mousePosition));

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
                string message = MsgSerialization.serialization(new LittosimMessage(ILittoSimConcept.GAMA_TOPIC, ILittoSimConcept.GAMA_AGENT, 1, 0, 0, DateTime.Now.ToString()));
                message = "ceci est un test de message MQTT de unity vers Gama";
                string topic = "li";
                //int msgId = GamaManager.client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                int msgId = GamaManager.client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message));
                Debug.Log("msgId is: " + msgId + " -> " + message);
                Debug.Log("Message sent to topic: " + topic);
            }
        }



        public void sendGamaMessage(Vector3 position)
        {
            switch (actionToDo)
            {
                case ILittoSimConcept.ACTION_URBANISE:
                    publishMessage(getSerializedMessage(ILittoSimConcept.ACTION_URBANISE, position));
                    break;
                case ILittoSimConcept.ACTION_DENSIFIE:
                    publishMessage(getSerializedMessage(ILittoSimConcept.ACTION_DENSIFIE, position));
                    break;
                case ILittoSimConcept.ACTION_DIGUE:
                    publishMessage(getSerializedMessage(ILittoSimConcept.ACTION_DIGUE, position));
                    break;
                case ILittoSimConcept.ACTION_AGRICULTURAL:
                    publishMessage(getSerializedMessage(ILittoSimConcept.ACTION_AGRICULTURAL, position));
                    break;
                case ILittoSimConcept.ACTION_NATURAL:
                    publishMessage(getSerializedMessage(ILittoSimConcept.ACTION_NATURAL, position));
                    break;
            }
            actionToDo = 0;
            gameNbr++;
        }

        public string getSerializedMessage(int idAction, Vector3 position)
        {
            return MsgSerialization.serialization(new LittosimMessage(ILittoSimConcept.GAMA_TOPIC, ILittoSimConcept.GAMA_AGENT, idAction, position.x, position.y, DateTime.Now.ToString()));
        }

        public void addCube(Vector3 position, Color color, int type, string name, string texte, int delay, int montant, GameObject parentObject)
        {
            GameObject createdObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            createdObject.transform.position = position;
            createdObject.transform.localScale = new Vector3(40, 40, 40);
            Renderer rend = createdObject.GetComponent<Renderer>();
            rend.material.color = color;
            createdObject.transform.SetParent(parentObject.transform);
            addObjectOnPanel(type, name, texte, delay, montant);
        }

        public void addSphere(Vector3 position, Color color, int type, string name, string texte, int delay, int montant, GameObject parentObject)
        {
            GameObject createdObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            createdObject.transform.position = position;
            createdObject.transform.localScale = new Vector3(40, 40, 40);
            Renderer rend = createdObject.GetComponent<Renderer>();
            rend.material.color = color;
            createdObject.transform.SetParent(parentObject.transform);
            addObjectOnPanel(type, name, texte, delay, montant);
        }

        public void addCapsule(Vector3 position, Color color, int type, string name, string texte, int delay, int montant, GameObject parentObject)
        {
            GameObject createdObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            createdObject.transform.position = position;
            createdObject.transform.localScale = new Vector3(40, 40, 40);
            Renderer rend = createdObject.GetComponent<Renderer>();
            rend.material.color = color;
            createdObject.transform.SetParent(parentObject.transform);
            addObjectOnPanel(type, name, texte, delay, montant);
        }

        public void addCylinder(Vector3 position, Color color, int type, string name, string texte, int delay, int montant, GameObject parentObject)
        {
            GameObject createdObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            createdObject.transform.position = position;
            createdObject.transform.localScale = new Vector3(40, 40, 40);
            Renderer rend = createdObject.GetComponent<Renderer>();
            rend.material.color = color;
            createdObject.transform.SetParent(parentObject.transform);
            addObjectOnPanel(type, name, texte, delay, montant);

        }

        public void addCube2(Vector3 position, Color color, int type, string name, string texte, int delay, int montant, GameObject parentObject)
        {
            GameObject createdObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            createdObject.transform.position = position;
            createdObject.transform.localScale = new Vector3(40, 40, 40);
            Renderer rend = createdObject.GetComponent<Renderer>();
            rend.material.color = color;
            createdObject.transform.SetParent(parentObject.transform);
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
            GameObject parentObject = GameObject.Find(uiManager.GetComponent<UIManager>().activePanel);
            switch (type)
            {
                case 1:

                    addCube(position, Color.red, type, name, texte, delay, montant, parentObject);
                    break;
                case 2:
                    //addSphere(position, Color.red, type, name, texte, delay, montant, parentObject);
                    addCube(position, Color.blue, type, name, texte, delay, montant, parentObject);
                    break;
                case 3:
                    //addCapsule(position, Color.red, type, name, texte, delay, montant, parentObject);
                    addCube(position, Color.green, type, name, texte, delay, montant, parentObject);
                    break;
                case 4:
                    //addCylinder(position, Color.red, type, name, texte, delay, montant, parentObject);
                    addCube(position, Color.yellow, type, name, texte, delay, montant, parentObject);
                    break;
                case 5:
                    //addCube2(position, Color.red, type, name, texte, delay, montant, parentObject);
                    addCube(position, Color.white, type, name, texte, delay, montant, parentObject);
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
                    lastPosition.y = lastPosition.y - lineHeight;
                }
            }
            //Debug.Log("Here to delete the action from the list the action " + name);
            updateValiderPosition();
        }

        public void gamaAddValidElement(object args)
        {
            object[] obj = (object[])args;
            int type = Int32.Parse((string)obj[0]);
            string name = (string)obj[1];
            string texte = (string)obj[2];
            int delay = Int32.Parse((string)obj[3]);

            GameObject panelParent = GameObject.Find(IUILittoSim.ACTION_LIST_RECAP_PANEL);
            createRecapActionPaneChild(type, name, panelParent, texte, delay);
        }

        public void publishMessage(string message)
        {
            Debug.Log("Prepare to send message");
            //int msgId = GamaManager.client.Publish(LITTOSSIM_TOPIC, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            int msgId = GamaManager.client.Publish(ILittoSimConcept.LITTOSIM_TOPIC, System.Text.Encoding.UTF8.GetBytes(message));
            Debug.Log("-- > msgId is: " + msgId + " -> " + message);
        }

        public void addObjectOnPanel(int type, string name, string texte, int delay, int montant)
        {
            GameObject ActionsPanelParent = GameObject.Find(IUILittoSim.ACTION_LIST_PANEL);
            activateValider();

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
                lastPosition.y = lastPosition.y - lineHeight;
                return lastPosition;
            }
            else
            {
                return initialPosition;
            }
        }

        public Vector3 getAtRecapActionPanelPosition()
        {
            if (recapActionsList.Count > 0)
            {
                lastRecapPosition = (recapActionsList[(recapActionsList.Count - 1)]).transform.position;
                lastRecapPosition.y = lastRecapPosition.y - lineHeight;
                return lastRecapPosition;
            }
            else
            {
                return initialRecapPosition;
            }
        }

        public void updateRecapActionPosition()
        {
            if (recapActionsList.Count > 0)
            {
                lastRecapPosition = initialRecapPosition;
                foreach (var gameObject in recapActionsList)
                {
                    gameObject.transform.position = lastRecapPosition;
                    lastRecapPosition.y = lastRecapPosition.y - lineHeight;
                }
            }
        }

        public Vector3 getAtMessagePanelPosition()
        {
            if (messagesList.Count > 0)
            {
                lastMessagePosition = (messagesList[(messagesList.Count - 1)]).transform.position;
                lastMessagePosition.y = lastMessagePosition.y - lineHeight;
                return lastMessagePosition;
            }
            else
            {
                return initialMessagePosition;
            }
        }

        public void createActionPaneChild(int type, string name, GameObject panelParent, string texte, string delay, string montant)
        {

            GameObject panelChild = Instantiate(ActionPanelPrefab);
            panelChild.name = name;
            panelChild.transform.position = getAtActionPanelPosition();
            panelChild.transform.SetParent(panelParent.transform);
            actionsList.Add(panelChild);
            panelChild.transform.Find(IUILittoSim.ACTION_TITLE).GetComponent<Text>().text = texte;

            panelChild.transform.Find(IUILittoSim.ACTION_CYCLE).transform.Find(IUILittoSim.ACTION_CYCLE_VALUE).GetComponent<Text>().text = (delay);
            panelChild.transform.Find(IUILittoSim.ACTION_BUDGET).GetComponent<Text>().text = (montant);

            updateValiderPosition();

            if (actionsList.Count >= 10)
            {
                RectTransform rt = panelParent.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, (rt.sizeDelta.y + (actionsList.Count - 10) * lineHeight));
            }
            actionCounter++;
        }

        public void updateValiderPosition()
        {
            if (actionsList.Count > 0)
            {
                Vector3 newPosition = actionsList[actionsList.Count - 1].transform.position;
                Vector3 position = valider.transform.position;
                position.y = newPosition.y - lineHeight;
                valider.transform.position = position;

                position = valider_text.transform.position;
                position.y = newPosition.y - lineHeight;
                valider_text.transform.position = position;

                position = valider_montant.transform.position;
                position.y = newPosition.y - lineHeight;
                valider_montant.transform.position = position;
            }
            else
            {
                deactivateValider();
            }
        }

        public void deactivateValider()
        {
            valider.SetActive(false);//.active = false;
            valider_text.SetActive(false);//.active = false;
            valider_montant.SetActive(false);//.active = false;
        }

        public void activateValider()
        {
            valider.SetActive(true);//.active = false;
            valider_text.SetActive(true);//.active = false;
            valider_montant.SetActive(true);//.active = false;
        }

        public void validateActionList()
        {
            string message = MsgSerialization.serialization(new LittosimMessage(ILittoSimConcept.GAMA_TOPIC, "GamaMainAgent", 100, 0, 0, DateTime.Now.ToString()));
            publishMessage(message);
        }

        public void sendDeleteAction(string name)
        {
            string message = MsgSerialization.serialization(new LittosimMessage(ILittoSimConcept.GAMA_TOPIC, "GamaMainAgent", 101, name, 0, 0, DateTime.Now.ToString()));
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
                    deactivateValider();
                }
            }
        }

        public void createRecapActionPaneChild(int type, string name, GameObject panelParent, string texte, int delay)
        {
            GameObject panelChild = Instantiate(ActionRecapPanelPrefab);
            string CycleIcon = geObjectComposedName(IUILittoSim.ACTION_RECAP_CYCLE_ICON, name);
            string ValideIcon = geObjectComposedName(IUILittoSim.ACTION_RECAP_VALIDE_ICON, name);
            string CyclePlus = geObjectComposedName(IUILittoSim.ACTION_RECAP_CYCLE_PLUS, name);
            string ActionTitre = geObjectComposedName(IUILittoSim.ACTION_RECAP_TITRE, name);

            panelChild.name = name;
            panelChild.transform.position = getAtRecapActionPanelPosition();
            panelChild.transform.SetParent(panelParent.transform);
            panelChild.transform.Find(IUILittoSim.ACTION_RECAP_TITRE).transform.name = ActionTitre;
            panelChild.transform.Find(IUILittoSim.ACTION_RECAP_CYCLE_ICON).transform.name = CycleIcon;
            panelChild.transform.Find(IUILittoSim.ACTION_RECAP_VALIDE_ICON).transform.name = ValideIcon;
            panelChild.transform.Find(IUILittoSim.ACTION_RECAP_CYCLE_PLUS).transform.name = CyclePlus;

            panelChild.transform.Find(ActionTitre).GetComponent<Text>().text = (texte);
            panelChild.transform.Find(CycleIcon).transform.Find(IUILittoSim.ACTION_RECAP_ROUND).GetComponent<Text>().text = (delay.ToString());

            GameObject.Find(CycleIcon).SetActive(false);
            GameObject.Find(ValideIcon).SetActive(false);
            GameObject.Find(CyclePlus).SetActive(false);

            recapActionsList.Add(panelChild);

            if (recapActionsList.Count >= 5)
            {
                RectTransform rt = panelParent.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, (rt.sizeDelta.y + ((recapActionsList.Count - 5) * lineHeight)));
            }
            recapActionCounter++;
            updateRecapActionPosition();
        }

        public void newInfoMessage(object args)
        {
            object[] obj = (object[])args;
            int type = Int32.Parse((string)obj[0]);
            string name = (string)obj[1];
            string texte = (string)obj[2];

            GameObject panelParent = GameObject.Find(IUILittoSim.MESSAGES_PANEL);
            GameObject panelChild = Instantiate(MessagePanelPrefab);
            panelChild.name = name;
            panelChild.transform.position = getAtMessagePanelPosition();
            panelChild.transform.SetParent(panelParent.transform);
            panelChild.transform.Find(IUILittoSim.MESSAGE_TITRE).GetComponent<Text>().text = (texte);
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
                    lastMessagePosition.y = lastMessagePosition.y - lineHeight;
                }
            }

            messageCounter++;
        }

        public void setInitialBudget(object args)
        {
            object[] obj = (object[])args;
            string value = (string)obj[0];
            GameObject.Find(IUILittoSim.BUDGET_INITIAL).GetComponent<Text>().text = value;
        }

        public void setRemainingBudget(object args)
        {
            object[] obj = (object[])args;
            string value = (string)obj[0];
            GameObject.Find(IUILittoSim.BUDGET_RESTANT).GetComponent<Text>().text = value;
        }

        public void setValiderMontant(string montant)
        {
            if (GameObject.Find(IUILittoSim.BUDGET_ACTION_LIST))
                GameObject.Find(IUILittoSim.BUDGET_ACTION_LIST).GetComponent<Text>().text = montant;
        }

        public void setValidActionActiveIcon(object args)
        {
            object[] obj = (object[])args;
            string actionName = (string)obj[0];
            bool icon1 = Boolean.Parse((string)obj[1]);
            bool icon2 = Boolean.Parse((string)obj[2]);
            bool icon3 = Boolean.Parse((string)obj[3]);

            Debug.Log("The action name to set valid icon is : --->  " + geObjectComposedName(IUILittoSim.ACTION_RECAP_VALIDE_ICON, actionName));
            GameObject Parent = GameObject.Find(actionName);

            Parent.transform.Find(geObjectComposedName(IUILittoSim.ACTION_RECAP_VALIDE_ICON, actionName)).gameObject.SetActive(icon1);
            Parent.transform.Find(geObjectComposedName(IUILittoSim.ACTION_RECAP_CYCLE_ICON, actionName)).gameObject.SetActive(icon2);
            Parent.transform.Find(geObjectComposedName(IUILittoSim.ACTION_RECAP_CYCLE_PLUS, actionName)).gameObject.SetActive(icon3);
        }

        public void setValidActionText(object args)
        {
            object[] obj = (object[])args;
            string actionName = (string)obj[0];
            string value1 = (string)obj[1];
            string value2 = (string)obj[2];

            GameObject Parent = GameObject.Find(actionName);
            Debug.Log("Change for the value of: " + geObjectComposedName(IUILittoSim.ACTION_RECAP_CYCLE_ICON, actionName) + " and put the value: " + value1);

            //Parent.transform.Find(IUILittoSim.ACTION_RECAP_VALIDE_ICON+"_"+actionName).GetComponentgameObject.SetActive(icon1);
            GameObject OB = Parent.transform.Find(geObjectComposedName(IUILittoSim.ACTION_RECAP_CYCLE_ICON, actionName)).gameObject;
            OB.transform.Find(IUILittoSim.ACTION_RECAP_ROUND).GetComponent<Text>().text = value1;
            OB = Parent.transform.Find(geObjectComposedName(IUILittoSim.ACTION_RECAP_CYCLE_PLUS, actionName)).gameObject;
            OB.transform.Find(IUILittoSim.ACTION_RECAP_CYCLE_PLUS_NBR).GetComponent<Text>().text = value2;
        }

        public string geObjectComposedName(string constVar, string name)
        {
            return constVar + "_" + name;
        }

    }

}
