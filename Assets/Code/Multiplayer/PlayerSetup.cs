using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour // Called by MultiplayerInator on Join
{
    short playerNum = 0;
    Vector2 playerPos;
    [SerializeField] GameObject ColorPrefab;

    void OnEnable()
    {
        GetPlayerInfoFromMultiplayerInator();
        SetSpawnPosAndLayers();
        AssignColor();
        AssignControls();
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
    }

    void AssignColor()
    {
        Color32[] myPalette = ColorPrefab.GetComponent<Colors>().Get_ColorPaletteForPlayer(playerNum);
        GetComponentInChildren<CenterOfBalance>().gameObject.GetComponent<SpriteRenderer>().color = myPalette[0];
        GetComponentInChildren<IpponCircle>().gameObject.GetComponent<SpriteRenderer>().color = myPalette[1];
        GetComponentInChildren<MassCenter>().gameObject.GetComponent<SpriteRenderer>().color = myPalette[2];
        GetComponent<Judoka>().leftFoot.gameObject.GetComponent<SpriteRenderer>().color = myPalette[3];
        GetComponent<Judoka>().rightFoot.gameObject.GetComponent<SpriteRenderer>().color = myPalette[4];
    }

    void AssignControls()
    {
        PlayerInput PI = GetComponent<PlayerInput>();
        if (PI.GetDevice<InputDevice>().displayName[0] == 'G' || PI.GetDevice<InputDevice>().displayName[0] == 'X')
            PI.SwitchCurrentActionMap(GameMaster.instance.Get_preferredControllerActionMapName());
    }
}
