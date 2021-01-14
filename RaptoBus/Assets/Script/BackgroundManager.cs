using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaptoBus
{
    public class BackgroundManager : MonoBehaviour
    {
        public static BackgroundManager Instance;
        public BackgroundParallax[] parallaxBG;
        private int curLvl = 0;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }


        private void Start()
        {
            parallaxBG = GetComponentsInChildren<BackgroundParallax>();
            GameManager.Instance.onReset += ResetSpeed;
            GameManager.Instance.onLaunch += ResetSpeed;
        }


        public void SpeedUp(int lvl)
        {
            // Lvl up only between lvl, not before 1st of after 3rd
            if (lvl > curLvl && lvl < 3)
            {
                foreach (BackgroundParallax bg in parallaxBG)
                {
                    StartCoroutine(bg.Accelerate(1f + (0.5f * lvl)));
                }
                Debug.Log("BG speed up");
                curLvl = lvl;
            }
        }

        public void ResetSpeed()
        {
            foreach(BackgroundParallax bg in parallaxBG)
            {
                bg.ResetSpeed();
            }
            curLvl = 0;
            Debug.Log("Reset BG speed");
        }

    }
}
