using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RaptoBus
{
    public class UIManager : MonoBehaviour
    {
        public GameObject failCanvas;
        public GameObject winCanvas;

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.onDefeat += DisplayFail;
            GameManager.Instance.onReset += ResetGame;
            GameManager.Instance.onWin += DisplayWin;
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
        }
    }
}
