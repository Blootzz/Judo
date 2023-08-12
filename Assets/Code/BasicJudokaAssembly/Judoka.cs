using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judoka : MonoBehaviour
{
    public Foot leftFoot;
    public Foot rightFoot;
    public bool isEngagedWithOpponent = false;

    // Needs to be called before Start() so that FeetCenterline.Start can use these references
    void Awake()
    {
        Foot[] footArray = GetComponentsInChildren<Foot>();
        foreach (Foot foot in footArray)
            if (foot.CompareTag("LeftFoot"))
                leftFoot = foot;
            else
                rightFoot = foot;
    }

}
