using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    int index = -1;
    public GameObject[] buttonArray;
    [SerializeField] GameObject BestOfScreen;

    public void OpenBestOfScreen()
    {
        Time.timeScale = 1; // reset from last game over;
        BestOfScreen.SetActive(true);
        gameObject.SetActive(false);
    }
    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("How To Play");
    }
    public void PlayTutorial()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Time.timeScale = 1; // reset from last game over;
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        print("Quitting game");
        Application.Quit();
    }

    //Checks if the object is a slider or button, then changes its color value
    private void setColor(int index, Color32 newColor)
    {
        Image buttonComponent = buttonArray[index].GetComponent<Image>();
        if (buttonComponent != null)
        {
              buttonArray[index].GetComponent<Button>().Select();
            buttonComponent.color = newColor;
        }
        else
        {
            ColorBlock color = buttonArray[index].GetComponent<Slider>().colors;
              buttonArray[index].GetComponent<Slider>().Select();
            color.normalColor = newColor;

            buttonArray[index].GetComponent<Slider>().colors = color;
        }
    }

    //Resets the index and sets the selected button to white
    void OnDisable()
    {
        if (index != -1)
        {

            setColor(index, new Color32(255, 255, 225, 225));
            Debug.Log("PrintOnEnable: script was enabled");
            index = -1;
        }
    }
}
