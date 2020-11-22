using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class Obstacle : MonoBehaviour
    {
        [Header("Movement")]
        public float speed;

        [Header("Pool Realated")]
        public bool available = true;
        public float spawnHeight;

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.playing)
            {
                transform.position += new Vector3(speed, 0, 0);
            }
        }

        private void OnBecameInvisible()
        {
            Free();
        }

        public void Free()
        {
            gameObject.SetActive(false);
            available = true;
        }

        internal void Launch()
        {
            gameObject.SetActive(true);
            available = false;
        }
    }
}
