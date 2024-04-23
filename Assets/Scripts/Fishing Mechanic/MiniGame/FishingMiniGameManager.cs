using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace Fishing
{
    public class FishingMiniGameManager : MonoBehaviour
    {
        // singleton
        public static FishingMiniGameManager instance;

        // fish
        [SerializeField] private List<FishType> fish = new List<FishType>();
        [SerializeField] private List<List<FishType>> FishLists = new List<List<FishType>>();

        // objs
        [SerializeField] private GameObject miniGameInstanceObj;

        // events
        public delegate void FishingMiniGameDelegate();
        public event FishingMiniGameDelegate OnFishCaught;

        // fish rarities lists
        private List<FishType> common = new List<FishType>();
        private List<FishType> uncommon = new List<FishType>();
        private List<FishType> salmonific = new List<FishType>();
        private List<FishType> fintastic = new List<FishType>();
        private List<FishType> marlinificent = new List<FishType>();

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            foreach (var f in fish) 
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
            OnFishCaught?.Invoke();
            List<FishType> fish = FishLists[GetFishType()];
            MiniGameInstance.instance.Initialize(fish[Random.Range(0, fish.Count)]);
        }
        private int GetFishType()
        {
            // get probabilities
            List<float> probabilities = new List<float>();
            foreach (KeyValuePair<EFishType, float> fish in FishingRod.instance.Probabilities)
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
