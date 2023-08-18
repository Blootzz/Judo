using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCenterline : MonoBehaviour
{
    /// <summary>
    /// This EdgeCollider2D exists so that MassCenter.cs can have a target for its raycast. This determines how off-balance they are
    /// </summary>
    
    Foot leftFoot;
    Foot rightFoot;
    EdgeCollider2D edgeCollider;
    List<Vector2> colliderPointList = new List<Vector2>(new Vector2[2]); // establish list of size 2

    // Start is called before the first frame update
    void Start()
    {
        leftFoot = GetComponentInParent<Judoka>().leftFoot;
        rightFoot = GetComponentInParent<Judoka>().rightFoot;
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    // Update position every frame
    void Update()
    {
        // position 0 = left foot
        colliderPointList[0] = leftFoot.transform.localPosition;
        // position 1 = right foot
        colliderPointList[1] = rightFoot.transform.localPosition;

        edgeCollider.SetPoints(colliderPointList);

        Debug.DrawLine(leftFoot.transform.position, rightFoot.transform.position, Color.black);
    }
}
