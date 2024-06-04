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
            SlotMachineScript.instance.GetChips();
        }
        public void QuitGambling()
        {
            EventManager.OnDayStart(TimeManager.instance.dayStartTime);
        }
    }
}

