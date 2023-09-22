using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignColors : MonoBehaviour
{
    public void AssignAll(short playerNum)
    {
        Color32[] myPalette = GameMaster.instance.Get_PXColorPalette(playerNum);
        GetComponentInChildren<CenterOfBalance>().gameObject.GetComponent<SpriteRenderer>().color = myPalette[0];
        GetComponentInChildren<IpponCircle>().gameObject.GetComponent<SpriteRenderer>().color = myPalette[1];
        GetComponentInChildren<IpponCircle>().transform.GetChild(1).GetComponent<SpriteRenderer>().color = myPalette[2];
        GetComponentInChildren<MassCenter>().gameObject.GetComponent<SpriteRenderer>().color = myPalette[2];
        GetComponent<Judoka>().leftFoot.gameObject.GetComponent<SpriteRenderer>().color = myPalette[3];
        GetComponent<Judoka>().rightFoot.gameObject.GetComponent<SpriteRenderer>().color = myPalette[4];
    }
}
