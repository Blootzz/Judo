using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance { get; private set; }

    [SerializeField] short roundNum = 0;
    [SerializeField] string preferredControllerMapName = "Controller3";

    void Awake()
    {
        if (instance != null & instance != this) // you should kill Yourself... NOW
            Destroy(this);
        else
            instance = this;
    }

    #region Getters and Setters
    public short Get_roundNum()
    {
        return roundNum;
    }
    public void IncrementRoundNum() // called before being used (starts at 0)
    {
        roundNum++;
    }
    public string Get_preferredControllerActionMapName()
    {
        return preferredControllerMapName;
    }
    #endregion
}
