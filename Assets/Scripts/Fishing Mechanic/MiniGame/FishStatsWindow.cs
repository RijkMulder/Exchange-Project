using Fishing.Minigame;
using Player.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fishing.Stats
{
    public class FishStatsWindow : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text size;

        [Header("Other")]
        [SerializeField] private Image img;
        [SerializeField] private GameObject backGround;
        [SerializeField] private StatsSlider valueSlider;
        [SerializeField] private StatsSlider tastySlider;

        public void Setup(FishType fish)
        {
            // set everything
            backGround.SetActive(true);
            title.text = $"{fish.fishName}";
            img.sprite = fish.fishSprite;

            // add new item to inventory
            FishStats stats = new()
            {
                size = (FishSize)UnityEngine.Random.Range(0, (int)Enum.GetValues(typeof(FishSize)).Cast<FishSize>().Max()),
                tastiness = UnityEngine.Random.Range(0, 100)
            };
            Inventory.instance.Add(fish, stats);

            // size
            size.text = $"Size: {stats.size}";

            // sliders
            valueSlider.Setup(fish.chipCount, 200);
            tastySlider.Setup(stats.tastiness, 100);

            // text color
            foreach(KeyValuePair<EFishType, int> f in FishingRod.instance.fishProbabilities)
            {
                if (fish.type == f.Key)
                {
                    foreach(Rarity r in FishingRod.instance.rarities)
                    {
                        if (r.rarity == f.Key) title.color = r.color;
                    }
                }
            }
        }
        public void Continue()
        {
            backGround.SetActive(false);
            FishingMiniGameManager.instance.ContinueFishing(true);
            WindowManager.Instance.ChangeWindow(true, WindowManager.Instance.windows[0]);
        }
    }
}


