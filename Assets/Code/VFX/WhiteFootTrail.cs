using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFootTrail : MonoBehaviour
{
    ParticleSystem myTrail;
    ParticleSystem.MainModule mainInstance;
    ParticleSystem.TrailModule trailInstance;
    ParticleSystem.MinMaxGradient originalCMinMaxGradient;
    [SerializeField] ParticleSystem.MinMaxGradient reapColor;

    void Start()
    {
        myTrail = GetComponent<ParticleSystem>();
        mainInstance = myTrail.main;
        trailInstance = myTrail.trails;
        originalCMinMaxGradient = mainInstance.startColor;
    }

    void Update()
    {
        transform.localScale = transform.parent.localScale;
    }

    public void SetTrailReap(bool active)
    {
        if (active)
        {
            mainInstance.startColor = reapColor;
            trailInstance.colorOverTrail = reapColor;
        }
        else
        {
            mainInstance.startColor = originalCMinMaxGradient;
            trailInstance.colorOverTrail = originalCMinMaxGradient;
        }
    }
}
