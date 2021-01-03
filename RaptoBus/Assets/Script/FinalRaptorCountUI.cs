using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RaptoBus
{
    public class FinalRaptorCountUI : MonoBehaviour
    {
        [SerializeField]
        Text countText, winLoseText;

        public void SetFinalText(int raptorCount)
        {
            countText.text = raptorCount.ToString();
            if (raptorCount == 0)
            {
                countText.color = Color.red;
                winLoseText.color = Color.red;
                winLoseText.text = "Perdu ! Réessayez !";
            }
            else
            {
                countText.color = Color.green;
                winLoseText.color = Color.green;
                winLoseText.text = "Gagné ! Retournez au menu principal.";
            }
        }
    }
}
