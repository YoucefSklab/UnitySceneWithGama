
namespace ummisco.gama.unity.datatype
{
    public struct GamaColor
    {

        public float value { get; set; }
        public float falpha { get; set; }
        public string name { get; set; }

        public GamaColor(float value, float falpha, string name)
        {
            this.value = value;
            this.falpha = falpha;
            this.name = name;
        }


        public override string ToString(){
            return string.Format("({0}, {1}, {2})", value, falpha,name);
        }


    }
}