﻿using System;
using UnityEngine;

namespace Nextzen.Unity
{
    [Serializable]
    public struct PolygonOptions
    {
        public Material Material;
        public ExtrusionType Extrusion;
        public UVMode UVMode;
        public float MinHeight;
        public float MaxHeight;

        public bool Enabled;
    }
}