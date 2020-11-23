using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RaptoBus
{
    public class UIManager : MonoBehaviour
    {
        public GameObject initCanvas;
        public GameObject gameCanvas;
        public GameObject failCanvas;
        public GameObject winCanvas;

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.onDefeat += DisplayFail;
            GameManager.Instance.onReset += ResetGame;
            GameManager.Instance.onWin += DisplayWin;
            GameManager.Instance.onInit += DisplayInit;
            GameManager.Instance.onLaunch += DisplayLaunch;
        }


        private void DisplayInit()
        {
            initCanvas.SetActive(true);
        }

        private void DisplayLaunch()
        {
            initCanvas.SetActive(false);
            gameCanvas.SetActive(true);
        }

        private void DisplayFail()
        {
            failCanvas.SetActive(true);
        }

        private void DisplayWin()
        {
            winCanvas.SetActive(true);
        }

        public void ResetGame()
        {
            failCanvas.SetActive(false);
            winCanvas.SetActive(false);
            // Reset game canvas - dist = 0, hide raptor count
        }
    }
}
