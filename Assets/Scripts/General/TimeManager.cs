using TMPro;
using UnityEngine;
//--
using Events;
using Fishing;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [SerializeField] private float minutesPerDay;

    public int dayEndTime;
    public int dayStartTime;

    // private
    private float currentTime;
    private TimeSpan span;
    private TimeSpan baseSpan;
    private bool doTime = true;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        baseSpan = new TimeSpan(1, dayStartTime, 0, 0);
    }
    private void OnEnable()
    {
        // stop clock
        EventManager.DayEnd += (param) => DayEnd();

        // pause
        EventManager.PauseTime += PauseTime;

        // start clock
        EventManager.DayStart += (param) => PauseTime(false);
    }
    private void DayEnd()
    {
        PauseTime(true);
        FishingRod.instance.enabled = false;

    }
    private void PauseTime(bool pause)
    {
        doTime = !pause;
    }
    private void Update()
    {
        if (!doTime) return; 
        currentTime += Time.deltaTime * (60 / minutesPerDay * (dayEndTime - dayStartTime));
        span = baseSpan + TimeSpan.FromSeconds(currentTime);
        if (span.Minutes % 5 == 0) EventManager.OnTimeChanged(span);
        if (span.Hours == dayEndTime)
        {
            EventManager.OnDayEnd(span.Days);
            baseSpan += new TimeSpan(1, 0, 0, 0);
            currentTime = 0;
        }
    }
}
