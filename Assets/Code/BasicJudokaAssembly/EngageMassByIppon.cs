using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageMassByIppon : MonoBehaviour
{
    [HideInInspector] public Judoka parentJudoka;
    //MassCenter footMassCenterReference;

    void Start()
    {
        parentJudoka = transform.parent.parent.GetComponent<Judoka>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IpponCircle>())
        {
            // acquire opponent mass. Sets target for MassMovement.Set_direction()
            parentJudoka.opponentMass = collision.gameObject.transform.parent.parent.GetComponentInChildren<MassCenter>();
            //print("Found: "+parentJudoka.opponentMass.name);
            // record local reference for comparison later
            //footMassCenterReference = parentJudoka.opponentMass;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IpponCircle>())
        {
            parentJudoka.opponentMass = null;
        }

        //// get the reference recorded in the opposite foot to this one
        //MassCenter otherFootMassCenterReference = GetComponent<Foot>().Get_otherFoot().gameObject.GetComponent<FootEngageMassByIppon>().footMassCenterReference;

        //if (otherFootMassCenterReference == null)
        //{
        //    footMassCenterReference = null;
        //    parentJudoka.opponentMass = null;
        //    return;
        //}

        //if (otherFootMassCenterReference != footMassCenterReference) // some third+ opponent in 3+ player match
        //{
        //    footMassCenterReference = null;
        //    return; // for 3-player match
        //}

        //footMassCenterReference = null; // in case other reference is still same mass center
    }
}
