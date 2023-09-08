using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseAbility : MonoBehaviour
{
    public void TogglePause(InputAction.CallbackContext context)
    {
        print("Attempting to toggle pause");
        if (!context.started)
            return;

        if (SceneManager.GetSceneByName("Pause Menu").isLoaded)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Pause Menu");
        }
        else
        {
            Time.timeScale = 0;
            SceneManager.LoadSceneAsync("Pause Menu", LoadSceneMode.Additive);
        }
    }
}
