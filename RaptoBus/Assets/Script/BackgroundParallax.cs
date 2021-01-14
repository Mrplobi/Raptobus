using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class BackgroundParallax : MonoBehaviour
    {
        [SerializeField]
        private float parallaxSpeed;
        public float coefSpeed = 1f;

        private float length;
        private Vector3 startPos;

        private float dist;
        private Vector3 newPos;

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.Playing)
            {

                // TODO
                // Progressively augment coefSpeed

                dist = Mathf.Repeat(Time.time * parallaxSpeed * coefSpeed, length);
                newPos = new Vector3(dist, 0, 0);
                transform.position = startPos - newPos;
            }
        }


        // Accelerate from current speed to newSpeed
        public void Accelerate(float newSpeed)
        {
            // TODO
        }


        // Reset to initial speed
        public void ResetSpeed()
        {
            coefSpeed = 1f;
            // TODO
        }
    }
}
