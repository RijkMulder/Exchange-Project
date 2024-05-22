using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Events;
using Player.Inventory;

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
        [SerializeField] private float revealTime;
        [SerializeField] private float holdTime;
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
            SetSprite(fish);

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
        private void SetSprite(FishType fish)
        {
            spriteHolder.sprite = fish.fishSprite;
            bool includes = Inventory.instance.inventoryList.Contains(fish);

            spriteHolder.color = includes ? Color.white : Color.black;
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

            if (hitAmntRemain == 0 || type == ESkillCheckType.HitSmall) 
            {
                StartCoroutine(EndCoroutine());
            }
            else SetupAreas(currentFish);
        }
        private IEnumerator EndCoroutine()
        {
            // disable everyting but sprite
            spriteHolder.transform.position = transform.position;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            // color
            if (spriteHolder.color == Color.black)
            {
                float t = 0f;
                while (t < revealTime)
                {
                    t += Time.deltaTime;
                    float prc = t / revealTime;
                    float amnt = Mathf.Lerp(0, 255, prc);
                    spriteHolder.color = new Color(amnt, amnt, amnt, amnt / 255);
                    yield return null;
                }
            }

            // hold
            yield return new WaitForSeconds(holdTime);
            FishingMiniGameManager.instance.ContinueFishing(true);
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

