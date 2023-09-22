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
    [SerializeField] Color32[] p4Colors = new Color32[5];
    [SerializeField] Color32[] p5Colors = new Color32[5];
    [SerializeField] Color32[] p6Colors = new Color32[5];
    [SerializeField] Color32[] p7Colors = new Color32[5];
    [SerializeField] Color32[] p8Colors = new Color32[5];

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
            case 4:
                return p4Colors;
            case 5:
                return p5Colors;
            case 6:
                return p6Colors;
            case 7:
                return p7Colors;
            case 8:
                return p8Colors;
            default:
                Debug.LogWarning("No suitable color palette found. Returning p1Colors");
                return p1Colors;
        }
    }

    public Color32 Get_PrimaryColorFromPalette(int colorNum)
    {
        switch(colorNum)
        {
            case 1:
                return p1Colors[2];
            case 2:
                return p2Colors[2];
            case 3:
                return p3Colors[2];
            case 4:
                return p4Colors[2];
            case 5:
                return p5Colors[2];
            case 6:
                return p6Colors[2];
            case 7:
                return p7Colors[2];
            case 8:
                return p8Colors[2];
            default:
                Debug.LogWarning("No suitable color palette found. Returning p1Colors");
                return p1Colors[2];
        }
    }
}
