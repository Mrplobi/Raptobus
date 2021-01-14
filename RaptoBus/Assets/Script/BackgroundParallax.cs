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
        public float accelerationRate = 0.01f;

        private float length;
        private Vector3 startPos;

        private float dist;
        private Vector3 newPos;

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            Debug.LogError("Length : " + name + " " + length);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (GameManager.Instance.Playing)
            {
                newPos = transform.position - new Vector3(parallaxSpeed * coefSpeed * Time.fixedDeltaTime, 0);
                if(transform.position.x <= -length)
                {
                    transform.position = new Vector3(31.7f*3,transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = newPos;
                }
            }
        }


        // Accelerate from current speed to newSpeed
        public IEnumerator Accelerate(float newSpeed)
        {
            while (coefSpeed < newSpeed)
            {
                coefSpeed += (newSpeed - coefSpeed) * Time.fixedDeltaTime;
                //coefSpeed += accelerationRate * Time.fixedDeltaTime;
                yield return new WaitForEndOfFrame();
            }
        }


        // Reset to initial speed
        public void ResetSpeed()
        {
            coefSpeed = 1f;
        }
    }
}
