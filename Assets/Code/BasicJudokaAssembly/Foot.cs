using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    bool isLifted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_isLifted(bool newState)
    {
        isLifted = newState;
    }
    public bool Get_isLifted()
    {
        return isLifted;
    }
}
