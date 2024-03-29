using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassCenter : MonoBehaviour
{
    Judoka parentJudoka;
    IpponCircle myIpponCirlce;
    float distanceToLeftFoot;
    float distanceToRightFoot;
    Vector2 posInfluenceSumPerFrame = new Vector2(0,0);
    public float TOTAL_SPEED_MULTIPLIER = 1;

    // variables for detecting proximity to FeetCenterline
    Vector2 centerLineSlope;
    Vector2 raycastDirection;
    RaycastHit2D[] hitObjects;
    LayerMask layerOfCenterline; // Player 1, Player 2, etc... This gives RaycastAll less stuff to look through

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
        UpdateFootDistances(); // measures distances from feet to here
        UpdateFootWeightDistribution(); // depending on distance from center of ippon, change feet weight
        EvaluateMassProximityInfluence(); // depending on proximity to center line, push or pull towards center point or closest foot

        ExecuteAllInfluences();
        posInfluenceSumPerFrame = Vector2.zero; // reset for next frame
        EvaluateIppon(); // measures distance from here to center of ippon circle
    }

    void UpdateFootDistances()
    {
        distanceToLeftFoot = Vector3.Distance(transform.position, parentJudoka.leftFoot.transform.position);
        distanceToRightFoot = Vector3.Distance(transform.position, parentJudoka.rightFoot.transform.position);
    }

    void UpdateFootWeightDistribution()
    {
        //float leftFootDistanceFraction = distanceToLeftFoot / (distanceToLeftFoot + distanceToRightFoot);
        // less distance = more weight
        parentJudoka.leftFoot.Set_weightFraction(1 - distanceToLeftFoot / (distanceToLeftFoot + distanceToRightFoot));
        parentJudoka.rightFoot.Set_weightFraction(distanceToLeftFoot / (distanceToLeftFoot + distanceToRightFoot));
    }
    void EvaluateMassProximityInfluence()
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
                PushOrPullToCenterline(target.distance, target.point);
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
                PushOrPullToCenterline(target.distance, target.point);
                return;
            }
        }

        // return statement has not been hit -> just compare distances
        //float distanceToLeftFoot = Vector2.Distance(transform.position, parentJudoka.leftFoot.transform.position);
        //float distanceToRightFoot = Vector2.Distance(transform.position, parentJudoka.rightFoot.transform.position);
        // pass in closest foot
        PushOrPullToCenterlineFoot((distanceToLeftFoot <= distanceToRightFoot) ? parentJudoka.leftFoot.transform.position : parentJudoka.rightFoot.transform.position);
    }

    void ExecuteAllInfluences()
    {
        //print(posInfluenceSumPerFrame);
        transform.position += Vector3.MoveTowards(Vector2.zero, posInfluenceSumPerFrame, TOTAL_SPEED_MULTIPLIER * Time.deltaTime);
    }

    void EvaluateIppon()
    { 
        if (Vector2.Distance(transform.position, myIpponCirlce.transform.position) >= myIpponCirlce.Get_Diameter() / 2)
            print("Ippon!");
    }

    void PushOrPullToCenterline(float distanceToCenterline, Vector3 targetPosition)
    {
        if (distanceToCenterline <= parentJudoka.balanceBoundary_insideStance)
        {
            //print("CENTERLINE <== pull");
            AddInfluenceToPosition(parentJudoka.CENTERLINE_PULL_STRENGTH * (targetPosition - transform.position));
        }
        else
        {
            //print("CENTERLINE push ==>");
            AddInfluenceToPosition(parentJudoka.CENTERLINE_PUSH_STRENGTH * (transform.position - targetPosition));
        }
    }

    void PushOrPullToCenterlineFoot(Vector2 closestFootPos)
    {
        if (Vector2.Distance(transform.position, closestFootPos) <= parentJudoka.balanceBoundary_outsideStance)
        {
            //print("FOOT <== pull");
            AddInfluenceToPosition(parentJudoka.CENTERLINE_PULL_STRENGTH * closestFootPos);
        }
        else
        {
            //print("FOOT push ==>");
            AddInfluenceToPosition(parentJudoka.CENTERLINE_PUSH_STRENGTH * -1 * closestFootPos);
        }
    }

    public void AddInfluenceToPosition(Vector2 influencePerFrame) // call this every frame wherever influence originates from
    {
        //print("Adding: " + influencePerFrame);
        posInfluenceSumPerFrame += influencePerFrame;
    }
}
