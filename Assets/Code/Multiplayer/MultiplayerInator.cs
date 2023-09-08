using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MultiplayerInator : MonoBehaviour
{
    [SerializeField] Transform P1SpawnCircle;
    [SerializeField] Transform P2SpawnCircle;
    [SerializeField] GameObject p1Panel;
    [SerializeField] GameObject p2Panel;
    short numPlayers = 0;
    [SerializeField] [Range(1,5)] short MAX_NUMPLAYERS = 2;

    // flags
    bool hasP1PressedStart = false;
    bool hasP1SpawnBeenAssigned = false;

    void Start()
    {
        // make spawn points invisible in editor
        P1SpawnCircle.gameObject.SetActive(false);
        P2SpawnCircle.gameObject.SetActive(false);

        BeginSpawnIn();
    }

    public void BeginSpawnIn() // called in start and if controllers need to reset
    {
        p1Panel.SetActive(true);
        p2Panel.SetActive(true);

        // reset flags
        hasP1PressedStart = false;
        hasP1SpawnBeenAssigned = false;
    }

    public void MakeNextPanelDisappear() // called by "Player Joined Event" in MultiplayerInator's Player Input Manager
    {
        if (!hasP1PressedStart)
        {
            p1Panel.GetComponent<Animator>().SetTrigger("Shrink");
            hasP1PressedStart = true;
        }
        else
        {
            p2Panel.GetComponent<Animator>().SetTrigger("Shrink");
            hasP1PressedStart = false; // in case this class needs to be used again
        }
    }

    public void SetupPlayer(out short pNum, out Vector2 spawnPos)
    {
        if (!hasP1SpawnBeenAssigned)
        {
            hasP1SpawnBeenAssigned = true;
            pNum = 1;
            spawnPos = P1SpawnCircle.position;
        }
        else
        {
            hasP1SpawnBeenAssigned = false; // in case this class needs to be used again
            pNum = 2;
            spawnPos = P2SpawnCircle.position;
        }
    }

    // prevent using PlayerInputManager's default player cap
    // event called by PlayerInputManager Player Joined Event
    public void IncrementPlayerCount()
    {
        numPlayers++;
        if (numPlayers >= MAX_NUMPLAYERS)
            GetComponent<PlayerInputManager>().DisableJoining();
    }
}
