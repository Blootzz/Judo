using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMovement : MonoBehaviour
{
    // this class is controlled either by ManualWASDControl or AI

    Vector2 direction = new Vector2(0,0);
    MassCenter massCenter;

    // Start is called before the first frame update
    void Start()
    {
        massCenter = GetComponent<MassCenter>();
    }

    // Update is called once per frame
    void Update()
    {
        massCenter.AddInfluenceToPosition(direction * massCenter.WASD_STRENGTH);
    }

    public void Set_direction(Vector2 newInput)
    {
        direction = newInput;
    }

}
