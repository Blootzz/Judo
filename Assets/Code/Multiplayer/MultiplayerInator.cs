using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerInator : MonoBehaviour
{
    Vector2 p1StartPos;
    Vector2 p2StartPos;

    void Start()
    {
        // assign positions
        p1StartPos = transform.GetChild(0).position;
        p2StartPos = transform.GetChild(1).position;
        // make spawn points invisible in editor
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        BeginSpawnIn();
    }

    void BeginSpawnIn() // called in start
    {

    }

}
