using Events;
using Fishing.Stats;
using Player.Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishInvCount : MonoBehaviour
{
    private TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        EventManager.FishCaught += UpdateText;
    }
    private void UpdateText(FishType type)
    {
        int amnt = 0;

        foreach (KeyValuePair<FishType, List<FishStats>> fish in Inventory.instance.inventoryDictionary)
        {
            Debug.Log(fish.Key);
            Debug.Log(fish.Value.Count);
            for (int i = 0; i < fish.Value.Count; i++)
            {
                amnt += fish.Value.Count;
            }
        }
        text.text = $"X {amnt}";
    }
}
