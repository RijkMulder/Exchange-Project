using Events;
using Fishing;
using Fishing.Stats;
using Player.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gambling
{
    public class GamblingManager : MonoBehaviour
    {
        public static GamblingManager Instance;
        public int coins;
        public int chips;

        public int speedUpgradeIndex;
        public int luckUpgradeIndex;
        private void Awake()
        {
            Instance = this;
        }
        public void StartGamblingDay()
        {
            WindowManager.Instance.ChangeWindow(false);
            GetChips();
            EventManager.OnEndOverview();
        }
        public void QuitGambling()
        {
            WindowManager.Instance.ChangeWindow(false);
            EventManager.OnDayStart(TimeManager.instance.dayStartTime);
        }

        // Calculate the amount of chips you get
        public void GetChips()
        {
            int amnt = 0;
            List<FishType> fish = new List<FishType>();
            foreach (KeyValuePair<FishType, FishStats> f in Inventory.instance.inventoryDictionary)
            {
                fish.Add(f.Key);
            }
            for (int i = 0; i < fish.Count; i++)
            {
                amnt += fish[i].chipCount;
            }
            GamblingManager.Instance.chips += amnt;
            Inventory.instance.inventoryDictionary.Clear();
        }
    }
}

