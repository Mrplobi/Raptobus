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

        protected float speedModifier = 1;
        protected float speedCoeff = 0.8f;

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
            if(lvl != 3)
            {
                speedModifier = lvl * speedCoeff;
            }
            // Slow lvl 3 a bit
            else
            {
                speedModifier = lvl * speedCoeff * 0.8f;
            }

            gameObject.SetActive(true);
            available = false;
        }
    }
}
