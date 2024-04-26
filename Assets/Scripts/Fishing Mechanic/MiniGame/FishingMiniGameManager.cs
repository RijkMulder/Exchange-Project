using System.Collections.Generic;
using UnityEngine;

namespace Fishing
{
    public class FishingMiniGameManager : MonoBehaviour
    {
        // singleton
        public static FishingMiniGameManager instance;

        // fish
        [SerializeField] private List<List<FishType>> FishLists = new List<List<FishType>>();

        // objs
        [SerializeField] private GameObject miniGameInstanceObj;

        // events
        public delegate void FishingMiniGameDelegate(FishType fish);
        public event FishingMiniGameDelegate OnFishMinigame;
        public event FishingMiniGameDelegate OnFishCaught;
        public event FishingMiniGameDelegate OnContinueFishing;

        // fish rarities lists
        private List<FishType> common = new List<FishType>();
        private List<FishType> uncommon = new List<FishType>();
        private List<FishType> salmonific = new List<FishType>();
        private List<FishType> fintastic = new List<FishType>();
        private List<FishType> marlinificent = new List<FishType>();

        private FishType newFish;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            FishType[] allFish = Resources.LoadAll<FishType>("Data/Fish");

            foreach (var f in allFish) 
            {
                switch (f.type)
                {
                    case EFishType.Common:
                        common.Add(f);
                        break;
                    case EFishType.Uncommon:
                        uncommon.Add(f);
                        break;
                    case EFishType.Fintastic:
                        fintastic.Add(f);
                        break;
                    case EFishType.Salmonific:
                        salmonific.Add(f);
                        break;
                    case EFishType.Marlinificent:
                        marlinificent.Add(f);
                        break;
                }
            }
            FishLists.Add(common);
            FishLists.Add(uncommon);
            FishLists.Add(salmonific);
            FishLists.Add(fintastic);
            FishLists.Add(marlinificent);
        }
        public void FishCaught()
        {
            // get fish
            List<FishType> fish = FishLists[GetFishType()];
            newFish = fish[Random.Range(0, fish.Count)];

            // turn mini game window on
            transform.GetChild(0).gameObject.SetActive(true);

            // invoke mini game
            OnFishMinigame?.Invoke(newFish);
        }
        public void ContinueFishing(bool succes)
        {
            if (!succes)OnContinueFishing?.Invoke(newFish);
            if (succes)OnFishCaught?.Invoke(newFish);
            transform.GetChild(0).gameObject.SetActive(false);
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
