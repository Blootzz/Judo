using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualWASDControl : MonoBehaviour
{
    MassMovement directionalMovement;
    Judoka parentJudoka;
    IpponCircle ipponCircle;

    // Start is called before the first frame update
    void Start()
    {
        directionalMovement = GetComponent<MassMovement>();
        parentJudoka = GetComponent<Judoka>();
        ipponCircle = GetComponentInChildren<IpponCircle>();
        directionalMovement.Set_direction(Vector2.zero, 0);
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

    // Used in Controller4 Action Map
    public void PushPullMass(InputAction.CallbackContext context)
    {
        // moved rest of shader rotation code to CenterOfBalance.cs

        // opponentMassMinusMass yields correct direction when pushing
        // push: x1
        // pull: x-1
        // use code to figure out which way the shader should go
        int pushDirectionCode = 0;
        if (context.ReadValue<float>() > 0.5f)
            pushDirectionCode = 1;
        if (context.ReadValue<float>() < -0.5f)
            pushDirectionCode = -1;

        directionalMovement.Set_direction(context.ReadValue<float>() * parentJudoka.Get_KUZUSHI_STRENGTH() * ipponCircle.opponentMassMinusMass, pushDirectionCode);
    }

}
