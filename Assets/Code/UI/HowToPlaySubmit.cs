using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HowToPlaySubmit : MonoBehaviour
{
    public void OpenMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneManager.LoadScene("Start Menu");
        }
    }
}
