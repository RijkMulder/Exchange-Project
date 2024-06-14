using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishStatsWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image img;

    [SerializeField] private GameObject backGround;

    public void Setup(FishType fish)
    {
        backGround.SetActive(true);
        title.text = $"You caught a {fish.fishName}!";
        img.sprite = fish.fishSprite;

    }
    public void Continue()
    {
        backGround.SetActive(false);
    }
}
