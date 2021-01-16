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
        // Delta to avoid gaps in between parallex rep
        public float delta = 0.1f;

        public bool isAccelerationStopped = false;

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (GameManager.Instance.Playing)
            {
                newPos = transform.position - new Vector3(parallaxSpeed * coefSpeed * Time.fixedDeltaTime, 0);
                if(newPos.x <= -length)
                {
                    transform.position = new Vector3(length * 4 * transform.localScale.x, transform.position.y, transform.position.z) - new Vector3(delta, 0, 0);
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
                if (isAccelerationStopped)
                {
                    yield break;
                }
                coefSpeed += (newSpeed - coefSpeed) * Time.fixedDeltaTime;
                yield return new WaitForEndOfFrame();
            }
            coefSpeed = newSpeed;
        }


        // Reset to initial speed
        public void ResetSpeed()
        {
            isAccelerationStopped = true;
            coefSpeed = 1f;
            transform.position = startPos;
            Debug.Log("pass in reset");
        }
    }
}
