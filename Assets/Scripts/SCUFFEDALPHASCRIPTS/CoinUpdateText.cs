using Gambling;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUpdateText : MonoBehaviour
{
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        text.text = $"Coins: {GamblingManager.Instance.coins}";
    }
}
