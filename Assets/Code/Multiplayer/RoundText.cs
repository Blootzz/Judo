using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundText : MonoBehaviour
{
    [SerializeField] MultiplayerInator multiplayerInator;
    [SerializeField] GameObject brush;
    [SerializeField] GameObject hajimeText;

    public void _Hajime() // called from UI animation
    {
        multiplayerInator.Hajime();
        brush.SetActive(true);
        hajimeText.SetActive(true);
    }
}
