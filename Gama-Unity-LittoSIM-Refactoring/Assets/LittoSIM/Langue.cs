using System;
namespace ummisco.gama.unity.littosim
{
    public class Langue
    {
        public string Element { get; set; }
        public string Element_fr { get; set; }
        public string Element_en { get; set; }

        public Langue(string element, string fr, string en)
        {
            this.Element = element;
            this.Element_en = en;
            this.Element_fr = fr;
        }

        public string ToString()
        {
            return ("Langue element: " + this.Element + " fr element: " + Element_fr + " en element: " + Element_en);
        }

    }
}
