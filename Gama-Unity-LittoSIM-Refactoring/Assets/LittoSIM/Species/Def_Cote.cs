using System;
using ummisco.gama.unity.GamaAgent;
using UnityEngine;

namespace ummisco.gama.unity.littosim.Agents
{
    public class Def_Cote : Agent
    {
        int dike_id;
        string type;
        string commune_name_shpfile;
        //Color color = ; //
        float height;
        bool ganivelle = false;
        float alt = 0f; 
        string status;  //  "bon" "moyen" "mauvais" 
        int length_def_cote;

        public Def_Cote()
        {


        }

        public void Start()
        {
             gameObject.GetComponent<Renderer>().material.color = Color.red;
        }


    }
}
