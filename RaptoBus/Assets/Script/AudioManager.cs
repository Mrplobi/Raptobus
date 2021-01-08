using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RaptoBus
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public AudioSource winAudio, failAudio, mainAudio;


        private void Awake()
        {
            if(Instance == null)
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
            GameManager.Instance.onDefeat += PlayFail;
            GameManager.Instance.onWin += PlayWin;
            GameManager.Instance.onReset += PlayMain;
            GameManager.Instance.onLaunch += PlayMain;
        }


        public void PlayWin()
        {
            mainAudio.Stop();
            winAudio.Play();
        }

        public void PlayFail()
        {
            mainAudio.Stop();
            failAudio.Play();
        }

        public void PlayMain()
        {
            winAudio.Stop();
            failAudio.Stop();
            mainAudio.Play();
        }

    }
}