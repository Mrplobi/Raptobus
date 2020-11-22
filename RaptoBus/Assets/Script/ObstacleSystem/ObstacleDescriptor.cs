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

    public enum ObstacleType { Box, Miku, Large, Raptor}
}