using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// my name spaces
using Events;
using FishingLine;
using Player.Inventory;
using Fishing.Minigame;

namespace Fishing
{
    public class FishingRod : MonoBehaviour
    {
        public static FishingRod instance;
        public FishingState state;

        [SerializeField] private FishBaitVisual fishSpotParticle;

        [Header("Fish Probabilities")]
        public Dictionary<EFishType, int> fishProbabilities = new Dictionary<EFishType, int>();
        public Rarity[] rarities;

        [Header("Fishing Settings")]
        public float minFishTime;
        public float maxFishTime;
        public FishType currentFish;

        private Coroutine fishingCoroutine;

        [HideInInspector]public bool caught = false;

        private void Awake()
        {
            instance = this;
            SetProbabilities();
            EventManager.DayStart += ToggleActive;
        }
        private void OnEnable()
        {
            EventManager.FishMiniGameStart += OnFishMiniGameStart;
            EventManager.ContinueFishing += OnContinueFishing;
            EventManager.FishCaught += OnFishCaught;
            EventManager.DayEnd += ToggleActive;
        }

        private void OnFishMiniGameStart(FishType fish)
        {
            ChangeState(FishingState.Caught, fish);
        }

        private void OnContinueFishing(FishType fish)
        {
            OnTriedCatch(false, fish);
        }

        private void OnFishCaught(FishType fish)
        {
            OnTriedCatch(true, fish);
        }

        private void Update()
        {
            if (transform.childCount == 0) return;
            switch (state)
            {
                case FishingState.Idle:
                    Idle();
                    break;
                case FishingState.Fishing:
                    Fishing();
                    Try();
                    break;
                case FishingState.Caught:
                    Caught();
                    break;
            }
        }
        private void Fishing()
        {
            if (Clicked() && !caught)
            {
                FishHook.instance.ResetPos();
            }
            if (fishingCoroutine == null)fishingCoroutine = StartCoroutine(GoFishing());
        }
        private void Idle()
        {
            if (fishingCoroutine != null)
            {
                StopCoroutine(fishingCoroutine);
                fishingCoroutine = null;
            }
            if (Clicked())
            {
                FishHook.instance.CastLine();
            }
        }
        private void Caught()
        {
            if (fishingCoroutine != null)
            {
                StopCoroutine(fishingCoroutine);
                fishingCoroutine = null;
            }
        }
        private void Try()
        {
            if (Clicked() && caught)
            { 
                FishingMiniGameManager.instance.FishCaught(); caught = false;
                EventManager.OnTimePause(true);
            }
        }
        private void OnTriedCatch(bool succes, FishType type)
        {
            if (!succes) ChangeState(FishingState.Fishing);
            else { FishHook.instance.ResetPos(); }
            EventManager.OnTimePause(false);
        }
        private bool Clicked()
        {
            if (Input.GetMouseButtonDown(0)) return true;
            return false;
        }
        private IEnumerator GoFishing()
        {
            // wait
            float waitTime = Random.Range(minFishTime, maxFishTime);
            yield return new WaitForSeconds(waitTime);

            // spawn bait
            FishBaitVisual obj = Instantiate(fishSpotParticle, FishHook.instance.transform.position, new Quaternion(0, 0, 0, 1));
            obj.Initialize();
            fishingCoroutine = null;
        }
        public void SetProbabilities()
        {
            fishProbabilities.Clear();
            foreach (Rarity rarity in rarities)
            {
                fishProbabilities.Add(rarity.rarity, rarity.probability);
            }
        }
        public void ChangeState(FishingState newState, FishType fish = null)
        {
            if (state == FishingState.Caught && fish != null) currentFish = fish;
            state = newState;
        }
        private void ToggleActive(int day)
        {
            enabled = !enabled;
        }
    }
}
