using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFirstAvailableButton : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        ReSelectFistButton();
    }

    public void ReSelectFistButton() //  aslo called by safety button
    {
        if (transform.GetChild(0).gameObject.activeSelf)
            transform.GetChild(0).GetComponent<Selectable>().Select();
        else
            transform.GetChild(1).GetComponent<Selectable>().Select();
    }

}
