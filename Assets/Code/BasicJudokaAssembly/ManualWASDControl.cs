using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualWASDControl : MonoBehaviour
{
    MassMovement directionalMovement;
    Vector2 input = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        directionalMovement = GetComponent<MassMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //input.x = Input.GetAxisRaw("Horizontal");
        //input.y = Input.GetAxisRaw("Vertical");
        //input = input.normalized;

        //directionalMovement.Set_direction(input);
    }

    public void SendInputToMovement(InputAction.CallbackContext context)
    {
        // if no new event is called, this direction applies to the next frame
        directionalMovement.Set_direction(context.ReadValue<Vector2>());
    }

}
