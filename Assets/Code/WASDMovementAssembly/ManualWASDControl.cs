using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualWASDControl : MonoBehaviour
{
    DirectionalMovement directionalMovement;
    Vector2 input = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        directionalMovement = GetComponent<DirectionalMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;

        directionalMovement.Set_direction(input);
    }
}
