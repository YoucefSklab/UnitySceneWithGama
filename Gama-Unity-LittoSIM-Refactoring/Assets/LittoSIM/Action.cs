using System;
namespace ummisco.gama.unity.littosim
{
    public class Action
    {
        public string name ;
        public int code ;
        public string button_help_message;
        public string button_icon_file;
        public int def_cote_index;
        public int UA_index;


        public Action()
        {

        }

        public Action(string name, int code, int delay, float cost, string entity, string help, string icon, int def_index, int ua_index)
        {
            this.name = name;
            this.code = code;
            this.button_help_message = help;
            this.button_icon_file = icon;
            this.def_cote_index = def_index;
            this.UA_index = ua_index;
        }
    }
}
