using Fishing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Image backgroundImg;
    public Image itemImg;
    public TMP_Text text;
    [SerializeField] private TMP_Text chipText;
    public void Initialize(FishType fish, int count, Rarity rarity)
    {
        backgroundImg.color = rarity.color;
        itemImg.sprite = fish.fishSprite;
        text.text = $"{count}";
        chipText.text = $"Chips: {fish.chipCount * count}";
    }
}
