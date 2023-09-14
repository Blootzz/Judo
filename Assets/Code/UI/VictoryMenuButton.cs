using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuButton : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
