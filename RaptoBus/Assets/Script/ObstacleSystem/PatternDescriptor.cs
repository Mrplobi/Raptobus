using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    [CreateAssetMenu(fileName = "Pattern", menuName = "PatternDescriptor", order = 0)]
    public class PatternDescriptor : ScriptableObject
    {
        public List<ObstacleDescriptor> obstacles;
        public int numberOfRaptors;
    }
}
