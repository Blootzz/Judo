using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judoka : MonoBehaviour
{
    public Foot leftFoot;
    public Foot rightFoot;

    // Start is called before the first frame update
    void Start()
    {
        Foot[] footArray = GetComponentsInChildren<Foot>();
        print(footArray);
        //
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
