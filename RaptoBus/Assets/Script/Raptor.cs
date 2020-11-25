using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class Raptor : MonoBehaviour
    {
        public float speed;

        /*
         * 
         * 
         * TODO
         * 
         * Keep the update movement for when raptor is missed and runs after bus
         * Otherwise raptor is considered a simple obstacle
         * 
         * 
        void Update()
        {
            if (GameManager.Instance.playing)
            {
                transform.position += new Vector3(speed, 0, 0);
            }
        }
        */
    }
}
