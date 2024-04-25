using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace Fishing
{
    public class MiniGameInstance : MonoBehaviour
    {
        public static MiniGameInstance instance;

        [Header("Refrences")]
        [SerializeField] private Radial mainRadial;
        [SerializeField] private Radial smallRadial;
        [SerializeField] private Radial radialPrefab;
        [SerializeField] private Image spriteHolder;

        [Header("Radial Settings")]
        [Range(1, 50)]
        [SerializeField] private float smallRadialPrc;

        [Header("Spinner")]
        [SerializeField] private GameObject spinner;
        public float spinnerDegrees;

        [Header("State")]
        [SerializeField] private FishType currentFish;
        private bool spinning;

        private void Awake()
        {
            FishingMiniGameManager.instance.OnFishMinigame += (FishType fish) => Initialize(fish);
            instance = this;
        }
        public void Initialize(FishType fish)
        {
            Debug.Log(" je dikke homo vader");
            currentFish = fish;
            // main radial
            mainRadial.Initialize(fish.chance, new Color(0, 0, 0));

            // small radial
            smallRadial.Initialize(fish.chance / 100 * smallRadialPrc, new Color(255, 0, 0));

            // sprite
            spriteHolder.sprite = fish.fishSprite;

            spinning = true;
        }
        private void Update()
        {
            // check hit
            if (spinning && Input.GetMouseButtonDown(0))
            {
                float[] radial = new float[] { currentFish.chance, currentFish.chance / 100 * smallRadialPrc };
                ESkillCheckType type = SkillCheck(radial, spinner.transform.eulerAngles.z);

                switch (type)
                {
                    case ESkillCheckType.Miss:
                        FishingMiniGameManager.instance.ContinueFishing(false);
                        break;
                    case ESkillCheckType.HitMain:
                        FishingMiniGameManager.instance.ContinueFishing(true);
                        spinning = false;
                        break;
                    case ESkillCheckType.HitSmall:
                        FishingMiniGameManager.instance.ContinueFishing(true);
                        spinning = false;
                        break;
                    default:
                        break;
                }
            }
        }
        private ESkillCheckType SkillCheck(float[] radialDegrees, float degrees)
        {
            if (degrees < radialDegrees[0])
            {
                if (degrees < radialDegrees[1]) return ESkillCheckType.HitSmall;
                return ESkillCheckType.HitMain;
            }
            return ESkillCheckType.Miss;
        }
        private void FixedUpdate()
        {
           if (spinning) spinner.transform.Rotate(Vector3.forward * spinnerDegrees * Time.deltaTime);
        }
    }
}

