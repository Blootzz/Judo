using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    [SerializeField] bool debug_mode = false;
    [SerializeField] float maxSpeed = 10;
    Vector2 moveTargetDestination = new Vector2(0, 0);
    bool instantlyTryingToReap = false;

    Judoka judoka;
    Foot activeFoot;

    void Start()
    {
        judoka = GetComponent<Judoka>();
        activeFoot = judoka.leftFoot; // default to prevent errors
    }

    void Update()
    {
        if (debug_mode)
            return;

        if (activeFoot.follow.isActive) // if being dragged by something
            return;

        if (activeFoot.Get_isLifted()) // set by ManualFootControl or AI
        {
            // Use moveTargetDestination to figure out where the foot should really go
            LimitFootByTrig();
            MoveFoot();
        }
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
        activeFoot.Set_isReaping(instantlyTryingToReap);
        activeFoot.Get_otherFoot().Set_isLifted(false);
    }

    public void PickUpRightFoot()
    {
        activeFoot = judoka.rightFoot;
        activeFoot.Set_isLifted(true);
        activeFoot.Set_isReaping(instantlyTryingToReap);
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
        instantlyTryingToReap = toReapOrNotToReap;
        if (activeFoot.Get_isLifted())
            activeFoot.Set_isReaping(instantlyTryingToReap);
    }

    // Movement
    void LimitFootByTrig()
    {
        float newDistanceToOtherFoot = Vector3.Distance(moveTargetDestination, activeFoot.Get_otherFoot().transform.position);
        if (newDistanceToOtherFoot > activeFoot.parentIpponCircle.Get_Diameter())
        {
            Vector2 targetDestinationMinusOtherFoot = moveTargetDestination - new Vector2(activeFoot.Get_otherFoot().transform.position.x, activeFoot.Get_otherFoot().transform.position.x);
            float theta = Mathf.Atan2(targetDestinationMinusOtherFoot.y, targetDestinationMinusOtherFoot.x); // RADIANS // 0 East, +/- 180 West, 90 North, -90 South

            moveTargetDestination.x = activeFoot.Get_otherFoot().transform.position.x + activeFoot.parentIpponCircle.Get_Diameter() * Mathf.Cos(theta);
            moveTargetDestination.y = activeFoot.Get_otherFoot().transform.position.y + activeFoot.parentIpponCircle.Get_Diameter() * Mathf.Sin(theta);;
        }
    }

    void MoveFoot()
    {
        activeFoot.rb.MovePosition(Vector3.MoveTowards(transform.position, moveTargetDestination, maxSpeed * Time.deltaTime));
    }

    public Vector2 Get_moveTargetDestination() => moveTargetDestination;
    public void Set_moveTargetDestination(Vector2 here, bool addToFoot)
    {
        if (!addToFoot)
            moveTargetDestination = here;
        else
            moveTargetDestination = here + new Vector2(activeFoot.transform.position.x, activeFoot.transform.position.y);
    }
}
