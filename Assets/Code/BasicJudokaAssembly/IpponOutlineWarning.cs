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
    bool isInDanger = false;
    bool isCoroutineRunning = false;

    Vector3 distanceToMass;
    float normalizedDistance;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ipponCircle = transform.parent.GetComponent<IpponCircle>();
        massCenter.inDanger += WarningOn;
        massCenter.noLongerInDanger += WarningOff;
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

    public void Set_originalColor()
    {
        // set original color one time
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    void WarningOn(object obj, EventArgs e)
    {
        isInDanger = true;
        if (!isCoroutineRunning)
        {
            StartCoroutine(nameof(Blink));
            isCoroutineRunning = true;
        }
    }
    void WarningOff(object obj, EventArgs e)
    {
        // stop if there was never danger in the first place
        if (!isInDanger)
            return;

        isInDanger = false;
        isCoroutineRunning = false;
        StopAllCoroutines();

        // ensure not stuck on warning color
        sr.color = originalColor;
        isColored = false;
    }

    IEnumerator Blink()
    {
        while (true)
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
            yield return new WaitForSecondsRealtime(blinkTime);
        }
    }

}
