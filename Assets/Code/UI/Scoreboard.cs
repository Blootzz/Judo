using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] GameObject p1Points;
    [SerializeField] GameObject p2Points;
    short maxPoints = 7; // maximum number of points to be displayed for one player

    void Start()
    {
        StartCoroutine(nameof(DisplayScoreBoard));
        GameMaster.instance.updateScoreForScoreboard += AddScore;
    }

    IEnumerator DisplayScoreBoard()
    {
        for(short i=0; i<maxPoints; i++)
        {
            // color filled circle
            p1Points.transform.GetChild(i).GetChild(1).GetComponent<UnityEngine.UI.Image>().color = GameMaster.instance.Get_PXColor(1);
            p2Points.transform.GetChild(i).GetChild(1).GetComponent<UnityEngine.UI.Image>().color = GameMaster.instance.Get_PXColor(2);

            if (i < GameMaster.instance.Get_NumRoundsToWin())
            {
                p1Points.transform.GetChild(i).gameObject.SetActive(true); // activate p1 Point1
                p1Points.transform.GetChild(i).GetChild(1).gameObject.SetActive(false); // deactivate Filled Circle for now
                p2Points.transform.GetChild(i).gameObject.SetActive(true); // activate p2 Point1
                p2Points.transform.GetChild(i).GetChild(1).gameObject.SetActive(false); // deactivate Filled Circle for now
                yield return new WaitForSecondsRealtime(0.25f);
            }
            else
            {
                p1Points.transform.GetChild(i).gameObject.SetActive(false);
                p2Points.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    void AddScore(object obj, MyEventArgs playerNum) // 1 for p1 ippon, 2 for p2 ippon
    {
        if (playerNum.args == 1)
        {
            int index = GameMaster.instance.Get_P1Score() - 1;
            // Points      =>      Point1      =>      FilledCircle.SetActive
            p1Points.transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        if (playerNum.args == 2)
        {
            int index = GameMaster.instance.Get_P2Score() - 1;
            // Points      =>      Point1      =>      FilledCircle.SetActive
            p2Points.transform.GetChild(index).transform.GetChild(1).gameObject.SetActive(true);
            return;
        }

        Debug.LogWarning("Not sure which score board player to add point to");
    }
}
