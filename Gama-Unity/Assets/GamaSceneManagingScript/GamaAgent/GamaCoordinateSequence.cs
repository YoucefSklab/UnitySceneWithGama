
using System.Collections.Generic;
using Nextzen.VectorData;
using UnityEngine;

namespace ummisco.gama.unity.GamaAgent
{
    public struct GamaCoordinateSequence
    {

        public List<Point> Points { get; set; }

        public GamaCoordinateSequence(List<Point> coordinates)
        {
            this.Points = coordinates;
        }

        public override string ToString()
        {
            string listInString = "[";
            foreach (var el in Points)
            {
                listInString += string.Format("({0}, {1}, {2}),", el.X, el.Y, el.Z);
            }
            listInString += "]";
            return listInString;
        }

        public Vector2[] getVector2Coordinates()
        {
            Debug.Log("Total Points is -> "+Points.Count);
            if(Points.Count == 0) {return null;}
            Vector2[] coord = new Vector2[Points.Count];
            for (int i = 0; i < Points.Count; i++)
            {
                Vector2 vect = new Vector2(Points[i].X, Points[i].Y);
                coord[i] = vect;
            }
            return coord;
        }


    }
}