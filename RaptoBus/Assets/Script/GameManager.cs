using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace RaptoBus
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public UnityAction onDefeat;
        public UnityAction onReset;
        public UnityAction onWin;
        public UnityAction onInit;
        public UnityAction onLaunch;

        public bool playing = true;

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
            InitGame();
        }

        public void InitGame()
        {
            onInit.Invoke();
            playing = false;
        }

        public void LaunchGame()
        {
            onLaunch.Invoke();
            playing = true;
        }

        public void ResetGame()
        {
            onReset.Invoke();
            playing = true;
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Defeat()
        {
            onDefeat.Invoke();
            playing = false;
        }
        public void Win()
        {
            onWin.Invoke();
            playing = false;
        }
    }
}
