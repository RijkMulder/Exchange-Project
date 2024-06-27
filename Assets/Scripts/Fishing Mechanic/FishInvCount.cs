using Events;
using Fishing.Stats;
using Player.Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishInvCount : MonoBehaviour
{
    private TMP_Text text;
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

        foreach (KeyValuePair<FishType, List<FishStats>> fish in Inventory.instance.inventoryDictionary)
        {
            for (int i = 0; i < fish.Value.Count; i++)
            {
                amnt += 1;
            }
        }
        text.text = $"X {amnt}";
    }
}
