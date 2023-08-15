using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField] Sprite originalFootSprite;
    [SerializeField] Sprite reapingFootSprite;
    Cursor cursor;
    IpponCircle parentIpponCircle;

    bool isLifted;
    bool isReaping = false; // used when extra button is being held down
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float MINSCALE = 0.1f;
    [SerializeField] float MAXSCALE = 0.9f;
    public float weightFraction = 0.5f;
    Vector3 localScaleVector;

    // Start is called before the first frame update
    void Start()
    {
        cursor = FindObjectOfType<Cursor>();
        parentIpponCircle = GetComponentInParent<Judoka>().GetComponentInChildren<IpponCircle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLifted)
        {
            FollowCursor();

            if (Input.GetKey(KeyCode.LeftShift))
                Set_isReaping(true);
            else
                Set_isReaping(false);
        }
        else
        {
            Set_isReaping(false);
        }
    }

    void FollowCursor()
    {
        float newDistanceToOtherFoot = Vector3.Distance(cursor.transform.position, Get_otherFoot().transform.position);
        if (newDistanceToOtherFoot > parentIpponCircle.Get_Diameter())
        {
            transform.position = Vector3.MoveTowards(transform.position, LimitFootByTrig(), maxSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cursor.transform.position, maxSpeed * Time.deltaTime);
        }
    }

    Vector3 LimitFootByTrig()
    {
        Vector3 cursorMinusOtherFoot = cursor.transform.position - Get_otherFoot().transform.position;
        float theta = Mathf.Atan2(cursorMinusOtherFoot.y, cursorMinusOtherFoot.x); // RADIANS // 0 East, +/- 180 West, 90 North, -90 South

        Vector3 newPosition = Vector3.zero;
        newPosition.x = Get_otherFoot().transform.position.x + parentIpponCircle.Get_Diameter() * Mathf.Cos(theta);
        newPosition.y = Get_otherFoot().transform.position.y + parentIpponCircle.Get_Diameter() * Mathf.Sin(theta);

        return newPosition;
    }

    //void AdjustFootBackToIpponCircle()
    //{
    //    float distance = Vector3.Distance(transform.position, Get_otherFoot().transform.position);
    //    if (distance > parentIpponCircle.GetComponent<CircleCollider2D>().radius * 2 * parentIpponCircle.transform.lossyScale.x)
    //    {
    //        // get difference vector from center of Ippon
    //        // set second foot to 2* that difference
    //        Vector3 ipponMinusPlantedFoot = parentIpponCircle.transform.position - Get_otherFoot().transform.position;
    //        transform.position = Get_otherFoot().transform.position + 2 * ipponMinusPlantedFoot;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Foot>())
        {
            // Foot interactions
            // normal block, clean reap, failed reap
            // normal block - depending on speed and weights, create a curved wall around loser of interation
            // clean reap - depending on weight, speed, and off-balanceness of victim, victim's foot is stunned for time
            // failed reap - attacker's foot is stunned for time
        }
    }

    void Set_isReaping(bool toReapOrNotToReap)
    {
        // don't do anything if update doesn't change anything
        if (isReaping == toReapOrNotToReap)
            return;

        isReaping = toReapOrNotToReap;
        if (isReaping)
        {
            GetComponent<SpriteRenderer>().sprite = reapingFootSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = originalFootSprite;
        }
    }

    public void Set_isLifted(bool newState)
    {
        isLifted = newState;
    }
    public bool Get_isLifted()
    {
        return isLifted;
    }

    public void Set_weightFraction(float newWeight)
    {
        weightFraction = Mathf.Clamp(newWeight, MINSCALE, MAXSCALE);
        localScaleVector.x = weightFraction; localScaleVector.y = weightFraction; localScaleVector.z = weightFraction;
        transform.localScale = localScaleVector;
    }
    public float Get_weightFraction()
    {
        return weightFraction;
    }

    public Foot Get_otherFoot()
    {
        if (GetComponentInParent<Judoka>().leftFoot == this)
            return GetComponentInParent<Judoka>().rightFoot;
        else
            return GetComponentInParent<Judoka>().leftFoot;
    }
}
