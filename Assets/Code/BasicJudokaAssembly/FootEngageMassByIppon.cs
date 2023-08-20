using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEngageMassByIppon : MonoBehaviour
{
    [HideInInspector] public Judoka parentJudoka;
    MassCenter footMassCenterReference;

    void Start()
    {
        parentJudoka = transform.parent.parent.GetComponent<Judoka>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IpponCircle>())
        {
            print("Found opponent");
            // acquire opponent mass. Sets target for MassMovement.Set_direction()
            parentJudoka.opponentMass = collision.gameObject.transform.parent.parent.GetComponentInChildren<MassCenter>();
            // record local reference for comparison later
            footMassCenterReference = parentJudoka.opponentMass;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<IpponCircle>())
            return; // not dealing with an ippon circle

        // get the reference recorded in the opposite foot to this one
        MassCenter otherFootMassCenterReference = GetComponent<Foot>().Get_otherFoot().gameObject.GetComponent<FootEngageMassByIppon>().footMassCenterReference;

        if (otherFootMassCenterReference == null)
        {
            footMassCenterReference = null;
            parentJudoka.opponentMass = null;
            return;
        }

        if (otherFootMassCenterReference != footMassCenterReference) // some third+ opponent in 3+ player match
        {
            footMassCenterReference = null;
            return; // for 3-player match
        }

        footMassCenterReference = null; // in case other reference is still same mass center
    }
}
