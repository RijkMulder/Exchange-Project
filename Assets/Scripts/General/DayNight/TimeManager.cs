using TMPro;
using UnityEngine;
//--
using Events;
using Fishing;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("Settings")]
    [SerializeField] private float minutesPerDay;
    [SerializeField] private int minutesPerUpdate;
    [SerializeField] private Popup tutorialPopup;

    [Space]
    public int dayEndTime;
    public int dayStartTime;
    public TimeSpan span;

    // private
    private float currentTime;
    private TimeSpan baseSpan;
    private bool doTime = true;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        baseSpan = new TimeSpan(1, dayStartTime, 0, 0);
        EventManager.OnDoTutorial(tutorialPopup);
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
        if (span.Minutes % minutesPerUpdate == 0) EventManager.OnTimeChanged(span);
        if (span.Hours == dayEndTime)
        {
            EventManager.OnDayEnd(span.Days);
            baseSpan += new TimeSpan(1, 0, 0, 0);
            currentTime = 0;
        }
    }
}
