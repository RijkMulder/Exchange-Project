using Fishing.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance;
        public Dictionary<FishType, List<FishStats>> inventoryDictionary = new Dictionary<FishType, List<FishStats>>();
        private void OnEnable()
        {
            instance = this;
        }
        public void Add(FishType type, FishStats stats)
        {
            if (!inventoryDictionary.ContainsKey(type))
            {
                inventoryDictionary[type] = new List<FishStats>();
            }

            inventoryDictionary[type].Add(stats);
        }
    }
}

