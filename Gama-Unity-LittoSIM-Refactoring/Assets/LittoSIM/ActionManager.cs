using System;
using System.Collections.Generic;
using System.IO;
using ummisco.gama.unity.littosim.ActionPrefab;
using ummisco.gama.unity.SceneManager;
using UnityEngine;

namespace ummisco.gama.unity.littosim
{
    public class ActionManager : MonoBehaviour
    {

        public Dictionary<string, Action> actions_dic = new Dictionary<string, Action>();
        public string path = IGamaManager.RESOURCES_PATH + "actions.csv";

        public ActionManager()
        {
            // Debug.Log("--  --  --  --  > The action code is " );
        }


        public void Start()
        {
            StreamReader reader = new StreamReader(path);

            string fileContent = reader.ReadToEnd();
            actions_dic = GetActionsList(fileContent);

            Dictionary<string, Action> us_actions_dic = GetUAActionsList();
            Dictionary<string, Action> def_cote_actions_dic = GetDefCotActionsList();

            Debug.Log("Actions total is " + actions_dic.Count);

            Debug.Log("UA Actions total is " + us_actions_dic.Count);

            SetUpUAActions(us_actions_dic);
            SetUpDefCoteActions(def_cote_actions_dic);


            Debug.Log("Def Cote total is " + def_cote_actions_dic.Count);

        }

        public Dictionary<string, Action> GetActionsList(string fileContent)
        {
            Dictionary<string, Action> actions_list = new Dictionary<string, Action>();
            string[] lines = fileContent.Split("\n"[0]);

            for (int i = 0; i < lines.Length; i++)
            {
                Action act = GetActionElement(lines[i]);
                //Debug.Log("----->  The langue element is  " + langueElement.ToString());
                if ((act.def_cote_index >= 1) || (act.UA_index >= 1))
                {
                    actions_list.Add(act.name, act);
                }
            }
            return actions_list;
        }

        public static Action GetActionElement(string line)
        {
            string[] splitString = line.Split(new string[] { ";" }, StringSplitOptions.None);

            Action act = new Action();
            act.name = splitString[0];
            if (!Int32.TryParse(splitString[1], out act.code)) { act.code = -1; }
            act.button_help_message = splitString[5];
            act.button_icon_file = splitString[6];
            if (!Int32.TryParse(splitString[7], out act.def_cote_index)) { act.def_cote_index = -1; }
            if (!Int32.TryParse(splitString[8], out act.UA_index)) { act.UA_index = -1; }

            return act;
        }


        public Dictionary<string, Action> GetUAActionsList()
        {
            Dictionary<string, Action> ua_actions_list = new Dictionary<string, Action>();

            foreach (KeyValuePair<string, Action> act in actions_dic)
            {
                if (act.Value.UA_index >= 1)
                {
                    ua_actions_list.Add(act.Key, act.Value);
                }
            }
            return ua_actions_list;
        }

        public Dictionary<string, Action> GetDefCotActionsList()
        {
            Dictionary<string, Action> def_cote_actions_list = new Dictionary<string, Action>();

            foreach (KeyValuePair<string, Action> act in actions_dic)
            {
                if (act.Value.def_cote_index >= 1)
                {
                    def_cote_actions_list.Add(act.Key, act.Value);
                }
            }
            return def_cote_actions_list;
        }

        public void SetUpUAActions(Dictionary<string, Action>  actions)
        {

            GameObject Ua_Panel = GameObject.Find(IUILittoSim.UA_PANEL);

            foreach (KeyValuePair<string, Action> act in actions)
            {
                GameObject action_button = Instantiate(GameObject.Find(ILittoSimConcept.LITTOSIM_MANANGER).GetComponent<LittosimManager>().ButtonActionPrefab);
                action_button.name = "UA_"+act.Key;
                Vector3 position = IActionButton.GetPosition(act.Value.UA_index);
                action_button.GetComponent<Button_Action_Prefab>().SetUp("UA_" + act.Key, act.Value.UA_index, act.Value.button_help_message, act.Value.button_icon_file, "UA", IActionButton.GetPosition(act.Value.UA_index));
                action_button.transform.SetParent(Ua_Panel.transform);
            }
        }

        public void SetUpDefCoteActions(Dictionary<string, Action> actions)
        {

            GameObject Def_Cote_Panel = GameObject.Find(IUILittoSim.DEF_COTE_PANEL);

            foreach (KeyValuePair<string, Action> act in actions)
            {
                GameObject action_button = Instantiate(GameObject.Find(ILittoSimConcept.LITTOSIM_MANANGER).GetComponent<LittosimManager>().ButtonActionPrefab);
                action_button.name = "Def_Cote_" + act.Key;
                Vector3 position = IActionButton.GetPosition(act.Value.def_cote_index);
                action_button.GetComponent<Button_Action_Prefab>().SetUp("Def_Cote_" + act.Key, act.Value.def_cote_index, act.Value.button_help_message, act.Value.button_icon_file, "Def_Cote", IActionButton.GetPosition(act.Value.def_cote_index));
                action_button.transform.SetParent(Def_Cote_Panel.transform);
            }
        }

      

    }
}
