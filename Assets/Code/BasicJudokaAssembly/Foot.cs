using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField] Sprite originalFootSprite;
    [SerializeField] Sprite reapingFootSprite;
    [SerializeField] GameObject blockingCurve; // arc opening upwards. Rotate on Z-axis (positive = CW)
    Rigidbody2D rb;
    Cursor cursor;
    Judoka judoka;
    IpponCircle parentIpponCircle;
    FollowTarget follow;

    [SerializeField] bool debug_mode = false;
    [SerializeField] bool isLifted = false;
    [SerializeField] bool isReaping = false; // used when extra button is being held down
    [SerializeField] float maxSpeed = 10;
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
        judoka = GetComponentInParent<Judoka>();
        follow = GetComponent<FollowTarget>();
        
        if (debug_mode)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        else
        {
            cursor = FindObjectOfType<Cursor>();
            parentIpponCircle = judoka.GetComponentInChildren<IpponCircle>();
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
            transform.position = Vector3.MoveTowards(transform.position, LimitFootByTrig(), maxSpeed);
        }
        else
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, cursor.transform.position, maxSpeed * Time.deltaTime));
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
                    //nothing
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
            //judoka.GetComponentInChildren<MassCenter>().AddInfluenceToPosition(judoka.REAPING_FOOT_STRENGTH * )
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
        if (judoka.leftFoot == this)
            return judoka.rightFoot;
        else
            return judoka.leftFoot;
    }
}
