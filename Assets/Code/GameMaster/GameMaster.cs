using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance { get; private set; }

    [SerializeField] short roundNum = 0;
    [SerializeField] string preferredControllerMapName = "Controller4";
    [SerializeField] short[] score = new short[2];
    [SerializeField] short victorNum = 0; // 0-no victor, 1-p1, 2-p2
    [SerializeField] short numRoundsToWin = 2;
    [SerializeField] short MAXNUMROUNDSTOWIN = 7;
    [SerializeField] short p1ColorNum = 0;
    [SerializeField] short p2ColorNum = 0;
    [SerializeField] GameObject ColorPrefab;

    public EventHandler<MyEventArgs> updateScoreForScoreboard;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance != null & instance != this) // you should kill Yourself... NOW
            Destroy(this);
        else
            instance = this;
    }

    void CheckVictory()
    {
        if (score[0] >= numRoundsToWin)
            victorNum = 1;
        if (score[1] >= numRoundsToWin)
            victorNum = 2;
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
        updateScoreForScoreboard?.Invoke(this, new MyEventArgs { args = 1 }); // listened to in Scoreboard.cs
    }
    public void PointP2()
    {
        score[1]++;
        CheckVictory();
        updateScoreForScoreboard?.Invoke(this, new MyEventArgs { args = 2 }); // listened to in Scoreboard.cs
    }
    public short Get_P1Score()
    {
        return score[0];
    }
    public short Get_P2Score()
    {
        return score[1];
    }

    public short Get_VictorNum()
    {
        return victorNum;
    }

    public void Increment_NumRoundsToWin(short round)
    {
        numRoundsToWin += round;
        if (numRoundsToWin > MAXNUMROUNDSTOWIN)
        {
            numRoundsToWin = 1;
            return;
        }
        if (numRoundsToWin < 1)
            numRoundsToWin = MAXNUMROUNDSTOWIN;
    }
    public short Get_NumRoundsToWin()
    {
        return numRoundsToWin;
    }

    public void Set_PXColor(short pNum, short colorNum)
    {
        if (pNum == 1)
        {
            p1ColorNum = colorNum;
            return;
        }
        if (pNum == 2)
        {
            p2ColorNum = colorNum;
            return;
        }
        Debug.LogWarning("Not sure what color to assign based off the pNum");
    }
    public Color32 Get_PXColor(short pNum)
    {
        if (pNum == 1)
            return ColorPrefab.GetComponent<Colors>().Get_PrimaryColorFromPalette(p1ColorNum);
        if (pNum == 2)
            return ColorPrefab.GetComponent<Colors>().Get_PrimaryColorFromPalette(p2ColorNum);
        Debug.LogWarning("Not Sure what player's color to return");
        return Color.black;
    }
    public Color32[] Get_PXColorPalette(short pNum)
    {
        if (pNum == 1)
            return ColorPrefab.GetComponent<Colors>().Get_ColorPaletteForPlayer(p1ColorNum);
        if (pNum == 2)
            return ColorPrefab.GetComponent<Colors>().Get_ColorPaletteForPlayer(p2ColorNum);
        Debug.LogWarning("Not Sure what player's color to return. Returning white");
        return ColorPrefab.GetComponent<Colors>().Get_ColorPaletteForPlayer(7);
    }

    #endregion
}

public class MyEventArgs : EventArgs
{
    public float args { get; set; }
}
