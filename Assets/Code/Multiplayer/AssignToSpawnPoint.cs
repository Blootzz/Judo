using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignToSpawnPoint : MonoBehaviour // Called by MultiplayerInator on Join
{
    public void AssignSpawnPoint() // called by "Player Joined Event" in MultiplayerInator's Player Input Manager
    {
        transform.position = FindObjectOfType<MultiplayerInator>().GetNextSpawnPoint();
        // SET LAYER AND COLORS HERE TOO??
    }
}
