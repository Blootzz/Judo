using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IpponOutlineWarning : MonoBehaviour
{
    [SerializeField] MassCenter massCenter;
    SpriteRenderer sr;
    IpponCircle ipponCircle;

    [SerializeField] float blinkTime = 0.2f;
    [SerializeField] float extraDangerThreshold = 0.9f;
    [SerializeField] Color32 lowDangerColor;
    [SerializeField] Color32 highDangerColor;
    Color32 originalColor;
    Color32 workingColor;
    bool isColored = false;
    bool isBlinkEnabled = false;

    Vector3 distanceToMass;
    float normalizedDistance;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = GetComponent<SpriteRenderer>().color;
        ipponCircle = transform.parent.GetComponent<IpponCircle>();
        massCenter.inDanger += WarningOn;
        massCenter.noLongerInDanger += WarningOff;

        StartCoroutine(nameof(Blink));
    }

    void Update()
    {
        distanceToMass = transform.position - massCenter.transform.position;
        normalizedDistance = distanceToMass.magnitude / (ipponCircle.Get_Diameter() / 2); // 0-1 fraction of radius

        if (normalizedDistance <= extraDangerThreshold)
            workingColor = lowDangerColor;
        else
            workingColor = highDangerColor;
    }

    void WarningOn(object obj, EventArgs e)
    {
        isBlinkEnabled = true;
    }
    void WarningOff(object obj, EventArgs e)
    {
        isBlinkEnabled = false;

        // ensure not stuck on warning color
        sr.color = originalColor;
        isColored = false;
    }

    void Set_WorkingColor(Color32 newColor)
    {
        workingColor = newColor;
    }

    IEnumerator Blink()
    {
        while (true)
        {
            if (isBlinkEnabled)
            {
                if (!isColored)
                {
                    sr.color = workingColor;
                    isColored = true;
                }
                else // is already colored
                {
                    sr.color = originalColor;
                    isColored = false;
                }
            }
            yield return new WaitForSecondsRealtime(blinkTime);
        }
    }

}
