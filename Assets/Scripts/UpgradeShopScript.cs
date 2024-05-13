using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeShopScript : MonoBehaviour
{
    [SerializeField] int coins;
    [SerializeField] TextMeshProUGUI coinCountText;
    [SerializeField] TextMeshProUGUI assistText;

    void Update()
    {
        coinCountText.text = "COINS: " + coins.ToString();
    }

    public void Buy(int cost)
    {
        if (cost > 0)
        {
            if (coins > 0 && coins >= cost)
            {
                coins -= cost;
                assistText.text = "";
            }
            else assistText.text = "Not enough money!";
        }
        else assistText.text = "Something went wrong ERROR CODE: 001";
    }
}
