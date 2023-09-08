using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassMovement : MonoBehaviour
{
    // this class is controlled either by ManualWASDControl or AI

    Vector2 direction = new Vector2(0,0);
    Judoka judoka;
    MassCenter massCenter;

    // Start is called before the first frame update
    void Start()
    {
        judoka = GetComponent<Judoka>();
        massCenter = GetComponentInChildren<MassCenter>();
    }

    // Update is called once per frame
    void Update()
    {
        // move self
        massCenter.AddInfluenceToPosition(direction * judoka.Get_WASD_STRENGTH());

        // move opponent if engaged
        if (judoka.opponentMass != null)
            judoka.opponentMass.AddInfluenceToPosition(judoka.Get_KUZUSHI_STRENGTH() * direction);
    }

    public void Set_direction(Vector2 newInput)
    {
        direction = newInput; // apply this value every frame
    }

}
