using System;
using UnityEngine;

namespace ummisco.gama.unity.littosim.ActionPrefab
{
    public class ButtonActionPrefab : MonoBehaviour
    {
        public string name { get; set; }
        public string code { get; set; }
        public string delay { get; set; }
        public float cost { get; set; }
        public string entity { get; set; }
        public string button_help_message { get; set; }
        public string button_icon_file { get; set; }


        public ButtonActionPrefab()
        {

        }

        public void test()
        {

        }
    }
}
