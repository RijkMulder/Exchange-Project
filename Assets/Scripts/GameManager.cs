using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int coins;
    public int chips;
    [SerializeField] TextMeshProUGUI coinsText;

    private void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        coinsText.text = "COINS: " + coins.ToString();
    }

    public void addCoins(int amount)
    {
        coins += amount;
        Refresh();
    }
}
