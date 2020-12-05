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




        private void Start()
        {
            spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void OnEnable()
        {
            isFollowingBus = false;
            isCollected = false;
            speed = baseSpeed;
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(-1, 1, 1);
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Collider>().enabled = true;
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
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    Free();
                }
            }
        }



        void Update()
        {
            // Follow bus if not collected
            if (GameManager.Instance.playing && isFollowingBus)
            {
                transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
                if(transform.position.x >= (Player.playerPos.x - Player.playerSize.x - spriteWidth/2))
                {
                    // Animate fall instead of rotation (if time for animations)
                    transform.Rotate(new Vector3(0, 0, -45));
                    speed = -2f;
                }
            }
        }


        // When freed (on restart) > reinit raptor
        public new void Free()
        {
            isFollowingBus = false;
            base.Free();
        }


        public void Collected()
        {
            isCollected = true;
            speed = 0f;
            GetComponent<SpriteRenderer>().flipX = false;
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            transform.SetParent(FindObjectOfType<Player>().transform);
            transform.position = new Vector3(Player.playerPos.x - Random.Range(0.1f, Player.playerSize.x / 2), Random.Range(transform.parent.position.y, Player.playerPos.y + Player.playerSize.y), 0.1f);
            GetComponent<Collider>().enabled = false;
        }

        /*
         * 
         * 
         * TODO
         * 
         * Keep the update movement for when raptor is missed and runs after bus
         * Otherwise raptor is considered a simple obstacle
         * 
         * Override Free() to jump into bus
         * Override OnBecomeInvisible() to chase after bus
         * 
        */
    }
}
