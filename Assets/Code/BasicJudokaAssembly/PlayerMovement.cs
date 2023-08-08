using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Judoka judoka;
    // Start is called before the first frame update
    void Start()
    {
        judoka = GetComponent<Judoka>();
    }

    // detect inputs from pllayer
    void Update()
    {
        // Shuffle
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            Shuffle_OnFrame();
            judoka.SetBothFeetDown();
            return;
        }

        // Pick up left foot
        if (Input.GetKeyDown(KeyCode.Mouse0))
            leftLeg
    }

    void Shuffle_OnFrame()
    {

    }
}
