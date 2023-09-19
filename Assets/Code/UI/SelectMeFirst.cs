using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SelectMeFirst : MonoBehaviour
{
    void OnEnable()
    {
        Selectable selectMe = GetComponent<Selectable>();
        if (selectMe)
            selectMe.Select();
        else
            Debug.LogWarning("No selectable UI component found");
    }



}
