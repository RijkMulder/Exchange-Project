using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fishing.Minigame
{
    public class HitVisual : MonoBehaviour
    {
        [SerializeField] private Toggle prefab;
        private List<Toggle> toggles = new List<Toggle>();
        private void Awake()
        {
            Debug.Log("kanker");
            MiniGameInstance miniGame = MiniGameInstance.instance;
            miniGame.OnInitialize += (int amnt) => Initialize(amnt);
            miniGame.OnSpinnerHit += (int amnt) => UpdateVisuals(amnt);
        }
        public void Initialize(int amount)
        {
            for (int i = 0; i < toggles.Count; i++)
            {
                Destroy(toggles[i].gameObject);
            }
            toggles.Clear();
            for (int i = 0; i < amount; i++) 
            { 
                Toggle t = Instantiate(prefab, transform.position, Quaternion.identity, transform); 
                toggles.Add(t);
            }
        }
        public void UpdateVisuals(int amount)
        {
            toggles[amount].isOn = true;
        }
    }
}
