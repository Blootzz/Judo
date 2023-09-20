using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IncrementButton : MonoBehaviour
{
    Animator animator;
    [SerializeField] Selectable numberButton;
    [SerializeField] short incrementValue = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject.Equals(gameObject))
        { 
            numberButton.Select();
            animator.Play("IncrementClick", -1, 0);
            GameMaster.instance.Increment_NumRoundsToWin(incrementValue);
        }
    }

    public void OnClick() // called by button component
    {
        //animator.Play("IncrementClick", -1, 0);
        //GameMaster.instance.Increment_NumRoundsToWin(incrementValue);
    }
}
