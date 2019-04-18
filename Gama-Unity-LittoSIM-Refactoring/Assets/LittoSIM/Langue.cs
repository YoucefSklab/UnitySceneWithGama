using System;
namespace ummisco.gama.unity.littosim
{
    public class Langue
    {
        public string element;
        public string value; 
       

        public Langue(string element, string value)
        {
            this.element = element;
            this.value = value;
           
        }

        public string ToString()
        {
            return ("Langue element: " + this.element + " value: " + this.value);
        }

    }
}
