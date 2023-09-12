using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = GameMaster.instance.Get_P1Score() + " - " + GameMaster.instance.Get_P2Score();
    }
}
