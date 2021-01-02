using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RaptoBus
{
    enum UIState { Init, Play, Fail, Win}
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public GameObject initCanvas;
        public GameObject gameCanvas;
        public GameObject failCanvas;
        public GameObject winCanvas;
        public GameObject pauseCanvas;

        private UIState state = UIState.Init;

        private int raptorCount;

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


        void Start()
        {
            GameManager.Instance.onDefeat += DisplayFail;
            GameManager.Instance.onReset += ResetGame;
            GameManager.Instance.onInit += DisplayInit;
            GameManager.Instance.onLaunch += DisplayLaunch;
            GameManager.Instance.onPause += Pause;
        }

        public void Pause()
        {
            if(!pauseCanvas.activeInHierarchy && GameManager.Instance.Playing)
            {
                GameManager.Instance.Playing = false;
                pauseCanvas.SetActive(true);
            }
            else if(pauseCanvas.activeInHierarchy)
            {
                GameManager.Instance.Playing = true;
                pauseCanvas.SetActive(false);
            }
        }

        public void ChangeState(InputAction.CallbackContext context)
        {
            switch (state)
            {
                case UIState.Init:
                    GameManager.Instance.LaunchGame();
                    break;
                case UIState.Play:
                    break;
                case UIState.Fail:
                    GameManager.Instance.ResetGame();
                    break;
                case UIState.Win:
                    GameManager.Instance.ExitGame();
                    break;
                default:
                    break;
            }
        }



        private void DisplayInit()
        {
            state = UIState.Init;
            initCanvas.SetActive(true);
        }

        private void DisplayLaunch()
        {
            state = UIState.Play;
            initCanvas.SetActive(false);
            gameCanvas.SetActive(true);
        }

        private void DisplayFail()
        {
            state = UIState.Fail;
            failCanvas.SetActive(true);
        }

        public void DisplayWin()
        {
            state = UIState.Win;
            winCanvas.SetActive(true);
        }

        public void ResetGame()
        {
            state = UIState.Play;
            failCanvas.SetActive(false);
            winCanvas.SetActive(false);
            gameCanvas.GetComponent<IngameUI>().Restart();
            // Reset game canvas - dist = 0, hide raptor count
        }

        public void AddRaptorCount()
        {
            gameCanvas.GetComponent<IngameUI>().DisplayRaptorCount();
        }
    }
}
