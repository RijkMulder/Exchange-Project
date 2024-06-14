using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gambling;

namespace UpgradeShop
{
    public class UpgradeShopScript : MonoBehaviour
    {
        public UpgradeShopScript Instance;

        public List<RodType> speedUpgrades = new List<RodType>();
        public List<RodType> luckUpgrades = new List<RodType>();

        public int currentSpeedUpgrade = 0;
        public int currentLuckUpgrade = 0;

        private void Awake()
        {
            Instance = this;
        }

        public void upgradeSpeed()
        {
            int cost = speedUpgrades[currentSpeedUpgrade].coins;
            int coins = GamblingManager.Instance.coins;
            if (cost <= coins)
            {
                currentSpeedUpgrade++;
                coins -= cost;
            }
        }

        public void upgradeluck()
        {
            int cost = luckUpgrades[currentLuckUpgrade].coins;
            int coins = GamblingManager.Instance.coins;
            if (cost <= coins)
            {
                currentLuckUpgrade++;
                coins -= cost;
            }
        }
    }
}