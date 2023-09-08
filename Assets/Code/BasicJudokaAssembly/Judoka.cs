using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Judoka : MonoBehaviour
{
    [Header("Position Influence Multipliers")]
    // these variables should not be changed by code. They should be used to multiply arguments of AddInfluenceToPos(Vector2 influence)
    [SerializeField] float WASD_STRENGTH = 1;
    [SerializeField] float CENTERLINE_PULL_STRENGTH = 1;
    [SerializeField] float CENTERLINE_PUSH_STRENGTH = 1;
    [SerializeField] float KUZUSHI_STRENGTH = 1;
    [SerializeField] float REAPING_PUSH_STRENGTH = 1;

    [Header("Push/Pull Boundary")]
    public float balanceBoundary_insideStance = 0.5f;
    public float balanceBoundary_outsideStance = 0.5f;

    [Header("Foot References")]
    public Foot leftFoot;
    public Foot rightFoot;

    [Header("")]
    public bool isEngagedWithOpponent = false;

    [HideInInspector] public MassCenter opponentMass;

    // Needs to be called before Start() so that FeetCenterline.Start can use these references
    void Awake()
    {
        Foot[] footArray = GetComponentsInChildren<Foot>();
        foreach (Foot foot in footArray)
            if (foot.CompareTag("LeftFoot"))
                leftFoot = foot;
            else
                rightFoot = foot;
    }

    #region Getters
    public float Get_WASD_STRENGTH()
    {
        return WASD_STRENGTH;
    }
    public float Get_CENTERLINE_PULL_STRENGTH()
    {
        return CENTERLINE_PULL_STRENGTH;
    }
    public float Get_CENTERLINE_PUSH_STRENGTH()
    {
        return CENTERLINE_PUSH_STRENGTH;
    }
    public float Get_KUZUSHI_STRENGTH()
    {
        return KUZUSHI_STRENGTH;
    }
    public float Get_REAPING_PUSH_STRENGTH()
    {
        return REAPING_PUSH_STRENGTH;
    }
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(Judoka))]
class JudokaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var judoka = (Judoka)target;
        if (judoka == null)
            return;

        EditorGUILayout.LabelField("Controls");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Designated Sticks"))
        {
            judoka.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }
        if (GUILayout.Button("Symmetric Sticks"))
        {
            judoka.GetComponent<PlayerInput>().SwitchCurrentActionMap("Controller2");
        }
        EditorGUILayout.EndHorizontal();

        DrawDefaultInspector();
    }
}
# endif