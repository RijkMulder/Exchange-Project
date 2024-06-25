using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gambling;
using Fishing;

namespace UpgradeShop
{
    public class UpgradeShopScript : MonoBehaviour
    {
        public static UpgradeShopScript Instance;
        public List<RodType> speedUpgrades = new List<RodType>();
        public List<LuckUpgrade> luckUpgrades = new List<LuckUpgrade>();
        public List<Vector2> speeds = new List<Vector2>();

        public TextMeshProUGUI speedPriceText;
        public TextMeshProUGUI luckPriceText;

        public Animator animator;

        public GameObject shop;

        public int currentSpeedUpgrade = 0;
        public int currentLuckUpgrade = 0;

        private void Awake()
        {
            Instance = this;
            SetInactive();
        }

        private void Start()
        {
            speedPriceText.text = "Upgrade Speed: " + speedUpgrades[currentSpeedUpgrade].coins.ToString();
            luckPriceText.text = "Upgrade Luck: " + luckUpgrades[currentLuckUpgrade].coins.ToString();
        }
        private void OnDisable()
        {
            AudioManager.Instance.Stop("Barmusic");
        }
        public void SetActive()
        {
            shop.SetActive(true);
        }

        public void SetInactive()
        {
            shop.SetActive(false);
        }

        public void upgradeSpeed()
        {
            int cost = speedUpgrades[currentSpeedUpgrade].coins;
            if (cost <= GamblingManager.Instance.coins)
            {
                currentSpeedUpgrade++;
                GamblingManager.Instance.coins -= cost;
                speedPriceText.text = "Upgrade Speed: " + speedUpgrades[currentSpeedUpgrade].coins.ToString();

                // apply values
                FishingRod.instance.minFishTime = speeds[currentSpeedUpgrade - 1].x;
                FishingRod.instance.maxFishTime = speeds[currentSpeedUpgrade - 1].y;
                animator.SetTrigger("Upgrade");
            }
        }

        public void upgradeluck()
        {
            int cost = luckUpgrades[currentLuckUpgrade].coins;
            if (cost <= GamblingManager.Instance.coins)
            {
                currentLuckUpgrade++;
                GamblingManager.Instance.coins -= cost;
                luckPriceText.text = "Upgrade Luck: " + luckUpgrades[currentLuckUpgrade].coins.ToString();

                // apply values
                Rarity[] rarity = luckUpgrades[currentLuckUpgrade - 1].rarities;
                FishingRod.instance.rarities = rarity;
                animator.SetTrigger("Upgrade");
            }
        }
        private IEnumerator SetTrigger()
        {
            yield return new WaitForEndOfFrame();
            animator.ResetTrigger("Upgrade");
        }
    }
}