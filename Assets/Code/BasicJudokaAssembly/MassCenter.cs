using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassCenter : MonoBehaviour
{
    Judoka parentJudoka;
    float distanceToLeftFoot;
    float distanceToRightFoot;
    IpponCircle myIpponCirlce;

    // Start is called before the first frame update
    void Start()
    {
        parentJudoka = GetComponentInParent<Judoka>();
        myIpponCirlce = parentJudoka.GetComponentInChildren<IpponCircle>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFootWeightDistribution();
        if (Vector2.Distance(transform.position, myIpponCirlce.transform.position) >= myIpponCirlce.Get_Diameter() / 2)
            print("Ippon!");
    }

    void UpdateFootWeightDistribution()
    {
        distanceToLeftFoot = Vector3.Distance(transform.position, parentJudoka.leftFoot.transform.position);
        distanceToRightFoot = Vector3.Distance(transform.position, parentJudoka.rightFoot.transform.position);
        //float leftFootDistanceFraction = distanceToLeftFoot / (distanceToLeftFoot + distanceToRightFoot);
        // less distance = more weight
        parentJudoka.leftFoot.Set_weightFraction(1 - distanceToLeftFoot / (distanceToLeftFoot + distanceToRightFoot));
        parentJudoka.rightFoot.Set_weightFraction(distanceToLeftFoot / (distanceToLeftFoot + distanceToRightFoot));
    }
}
