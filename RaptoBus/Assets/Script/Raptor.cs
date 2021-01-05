using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class Raptor : Obstacle
    {

        private bool isFollowingBus = false;
        private float baseSpeed = -10;
        private static float spriteWidth;
        private bool isCollected = false;
        private AudioSource soundEffect;

        [SerializeField]
        SpriteRenderer bodyRenderer;



        private void Start()
        {
            spriteWidth = bodyRenderer.bounds.size.x;
            soundEffect = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            isFollowingBus = false;
            isCollected = false;
            speed = baseSpeed;
            transform.localScale = new Vector3(-1, 1, 1);
            GetComponent<Collider>().enabled = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetTrigger("Reset");
        }


        // If raptor becomes invisible on left of screen (ie: without being collected) > make it follow the bus
        public void OnBecameInvisible()
        {
            if (transform.position.x < -10 && !isCollected)
            {
                if (isFollowingBus == false)
                {
                    isFollowingBus = true;
                    speed = 1f;
                    transform.position = new Vector3(-17, transform.position.y, transform.position.z);
                    GetComponent<Animator>().SetBool("IsChasingBus", true);
                }
                else
                {
                    GetComponent<Collider>().enabled = false;
                    Free();
                }
            }
        }



        void Update()
        {
            // Follow bus if not collected
            if (GameManager.Instance.Playing && isFollowingBus)
            {
                transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
                //transform.position.x >= (Player.playerPos.x - Player.playerSize.x - spriteWidth/2)
                if (transform.position.x >= (Player.playerPos.x - Player.playerSize.x))
                {
                    // Animate fall instead of rotation (if time for animations)
                    GetComponent<Animator>().SetTrigger("Fall");
                    speed = -2f;
                    GetComponent<Animator>().SetBool("IsChasingBus", false);
                }
            }
        }


        public void Collected()
        {
            isCollected = true;
            GetComponent<Animator>().enabled = false;
            soundEffect.Play();
            speed = 0f;
            foreach(SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>())
            {
                r.flipX = true;
            }
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            transform.SetParent(FindObjectOfType<Player>().transform);
            transform.position = new Vector3(Player.playerPos.x - Random.Range(0.1f, Player.playerSize.x / 2), Random.Range(transform.parent.position.y, Player.playerPos.y + Player.playerSize.y), 0.1f);
            GetComponent<Collider>().enabled = false;
        }


       
        public new void Free()
        {
            gameObject.SetActive(false);
            available = false;
            // Sprite goes boom if reused
            // But it's game that goes boom if destroyed instead of freed
        }
    }
}
