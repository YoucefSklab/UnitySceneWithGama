using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ummisco.gama.unity.littosim.ActionPrefab
{
    public class TooltipView : MonoBehaviour
    {
        public string help_text = " ";
        public Vector3 pos = new Vector3();




        private void FixedUpdate()
        {
           
        }

        public bool IsActive
        {
            get
            {
                return gameObject.activeSelf;
            }
        }
        //public CanvasGroup tooltip;


        void Awake()
        {
            instance = this;
            gameObject.GetComponent<Text>().text = "ttt";
            //HideTooltip();
            SetTooltipInvisible();
        }

        public void SetVisible()
        {
            //gameObject.SetActive(true);
            SetTooltipInvisible();
        }

        public void ShowTooltip()
        {
            //Debug.Log("The game object name is " + gameObject.name);
            //Debug.Log("The game object text value is " + gameObject.GetComponent<Text>().text);
            //Debug.Log("The game object text value is " + GameObject.Find("TooltipView").GetComponent<Text>().text);
            //gameObject.SetActive(true);
            gameObject.GetComponent<Text>().text = help_text;
            transform.position = new Vector3(pos.x, pos.y - 80f, 0f);
            SetTooltipVisible();
        }

        public void HideTooltip()
        {
            //gameObject.SetActive(false);
            SetTooltipInvisible();
        }


        public void SetTooltipVisible()
        {
            CanvasGroup canvas = gameObject.GetComponent<CanvasGroup>();
            canvas.alpha = 1f;
            canvas.blocksRaycasts = true;
        }

        public void SetTooltipInvisible()
        {
            CanvasGroup canvas = gameObject.GetComponent<CanvasGroup>();
            canvas.alpha = 0f;
            canvas.blocksRaycasts = false;
        }



        // Standard Singleton Access 
        private static TooltipView instance;

        public static TooltipView Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.FindObjectOfType<TooltipView>();
                return instance;
            }
        }
    }
}