using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class Raptor : MonoBehaviour
    {
        public float speed;

        void Update()
        {
            if (GameManager.Instance.playing)
            {
                transform.position += new Vector3(speed, 0, 0);
            }
        }
    }
}
