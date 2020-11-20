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

        public void ResetGame()
        {
            onReset.Invoke();
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
