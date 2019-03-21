/*
    CSVReader by Dock. (24/8/11)
    http://starfruitgames.com
 
    usage: 
    CSVReader.SplitCsvGrid(textString)
 
    returns a 2D string array. 
 
    Drag onto a gameobject for a demo of CSV parsing.
*/

using UnityEngine;
using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using ummisco.gama.unity.littosim;
using System;

public class CSVReader : MonoBehaviour
{
    public string path = "Assets/ressources/langs_def.csv";
    public Dictionary<string, Langue> langueDic = new Dictionary<string, Langue>();

    public void Start()
    {
        StreamReader reader = new StreamReader(path);
        string fileContent = reader.ReadToEnd();
        langueDic = GetInDictionnary(fileContent);

        SetLangue("MSG_IMPOSSIBLE_DELETE_ADAPTED", "fr");
        SetLangue("MSG_IMPOSSIBLE_DELETE_ADAPTED", "en");
    }

    public void loadCSVFile()
    {
        StreamReader reader = new StreamReader(path);
        string fileContent = reader.ReadToEnd();
        langueDic = GetInDictionnary(fileContent);
    }

    // get csv langue in dictionnary
    static public Dictionary<string, Langue> GetInDictionnary(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);
        Dictionary<string, Langue> langue = new Dictionary<string, Langue>();

        string allFile = "";

        for (int i = 0; i < lines.Length; i++)
        {
            Langue langueElement = GetLangueElements(lines[i]);
            //Debug.Log("----->  The langue element is  " + langueElement.ToString());
            langue.Add(langueElement.Element, langueElement);
            allFile += "public static string "+langueElement.Element + " = \"" + langueElement.Element_fr + "\"; \n";
        }

        //Debug.Log("----->  All is " + allFile);
        //Debug.Log("----->  The lines length is " + lines.Length);
       
        return langue;
    }



    public static Langue GetLangueElements(string line)
    {
        string langueElement = "";
        string fr = "";
        string en = "";

        string[] splitString = line.Split(new string[] { ";"}, StringSplitOptions.None);

        langueElement = splitString[0];
        fr = splitString[1];
        en = splitString[2];

        return new Langue(langueElement, fr, en);
    }

    public void SetLangue(string langueElement, string langue)
    {
        Langue tempElement = null;
        if (langueDic.TryGetValue(langueElement, out tempElement))
        {
            if (langue.Equals("fr"))
            {
                Debug.Log("The element is : " + tempElement.Element + " and value is " + tempElement.Element_fr);
            }
            else if (langue.Equals("en"))
            {
                Debug.Log("The element is : " + tempElement.Element + " and value is " + tempElement.Element_en);
            }

        }
    }

}