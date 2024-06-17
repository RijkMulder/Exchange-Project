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

        public void UpdatePage(FishType fish, int amnt)
        {
            img.sprite = fish.fishSprite;
            count.text = amnt.ToString();
        }
    }
}

