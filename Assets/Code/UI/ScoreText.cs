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
        FindObjectOfType<MultiplayerInator>().StartNewRound();
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Ippon");
    }
}
