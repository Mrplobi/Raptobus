using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField]
    private float parallaxSpeed;

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
        dist = Mathf.Repeat(Time.time * parallaxSpeed, length);
        newPos = new Vector3(dist, 0, 0);
        transform.position = startPos - newPos;
    }
}
