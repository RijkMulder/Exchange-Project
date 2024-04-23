using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fishing
{
    public class MiniGameInstance : MonoBehaviour
    {
        public static MiniGameInstance instance;

        [SerializeField] private Radial radialPrefab;
        [Tooltip("The amount of space the small radial takes up of the big radial.")][Range(1, 50)][SerializeField] private float smallRadialPrc;

        private Radial mainRadial;
        private Radial smallRadial;
        private void Awake()
        {
            instance = this;
        }
        public void Initialize(FishType fish)
        {
            Clear();
            // main radial
            mainRadial = Instantiate(radialPrefab, FishingMiniGameManager.instance.transform.position, Quaternion.identity, transform);
            mainRadial.Initialize(fish.chance, new Color(0, 0, 0));

            // small radial
            smallRadial = Instantiate(radialPrefab, FishingMiniGameManager.instance.transform.position, Quaternion.identity, transform);
            smallRadial.Initialize(fish.chance / 100 * smallRadialPrc, new Color(255, 0, 0));

        }
        private void Clear()
        {
            Destroy(mainRadial);
            Destroy(smallRadial);
        }
    }
}

