using UnityEngine;
using Events;
using TMPro;
using System;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    private void OnEnable()
    {
        EventManager.TimeChanged += UpdateTime;
    }
    private void OnDisable()
    {
        EventManager.TimeChanged -= UpdateTime;
    }
    private void UpdateTime(TimeSpan span)
    {
        minuteHand.localEulerAngles = new Vector3(0, 0, -Mathf.Lerp(0, 360, (float)span.Minutes / 60f));
        hourHand.localEulerAngles = new Vector3(0, 0, -((float)span.Hours) / 12f * 360f);
    }
}
