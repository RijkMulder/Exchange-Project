using Fishing;
using Player.Inventory;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI instance;
        [SerializeField] private InventoryItem itemPrefab;
        private void Awake()
        {
            instance = this;
        }
        public void Initialize()
        {
            var fishTypes = Inventory.instance.inventoryList.ToArray();
            var rarities = FishingRod.instance.rarities.Select(r => r.rarity).ToArray();

            foreach (var rarity in rarities)
            {
                var fishByType = fishTypes
                    .Where(f => f.type == rarity)
                    .GroupBy(f => f.name)
                    .ToDictionary(g => g.Key, g => g.Count());

                foreach (var (name, count) in fishByType)
                {
                    var fishType = fishTypes.First(f => f.name == name);
                    GenerateItem(fishType, count);
                }
            }
        }
        private void GenerateItem(FishType fish, int count)
        {
            InventoryItem item = Instantiate(itemPrefab, transform.position, Quaternion.identity, transform);
            item.img.sprite = fish.fishSprite;
            item.text.text = $"{count}";
            item.CalculateChips(fish, count);
        }
    }
}

