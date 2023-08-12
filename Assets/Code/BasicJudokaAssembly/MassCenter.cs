using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassCenter : MonoBehaviour
{
    [SerializeField] float balanceBoundary_insideStance = 0.5f;
    [SerializeField] float balanceBoundary_outsideStance = 0.5f;

    Judoka parentJudoka;
    float distanceToLeftFoot;
    float distanceToRightFoot;
    IpponCircle myIpponCirlce;

    // variables for detecting proximity to FeetCenterline
    Vector2 centerLineSlope;
    Vector2 raycastDirection;
    RaycastHit2D[] hitObjects;
    public LayerMask layerOfCenterline; // Player 1, Player 2, etc... This gives RaycastAll less stuff to look through

    // Start is called before the first frame update
    void Start()
    {
        parentJudoka = GetComponentInParent<Judoka>();
        myIpponCirlce = parentJudoka.GetComponentInChildren<IpponCircle>();
        // left shift operator. takes 00000001 and shifts 1 bit to the left by numbered layer (7 in this case)
        layerOfCenterline = 1 << (parentJudoka.GetComponentInChildren<FeetCenterline>().gameObject.layer);
        //print(layerOfCenterline.value); // returns 128 - value of binary after left shift (01000000)
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFootWeightDistribution();
        EvaluateMassProximity(); // depending on proximity to center line, push or pull towards center point or closest foot
        if (Vector2.Distance(transform.position, myIpponCirlce.transform.position) >= myIpponCirlce.Get_Diameter() / 2)
            print("Ippon!");
    }

    void EvaluateMassProximity()
    {
        // if raycast hits FeetCenterline, get distance
        // origin = here
        // direction = perpendicular to leftFoot-rightFoot in both directions
        // distance could be ippon diameter/2 but we're not checking for ippon here, so might as well be safe and extend the raycast distance
        // either search on layer for component FeetCenterline OR check if any result is the same object as a reference object
        // if raycast doesn't hit feetCenterline, try flipping direction
        // if that doesn't work, get smallest distance to either point

        centerLineSlope = parentJudoka.rightFoot.transform.position - parentJudoka.leftFoot.transform.position; // left-right vs. right-left doesn't matter -> direction vector will be negated
        raycastDirection = new Vector2(1 / centerLineSlope.x, -1 / centerLineSlope.y); // negative reciprocal to find direction perpendicular to centerLineSlope

        hitObjects = Physics2D.RaycastAll(transform.position, raycastDirection, myIpponCirlce.Get_Diameter(), layerOfCenterline);
        Debug.DrawRay(transform.position, raycastDirection, Color.blue, 0.1f);


        foreach (RaycastHit2D target in hitObjects)
        {
            if (target.collider.GetComponent<EdgeCollider2D>())
            {
                PushOrPullToCenterline(target.distance);
                return;
            }
        }

        // return statement has not been hit -> flip direction and try again
        raycastDirection *= -1;
        hitObjects = Physics2D.RaycastAll(transform.position, raycastDirection, myIpponCirlce.Get_Diameter(), layerOfCenterline);
        Debug.DrawRay(transform.position, raycastDirection, Color.green, 0.1f);

        foreach (RaycastHit2D target in hitObjects)
        {
            if (target.collider.GetComponent<EdgeCollider2D>())
            {
                PushOrPullToCenterline(target.distance);
                return;
            }
        }

        // return statement has not been hit -> just compare distances
        float distanceToLeftFoot = Vector2.Distance(transform.position, parentJudoka.leftFoot.transform.position);
        float distanceToRightFoot = Vector2.Distance(transform.position, parentJudoka.rightFoot.transform.position);
        // pass in closest foot
        PushOrPullToFoot((distanceToLeftFoot <= distanceToRightFoot) ? parentJudoka.leftFoot.transform.position : parentJudoka.rightFoot.transform.position);
    }

    void PushOrPullToCenterline(float distanceToCenterline)
    {
        if (distanceToCenterline <= balanceBoundary_insideStance)
        {
            print("CENTERLINE PROXIMITY -> pull in to center");
        }
        else
        {
            print("CENTERLINE PROXIMITY -> push out from center");
        }
    }

    void PushOrPullToFoot(Vector2 closestFootPos)
    {
        if (Vector2.Distance(transform.position, closestFootPos) <= balanceBoundary_outsideStance)
        {
            print("FOOT PROXIMITY -> pull to closest foot or center");
        }
        else
        {
            print("FOOT PROXIMITY -> push out from center");
        }
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
