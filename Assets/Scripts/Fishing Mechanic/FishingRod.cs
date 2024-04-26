using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// my namespaces
using FishingLine;
using Player.Inventory;
namespace Fishing
{
    public class FishingRod : MonoBehaviour, IDamageable
    {
        public static FishingRod instance;
        public FishingState state;

        [SerializeField] private GameObject fishSpotParticle;

        [Header("Fish Probabilities")]
        public Dictionary<EFishType, int> fishProbabilities = new Dictionary<EFishType, int>();

        [Header("Fishing Settings")]
        public float minFishTime;
        public float maxFishTime;

        [Header("Health")]
        [SerializeField] private int maxHealth;
        [SerializeField] private int health;
        [SerializeField] private Image healthBar;

        private Coroutine fishingCoroutine;
        [SerializeField] private FishProbabilities fishProbabilitiesdata;

        private bool caught = false;
        private FishType currentFish;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            health = maxHealth;

            FishingMiniGameManager.instance.OnFishMinigame += (FishType fish) => ChangeState(FishingState.Caught, fish);
            FishingMiniGameManager.instance.OnContinueFishing += (FishType fish) => OnTriedCatch(false, fish);
            FishingMiniGameManager.instance.OnFishCaught += (FishType fish) => OnTriedCatch(true, fish);
            SetProbabilities();
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
                ChangeHealth(-1);
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
            float waitTime = Random.Range(minFishTime, maxFishTime);
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
            int[] probs = new int[] { fishProbabilitiesdata.common, fishProbabilitiesdata.uncommon, 
                fishProbabilitiesdata.salmonific, fishProbabilitiesdata.fintastic, 
                fishProbabilitiesdata.marlinificent };
            for (int i = 0; i < probs.Length; i++) { fishProbabilities[(EFishType)i] = probs[i]; }
        }
        public void ChangeState(FishingState newState, FishType fish = null)
        {
            if (state == FishingState.Caught && fish != null) currentFish = fish;
            state = newState;
        }

        public void ChangeHealth(int value)
        {
            health += value;
            healthBar.fillAmount = Mathf.Clamp((float)health / (float)maxHealth, 0, 1);
            if (health <= 0) Destroy(gameObject);
        }
    }
}
