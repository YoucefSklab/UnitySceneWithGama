﻿using UnityEngine;
using System.Collections;


namespace ummisco.gama.unity.littosim
{
    public class UIManager : MonoBehaviour
    {
        // Use this for initialization

        public string activePanel = IUILittoSim.MAP_PANEL;

        public UIManager()
        {

        }
        void Awake()
        {
            activePanel = IUILittoSim.MAP_PANEL;
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetUp()
        {

        }

        public void setActivePanel(string panelName)
        {
            if (panelName.Equals(IUILittoSim.ONGLET_AMENAGEMENT))
            {
                setCanvasVisible(IUILittoSim.MAP_PANEL);
                setCanvasInvisible(IUILittoSim.DEFENSE_PANEL);

                SetTargetInvisible(GameObject.Find(IUILittoSim.DEFENSE_PANEL));
                SetTargetVisible(GameObject.Find(IUILittoSim.MAP_PANEL));

                activePanel = IUILittoSim.MAP_PANEL;
            }
            else if (panelName.Equals(IUILittoSim.ONGLET_DEFENSE))
            {
                setCanvasVisible(IUILittoSim.DEFENSE_PANEL);
                setCanvasInvisible(IUILittoSim.MAP_PANEL);

                SetTargetInvisible(GameObject.Find(IUILittoSim.MAP_PANEL));
                SetTargetVisible(GameObject.Find(IUILittoSim.DEFENSE_PANEL));

                activePanel = IUILittoSim.DEFENSE_PANEL;
            }
        }

        void SetTargetInvisible(GameObject Target)
        {
            foreach (Renderer r in Target.GetComponentsInChildren(typeof(Renderer)))
            {
                r.enabled = false;
            }
        }

        void SetTargetVisible(GameObject Target)
        {
            foreach (Renderer r in Target.GetComponentsInChildren(typeof(Renderer)))
            {
                r.enabled = true;
            }
        }

        public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
        {
            //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
            // Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            //Vector3 screenPos = Camera.main.ScreenToWorldPoint(worldPos);
            Vector3 screenPos = worldPos;
            Vector2 movePos;

            //Convert the screenpoint to ui rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
            //Convert the local point to world point
            return parentCanvas.transform.TransformPoint(movePos);
        }

        public void setCanvasVisible(string name)
        {
            GameObject panel = GameObject.Find(name);
            CanvasGroup canvas = panel.GetComponent<CanvasGroup>();
            canvas.alpha = 1f;
            canvas.blocksRaycasts = true;
        }

        public void setCanvasInvisible(string name)
        {
            GameObject panel = GameObject.Find(name);
            CanvasGroup canvas = panel.GetComponent<CanvasGroup>();
            canvas.alpha = 0f;
            canvas.blocksRaycasts = false;
        }

        public string getActivePanel()
        {
            //if (activePanel != null) 
            return activePanel;
            /*
            if (activePanel != null)
            {
                Debug.Log("The methode getActivePanel is called, and the value of local variable is " + activePanel);
            }
            else
            {
                Debug.Log("The methode getActivePanel is called, but the local value is null");
            }

            return IUILittoSim.MAP_PANEL;
            */
        }

    }
}