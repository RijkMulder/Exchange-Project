using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Events;

namespace Fishing.Minigame
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
        [SerializeField] private float spinnerDegrees;
        [SerializeField] private float spinnerResetTime;

        [Header("State")]
        [SerializeField] private FishType currentFish;

        // private
        private bool spinning;
        private int hitAmntRemain;
        private void Awake()
        {
            EventManager.FishMiniGameStart += (FishType fish) => Initialize(fish);
            instance = this;
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
                        spinning = false;
                        FishingMiniGameManager.instance.ContinueFishing(false);
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
            if (hitAmntRemain == 0 || type == ESkillCheckType.HitSmall) FishingMiniGameManager.instance.ContinueFishing(true);
            if (gameObject.activeInHierarchy) StartCoroutine(ResetSpinner());
        }
        private IEnumerator ResetSpinner()
        {
            spinning = false;
            float t = 0;
            while (t < spinnerResetTime)
            {
                t += Time.deltaTime;
                float prc = t / spinnerResetTime;
                spinner.transform.Rotate(Vector3.forward * spinnerDegrees * Time.deltaTime, prc);
                yield return null;
            }
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

