using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Prices
{
    public string type;
    public int price;
}

public class UpgradeShopScript : MonoBehaviour
{
    [SerializeField] int coins;
    [SerializeField] TextMeshProUGUI coinCountText;
    [SerializeField] TextMeshProUGUI assistText;
    [SerializeField] Prices[] prices;
    [SerializeField] TextMeshProUGUI[] buttonTexts;
    

    void Update()
    {
        coinCountText.text = "COINS: " + coins.ToString();
    }

    public void Buy(string type)
    {
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
            if (coins > 0 && coins >= cost)
            {
                coins -= cost;
                assistText.text = "";
                prices[currentArrayItem].price += 50;
                buttonTexts[currentArrayItem].text = "+ " + prices[currentArrayItem].type.ToString() + " (" + prices[currentArrayItem].price.ToString() + " Coins)";
                Debug.Log("bought: " + prices[currentArrayItem].type.ToString() + " for: " + cost);
            }
            else assistText.text = "Not enough money!";
        }
        else assistText.text = "Something went wrong ERROR CODE: 001";
    }
}
