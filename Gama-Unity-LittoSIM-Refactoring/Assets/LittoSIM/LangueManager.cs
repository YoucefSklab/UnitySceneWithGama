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
            string lng = "en";
            GameObject obj = null;
            obj = GameObject.Find(IGamaManager.CSV_READER);
            obj.SendMessage("loadCSVFile");
            langueDic = obj.GetComponent<CSVReader>().langueDic;
           
            //Debug.Log("The disctionnary length is " + langueDic.Count);
            //Debug.Log(" -------------------------------> " + GetLangueElementValue(langueDic, ILangue.MSG_INITIAL_BUDGET, "en", ILangue.MSG_INITIAL_BUDGET));

            GameObject.Find(ILittoSimConcept.MSG_INITIAL_BUDGET).GetComponent<Text>().text = GetLangueElementValue(langueDic, "MSG_INITIAL_BUDGET", lng,  ILangue.MSG_INITIAL_BUDGET);
            GameObject.Find(ILittoSimConcept.MSG_REMAINING_BUDGET).GetComponent<Text>().text = GetLangueElementValue(langueDic, "MSG_REMAINING_BUDGET", lng, ILangue.MSG_REMAINING_BUDGET);
            GameObject.Find(ILittoSimConcept.LEGEND_UNAM).GetComponentInChildren<Text>().text = "  "+GetLangueElementValue(langueDic, "LEGEND_UNAM", lng, ILangue.LEGEND_UNAM);
            GameObject.Find(ILittoSimConcept.LEGEND_DYKE).GetComponentInChildren<Text>().text = "  " + GetLangueElementValue(langueDic, "LEGEND_DYKE", lng, ILangue.LEGEND_DYKE);
            GameObject.Find(ILittoSimConcept.LEGEND_NAME_ACTIONS).GetComponent<Text>().text = GetLangueElementValue(langueDic, "LEGEND_NAME_ACTIONS", lng, ILangue.LEGEND_NAME_ACTIONS);

        }

        public string GetLangueElementValue(Dictionary<string, Langue> dic, string elementName, string langue, string defaultName)
        {
            Langue tempElement = null;
            if (dic.TryGetValue(elementName, out tempElement))
            {
                if (langue.Equals("fr"))
                {
                    return tempElement.Element_fr;
                }
                else if (langue.Equals("en"))
                {
                    return tempElement.Element_en;
                }
            }
 
            return defaultName;
        }
    }
}
