using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeepNumberButtonSelected : MonoBehaviour
{
    [SerializeField] Selectable numberButton;

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject.Equals(gameObject))
            numberButton.Select();
    }
}
