using Events;
using TMPro;
using UnityEngine;

namespace Fishing.Minigame
{
    public class MinigameTimer : MonoBehaviour
    {
        public int maxTime;
        private float timer;
        private TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
            timer = maxTime;
        }
        private void Update()
        {
            if (MiniGameInstance.instance.missCoroutine == null)timer -= Time.deltaTime;
            text.text = timer.ToString("f0");
            if (timer <= 0.5f)
            { 
                EventManager.OnFishMiniGameEnd(1); 
            }
        }
        private void OnEnable()
        {
            EventManager.SpinnerHit += ResetTimer;
            EventManager.MinigameMiss += ResetTimer;
        }
        private void OnDisable()
        {
            EventManager.SpinnerHit -= ResetTimer;
        }
        private void ResetTimer(int t)
        {
            if (t == 0) Destroy(gameObject);
            timer = maxTime;
        }
    }
}
