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
        public List<ReelerUpgrade> reelerUpgrades = new List<ReelerUpgrade>();
        public List<LuckUpgrade> luckUpgrades = new List<LuckUpgrade>();
        public List<Vector2> speeds = new List<Vector2>();

        public Animator animator;

        public GameObject shop;

        public int currentSpeedUpgrade = 0;
        public int currentLuckUpgrade = 0;
        public int currentReelerUpgrade = 0;


        // tmp
        [SerializeField] private TMP_Text speedPriceText;
        [SerializeField] private TMP_Text luckPriceText;
        [SerializeField] private TMP_Text reelerPriceText;

        [SerializeField] private TMP_Text speedIndexText;
        [SerializeField] private TMP_Text luckIndexText;
        [SerializeField] private TMP_Text reelerIndexText;
        private void Awake()
        {
            Instance = this;
            SetInactive();
        }
        private void Start()
        {
            speedPriceText.text = speedUpgrades[currentSpeedUpgrade].coins.ToString();
            luckPriceText.text = luckUpgrades[currentLuckUpgrade].coins.ToString();
            reelerPriceText.text = reelerUpgrades[currentReelerUpgrade].coins.ToString();

            speedIndexText.text = $"lvl {currentSpeedUpgrade}/{speedUpgrades.Count}";
            luckIndexText.text = $"lvl {currentLuckUpgrade}/{luckUpgrades.Count}";
            reelerIndexText.text = $"lvl {currentReelerUpgrade}/{reelerUpgrades.Count}";
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
            if (cost <= GamblingManager.Instance.coins && currentSpeedUpgrade < speedUpgrades.Count)
            {
                currentSpeedUpgrade++;
                GamblingManager.Instance.coins -= cost;

                // apply values
                FishingRod.instance.minFishTime = speeds[currentSpeedUpgrade - 1].x;
                FishingRod.instance.maxFishTime = speeds[currentSpeedUpgrade - 1].y;
                animator.SetTrigger("Upgrade");

                speedPriceText.text = currentSpeedUpgrade == speedUpgrades.Count ? "max" : speedUpgrades[currentSpeedUpgrade].coins.ToString();
                speedIndexText.text = $"lvl {currentSpeedUpgrade}/{speedUpgrades.Count}";
            }
        }

        public void upgradeluck()
        {
            int cost = luckUpgrades[currentLuckUpgrade].coins;
            if (cost <= GamblingManager.Instance.coins && currentLuckUpgrade < luckUpgrades.Count)
            {
                currentLuckUpgrade++;
                GamblingManager.Instance.coins -= cost;

                // apply values
                Rarity[] rarity = luckUpgrades[currentLuckUpgrade - 1].rarities;
                FishingRod.instance.rarities = rarity;
                SetDegrees(currentReelerUpgrade == 0 ? 0 : currentReelerUpgrade - 1);
                animator.SetTrigger("Upgrade");

                luckPriceText.text = currentLuckUpgrade == luckUpgrades.Count ? "max" : luckUpgrades[currentLuckUpgrade].coins.ToString();
                luckIndexText.text = $"lvl {currentLuckUpgrade}/{luckUpgrades.Count}";
            }
        }
        public void UpgradeReeler()
        {
            int cost = reelerUpgrades[currentReelerUpgrade].coins;
            if (cost <= GamblingManager.Instance.coins && currentReelerUpgrade < reelerUpgrades.Count)
            {
                currentReelerUpgrade++;
                GamblingManager.Instance.coins -= cost;

                // apply values
                SetDegrees(currentReelerUpgrade - 1);

                reelerPriceText.text = currentReelerUpgrade == reelerUpgrades.Count ? "max" : reelerUpgrades[currentReelerUpgrade].coins.ToString();
                reelerIndexText.text = $"lvl {currentReelerUpgrade}/{reelerUpgrades.Count}";
            }
        }
        private void SetDegrees(int index)
        {
            for (int i = 0; i < FishingRod.instance.rarities.Length; i++)
            {
                FishingRod.instance.rarities[i].skillCheckHitDegrees = reelerUpgrades[index].hitDegrees[i];
            }
        }
        private IEnumerator SetTrigger()
        {
            yield return new WaitForEndOfFrame();
            animator.ResetTrigger("Upgrade");
        }
    }
}