
using System.Collections.Generic;
using Nextzen.VectorData;

namespace ummisco.gama.unity.GamaAgent
{
    public struct GamaCoordinateSequence
    {

        public List<Point> Point { get; set; }

        public GamaCoordinateSequence(List<Point> coordinates)
        {
            this.Point = coordinates;
        }

        public override string ToString()
        {
            string listInString = "[";
            foreach (var el in Point)
            {
                listInString += string.Format("({0}, {1}, {2}),", el.X, el.Y, el.Z);
            }
            listInString +="]";
            return listInString;
        }


    }
}