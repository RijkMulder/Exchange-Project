using Events;
using Player.Inventory;
using System.Collections;
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
            WindowManager.Instance.ChangeWindow();
            GetChips();
            EventManager.OnEndOverview();
        }
        public void QuitGambling()
        {
            WindowManager.Instance.ChangeWindow();
            EventManager.OnDayStart(TimeManager.instance.dayStartTime);
        }

        // Calculate the amount of chips you get
        public void GetChips()
        {
            int amnt = 0;
            FishType[] fish = Inventory.instance.inventoryList.ToArray();
            for (int i = 0; i < fish.Length; i++)
            {
                amnt += fish[i].chipCount;
            }
            GamblingManager.Instance.chips += amnt;
            Inventory.instance.inventoryList.Clear();
        }
    }
}

