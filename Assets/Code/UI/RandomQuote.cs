using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomQuote : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI tmpro;
    [SerializeField] string[] quoteList;

    void OnEnable()
    {
        tmpro.text = quoteList[Random.Range(0, quoteList.Length - 1)];
    }
}
