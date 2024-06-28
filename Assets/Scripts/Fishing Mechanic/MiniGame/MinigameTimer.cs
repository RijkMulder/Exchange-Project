using Events;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Fishing.Minigame
{
    public class MinigameTimer : MonoBehaviour
    {
        public int maxTime;
        private float timer;
        [SerializeField] private Bubble bubble;

        private List<Bubble> bubbles = new List<Bubble>();
        bool done;
        private void Start()
        {
            MakeBubbles();
            timer = maxTime;
        }
        private void Update()
        {
            if (MiniGameInstance.instance.missCoroutine == null && WindowManager.Instance.transition == null )timer -= Time.deltaTime; 
            
            if (timer <= 0.5f && !done)
            { 
                done = true;
                EventManager.OnFishMiniGameEnd(1);
                WindowManager.Instance.ChangeWindow(true, WindowManager.Instance.windows[0]);
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
            EventManager.MinigameMiss -= ResetTimer;
        }
        private void ResetTimer(int t)
        {
            if (t == 0) Destroy(gameObject);
            timer = maxTime;
            MakeBubbles();
        }
        private void MakeBubbles()
        {
            // if reset, clear list
            if (bubbles.Count > 0)
            {
                for (int i = 0; i < bubbles.Count; i++)
                {
                    if (bubbles[i] != null)Destroy(bubbles[i].gameObject);
                }
                bubbles.Clear();
            }

            for (int i = 0; i < maxTime; i++)
            {
                Bubble newB = Instantiate(bubble, transform);
                newB.Pop(maxTime - (i));
                bubbles.Add(newB);
            }
        }
    }
}
