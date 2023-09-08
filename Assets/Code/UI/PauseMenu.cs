using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("Pause Menu");
        Time.timeScale = 1;
        // Unity says UnloadScene(... is unsafe
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Start Menu");
        // closes pause menu and opens main menu. Hopefully this closes the game
    }
}
