using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerInator : MonoBehaviour
{
    [SerializeField] GameObject P1SpawnCircle;
    [SerializeField] GameObject P2SpawnCircle;
    [SerializeField] GameObject p1Panel;
    [SerializeField] GameObject p2Panel;
    Vector2 p1StartPos;
    Vector2 p2StartPos;

    bool isWaitingOnP1 = true;

    void Start()
    {
        // assign positions
        p1StartPos = P1SpawnCircle.transform.position;
        p2StartPos = P2SpawnCircle.transform.position;
        // make spawn points invisible in editor
        P1SpawnCircle.SetActive(false);
        P2SpawnCircle.SetActive(false);
        
        BeginSpawnIn();
    }

    public void BeginSpawnIn() // called in start and if controllers need to reset
    {
        p1Panel.SetActive(true);
        p2Panel.SetActive(true);
        isWaitingOnP1 = true;
    }

    public void MakeNextPanelDisappear()
    {
        if (isWaitingOnP1)
        {
            p1Panel.GetComponent<Animator>().SetTrigger("Shrink");
            isWaitingOnP1 = false;
        }
        else
            p2Panel.GetComponent<Animator>().SetTrigger("Shrink");

    }
}
