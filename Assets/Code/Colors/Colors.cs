using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    /// <summary>
    /// Order:
    /// 1- Center of balance
    /// 2- Ippon Circle
    /// 3- Mass Center / Outline
    /// 4- Left Foot
    /// 5- Right Foot
    /// </summary>
    
    [Header("0-Center, 1-Ippon, 2-Mass, 3-Left, 4-Right")]
    [SerializeField] Color32[] p1Colors = new Color32[5];
    [SerializeField] Color32[] p2Colors = new Color32[5];
    [SerializeField] Color32[] p3Colors = new Color32[5];

    public Color32[] Get_ColorPaletteForPlayer(int playerNum)
    {
        switch(playerNum)
        {
            case 1:
                return p1Colors;
            case 2:
                return p2Colors;
            case 3:
                return p3Colors;
            default:
                Debug.LogWarning("No suitable color palette found. Returning p1Colors");
                return p1Colors;
        }
    }
}
