using Events;
using System.Collections;
using UnityEngine;

namespace Gambling
{
    public class GamblingManager : MonoBehaviour
    {
        public static GamblingManager Instance;
        public int coins;
        public int chips;

        public int speedUpgradeIndex;
        public int luckUpgradeIndex;
        private void Awake()
        {
            Instance = this;
        }
        public void StartGamblingDay()
        {
            StartCoroutine(StartGambling());
        }
        private IEnumerator StartGambling()
        {
            yield return new WaitForSeconds(0.1f);
            WindowManager.Instance.ChangeWindow();
            EventManager.OnEndOverview();
        }
        public void QuitGambling()
        {
            WindowManager.Instance.ChangeWindow();
            EventManager.OnDayStart(TimeManager.instance.dayStartTime);
        }
    }
}

