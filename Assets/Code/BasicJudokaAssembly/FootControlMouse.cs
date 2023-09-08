using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FootControlMouse : MonoBehaviour
{
    [SerializeField] bool followCursorInsteadOfDelta = false;
    FootController footController;
    Cursor cursor;
    [SerializeField] float MOUSE_SENSITIVITY = 1;
    [SerializeField] float GAMEPAD_SENSITIVITY = 1;
    float sensitivity = 1;

    // for Controller2 action map
    bool isLeftStickTheFootStick = true;

    [SerializeField] bool HIDE_MOUSE = false;

    void Start()
    {
        footController = GetComponent<FootController>();
        cursor = FindObjectOfType<Cursor>();

        if (HIDE_MOUSE)
        {
            UnityEngine.Cursor.visible = false; // makes mouse invisible
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            Debug.LogWarning("Hiding Mouse");
        }
    }

    // detect inputs from pllayer
    void Update()
    {
        // movement
        if (followCursorInsteadOfDelta)
            FollowCursor();
    }

    void FollowCursor() // only to be used if not using the event-based input
    {
        if (cursor != null)
            footController.Set_moveTarget(cursor.transform.position, true); // true means input will be treated like destination
        else
            Debug.LogWarning("Trying to follow non-existant cursor");
    }

    // Events
    #region Events for controls
    public void TranslateByDelta(InputAction.CallbackContext context)
    {
        if (!followCursorInsteadOfDelta)
        {
            char controllerInitial = context.control.device.name[0];
            switch(controllerInitial)
            {
                case 'M':
                    sensitivity = MOUSE_SENSITIVITY;
                    break;
                case 'X':
                    sensitivity = GAMEPAD_SENSITIVITY;
                    break;
                case 'G':
                    sensitivity = GAMEPAD_SENSITIVITY;
                    break;
                default:
                    Debug.LogWarning("No input device option found for moving");
                    break;
            }
            footController.Set_moveTarget(context.ReadValue<Vector2>() * sensitivity, false); // false means input will be treated as direction
        }
    }
    public void PickUpLeft(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
        {
            footController.PickUpLeftFoot();
            // for Controller2 input map
            if (GetComponent<PlayerInput>().currentActionMap.name.Equals("Controller2"))
                isLeftStickTheFootStick = true;
        }
        else
            footController.LowerLeftFoot();
    }
    public void PickUpRight(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() == true)
        {
            footController.PickUpRightFoot();
            // for Controller2 input map
            if (GetComponent<PlayerInput>().currentActionMap.name.Equals("Controller2"))
                isLeftStickTheFootStick = false;
        }
        else
            footController.LowerRightFoot();
    }
    public void Reap(InputAction.CallbackContext context)
    {
        footController.ReapOnOrOff(context.ReadValueAsButton());
    }
    #endregion

    #region Controller2 Map Events
    public void MoveEitherStick(InputAction.CallbackContext context)
    {
        if (context.action.name.Equals("Move Left Stick")) // if left stick is being used
        {
            if (isLeftStickTheFootStick)
                TranslateByDelta(context);
            else
                GetComponent<MassMovement>().Set_direction(context.ReadValue<Vector2>());
        }
        else // if right stick is being used
        {
            if (!isLeftStickTheFootStick) // if right stick is for moving
                TranslateByDelta(context);
            else
                GetComponent<MassMovement>().Set_direction(context.ReadValue<Vector2>());
        }
    }
    #endregion

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
