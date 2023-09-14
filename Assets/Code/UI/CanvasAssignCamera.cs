using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAssignCamera : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
