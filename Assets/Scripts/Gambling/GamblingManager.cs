using Events;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gambling
{
    public class GamblingManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.DayEnd += StartGamblingDay;
        }
        private void OnDisable()
        {
            EventManager.DayEnd -= StartGamblingDay;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) QuitGambling();
        }
        private void StartGamblingDay(int day)
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

