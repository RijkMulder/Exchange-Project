using Fishing.Stats;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logbook
{
    public class LogbookPage : MonoBehaviour
    {
        public Image img;
        public TMP_Text title;
        public TMP_Text description;
        public TMP_Text count;
        public Button nextPageButton;
        public Button previousPageButton;
        public StatsSlider valueSlider;
        public StatsSlider tastySlider;
        public StatsSlider sizeSlider;
        public StatsSlider fishabilitySlider;

        public void UpdatePage(FishType fish, int amnt, FishStats stats)
        {
            img.sprite = fish.fishSprite;
            count.text = amnt.ToString();
            valueSlider.Setup(fish.chipCount, 200);
            tastySlider.Setup(stats.tastiness, 100);
            sizeSlider.Setup((int)stats.size, Enum.GetValues(typeof(FishSize)).Length);
            fishabilitySlider.Setup(UnityEngine.Random.Range(0, 100), 100);
        }
    }
}

