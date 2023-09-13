using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class WaitForHajime : MonoBehaviour
{
    MultiplayerInator multiplayerInator;

    public void Start()
    {
        // Don't get permission to play until MultiplayerInator.Hajime()
        GetComponent<PlayerInput>().DeactivateInput();

        if (multiplayerInator == null)
        {
            multiplayerInator = FindObjectOfType<MultiplayerInator>();
            multiplayerInator.hajimeEvent += ActivateControls;
        }
    }

    void ActivateControls(object sender, EventArgs e)
    {
        GetComponent<PlayerInput>().ActivateInput();
    }
}
