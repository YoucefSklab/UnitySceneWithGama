
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
            if (Points.Count == 0) { return null; }
            Vector2[] coord = new Vector2[Points.Count];
            foreach (Point p in Points)
            {
                coord[Points.IndexOf(p)] = new Vector2(p.X, p.Y); ;
            }
            return coord;
        }

        public Vector3[] getVector3Coordinates()
        {
            if (Points.Count == 0) { return null; }
            Vector3[] coord = new Vector3[Points.Count];
            foreach (Point p in Points)
            {
                coord[Points.IndexOf(p)] = new Vector3(p.X, p.Y, p.Z);
            }
            return coord;
        }

    }
}