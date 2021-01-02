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
        public UnityAction onPause;

        private bool playing = true;

        public int maxRaptor = 30;

        [Header("Ending")]
        public GameObject endingPrefab;
        public GameObject endingParent;

        public bool Playing { get => playing; set => playing = value; }

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
            Playing = false;
        }

        public void LaunchGame()
        {
            onLaunch.Invoke();
            Playing = true;
        }

        public void PauseGame()
        {
            onPause.Invoke();
        }

        public void ResetGame()
        {
            onReset.Invoke();
            Playing = true;
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitGame()
        {
            ResetGame();
        }

        public void Defeat()
        {
            onDefeat.Invoke();
            Playing = false;
        }
        public void Win()
        {
            Instantiate(endingPrefab, endingParent.transform);
            onWin.Invoke();
            Playing = false;
        }
    }
}
