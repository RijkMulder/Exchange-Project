using FishingLine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fishing
{
    public class FishingRod : MonoBehaviour
    {
        public static FishingRod instance;
        public FishingState state;

        [SerializeField] private GameObject fishSpotParticle;

        [Header("Probabilities")]
        public Dictionary<EFishType, float> Probabilities = new Dictionary<EFishType, float>
{
    { EFishType.Common, 80f },
    { EFishType.Uncommon, 15f },
    { EFishType.Salmonific, 10f },
    { EFishType.Fintastic, 5f },
    { EFishType.Marlinificent, 1f }
};

        [Header("setttings")]
        public float minFishTime;
        public float maxFishTime;

        private Coroutine fishingCoroutine;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            FishingMiniGameManager.instance.OnFishCaught += () => ChangeState(FishingState.Caught);
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
                    break;
            }
        }
        private void Fishing()
        {
            if (Clicked())
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
        private bool Clicked()
        {
            if (Input.GetMouseButtonDown(0)) return true;
            return false;
        }
        private IEnumerator GoFishing()
        {
            yield return new WaitForSeconds(Random.Range(minFishTime, maxFishTime));
            Instantiate(fishSpotParticle, FishHook.instance.transform.position, Quaternion.identity);
            FishingMiniGameManager.instance.FishCaught();
            fishingCoroutine = null;
            yield break;
        }
        public void ChangeState(FishingState newState)
        {
            state = newState;
        }
    }
}
