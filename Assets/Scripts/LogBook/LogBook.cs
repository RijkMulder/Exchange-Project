using Events;
using Fishing.Stats;
using Player.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logbook
{
    public class LogBook : MonoBehaviour
    {
        public static LogBook instance;
        public Dictionary<FishType, (FishStats, int)> fishDictionary = new Dictionary<FishType, (FishStats, int)>();
        private void Awake()
        {
            instance = this;

            // create items
            FishType[] fish = Resources.LoadAll<FishType>("Data/Fish");

            for (int i = 0; i < fish.Length; i++)
            {
                NewItem(fish[i]);
            }
            LogBookPageManager.instance.MakePages();
            EventManager.DayEnd += CheckInventory;
        }
        public void CheckInventory(int d)
        {
            foreach (var item in Inventory.instance.inventoryDictionary)
            {
                FishType type = item.Key;
                if (fishDictionary.ContainsKey(type)) UpdateItem(type);
            }
        }
        private void NewItem(FishType type)
        {
            FishStats stats = new FishStats();
            fishDictionary.Add(type, (stats, 0));
        }
        private void UpdateItem(FishType key)
        {
            // update data
            (FishStats, int) currentItem = fishDictionary[key];
            (FishStats, int) newItem = (currentItem.Item1, currentItem.Item2 + 1);
            fishDictionary[key] = newItem;
            Debug.Log("kankersaus");

            // update page
            foreach (LogbookPage page in LogBookPageManager.instance.pages)
            {
                if (page.title.text == key.fishName)
                {
                    page.UpdatePage(key, newItem.Item2);
                }
            }
        }
        public Sprite GetFishSprite(FishType fish)
        {
            foreach (LogbookPage page in LogBookPageManager.instance.pages)
            {
                if (page.title.text == fish.fishName) return fishDictionary.ContainsKey(fish) ? fish.fishSprite : fish.fishUnknownSprite;
            }
            return null; /* should never reach this code */
        }
    }
}

