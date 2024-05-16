using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// my name spaces
using Events;
using FishingLine;
using Player.Inventory;

namespace Fishing
{
    public class FishingRod : MonoBehaviour
    {
        public static FishingRod instance;
        public FishingState state;

        [SerializeField] private GameObject fishSpotParticle;

        [Header("Fish Probabilities")]
        public Dictionary<EFishType, int> fishProbabilities = new Dictionary<EFishType, int>();
        public Rarity[] rarities;

        [Header("Fishing Settings")]
        public float minFishTime;
        public float maxFishTime;

        private Coroutine fishingCoroutine;

        private bool caught = false;
        private FishType currentFish;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            SetProbabilities();
        }
        private void OnEnable()
        {
            EventManager.FishMiniGameStart += OnFishMiniGameStart;
            EventManager.ContinueFishing += OnContinueFishing;
            EventManager.FishCaught += OnFishCaught;
        }

        private void OnDisable()
        {
            EventManager.FishMiniGameStart -= OnFishMiniGameStart;
            EventManager.ContinueFishing -= OnContinueFishing;
            EventManager.FishCaught -= OnFishCaught;
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
        private void Try()
        {
            if (Clicked() && caught)
            { FishingMiniGameManager.instance.FishCaught(); caught = false; }
        }
        private void OnTriedCatch(bool succes, FishType type)
        {
            if (!succes) ChangeState(FishingState.Fishing);
            else { FishHook.instance.ResetPos(); Inventory.instance.Add(type); }
        }
        private bool Clicked()
        {
            if (Input.GetMouseButtonDown(0)) return true;
            return false;
        }
        private IEnumerator GoFishing()
        {
            float waitTime = UnityEngine.Random.Range(minFishTime, maxFishTime);
            // wait half time for particle
            yield return new WaitForSeconds(waitTime / 2);
            GameObject obj = Instantiate(fishSpotParticle, FishHook.instance.transform.position, Quaternion.identity);
            Destroy(obj, waitTime / 2);
            caught = true;

            // after particle, catch fish
            yield return new WaitForSeconds(waitTime / 2);
            caught = false;
            fishingCoroutine = null;
            yield break;
        }
        private void SetProbabilities()
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
    }
}
