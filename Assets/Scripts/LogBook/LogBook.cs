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
            EventManager.FishCaught += UpdateItem;
        }
        private void Start()
        {
            LogBookPageManager.instance.MakePages();
            transform.parent.gameObject.SetActive(false);
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

            // update page
            foreach (LogbookPage page in LogBookPageManager.instance.pages)
            {
                if (page.title == null) continue;
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
                if (page.title.text == fish.fishName) return fishDictionary[fish].Item2 > 0 ? fish.fishSprite : fish.fishUnknownSprite;
            }
            return null; /* should never reach this code */
        }
    }
}

