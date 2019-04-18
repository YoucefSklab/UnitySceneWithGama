using System;
using System.Collections.Generic;
using ummisco.gama.unity.SceneManager;
using UnityEngine;
using UnityEngine.UI;

namespace ummisco.gama.unity.littosim
{
    public class LangueManager : MonoBehaviour
    {
        public static string langue = "fr";
        public Dictionary<string, Langue> langueDic = new Dictionary<string, Langue>();

        public LangueManager()
        {

        }

        void Start()
        {
            string lng = "fr";
            GameObject obj = null;
            obj = GameObject.Find(IGamaManager.CSV_READER);
            obj.GetComponent<CSVReader>().lng = lng;
            obj.SendMessage("loadCSVFile");
            langueDic = obj.GetComponent<CSVReader>().langueDic;
            SetUpLangueDictionnary();

            //Debug.Log("The disctionnary length is " + langueDic.Count);
            //Debug.Log(" -------------------------------> " + GetLangueElementValue(langueDic, ILangue.MSG_INITIAL_BUDGET, "en", ILangue.MSG_INITIAL_BUDGET));

            GameObject.Find(ILittoSimConcept.MSG_INITIAL_BUDGET).GetComponent<Text>().text = ILangue.GetLangueElement(ILangue.MSG_INITIAL_BUDGET);
            GameObject.Find(ILittoSimConcept.MSG_REMAINING_BUDGET).GetComponent<Text>().text = ILangue.GetLangueElement(ILangue.MSG_REMAINING_BUDGET); 
            GameObject.Find(ILittoSimConcept.LEGEND_UNAM).GetComponentInChildren<Text>().text = "  " + ILangue.GetLangueElement(ILangue.LEGEND_UNAM);
            GameObject.Find(ILittoSimConcept.LEGEND_DYKE).GetComponentInChildren<Text>().text = "  " + ILangue.GetLangueElement(ILangue.LEGEND_DYKE);
            GameObject.Find(ILittoSimConcept.LEGEND_NAME_ACTIONS).GetComponent<Text>().text = ILangue.GetLangueElement(ILangue.LEGEND_NAME_ACTIONS);

            ILangue.GetAllAsVariables();

        }

        public string GetLangueElementValue(Dictionary<string, Langue> dic, string elementName, string langue, string defaultName)
        {
            Langue tempElement = null;
            if (dic.TryGetValue(elementName, out tempElement))
            {
                if (langue.Equals("fr"))
                {
                    return tempElement.value;
                }
                else if (langue.Equals("en"))
                {
                    return tempElement.value;
                }
            }
            return defaultName;
        }

        public void SetUpLangueDictionnary()
        {
            ILangue.current_langue.Clear();
            foreach (KeyValuePair<string, Langue> lng in langueDic)
            {
                ILangue.current_langue.Add(lng.Key, lng.Value.value);
                Debug.Log("Langue element added is : " + lng.Key + " it's value is "+ lng.Value.value);
            }

            Debug.Log("The dic size is " + ILangue.current_langue.Count);

        }
    }
}
