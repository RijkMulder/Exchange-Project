using Events;
using Player.Inventory;
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
        text.text = $"X {Inventory.instance.inventoryDictionary.Count}";
    }
}
