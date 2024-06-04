using Gambling;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChipUpdateText : MonoBehaviour
{
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        text.text = $"Chips: {GamblingManager.Instance.chips}";
    }
}
