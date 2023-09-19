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

    public void _FinishIpponScreen()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ippon");
        if (GameMaster.instance.Get_VictorNum() == 0)
            FindObjectOfType<MultiplayerInator>().StartNewRound();
        else
        {
            print("Starting to load victory");
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Victory", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }
}
