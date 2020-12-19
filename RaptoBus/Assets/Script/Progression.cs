using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RaptoBus
{
    public class Progression : MonoBehaviour
    {
        public static float totalDist = 0;
        float globalDist = 0; // used to increment difficulty lvl

        public Slider progressDisplay;

        private void Start()
        {
            GameManager.Instance.onReset += Reset;
        }

        private void Update()
        {
            if(GameManager.Instance.Playing)
            {
                globalDist += Time.deltaTime;
                progressDisplay.value = globalDist / totalDist;
                if(globalDist >= totalDist)
                {
                    GameManager.Instance.Win();
                }
            }
        }

        private void Reset()
        {
            globalDist = 0;
        }
    }
}