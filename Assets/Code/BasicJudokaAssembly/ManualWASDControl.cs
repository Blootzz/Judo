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
    IpponCircle ipponCircle;
    Vector3 workingIpponDirection;

    // Start is called before the first frame update
    void Start()
    {
        directionalMovement = GetComponent<MassMovement>();
        parentJudoka = GetComponent<Judoka>();
        ipponCircle = GetComponentInChildren<IpponCircle>();
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
        float newIpponAngle = Mathf.Atan2(opponentMassMinusMass.y, opponentMassMinusMass.x) * Mathf.Rad2Deg;
        ipponCircle.transform.eulerAngles = new Vector3(0, 0, newIpponAngle);

        // opponentMassMinusMass yields correct direction when pushing
        // push: x1
        // pull: x-1
        // use code to figure out which way the shader should go
        int pushDirectionCode = 0;
        if (context.ReadValue<float>() > 0.5f)
            pushDirectionCode = 1;
        if (context.ReadValue<float>() < -0.5f)
            pushDirectionCode = -1;

        directionalMovement.Set_direction(context.ReadValue<float>() * parentJudoka.Get_KUZUSHI_STRENGTH() * opponentMassMinusMass, pushDirectionCode);
    }

}
