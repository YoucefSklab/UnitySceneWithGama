using System;
using UnityEngine;
using UnityEngine.UI;

namespace ummisco.gama.unity.littosim.ActionPrefab
{
    public class Button_Action_Prefab : MonoBehaviour
    {
        public string action_name;
        public int code;
        public string button_help_message;
        public string button_icon;
        public string type;
        public Vector3 position;


        public Button_Action_Prefab(string action_name, int action_code, string msg_help, string icon, string type, Vector3 position)
        {
            this.action_name = action_name;
            code = action_code;
            button_help_message = msg_help;
            button_icon = icon;
            this.type = type;
            this.position = position;

            Debug.Log("--  --  --  --  > The action code is " + code);
        }

        public void SetUp(string action_name, int action_code, string msg_help, string icon, string type, Vector3 position)
        {
            this.action_name = action_name;
            code = action_code;
            button_help_message = msg_help;
            button_icon = icon;
            this.type = type;
            this.position = position;

            Sprite ImageIcon = Resources.Load<Sprite>(icon);
            gameObject.GetComponent<Image>().overrideSprite = ImageIcon;
            gameObject.transform.position = position;
        }

        public void onAddButtonClicked()
        {
            Debug.Log("--  --  --  --  > The action code is " + code);// + action.code);
            LittosimManager.actionToDo = code;
        }


    }
}
