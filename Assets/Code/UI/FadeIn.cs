using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Disable this component if fade behavior is not desired

    public float fadeTime = 5f;
    public bool useWhiteInsteadOfBlack = false;
    UnityEngine.UI.Image coverImage;
    Color tempColor; // used to modify alpha channel without "new" in coroutine

    void Awake()
    {
        coverImage = GetComponent<UnityEngine.UI.Image>();

        if (useWhiteInsteadOfBlack)
            tempColor = Color.white;
        else
            tempColor = Color.black;

        StartCoroutine(nameof(FadeInFromSolid));
    }

    IEnumerator FadeInFromSolid()
    {
        while (tempColor.a > 0)
        {
            tempColor.a -= Time.unscaledDeltaTime / fadeTime;
            coverImage.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
