using Events;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gambling
{
    public class GamblingManager : MonoBehaviour
    {
        public static GamblingManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        public void StartGamblingDay()
        {
            StartCoroutine(StartDay());
        }
        private IEnumerator StartDay()
        {
            LoadScene("SlotMachine");
            yield return new WaitForSeconds(.1f);
            SlotMachineScript.instance.GetChips();
        }
        public void QuitGambling()
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

