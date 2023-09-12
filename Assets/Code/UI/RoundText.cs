using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundText : MonoBehaviour
{
    [SerializeField] MultiplayerInator multiplayerInator;
    [SerializeField] GameObject brush;
    [SerializeField] GameObject hajimeText;

    void OnEnable()
    {
        GameMaster.instance.IncrementRoundNum(); // starting at zero
        GetComponent<TMPro.TextMeshProUGUI>().text = "Round " + GameMaster.instance.Get_roundNum().ToString();
    }

    public void _Hajime() // called from UI animation
    {
        multiplayerInator.Hajime();
        brush.SetActive(true);
        hajimeText.SetActive(true);
    }
}
