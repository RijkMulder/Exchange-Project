using Events;
using TMPro;
using UnityEngine;

public class DayText : FadeUIText
{
    private void OnEnable()
    {
        EventManager.DayEnd += UpdateString;
    }
    private void OnDisable()
    {
        EventManager.DayEnd -= UpdateString;
    }
    private void UpdateString(int day)
    {
        StartCoroutine(FadeImg());
        text.text = $"Day {day-1} has ended.";
    }
}
