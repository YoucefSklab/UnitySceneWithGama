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
        public Boolean isOn = false;

        private void FixedUpdate()
        {
            Debug.Log("The action code to do is: " + LittosimManager.actionToDo);
            if (LittosimManager.actionToDo == code)
            {
                ColorBlock cb = gameObject.GetComponent<Button>().colors;
                cb.normalColor = Color.red;
                gameObject.GetComponent<Button>().colors = cb;
            }
            else
            {
                ColorBlock cb = gameObject.GetComponent<Button>().colors;
                cb.normalColor = Color.white;
                gameObject.GetComponent<Button>().colors = cb;
            }
        }

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

        public void ShowTooltip()
        {
            if (!isOn)
            {
                GameObject showTooltip = GameObject.Find("TooltipView");
                showTooltip.SetActive(true);
                showTooltip.GetComponent<TooltipView>().SetVisible();//.SendMessage("SetVisible");
                string lng = "";

                if (ILangue.current_langue.TryGetValue(this.button_help_message, out lng)) {
                    showTooltip.GetComponent<TooltipView>().help_text = lng;
                }
                else
                {
                    showTooltip.GetComponent<TooltipView>().help_text = "??";
                }

                showTooltip.GetComponent<TooltipView>().pos = this.transform.position;
                showTooltip.SendMessage("ShowTooltip");
                isOn = true;
                Debug.Log("Tooltip showed");
            }
        }

        public void HideTooltip()
        {
            if (isOn)
            {
                GameObject showTooltip = GameObject.Find("TooltipView");
                showTooltip.GetComponent<TooltipView>().HideTooltip();//.SendMessage("HideTooltip");
                isOn = false;
            }
        }
    }
}
