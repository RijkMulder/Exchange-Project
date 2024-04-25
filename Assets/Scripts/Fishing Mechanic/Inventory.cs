using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance;
        [SerializeField] private List<FishType> inventoryList = new List<FishType>();
        private void Awake()
        {
            instance = this;
        }
        public void Add(FishType type)
        {
            inventoryList.Add(type);
        }
    }
}

