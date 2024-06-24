using Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calender : MonoBehaviour
{
    [SerializeField] private TMP_Text count;

    private void OnEnable()
    {
        EventManager.DayEnd += UpdateText;
    }
    private void UpdateText(int d)
    {
        count.text = (d + 1).ToString();
    }
}
