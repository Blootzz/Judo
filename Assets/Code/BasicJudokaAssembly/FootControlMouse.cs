using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FootControlMouse : MonoBehaviour
{
    [SerializeField] bool followCursorInsteadOfDelta = false;
    FootController footController;
    Cursor cursor;

    void Start()
    {
        footController = GetComponent<FootController>();
        cursor = FindObjectOfType<Cursor>();
    }

    // detect inputs from pllayer
    void Update()
    {
        // movement
        if (followCursorInsteadOfDelta)
            FollowCursor();
    }

    void FollowCursor()
    {
        if (cursor != null)
            footController.Set_moveTargetDestination(cursor.transform.position, false); // false means no other operations are done to moveTargetDestination
        else
            Debug.LogWarning("Trying to follow non-existant cursor");
    }

    public void TranslateByDelta(InputAction.CallbackContext context)
    {
        if (!followCursorInsteadOfDelta)
            footController.Set_moveTargetDestination(context.ReadValue<Vector2>(), true); // true means the parameter will be added to existing position
    }

    // Events
    public void PickUpLeft(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
            footController.PickUpLeftFoot();
        else
            footController.LowerLeftFoot();
    }
    public void PickUpRight(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
            footController.PickUpRightFoot();
        else
            footController.LowerRightFoot();
    }

    public void Reap(InputAction.CallbackContext context)
    {
        footController.ReapOnOrOff(context.ReadValueAsButton());
    }

//footController.ReapOnOrOff(Input.GetKey(KeyCode.LeftShift));


    /*void ProcessButtons() // Old input system
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
    }*/
}
