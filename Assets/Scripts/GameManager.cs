using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int coins;
    public int chips;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI chipsText;

    private void Awake()
    {
        Refresh();
    }

    public void Refresh()
    {
        coinsText.text = "COINS: " + coins.ToString();
        chipsText.text = "CHIPS: " + chips.ToString();
    }

    public void addCoins(int amount)
    {
        coins += amount;
        Refresh();
    }
    public void addChips(int amount)
    {
        chips += amount;
        Refresh();
    }
}