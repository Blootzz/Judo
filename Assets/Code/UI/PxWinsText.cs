using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PxWinsText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "PlayeR " + GameMaster.instance.Get_VictorNum() + " WINS";
        GameObject blackBrush = transform.parent.GetChild(1).gameObject;
        Color darkVictorColor = GameMaster.instance.Get_PXColorPalette(GameMaster.instance.Get_VictorNum())[1];
        if (darkVictorColor.Equals(Color.white))
            print("color is white");
        blackBrush.GetComponent<Image>().color = darkVictorColor;
        blackBrush.transform.GetChild(0).GetComponent<Image>().color = darkVictorColor;
    }
}
