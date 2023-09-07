using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judoka : MonoBehaviour
{
    [Header("Position Influence Multipliers")]
    // these variables should not be changed by code. They should be used to multiply arguments of AddInfluenceToPos(Vector2 influence)
    public float WASD_STRENGTH = 1;
    public float CENTERLINE_PULL_STRENGTH = 1;
    public float CENTERLINE_PUSH_STRENGTH = 1;
    public float KUZUSHI_STRENGTH = 1;
    public float REAPING_FOOT_STRENGTH = 1;

    [Header("Push/Pull Boundary")]
    public float balanceBoundary_insideStance = 0.5f;
    public float balanceBoundary_outsideStance = 0.5f;

    [Header("Foot References")]
    public Foot leftFoot;
    public Foot rightFoot;

    [Header("")]
    public bool isEngagedWithOpponent = false;

    [HideInInspector] public MassCenter opponentMass;

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
