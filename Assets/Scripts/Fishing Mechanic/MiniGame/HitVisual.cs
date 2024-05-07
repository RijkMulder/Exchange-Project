using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;
namespace Fishing.Minigame
{
    public class HitVisual : MonoBehaviour
    {
        [SerializeField] private Toggle prefab;
        private List<Toggle> toggles = new List<Toggle>();
        private void OnEnable()
        {
            EventManager.InitializeMinigame += Initialize;
            EventManager.SpinnerHit += UpdateVisuals;
        }
        private void OnDisable()
        {
            EventManager.InitializeMinigame -= Initialize;
            EventManager.SpinnerHit -= UpdateVisuals;
        }
        public void Initialize(int amount)
        {
            ClearToggles();
            for (int i = 0; i < amount; i++) 
            {
                Toggle t = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform); 
                toggles.Add(t);
            }
        }
        private void ClearToggles()
        {
            for (int i = 0; i < toggles.Count; i++)
            {
                Destroy(toggles[i].gameObject);
            }
            toggles.Clear();
        }
        public void UpdateVisuals(int amount)
        {
            toggles[amount].isOn = true;
        }
    }
}
