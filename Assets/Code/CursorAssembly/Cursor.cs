using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    Vector3 realCursorPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        realCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        realCursorPos.z = 0;
        transform.position = realCursorPos;
    }
}
