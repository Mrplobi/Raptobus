using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class Player : MonoBehaviour
    {
        [Header("Ground detection variables")]
        public float groudDetectDistance;
        public float height;

        [Header("Jump")]
        public float jumpPower;
        public float fallSpeed;

        private int raptorCount = 0;

        bool ascending;

        Rigidbody body;

        public int RaptorCount { get => raptorCount; private set => raptorCount = value; }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.playing)
            {
                //Debug.Log(Input.GetAxis("Jump"));
                Jump();
                FastFall();
            }
        }

        private bool isGrounded()
        {
            if (ascending) { return false; }
            Ray ray = new Ray(transform.position - new Vector3(0, height, 0), -transform.up);
            Debug.DrawRay(transform.position - new Vector3(0, height, 0), -transform.up);
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, groudDetectDistance, mask))
            {
                return true;
            }
            return false;
        }

        private void Jump()
        {
            if (isGrounded() && Input.GetAxis("Jump") == 1f)
            {
                ascending = true;
                body.velocity = new Vector3(0, jumpPower);
            }
            else if (ascending)
            {
                if (body.velocity.y <= 0)
                {
                    ascending = false;
                }
                else if (Input.GetAxis("Jump") != 1f)
                {
                    body.velocity = new Vector3(0, body.velocity.y / 2);
                    ascending = false;
                }
            }
        }

        private void FastFall()
        {
            if (!isGrounded() && Input.GetAxis("Jump") == -1f)
            {
                body.velocity = new Vector3(0, body.velocity.y - fallSpeed);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Raptor>() != null)
            {
                AddRaptor();
            }
            else if (other.GetComponent<Obstacle>() != null)
            {
                GameManager.Instance.Defeat();
            }
        }

        private void AddRaptor()
        {
            RaptorCount++;
            Debug.Log("Raptor Get! " + raptorCount + "/30");
        }
    }
}
