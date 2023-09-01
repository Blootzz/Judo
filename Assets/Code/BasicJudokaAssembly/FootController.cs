using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    [SerializeField] bool debug_mode = false;
    [SerializeField] float maxSpeed = 10;
    Vector3 moveTargetDirection = new Vector3(0, 0); // updated in FootControlMouse every time the input is detected. Not normalized
    Vector3 targetDestination = new Vector3(0, 0); // updated in FootControlMouse every time the input is detected. Not normalized
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

        if (activeFoot.follow == null || activeFoot.follow.isActive) // if being dragged by something
            return;

        if (activeFoot.Get_isLifted()) // set by ManualFootControl or AI
        {
            // Use targetDestination to figure out where the foot should really go
            targetDestination = moveTargetDirection + activeFoot.transform.position;
            LimitFootByTrig(); // corrects targetDestination if necessary
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
        moveTargetDirection = Vector3.zero;
    }

    public void PickUpRightFoot()
    {
        activeFoot = judoka.rightFoot;
        activeFoot.Set_isLifted(true);
        activeFoot.Set_isReaping(instantlyTryingToReap);
        activeFoot.Get_otherFoot().Set_isLifted(false);
        moveTargetDirection = Vector3.zero;
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
        float newDistanceToOtherFoot = Vector3.Distance(targetDestination, activeFoot.Get_otherFoot().transform.position);
        if (newDistanceToOtherFoot > activeFoot.parentIpponCircle.Get_Diameter())
        {
            Vector2 targetDestinationMinusOtherFoot = targetDestination - activeFoot.Get_otherFoot().transform.position;
            float theta = Mathf.Atan2(targetDestinationMinusOtherFoot.y, targetDestinationMinusOtherFoot.x); // RADIANS // 0 East, +/- 180 West, 90 North, -90 South

            targetDestination.x = activeFoot.Get_otherFoot().transform.position.x + activeFoot.parentIpponCircle.Get_Diameter() * Mathf.Cos(theta);
            targetDestination.y = activeFoot.Get_otherFoot().transform.position.y + activeFoot.parentIpponCircle.Get_Diameter() * Mathf.Sin(theta);;
        }
    }

    void MoveFoot() // called in Update() here
    {
        activeFoot.rb.MovePosition(Vector3.MoveTowards(activeFoot.transform.position, targetDestination, maxSpeed * Time.deltaTime));
    }

    public void Set_moveTarget(Vector3 position, bool treatAsDestination)
    {
        if (treatAsDestination)
            moveTargetDirection = position - activeFoot.transform.position; // treats position like destination
        else
            moveTargetDirection = position; // treats position like destination
    }
}
