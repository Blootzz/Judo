using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassMovement : MonoBehaviour
{
    // this class is controlled either by ManualWASDControl or AI

    Vector2 direction = new Vector2(0,0);
    Judoka judoka;
    MassCenter massCenter;
    SpriteRenderer ipponSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        judoka = GetComponent<Judoka>();
        massCenter = GetComponentInChildren<MassCenter>();
        ipponSpriteRenderer = judoka.GetComponentInChildren<IpponCircle>().GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // move self
        massCenter.AddInfluenceToPosition(direction * judoka.Get_WASD_STRENGTH());

        // move opponent if engaged
        if (judoka.opponentMass != null)
            judoka.opponentMass.AddInfluenceToPosition(judoka.Get_KUZUSHI_STRENGTH() * direction);
    }

    public void Set_direction(Vector2 newInput)
    {
        direction = newInput; // apply this value every frame
    }
    public void Set_direction(Vector2 newInput, int pushDirectionCode)
    {
        direction = newInput; // apply this value every frame

        switch(pushDirectionCode)
        {
            case 0:
                ipponSpriteRenderer.material.SetInt("_isActive", 0);
                break;
            case 1:
                ipponSpriteRenderer.material.SetInt("_isActive", 1);
                ipponSpriteRenderer.material.SetInt("_Direc_PushPos_PullNeg", pushDirectionCode);
                break;
            case -1:
                ipponSpriteRenderer.material.SetInt("_isActive", 1);
                ipponSpriteRenderer.material.SetInt("_Direc_PushPos_PullNeg", pushDirectionCode);
                break;
            default:
                Debug.LogWarning("Not sure what direction to move shader based of pushDirectionCode");
                break;
        }
            
    }

}
