using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ground detection variables")]
    public float groudDetectDistance;
    public float height;

    [Header("Jump")]
    public float jumpPower;
    public float fallSpeed;

    bool ascending;

    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log(Input.GetAxis("Jump"));
        Jump();
        FastFall();
    }

    public bool isGrounded()
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

    public void Jump()
    {
        if(isGrounded() && Input.GetAxis("Jump") == 1f)
        {
            ascending = true;
            rigidbody.velocity = new Vector3(0, jumpPower);
        }
        else if(ascending)
        {
            if (rigidbody.velocity.y < 0)
            {
                ascending = false;
            }
            else if(Input.GetAxis("Jump") != 1f)
            {
                rigidbody.velocity = new Vector3(0, rigidbody.velocity.y/2);
                ascending = false;
            }
        }
    }

    private void FastFall()
    {
        if(!isGrounded() && Input.GetAxis("Jump") == -1f)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y - fallSpeed);
        }
    }
}
