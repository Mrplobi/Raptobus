using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class Ending : MonoBehaviour
    {
        public Vector2 endingPosition;
        public float speed;

        private void Start()
        {
            GameManager.Instance.onReset += RemoveObject;
        }

        private void Update()
        {
            if (transform.localPosition.x > endingPosition.x)
            {
                transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }

        public void RemoveObject()
        {
            GameManager.Instance.onReset -= RemoveObject;
            Destroy(gameObject);
        }
    }    
}