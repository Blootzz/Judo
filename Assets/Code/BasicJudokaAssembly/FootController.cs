using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    Judoka judoka;
    Foot activeFoot;

    void Start()
    {
        judoka = GetComponent<Judoka>();
        activeFoot = judoka.leftFoot; // default to prevent errors
    }

    public void Shuffle_OnFrame()
    {
        judoka.rightFoot.Set_isLifted(false);
        judoka.leftFoot.Set_isLifted(false);
    }

    public void PickUpLeftFoot()
    {
        activeFoot = judoka.leftFoot;
        activeFoot.Set_isLifted(true);
        activeFoot.Get_otherFoot().Set_isLifted(false);
    }

    public void PickUpRightFoot()
    {
        activeFoot = judoka.rightFoot;
        activeFoot.Set_isLifted(true);
        activeFoot.Get_otherFoot().Set_isLifted(false);
    }

    public void LowerLeftFoot()
    {
        judoka.leftFoot.Set_isLifted(false);
        judoka.leftFoot.Set_isReaping(false);
    }
    public void LowerRightFoot()
    {
        judoka.rightFoot.Set_isLifted(false);
        judoka.rightFoot.Set_isReaping(false);
    }

    public void ReapOnOrOff(bool toReapOrNotToReap)
    {
        if (activeFoot.Get_isLifted())
            activeFoot.Set_isReaping(toReapOrNotToReap);
    }
}
