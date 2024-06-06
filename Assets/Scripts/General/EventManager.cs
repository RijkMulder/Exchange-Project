using System;
using UnityEngine.Events;

namespace Events
{
    public static class EventManager 
    {
        public static event UnityAction<float, float> PlayerHealthUpdate;
        public static event UnityAction<FishType> FishMiniGameStart;
        public static event UnityAction<FishType> FishCaught;
        public static event UnityAction<FishType> ContinueFishing;
        public static event UnityAction<int> SpinnerHit;
        public static event UnityAction<int> DayEnd;
        public static event UnityAction<int> DayStart;
        public static event UnityAction<TimeSpan> TimeChanged;
        public static event UnityAction<bool> PauseTime;
        public static event UnityAction EndOverview;
        public static event UnityAction<float, bool> ScreenShake;

        public static void OnHealthChanged(float value, float max) => PlayerHealthUpdate?.Invoke(value, max);
        public static void OnFishMiniGameStart(FishType value) => FishMiniGameStart?.Invoke(value);
        public static void OnFishCaught(FishType value) => FishCaught?.Invoke(value);
        public static void OnContinueFishing(FishType value) => ContinueFishing?.Invoke(value);
        public static void OnSpinnerHit(int value) => SpinnerHit?.Invoke(value);
        public static void OnDayEnd(int value) => DayEnd?.Invoke(value);
        public static void OnDayStart(int value) => DayStart?.Invoke(value);
        public static void OnTimeChanged(TimeSpan value) => TimeChanged?.Invoke(value);
        public static void OnTimePause(bool value) => PauseTime?.Invoke(value);
        public static void OnEndOverview() => EndOverview?.Invoke();
        public static void OnScreenShake(float value, bool condition) => ScreenShake?.Invoke(value, condition);
    }
}

