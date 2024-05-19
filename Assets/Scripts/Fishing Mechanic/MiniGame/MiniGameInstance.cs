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
        [SerializeField] private Image spriteHolder;
        [SerializeField] private GameObject spinnerTransform;
        [SerializeField] private MiniGameSpinner spinner;

        [Header("Settings")]
        [Range(1, 50)]
        [SerializeField] private float smallRadialPrc;
        [SerializeField] private float spinnerSpeed;
        [SerializeField] private float maxSpeedMultiplier;
        [SerializeField] private KeyCode hitKey;
        
        // private
        private FishType currentFish;
        private int hitAmntRemain;
        private int misses;
        private float speed;

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

            // radials
            mainRadial.Initialize(fish.chance);
            smallRadial.Initialize(fish.chance / 100 * smallRadialPrc);

            // sprite
            spriteHolder.sprite = fish.fishSprite;

            // setup
            misses = 0;
            SetupAreas(fish);
        }
        private void SetupAreas(FishType fish)
        {
            // random rotation
            float amnt = Random.Range(90, 360 - fish.chance);

            // apply rotation
            mainRadial.transform.Rotate(transform.forward * amnt);
            smallRadial.transform.Rotate(transform.forward * amnt);

            // random direction and speed for spinner
            int dir = Random.value < 0.5 ? -1 : 1;
            speed = spinnerSpeed * Random.Range(1, maxSpeedMultiplier) * dir;

        }
        private void Update()
        {
            // check hit
            if (Input.GetKeyDown(hitKey))
            {
                ESkillCheckType type = spinner.type;

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

            if (hitAmntRemain == 0 || type == ESkillCheckType.HitSmall) FishingMiniGameManager.instance.ContinueFishing(true);
            else SetupAreas(currentFish);
        }
        private void Miss()
        {
            if (misses == 0)
            {
                Initialize(currentFish);
                EventManager.OnMiniGameMiss();
                misses++;
                return;
            }
            FishingMiniGameManager.instance.ContinueFishing(false);
        }
        private void FixedUpdate()
        {
           spinnerTransform.transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
        }
    }
}

