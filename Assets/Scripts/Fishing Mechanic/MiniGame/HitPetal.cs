using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;
namespace Fishing.Minigame
{
    public class HitPetal : MonoBehaviour
    {
        public Sprite gray;
        public Sprite white;
        public Sprite green;
        public Sprite red;

        private Image img;
        private void Awake()
        {
            img = GetComponent<Image>();
        }

        public void SetState(Sprite sprite)
        {
            img.sprite = sprite;
            if (sprite == gray) img.color = new Color(255, 255, 255, 0.6f);
            else img.color = Color.white;
        }
    }
}
