using System;
namespace ummisco.gama.unity.littosim
{
    public class Action
    {
        public string name { get; set; }
        public int code { get; set; }
        public int delay { get; set; }
        public float cost { get; set; }
        public string entity { get; set; }
        public string button_help_message { get; set; }
        public string button_icon_file { get; set; }

        public Action()
        {

        }
    }
}
