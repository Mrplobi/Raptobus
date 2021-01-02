using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        public float maxFallSpeed;

        [Header("Actions")]
        public InputActionMap busControls;

        private int raptorCount = 0;

        bool ascending = false;
        bool fastFalling = false;

        Rigidbody body;

        public static Vector3 playerPos;
        public static Vector3 playerSize;

        public int RaptorCount { get => raptorCount; private set => raptorCount = value; }

        private void Awake()
        {
            busControls["Jump"].performed += Jump;
            busControls["FastFall"].performed += FastFall;
        }

        private void Start()
        {
            body = GetComponent<Rigidbody>();
            playerPos = transform.position;
            playerSize = GetComponent<SpriteRenderer>().bounds.size;
            GameManager.Instance.onWin += Arrive;
            GameManager.Instance.onLaunch += ToggleControls;
            GameManager.Instance.onDefeat += ToggleControls;
            GameManager.Instance.onReset += ResetPos;
        }

        private void FixedUpdate()
        {
            if (fastFalling == true)
            {
                if (isGrounded())
                {
                    fastFalling = false;
                    return;
                }
                else if (body.velocity.y > maxFallSpeed)
                {
                    body.velocity = new Vector3(0, body.velocity.y - fallSpeed);
                }
            }
        }

        public void ToggleControls()
        {
            if(busControls.enabled)
            {
                busControls.Disable();
            }
            else
            {
                busControls.Enable();
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

        private void Jump(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.Playing)
            {
                if (isGrounded())
                {
                    ascending = true;
                    body.velocity = new Vector3(0, jumpPower);
                }
                else if (ascending)
                {
                    body.velocity = new Vector3(0, body.velocity.y / 2);
                    ascending = false;
                }
            }
        }

        private void FastFall(InputAction.CallbackContext context)
        {
            if (!isGrounded())
            {
                body.velocity = new Vector3(0, body.velocity.y - fallSpeed);
                fastFalling = true;
                ascending = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Raptor>() != null)
            {
                AddRaptor();
                other.GetComponent<Raptor>().Collected();
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
            UIManager.Instance.AddRaptorCount();
        }

        public void Arrive()
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetTrigger("Arrive");
            ToggleControls();
        }

        public void CallVictoryScreen()
        {
            GetComponent<Animator>().enabled = false;
            UIManager.Instance.DisplayWin();
        }

        public void ResetPos()
        {
            transform.position = playerPos;
            ToggleControls();
        }
    }
}
