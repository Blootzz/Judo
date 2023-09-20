using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class MultiplayerInator : MonoBehaviour
{
    [SerializeField] Transform P1SpawnCircle;
    [SerializeField] Transform P2SpawnCircle;
    [SerializeField] GameObject p1Panel;
    [SerializeField] GameObject p2Panel;
    [SerializeField] GameObject p1ColorMenu;
    [SerializeField] GameObject p2ColorMenu;
    short numPlayers = 0;
    [SerializeField] [Range(1,5)] short MAX_NUMPLAYERS = 2;
    [SerializeField] GameObject roundText;
    GameObject player1;
    GameObject player2;

    // flags
    bool hasP1PressedStart = false;
    bool hasP1SpawnBeenAssigned = false;

    // Event for allowing players to gain control
    public event EventHandler hajimeEvent;

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
            p1ColorMenu.gameObject.SetActive(true);
            hasP1PressedStart = true;
        }
        else
        {
            p2ColorMenu.gameObject.SetActive(true);
            hasP1PressedStart = false; // in case this class needs to be used again
        }
    }

    public void SetupPlayer(GameObject player, out short pNum, out Vector2 spawnPos)
    {
        if (!hasP1SpawnBeenAssigned)
        {
            player1 = player;
            hasP1SpawnBeenAssigned = true;
            pNum = 1;
            spawnPos = P1SpawnCircle.position;
        }
        else
        {
            player2 = player;
            player1.GetComponent<Judoka>().opponent = player2;
            player2.GetComponent<Judoka>().opponent = player1;
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
        {
            GetComponent<PlayerInputManager>().DisableJoining();
            RoundAndHajimeUI();
        }
    }

    void RoundAndHajimeUI()
    {
        // kick off and on to call OnEnable
        roundText.SetActive(false);
        roundText.SetActive(true);
    }

    public void Hajime() // called in _Hajime() in RoundText.cs
    {
        // ?.Invoke checks if null before calling event
        hajimeEvent?.Invoke(this, EventArgs.Empty); // event subscribed to in WaitForHajime.cs
    }

    public void StartNewRound()
    {
        player1.GetComponent<PlayerSetup>().ResetPosition();
        player2.GetComponent<PlayerSetup>().ResetPosition();
        player1.GetComponent<WaitForHajime>().Start();
        player2.GetComponent<WaitForHajime>().Start();

        RoundAndHajimeUI();

        Time.timeScale = 1;
    }
}
