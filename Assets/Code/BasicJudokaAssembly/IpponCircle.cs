using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IpponCircle : MonoBehaviour
{
    public float Get_Diameter()
    {
        return GetComponent<CircleCollider2D>().radius * 2 * transform.lossyScale.x;
    }
}
