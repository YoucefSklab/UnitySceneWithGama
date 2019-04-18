using System;
using ummisco.gama.unity.GamaAgent;
using UnityEngine;

namespace ummisco.gama.unity.littosim.Agents
{
    public class UA : Agent
    {

        string ua_name = "";
        int id;
        int ua_code = 0;
        //Color my_color = cell_color() update: cell_color();
        int population;
        string classe_densite = " ";
        int cout_expro = 0;
        bool isUrbanType; 
        bool isAdapte; 
        bool isEnDensification; 

        public UA()
        {

        }

         public void Start()
        {
             gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

    }
}
