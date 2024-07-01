using Events;
using Fishing.Stats;
using Player.Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishInvCount : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private Sprite[] barrelSprites;
    [SerializeField] private int fishUpdateAmnt;
    [SerializeField] private Image barrelImg;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
        EventManager.FishCaught += UpdateText;
        EventManager.DayStart += ResetString;
    }
    private void ResetString(int d)
    {
        text.text = "X 0";
    }
    private void UpdateText(FishType type)
    {
        int amnt = 0;

        // find amount of fish
        foreach (KeyValuePair<FishType, List<FishStats>> fish in Inventory.instance.inventoryDictionary)
        {
            amnt += fish.Value.Count;
        }

        // update sprite if neccesery
        if (amnt % fishUpdateAmnt == 0)
        {
            // check if next sprite is availible
            int index = (amnt / fishUpdateAmnt) - 1;
            if (index > barrelSprites.Length - 1) return;

            // set sprite
            barrelImg.sprite = barrelSprites[index];
        }
        text.text = $"X {amnt}";
    }
}
