using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] short playerNum;
    [SerializeField] [Range(0, 7)] short colorIndex;

    public void SelectMe() // called in button event
    {
        if (playerNum == 1 || playerNum == 2)
            GameMaster.instance.Set_PXColor(playerNum, colorIndex);
        else
            Debug.LogWarning("PlayerNum not set");

        transform.parent.parent.parent.gameObject.GetComponent<Animator>().SetTrigger("Shrink");
    }
}
