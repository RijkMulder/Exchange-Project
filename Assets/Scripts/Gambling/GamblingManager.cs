using Events;
using System.Collections;
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
            StartCoroutine(StartDay());
        }
        private IEnumerator StartDay()
        {
            LoadScene("SlotMachine");
            yield return new WaitForSeconds(.1f);
            SlotMachineScript.instance.GetChips();
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

