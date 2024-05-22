using System.Collections.Generic;
using UnityEngine;
using Events;
using FishingLine;

namespace Fishing.Minigame
{
    /// <summary>
    /// Minigame manager gets random fish to catch and initializes mini game instance.
    /// </summary>
    public class FishingMiniGameManager : MonoBehaviour
    {
        // singleton
        public static FishingMiniGameManager instance;

        // objs
        [SerializeField] private GameObject miniGameInstanceObj;
        [SerializeField] private FishReelVisual reelVisual;

        // fish
        private List<List<FishType>> fishLists = new List<List<FishType>>();
        private FishType newFish;
        private FishType[] allFish;

        private GameObject minigame;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            allFish = Resources.LoadAll<FishType>("Data/Fish");
            // dynamically make a list for every rarity type
            foreach (KeyValuePair<EFishType, int> fish in FishingRod.instance.fishProbabilities)
            {
                EFishType rarity = fish.Key;
                List<FishType> currentFishRarity = new List<FishType>();
                for (int i = 0; i < allFish.Length; i++)
                {
                    if (allFish[i].type == rarity) currentFishRarity.Add(allFish[i]);
                }
                fishLists.Add(currentFishRarity);
            }
        }
        public void FishCaught()
        {
            // get fish
            int random = GetFishType();
            newFish = fishLists[random][Random.Range(0, fishLists[random].Count)];

            // turn mini game window on
            minigame = Instantiate(miniGameInstanceObj, transform);

            // invoke mini game
            EventManager.OnFishMiniGameStart(newFish);
        }
        public void ContinueFishing(bool succes)
        {
            if (!succes) EventManager.OnContinueFishing(newFish);
            if (succes)
            {
                Instantiate(reelVisual, FishHook.instance.transform);
                EventManager.OnFishCaught(newFish);
            }
            Destroy(minigame);
        }
        private int GetFishType()
        {
            // get probabilities
            List<float> probabilities = new List<float>();
            foreach (KeyValuePair<EFishType, int> fish in FishingRod.instance.fishProbabilities)
            {
                probabilities.Add(fish.Value);
            }

            // total probability
            float totalProbability = 0f;
            foreach (float f in probabilities) totalProbability += f;

            // random
            float randomValue = Random.value * totalProbability;

            // find value
            float crawlingValue = 0f;
            for (int i = 0; i < probabilities.Count; i++)
            {
                crawlingValue += probabilities[i];
                if (crawlingValue >= randomValue) return i;
            }
            return -1;
        }
    }
}
