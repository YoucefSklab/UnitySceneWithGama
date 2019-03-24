using System;
using UnityEngine;

namespace ummisco.gama.unity.littosim
{

    public class IActionButton
    {
        public static float max_x = 1280;
        public static float min_x = -1510;
        public static int action_nbr = 10;

        public static float y = 1300f;
        public static float z = 0f;

        /*
        public static Vector3 position1 = new Vector3(-1222, y, z);
        public static Vector3 position2 = new Vector3(-944, y, z);
        public static Vector3 position3 = new Vector3(-666, y, z);
        public static Vector3 position4 = new Vector3(-388, y, z);
        public static Vector3 position5 = new Vector3(-110, y, z);
        public static Vector3 position6 = new Vector3(168, y, z);
        public static Vector3 position7 = new Vector3(446, y, z);
        public static Vector3 position8 = new Vector3(724, y, z);
        public static Vector3 position9 = new Vector3(1002, y, z);
        public static Vector3 position10 = new Vector3(1280, y, z);
        */

        public static Vector3 GetPosition(int pos_nbr)
        {
            float x = 0f;
            if (pos_nbr >= 2)
            {
                x = (((max_x + (-1 * min_x)) / (action_nbr + 1)) * (pos_nbr - 1 ) ) + min_x;
                return new Vector3(x, y, z);
            }
            else
            {
                return new Vector3(min_x, y, z);
            }
           
        }

    }
}
