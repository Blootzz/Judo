using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = GameMaster.instance.Get_P1Score() + " - " + GameMaster.instance.Get_P2Score();
        // reset score is done in GameMaster when round 1 starts
    }

    public void _FinishIpponScreen()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ippon");
        if (GameMaster.instance.Get_VictorNum() == 0)
            FindObjectOfType<MultiplayerInator>().StartNewRound();
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Victory", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }
}
