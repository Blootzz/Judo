using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour // Called by MultiplayerInator on Join
{
    short playerNum = 0;
    Vector2 playerPos;

    void OnEnable()
    {
        GetPlayerInfoFromMultiplayerInator();
        SetSpawnPosAndLayers();
    }

    void GetPlayerInfoFromMultiplayerInator()
    {
        // gets playerNum and playerPos
        FindObjectOfType<MultiplayerInator>().SetupPlayer(out playerNum, out playerPos);
    }

    void SetSpawnPosAndLayers()
    {
        gameObject.name = "Player" + playerNum.ToString(); // rename object
        transform.position = playerPos; // set position

        #region Set layers of game object and every nested child

        gameObject.layer = LayerMask.NameToLayer("Player" + playerNum.ToString()); // game object
        foreach(Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.layer = LayerMask.NameToLayer("Player" + playerNum.ToString()); // child
            foreach (Transform childOfChildTransform in childTransform.transform)
                childOfChildTransform.gameObject.layer = LayerMask.NameToLayer("Player" + playerNum.ToString()); // child of child
        }
        #endregion

        // SET LAYER AND COLORS HERE TOO??
    }
}
