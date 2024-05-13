using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gambling
{
    public class GamblingManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.K))
            {
                QuitGambling();
            }
        }
        public void StartGamblingDay()
        {
            LoadScene("SlotMachine");
        }
        private void QuitGambling()
        {
            EventManager.OnDayStart(8);
            LoadScene("RijkFishingMechanic");
        }
        private void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}

