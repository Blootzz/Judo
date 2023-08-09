using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IpponCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Get_Diameter()
    {
        return GetComponent<CircleCollider2D>().radius * 2 * transform.lossyScale.x;
    }
}
