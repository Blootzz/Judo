using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHajimeFromAnimation : MonoBehaviour
{
    [SerializeField] MultiplayerInator multiplayerInator;

    public void _Hajime()
    {
        multiplayerInator.Hajime();
    }
}
