using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCallRound : MonoBehaviour
{
    [SerializeField] GameObject scoreboard;
    [SerializeField] GameObject roundText; // this will then call hajime
    // animation event
    public void OnDisable()
    {
        scoreboard.SetActive(true);
        Invoke(nameof(BeginGameWithRoundText), 1f);
    }

    void BeginGameWithRoundText()
    {
        if (roundText != null)
            roundText.SetActive(true);
    }
}
