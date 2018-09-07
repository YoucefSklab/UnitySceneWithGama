using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Nextzen.VectorData;

namespace ummisco.gama.unity.GamaAgent
{
    public class Agent
    {

        public string agentName {set; get;}
        public GamaCoordinateSequence agentCoordinate {set; get;}
        public GamaColor color {set; get;}
        public Vector3 rotation {set; get;}
        public Vector3 scale {set; get;}
        public bool isRotate {set; get;}
        public bool isOnInputMove {set; get;}
        public Rigidbody rb {set; get;}
        public float speed {set; get;}
        public string species {set; get;}
        public int index {set; get;}
        public string nature {get; set;}
        public string type {get; set;}
        public int hight {get; set;}







    }
}

