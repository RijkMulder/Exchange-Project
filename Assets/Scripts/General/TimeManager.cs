using TMPro;
using UnityEngine;
//--
using Events;
using Fishing;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float timeScale;
    [SerializeField] private float currentTime;
    [SerializeField] private int dayEndTime;

    private float dayLength = 24f;
    private int dayCount = 1;

    bool doTime = true;
    private void Start()
    {
        currentTime *= 60;
    }
    private void OnEnable()
    {
        EventManager.DayEnd += StopClock;
        EventManager.DayStart += StartClock;
    }
    private void StopClock(int day)
    {
        doTime = false;
        FishingRod.instance.enabled = false;
    }
    private void StartClock(int time)
    {
        doTime = true;
        currentTime = time * 60;
    }
    private void Update()
    {
        if (!doTime) return; 
        currentTime += Time.deltaTime * timeScale;
        currentTime %= dayLength * 60f;
        int hour = (int)(currentTime / 60f) % 24;
        int minute = (int)(currentTime % 60f);
        if (minute % 5 == 0)
        {
            EventManager.OnTimeChanged($"Day {dayCount} - {hour:00}:{minute:00}");
        }
        if (hour == dayEndTime - 1 && minute > 58f)
        {
            currentTime = 0f;
            dayCount++;
            EventManager.OnDayEnd(dayCount);
        }
    }
}
