using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gambling;

namespace UpgradeShop
{
    public class UpgradeShopScript : MonoBehaviour
    {
        public static UpgradeShopScript Instance;

        public List<RodType> speedUpgrades = new List<RodType>();
        public List<RodType> luckUpgrades = new List<RodType>();

        public TextMeshProUGUI speedPriceText;
        public TextMeshProUGUI luckPriceText;

        public Animator animator;

        public GameObject shop;

        public int currentSpeedUpgrade = 0;
        public int currentLuckUpgrade = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            speedPriceText.text = "Upgrade Speed: " + speedUpgrades[currentSpeedUpgrade].coins.ToString();
            luckPriceText.text = "Upgrade Luck: " + luckUpgrades[currentLuckUpgrade].coins.ToString();
        }

        public void SetActive()
        {
            AnimateIn();
        }

        public void SetInactive()
        {
            StartCoroutine(AnimateOut());
        }

        void AnimateIn()
        {
            animator.SetTrigger("FallIn");
            shop.SetActive(true);
        }

        IEnumerator AnimateOut()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                animator.SetTrigger("FallOut");
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
                animator.SetTrigger("End");
                shop.SetActive(false);
            }
        }

        public void upgradeSpeed()
        {
            int cost = speedUpgrades[currentSpeedUpgrade].coins;
            if (cost <= GamblingManager.Instance.coins)
            {
                currentSpeedUpgrade++;
                GamblingManager.Instance.coins -= cost;
                speedPriceText.text = "Upgrade Speed: " + speedUpgrades[currentSpeedUpgrade].coins.ToString();
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
            }
        }
    }
}