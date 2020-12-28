using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class Obstacle : MonoBehaviour
    {
        [Header("Movement")]
        public float speed = -10;

        [Header("Pool Realated")]
        public bool available = true;
        public float spawnHeight;
        public ObstacleType type;

        private float speedModifier = 1;
        private float speedCoeff = 0.8f;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (GameManager.Instance.Playing)
            {
                transform.position += new Vector3(speed*Time.deltaTime*speedModifier, 0, 0);
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

        internal void Launch(int lvl)
        {
            speedModifier = lvl*speedCoeff;
            gameObject.SetActive(true);
            available = false;
        }
    }
}
