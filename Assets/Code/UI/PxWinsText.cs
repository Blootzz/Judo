using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PxWinsText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "PlayeR " + GameMaster.instance.Get_VictorNum() + " WINS";
    }
}
