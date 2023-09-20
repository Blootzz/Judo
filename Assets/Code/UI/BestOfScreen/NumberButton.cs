using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NumberButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myTMPro;
    short roundsToWin;
    int bestOf;

    // Update is called once per frame
    void Update()
    {
        roundsToWin = GameMaster.instance.Get_NumRoundsToWin();
        bestOf = roundsToWin * 2 - 1;
        myTMPro.text = bestOf.ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Multiplayer test");
    }
}
