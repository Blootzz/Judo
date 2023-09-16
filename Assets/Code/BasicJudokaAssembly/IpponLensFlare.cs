using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IpponLensFlare : MonoBehaviour
{
    [SerializeField] GameObject lensFlarePrefab;

    void Start()
    {
        GetComponent<MassCenter>().iJustGotIppowned += InstantiateLensFlare;
    }

    void InstantiateLensFlare(object obj, EventArgs e)
    {
        GameObject flare = Instantiate(lensFlarePrefab, transform);
        Destroy(flare, 1);
    }
}
