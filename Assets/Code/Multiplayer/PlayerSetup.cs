using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerSetup : MonoBehaviour // Called by MultiplayerInator on Join
{
    short playerNum = 0; // either 1 or 2
    Vector2 playerSpawnPos;

    void OnEnable()
    {
        GetPlayerInfoFromMultiplayerInator();
        ResetPosition();
        SetLayers();
        AssignColor();
        AssignControls();
        GetComponentInChildren<MassCenter>().iJustGotIppowned += GivePointToOtherPlayer;
    }

    void GetPlayerInfoFromMultiplayerInator()
    {
        // gets playerNum and playerPos
        FindObjectOfType<MultiplayerInator>().SetupPlayer(this.gameObject, out playerNum, out playerSpawnPos);
    }

    void SetLayers()
    {
        gameObject.name = "Player" + playerNum.ToString(); // rename object

        #region Set layers of game object and every nested child

        gameObject.layer = LayerMask.NameToLayer("Player" + playerNum.ToString()); // game object
        foreach(Transform childTransform in gameObject.transform)
        {
            childTransform.gameObject.layer = LayerMask.NameToLayer("Player" + playerNum.ToString()); // child
            foreach (Transform childOfChildTransform in childTransform.transform)
                childOfChildTransform.gameObject.layer = LayerMask.NameToLayer("Player" + playerNum.ToString()); // child of child
        }
        #endregion

        #region Ippon circle outline/mask layers
        GameObject ipponCircle = GetComponentInChildren<CenterOfBalance>().GetComponentInChildren<IpponCircle>().gameObject;
        ipponCircle.transform.GetChild(0).GetComponent<SpriteMask>().frontSortingOrder = playerNum*2 + 2;
        ipponCircle.transform.GetChild(0).GetComponent<SpriteMask>().backSortingOrder = playerNum*2 + 1;
        ipponCircle.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = playerNum * 2 + 2;

        //ipponCircle.transform.GetChild(0).GetComponent<SpriteMask>().renderingLayerMask = (uint) 1 << playerNum;
        //ipponCircle.transform.GetChild(1).GetComponent<SpriteRenderer>().renderingLayerMask = (uint) 1 << playerNum;
        //print("Setting to 1 << playerNum: " + ((uint) 1 << playerNum));
        #endregion
    }

    void AssignColor()
    {
        print("assign color here");
        Color32[] myPalette = GameMaster.instance.Get_PXColorPalette(playerNum);
        GetComponentInChildren<CenterOfBalance>().gameObject.GetComponent<SpriteRenderer>().color = myPalette[0];
        GetComponentInChildren<IpponCircle>().gameObject.GetComponent<SpriteRenderer>().color = myPalette[1];
        GetComponentInChildren<IpponCircle>().transform.GetChild(1).GetComponent<SpriteRenderer>().color = myPalette[2];
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

    void GivePointToOtherPlayer(object sender, EventArgs e)
    {
        switch(playerNum)
        {
            case 1:
                GameMaster.instance.PointP2();
                break;
            case 2:
                GameMaster.instance.PointP1();
                break;
            default:
                Debug.LogWarning("Not sure who to give point based off playerNum");
                break;
        }
    }

    public void ResetPosition()
    {
        transform.position = playerSpawnPos;
        GetComponentInChildren<MassCenter>().transform.position = playerSpawnPos;
        GetComponentInChildren<CenterOfBalance>().transform.position = playerSpawnPos;
        GetComponentInChildren<FeetCenterline>().transform.GetChild(0).transform.position = playerSpawnPos - new Vector2(1, 0); // left foot
        GetComponentInChildren<FeetCenterline>().transform.GetChild(1).transform.position = playerSpawnPos - new Vector2(-1, 0); // right foot
    }
}
