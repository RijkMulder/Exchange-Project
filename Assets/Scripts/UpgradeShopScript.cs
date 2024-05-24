using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gambling;
using Fishing;
using System;

[Serializable]
public class Prices
{
    public string type;
    public int price;
}
[Serializable]
public struct LuckUpgrades
{
    public Rarity[] rarities;
}
public class UpgradeShopScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI assistText;
    [SerializeField] Prices[] prices;
    [SerializeField] TextMeshProUGUI[] buttonTexts;
    [SerializeField] private FishingRod getSetter;

    // upgrade tracker
    public List<Vector2> speedUpgrades = new List<Vector2>();
    public List<LuckUpgrades> LuckUpgrades = new List<LuckUpgrades>();
    private void Start()
    {
        buttonTexts[0].text = "+ " + prices[0].type.ToString() + " (" + (prices[0].price * (GamblingManager.Instance.speedUpgradeIndex + 1)).ToString() + " Coins)";
        buttonTexts[1].text = "+ " + prices[1].type.ToString() + " (" + (prices[1].price * (GamblingManager.Instance.luckUpgradeIndex + 1)).ToString() + " Coins)";
    }
    public void Buy(string type)
    {
        if (type == "speed" && GamblingManager.Instance.speedUpgradeIndex == speedUpgrades.Count
            || type == "luck" && GamblingManager.Instance.luckUpgradeIndex == LuckUpgrades.Count) return;
        int cost = 0;
        int currentArrayItem = 0;
        for (int i = 0; i < prices.Length; i++)
        {
            if (prices[i].type == type)
            {
                cost = prices[i].price;
                currentArrayItem = i;
            }
        }
        if (cost > 0)
        {
            if (GamblingManager.Instance.coins >= cost)
            {
                GamblingManager.Instance.coins -= cost;
                assistText.text = "";
                prices[currentArrayItem].price += 50;
                buttonTexts[currentArrayItem].text = "+ " + prices[currentArrayItem].type.ToString() + " (" + prices[currentArrayItem].price.ToString() + " Coins)";
                // add functions below (this is a quicly made)
                if (type == "speed") 
                {
                    getSetter.minFishTime = speedUpgrades[GamblingManager.Instance.speedUpgradeIndex].x;
                    getSetter.maxFishTime = speedUpgrades[GamblingManager.Instance.speedUpgradeIndex].y;
                    GamblingManager.Instance.speedUpgradeIndex++;
                };
                if (type == "luck")
                {
                    getSetter.rarities = LuckUpgrades[GamblingManager.Instance.luckUpgradeIndex].rarities;
                    GamblingManager.Instance.luckUpgradeIndex++; 
                };
            }
            else assistText.text = "Not enough money!";
        }
        else assistText.text = "Something went wrong ERROR CODE: 001";
    }
}
