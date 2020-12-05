using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace RaptoBus
{
    public class IngameUI : MonoBehaviour
    {
        public GameObject counterRaptor;
        public Text raptorCountText;
        public Text raptorMaxCountText;
        private int raptorCount = 0;

        private void Start()
        {
            counterRaptor.SetActive(false);
        }

        public void DisplayRaptorCount()
        {
            if (!counterRaptor.activeInHierarchy)
            {
                counterRaptor.SetActive(true);
                raptorMaxCountText.text = " / " + GameManager.Instance.maxRaptor.ToString();
            }
            raptorCount++;
            raptorCountText.text = raptorCount.ToString();
        }


        public void Restart()
        {
            raptorCountText.text = "0";
            counterRaptor.SetActive(false);
            raptorCount = 0;
        }


        private void OnDisable()
        {
            raptorCountText.text = "0";
            counterRaptor.SetActive(false);
            raptorCount = 0;
        }
    }
}
