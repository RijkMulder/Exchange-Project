using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Events;
using System;

namespace Fishing.Minigame
{
    public class MiniGameInstance : MonoBehaviour
    {
        public static MiniGameInstance instance;

        [Header("Refrences")]
        [SerializeField] private Radial mainRadial;
        [SerializeField] private Radial smallRadial;
        [SerializeField] private Image spriteHolder;

        [Header("Radial Settings")]
        [Range(1, 50)]
        [SerializeField] private float smallRadialPrc;
        

        [Header("Spinner")]
        [SerializeField] private GameObject spinner;
        [SerializeField] private float spinnerDegrees;
        [SerializeField] private float spinnerResetTime;

        [Header("State")]
        [SerializeField] private FishType currentFish;

        // private
        private bool spinning;
        private int hitAmntRemain;
        private int misses;
        private void Awake()
        {
            instance = this;
        }
        private void OnEnable()
        {
            EventManager.FishMiniGameStart += Initialize;
        }
        private void OnDisable()
        {
            EventManager.FishMiniGameStart -= Initialize;
        }
        public void Initialize(FishType fish)
        {
            EventManager.OnInitializeMinigame(fish.hitAmnt);
            currentFish = fish;
            hitAmntRemain = fish.hitAmnt;
            // main radial
            mainRadial.Initialize(fish.chance);

            // small radial
            smallRadial.Initialize(fish.chance / 100 * smallRadialPrc);

            // sprite
            spriteHolder.sprite = fish.fishSprite;

            // misses
            misses = 0;

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
                        Miss();
                        break;
                    case ESkillCheckType.HitMain:
                        Hit(type);
                        break;
                    case ESkillCheckType.HitSmall:
                        Hit(type);
                        break;
                }
            }
        }
        private void Hit(ESkillCheckType type)
        {
            hitAmntRemain--;
            EventManager.OnSpinnerHit(hitAmntRemain);
            if (hitAmntRemain == 0 || type == ESkillCheckType.HitSmall) 
            { 
                StartCoroutine(ResetSpinner(true));
                misses = 0;
            }
            else if (gameObject.activeInHierarchy) StartCoroutine(ResetSpinner(false));
        }
        private void Miss()
        {
            if (misses == 0)
            {
                Initialize(currentFish);
                StartCoroutine(ResetSpinner(false));
                EventManager.OnMiniGameMiss();
                misses++;
                return;
            }
            spinning = false;
            FishingMiniGameManager.instance.ContinueFishing(false);
        }
        private IEnumerator ResetSpinner(bool lastHit)
        {
            spinning = false;
            float t = 0;
            while (t < spinnerResetTime)
            {
                t += Time.deltaTime;
                float speed = spinnerDegrees * Time.deltaTime;
                spinner.transform.Rotate(Vector3.forward * speed * 3);
                yield return null;
            }
            if (lastHit) FishingMiniGameManager.instance.ContinueFishing(true);
            spinning = true;
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

