using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
    [SerializeField] Sprite originalFootSprite;
    [SerializeField] Sprite reapingFootSprite;
    [SerializeField] GameObject blockingCurve; // arc opening upwards. Rotate on Z-axis (positive = CW)
    Rigidbody2D rb;
>>>>>>> Stashed changes
=======
    [SerializeField] Sprite originalFootSprite;
    [SerializeField] Sprite reapingFootSprite;
>>>>>>> Mass-Center
    Cursor cursor;
    IpponCircle parentIpponCircle;
    FollowTarget follow;

<<<<<<< Updated upstream
    bool isLifted;
<<<<<<< HEAD
    [SerializeField] float maxSpeed = 1;
=======
    [SerializeField] bool debug_mode = false;
    [SerializeField] bool isLifted = false;
    [SerializeField] bool isReaping = false; // used when extra button is being held down
    [SerializeField] float maxSpeed = 10;
>>>>>>> Stashed changes
=======
    bool isReaping = false; // used when extra button is being held down
    [SerializeField] float maxSpeed = 10;
>>>>>>> Mass-Center
    [SerializeField] float MINSCALE = 0.1f;
    [SerializeField] float MAXSCALE = 0.9f;
    [SerializeField] float BLOCK_LIFETIME = 1f;
    public float weightFraction = 0.5f;
    public bool disableCollisionCheckOneFrame = false;
    Vector3 localScaleVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        follow = GetComponent<FollowTarget>();
        
        if (debug_mode)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        else
        {
            cursor = FindObjectOfType<Cursor>();
            parentIpponCircle = GetComponentInParent<Judoka>().GetComponentInChildren<IpponCircle>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debug_mode)
            return;

        if (follow.isActive)
            return;

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
<<<<<<< HEAD
<<<<<<< Updated upstream
            transform.position = Vector3.MoveTowards(transform.position, LimitFootByTrig(), maxSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cursor.transform.position, maxSpeed);
=======
            rb.MovePosition(Vector3.MoveTowards(transform.position, LimitFootByTrig(), maxSpeed * Time.deltaTime));
        }
        else
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, cursor.transform.position, maxSpeed * Time.deltaTime));
>>>>>>> Stashed changes
=======
            transform.position = Vector3.MoveTowards(transform.position, LimitFootByTrig(), maxSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cursor.transform.position, maxSpeed * Time.deltaTime);
>>>>>>> Mass-Center
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

<<<<<<< HEAD
<<<<<<< Updated upstream
    void AdjustFootBackToIpponCircle()
=======
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
>>>>>>> Mass-Center
    {
        if (collision.gameObject.GetComponent<Foot>())
        {
<<<<<<< HEAD
            // get difference vector from center of Ippon
            // set second foot to 2* that difference
            Vector3 ipponMinusPlantedFoot = parentIpponCircle.transform.position - Get_otherFoot().transform.position;
            transform.position = Get_otherFoot().transform.position + 2 * ipponMinusPlantedFoot;
=======
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

    private void OnCollisionEnter2D(Collision2D collider)
    {
        // Initial checks
        if (disableCollisionCheckOneFrame) // used by PreventOtherFootOnTriggerEnter() to prevent both feet from identifying one
        {
            disableCollisionCheckOneFrame = false;
            return;
        }
        // if not lifted, other foot should be managing the collision
        if (!isLifted)
            return;

        // Foot interactions
        if (collider.gameObject.GetComponent<Foot>())
        {
            Foot opponentFoot = collider.gameObject.GetComponent<Foot>();
            print(this.gameObject.name+" colliding with: "+opponentFoot.name);

            // this foot lifted, but not reaping
            if (!this.isReaping)
            {
                if (!opponentFoot.isLifted) // other is planted
                {
                    print("lifted on planted");
                    CreateNormalBlock(collider.GetContact(0));
                    goto FinishCollision;
                }
                if (!opponentFoot.isReaping) // other is lifted, but not reaping
                {
                    print("lifted on lifted");
                    // nothing
                    goto FinishCollision;
                }
                if (opponentFoot.isReaping) // other foot is reaping
                {
                    print("Evaluate lift vs reap");
                    this.GetDraggedBy(opponentFoot.gameObject, true);
                    goto FinishCollision;
                }
            }

            // this foot is reaping
            if (this.isReaping)
            {
                if (!opponentFoot.isLifted) // other foot is planted
                {
                    // push
                    opponentFoot.GetDraggedBy(this.gameObject, true);
                    goto FinishCollision;
                }
                if (!opponentFoot.isReaping) // other foot is lifted, but not reaping
                {
                    // big push
                    print("reap on lifted");
                    opponentFoot.GetDraggedBy(this.gameObject, true);
                    goto FinishCollision;
                }
                if (opponentFoot.isReaping) // other foot is reaping
                {
                    print("Reap on reap");
                    goto FinishCollision;
                }
            }
            Debug.LogWarning("Undetermined foot behavior");

            FinishCollision:
            {
                PreventOtherFootOnTriggerEnter(opponentFoot);
                return;
            }
        }
    }

    void PreventOtherFootOnTriggerEnter(Foot opponentFoot)
    {
        opponentFoot.disableCollisionCheckOneFrame = true;
    }

    void CreateNormalBlock(ContactPoint2D contactPoint)
    {
        Vector2 diff = contactPoint.collider.gameObject.transform.position - transform.position;
        float angleDegrees = Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x);
        //print("Angle: " + angleDegrees);
        GameObject blockInstance = Instantiate(blockingCurve, contactPoint.point, Quaternion.Euler(0, 0, angleDegrees));
        Destroy(blockInstance, BLOCK_LIFETIME);
    }

    void GetDraggedBy(GameObject target, bool on)
    {
        if (on)
        {
            GetComponent<FollowTarget>().StartFollowing(target);
        }
        else
        {
            GetComponent<FollowTarget>().StopFollowing();
=======
            // Foot interactions
            // normal block, clean reap, failed reap
            // normal block - depending on speed and weights, create a curved wall around loser of interation
            // clean reap - depending on weight, speed, and off-balanceness of victim, victim's foot is stunned for time
            // failed reap - attacker's foot is stunned for time
>>>>>>> Mass-Center
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
<<<<<<< HEAD
>>>>>>> Stashed changes
=======
>>>>>>> Mass-Center
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
