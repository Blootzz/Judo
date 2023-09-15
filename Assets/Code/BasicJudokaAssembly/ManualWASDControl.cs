using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManualWASDControl : MonoBehaviour
{
    MassMovement directionalMovement;
    Vector2 opponentMassMinusMass;
    Vector2 input = new Vector2(0,0);
    Judoka parentJudoka;

    // Start is called before the first frame update
    void Start()
    {
        directionalMovement = GetComponent<MassMovement>();
        parentJudoka = GetComponent<Judoka>();
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
        opponentMassMinusMass = parentJudoka.opponent.GetComponentInChildren<MassCenter>().transform.position - GetComponentInChildren<MassCenter>().transform.position;
        opponentMassMinusMass = opponentMassMinusMass.normalized;
        directionalMovement.Set_direction(context.ReadValue<float>() * parentJudoka.Get_KUZUSHI_STRENGTH() * opponentMassMinusMass);
    }

}
