using Events;
using Fishing;
using Fishing.Stats;
using Player.Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI instance;
        [SerializeField] private InventoryItem itemPrefab;

        private List<GameObject> currentItems =  new List<GameObject>();
        private void Awake()
        {
            instance = this;
            EventManager.DayStart += ResetInv;
        }
        public void Initialize()
        {
            List<FishType> inv = new List<FishType>();
            foreach (KeyValuePair<FishType, List<FishStats>> f in Inventory.instance.inventoryDictionary)
            {
                for (int i = 0; i < f.Value.Count; i++)
                {
                    inv.Add(f.Key);
                }
            }

            Rarity[] rarities = FishingRod.instance.rarities;
            EFishType[] types = rarities.Select(r => r.rarity).ToArray();

            for (int i = 0; i < types.Length; i++)
            {
                Dictionary<string, int> fishByType = inv
                    .Where(f => f.type == types[i])
                    .GroupBy(f => f.fishName)
                    .ToDictionary(g => g.Key, g => g.Count());

                foreach (var (name, count) in fishByType)
                {
                    FishType fishType = inv.First(f => f.fishName == name);
                    GenerateItem(fishType, count, rarities[i]);
                }
            }
        }
        private void GenerateItem(FishType fish, int count, Rarity rarity)
        {
            InventoryItem item = Instantiate(itemPrefab, transform.position, Quaternion.identity, transform);
            item.Initialize(fish, count, rarity);
            currentItems.Add(item.gameObject);
        }
        private void ResetInv(int d)
        {
            for (int i = 0; i < currentItems.Count; i++)
            {
                Destroy(currentItems[i]);
            }
            currentItems.Clear();
        }
    }
}

