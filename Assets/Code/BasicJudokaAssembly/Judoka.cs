using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judoka : MonoBehaviour
{
    public Foot leftFoot;
    public Foot rightFoot;

    // Start is called before the first frame update
    void Start()
    {
        Foot[] footArray = GetComponentsInChildren<Foot>();
        foreach (Foot foot in footArray)
            if (foot.CompareTag("LeftFoot"))
                leftFoot = foot;
            else
                rightFoot = foot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBothFeetDown()
    {
        rightFoot.Set_isLifted(false);
        leftFoot.Set_isLifted(false);
    }
}
