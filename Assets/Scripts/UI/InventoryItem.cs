using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Image img;
    public TMP_Text text;
    [SerializeField] private TMP_Text chipText;
    public void CalculateChips(FishType fish, int count)
    {
        chipText.text = $"Chips: {fish.chipCount * count}";
    }
}
