using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ColorButton : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] short playerNum;
    [SerializeField] [Range(1, 8)] short colorNum;
    [SerializeField] MultiplayerInator multiplayerInator;

    public void SelectMe() // called in button event
    {
        // record player color to static GameMaster
        if (playerNum == 1 || playerNum == 2)
        {
            GameMaster.instance.Set_PXColor(playerNum, colorNum);
        }
        else
            Debug.LogWarning("PlayerNum not set");

        // use multiplayerInator to actually assign player colors
        multiplayerInator.ColorPlayer(playerNum);

        // P1Panel or P2Panel
        transform.parent.parent.parent.gameObject.GetComponent<Animator>().SetTrigger("Shrink");
        // lol

        // disable all buttons owned by parent
        foreach (Transform child in transform.parent)
            child.GetComponent<Selectable>().enabled = false;

        // if P1 just selected color, disable it for P2
        if (playerNum == 1)
        {
            // I'LL DO IT AGAIN AND THEN SOME
            transform.parent.parent.parent.parent.GetChild(1).GetChild(1).GetChild(2).GetChild(transform.GetSiblingIndex()).gameObject.SetActive(false);
            // colors -> color menu -> P1Panel -> canvas -> P2Panel -> ColorMenu -> Colors -> this button's sibling index
        }

        // use multiplayerInator to enable spawning again
        multiplayerInator.ReEnableJoining();
    }
}
