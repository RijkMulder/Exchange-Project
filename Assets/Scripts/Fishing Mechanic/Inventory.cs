using Fishing.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance;
        public Dictionary<FishType, FishStats> inventoryDictionary = new Dictionary<FishType, FishStats>();
        private void OnEnable()
        {
            instance = this;
        }
        public void Add(FishType type, FishStats stats)
        {
            inventoryDictionary.Add(type, stats);
        }
    }
}

