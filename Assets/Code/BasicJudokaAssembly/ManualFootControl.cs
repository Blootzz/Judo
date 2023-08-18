using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualFootControl : MonoBehaviour
{
    Judoka judoka;
    Foot activeFoot;
    Vector2 mouseDragStartPoint;

    // Start is called before the first frame update
    void Start()
    {
        judoka = GetComponent<Judoka>();
        activeFoot = judoka.leftFoot; // default to prevent errors
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
            activeFoot = judoka.leftFoot;
            activeFoot.Set_isLifted(true);
            return;
        }
        // Pick up right foot
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            activeFoot = judoka.rightFoot;
            activeFoot.Set_isLifted(true);
            return;
        }
        // Lower left foot
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            judoka.leftFoot.Set_isLifted(false);
            return;
        }
        // Lower right foot
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            judoka.rightFoot.Set_isLifted(false);
            return;
        }

        // Reap
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeFoot.Set_isReaping(true);
        }
        else
            activeFoot.Set_isReaping(false);

    }

    void Shuffle_OnFrame()
    {

    }
}
