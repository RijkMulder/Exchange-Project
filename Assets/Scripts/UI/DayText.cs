using Events;
using TMPro;
using UnityEngine;

public class DayText : MonoBehaviour
{
    TMP_Text text;
    private void OnEnable()
    {
        text = GetComponent<TMP_Text>();
        EventManager.DayEnd += UpdateString;
    }
    private void OnDisable()
    {
        EventManager.DayEnd -= UpdateString;
    }
    private void UpdateString(int day)
    {
        text.text = $"Day {day-1} has ended.";
    }
}
