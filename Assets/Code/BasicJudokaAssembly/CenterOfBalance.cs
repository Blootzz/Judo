using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfBalance : MonoBehaviour
{
    // Position determined by feet locations and weights
    Judoka parentJudoka;

    // Start is called before the first frame update
    void Start()
    {
        parentJudoka = GetComponentInParent<Judoka>();
    }

    // Update is called once per frame
    void Update()
    {
        // foot weightFractions should add to 1
        EnsureWeightFractionsAreCorrect();
        transform.position =
            parentJudoka.leftFoot.transform.position * parentJudoka.leftFoot.weightFraction +
            parentJudoka.rightFoot.transform.position * parentJudoka.rightFoot.weightFraction;
    }

    void EnsureWeightFractionsAreCorrect()
    {
        float sum = parentJudoka.leftFoot.weightFraction + parentJudoka.rightFoot.weightFraction;
        if (Mathf.Abs(1-sum) > 0.001f)
            Debug.LogWarning("Weight fractions add to: "+(parentJudoka.leftFoot.weightFraction + parentJudoka.rightFoot.weightFraction)+"\nLeftFoot: " + parentJudoka.leftFoot.weightFraction + " | RightFoot: " + parentJudoka.rightFoot.weightFraction);
    }
}
