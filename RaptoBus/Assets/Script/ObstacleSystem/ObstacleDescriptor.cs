using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    [System.Serializable]
    public class ObstacleDescriptor
    {
        public ObstacleType type;
        public float offset;
    }

    public enum ObstacleType { Raptor, Box, Madorobo1, Madorobo2, OpenGL, Foot }
}