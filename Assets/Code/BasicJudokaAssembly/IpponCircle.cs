using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IpponCircle : MonoBehaviour
{
    [HideInInspector] public Vector2 opponentMassMinusMass;
    Judoka parentJudoka;
    MassCenter opponentMassCenter;
    MassCenter myMassCenter;
    float workingShaderAngle;

    void Start()
    {
        parentJudoka = transform.parent.parent.GetComponent<Judoka>();
        //opponentMassCenter = parentJudoka.opponent.GetComponentInChildren<MassCenter>();
        myMassCenter = parentJudoka.GetComponentInChildren<MassCenter>();
    }

    void Update()
    {
        if (opponentMassCenter == null)
        {
            if (parentJudoka.opponent == null)
                return;
            opponentMassCenter = parentJudoka.opponent.GetComponentInChildren<MassCenter>();
        }
        // rotate shader to opponent
        opponentMassMinusMass = opponentMassCenter.transform.position - myMassCenter.transform.position;
        opponentMassMinusMass = opponentMassMinusMass.normalized;
        workingShaderAngle = Mathf.Atan2(opponentMassMinusMass.y, opponentMassMinusMass.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, workingShaderAngle);
    }

    public float Get_Diameter()
    {
        return GetComponent<CircleCollider2D>().radius * 2 * transform.lossyScale.x;
    }
}
