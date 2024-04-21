using FishingLine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Fishing
{
    public class FishingRod : MonoBehaviour
    {
        public static FishingRod instance;
        public FishingRodStats currentStats;
        public FishingState state;
        [SerializeField] private GameObject fishSpotParticle;

        private Coroutine fishingCoroutine;
        private void Awake()
        {
            instance = this;
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

            if (fishingCoroutine == null)fishingCoroutine = StartCoroutine(GoFishing(currentStats));
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
        private IEnumerator GoFishing(FishingRodStats stats)
        {
            yield return new WaitForSeconds(Random.Range(stats.minFishTime, stats.maxFishTime));
            Instantiate(fishSpotParticle, FishHook.instance.transform.position, Quaternion.identity);

            fishingCoroutine = null;
            yield break;
        }
    }
}
