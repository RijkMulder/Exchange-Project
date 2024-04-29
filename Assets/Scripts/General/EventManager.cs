using UnityEngine.Events;

namespace Events
{
    public static class EventManager 
    {
        public static event UnityAction<float, float> PlayerHealthUpdate;
        public static event UnityAction<FishType> FishMiniGameStart;
        public static event UnityAction<FishType> FishCaught;
        public static event UnityAction<FishType> ContinueFishing;
        public static event UnityAction<int> InitializeMinigame;
        public static event UnityAction<int> SpinnerHit;

        public static void OnHealthChanged(float value, float max) => PlayerHealthUpdate?.Invoke(value, max);
        public static void OnFishMiniGameStart(FishType value) => FishMiniGameStart?.Invoke(value);
        public static void OnFishCaught(FishType value) => FishCaught?.Invoke(value);
        public static void OnContinueFishing(FishType value) => ContinueFishing?.Invoke(value);
        public static void OnInitializeMinigame(int value) => InitializeMinigame?.Invoke(value);
        public static void OnSpinnerHit(int value) => SpinnerHit?.Invoke(value);
    }
}

