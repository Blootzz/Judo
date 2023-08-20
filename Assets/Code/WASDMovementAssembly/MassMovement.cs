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
        massCenter.AddInfluenceToPosition(direction * judoka.WASD_STRENGTH);
    }

    public void Set_direction(Vector2 newInput)
    {
        direction = newInput;
        if (judoka.opponentMass != null)
            judoka.opponentMass.AddInfluenceToPosition(judoka.OPPONENT_STRENGTH * direction);
    }

}
