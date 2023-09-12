using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCallRound : MonoBehaviour
{
    [SerializeField] GameObject roundText; // this will then call hajime

    // animation event
    public void OnDisable()
    {
        if (roundText != null)
            roundText.SetActive(true);
    }
}
