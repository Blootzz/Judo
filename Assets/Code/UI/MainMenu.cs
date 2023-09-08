using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    int index = -1;
    public GameObject[] buttonArray;
    //public TMPro.TextMeshProUGUI textThatMightBeGlowing;

    private void Awake()
    {
        //textThatMightBeGlowing.fontSharedMaterial.SetFloat("_GlowPower", 0);
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Time.timeScale = 1; // reset from last game over;
        SceneManager.LoadScene("Game");
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


    //public void Update()
    //{
    //    //Removes the highlight if the mouse button is clicked
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (index != -1)
    //        {
    //            setColor(index, new Color32(255, 255, 225, 225));
    //        }
    //        index = -1;
    //    }

    //    //Controls slider
    //    if (Input.GetKeyDown("left") || Input.GetKeyDown("right"))
    //    {
    //        if (index != -1)


    //        {
    //            Slider sliding = buttonArray[index].GetComponent<Slider>();
    //            if (sliding != null)
    //            {
    //                  sliding.Select();
    //            }
    //        }
    //    }

    //    //Change index value to select a button
    //    if (Input.GetKeyDown("up") || Input.GetKeyDown("down"))
    //    {
    //        if (index == -1)
    //        {
    //            index = 0;
    //            setColor(index, new Color32(0, 255, 0, 225));
    //        }
    //        else
    //        {
    //            setColor(index, new Color32(255, 255, 225, 225));
    //            if (Input.GetKeyDown("up"))
    //            {
    //                index -= 1;
    //                Debug.Log("up key was pressed");
    //            }
    //            if (Input.GetKeyDown("down"))
    //            {
    //                index += 1;
    //                Debug.Log("down key was pressed");
    //            }
    //        }

    //        //Keeps index in range of buttons
    //        if (index < 0)
    //        {
    //            index = buttonArray.Length - 1;
    //        }
    //        index = index % buttonArray.Length;
    //        setColor(index, new Color32(0, 255, 0, 225));
    //    }

    //    //Run button script
    //    if (Input.GetKeyDown("return"))
    //    {
    //        Image buttonComponent = buttonArray[index].GetComponent<Image>();
    //        if (buttonComponent != null)
    //        {
    //            print("Picked Button");
    //            if (index == -1)
    //            {
    //                index = 0;
    //                buttonComponent.color = new Color32(0, 255, 0, 225);
    //            }
    //            else
    //            {
    //                buttonArray[index].GetComponent<Button>().onClick.Invoke();
    //            }
    //        }
    //    }
    //}
}
