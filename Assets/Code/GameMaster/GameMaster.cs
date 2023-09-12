using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance { get; private set; }

    [SerializeField] short roundNum = 0;
    [SerializeField] string preferredControllerMapName = "Controller3";
    [SerializeField] short scoreToWin = 3;
    [SerializeField] short[] score = new short[2];

    void Awake()
    {
        if (instance != null & instance != this) // you should kill Yourself... NOW
            Destroy(this);
        else
            instance = this;
    }

    void CheckVictory()
    {
        if (score[0] >= scoreToWin)
            print("P1 wins!");
        if (score[1] >= scoreToWin)
            print("P2 wins!");
    }

    #region Getters and Setters
    public short Get_roundNum()
    {
        return roundNum;
    }
    public void IncrementRoundNum() // called before being used (starts at 0)
    {
        roundNum++;
        if (roundNum == 1)
            ClearScore();
    }
    public string Get_preferredControllerActionMapName()
    {
        return preferredControllerMapName;
    }

    // Score
    void ClearScore()
    {
        score[0] = 0;
        score[1] = 0;
    }
    public void PointP1()
    {
        score[0]++;
        CheckVictory();
    }
    public void PointP2()
    {
        score[1]++;
        CheckVictory();
    }

    #endregion
}
