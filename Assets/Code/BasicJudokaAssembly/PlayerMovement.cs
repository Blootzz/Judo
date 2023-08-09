using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Judoka judoka;
    Vector2 mouseDragStartPoint;

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
            judoka.rightFoot.Set_isLifted(false);
            judoka.leftFoot.Set_isLifted(false);
            return;
        }

        // Pick up left foot
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            judoka.leftFoot.Set_isLifted(true);
        }
        // Pick up right foot
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            judoka.rightFoot.Set_isLifted(true);
        }
        // Lower left foot
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            judoka.leftFoot.Set_isLifted(false);
        }
        // Lower right foot
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            judoka.rightFoot.Set_isLifted(false);
        }


    }

    void Shuffle_OnFrame()
    {

    }
}
