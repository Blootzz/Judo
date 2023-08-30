using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootControlGamepad : MonoBehaviour
{
    FootController footController;

    void Start()
    {
        footController = GetComponent<FootController>();
    }

    // detect inputs from pllayer
    void Update()
    {
        // Shuffle
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            footController.Shuffle_OnFrame();
            return;
        }

        // Pick up left foot
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            footController.PickUpLeftFoot();
            return;
        }
        // Pick up right foot
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            footController.PickUpRightFoot();
            return;
        }
        // Lower left foot
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            footController.LowerLeftFoot();
            return;
        }
        // Lower right foot
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            footController.LowerRightFoot();
            return;
        }

        footController.ReapOnOrOff(Input.GetKey(KeyCode.LeftShift));

    }
}
