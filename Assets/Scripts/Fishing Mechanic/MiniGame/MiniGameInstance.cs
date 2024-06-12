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
        [SerializeField] private HitPetal[] petals;
        [SerializeField] private SpriteRenderer spriteHolder;
        [SerializeField] private GameObject spinnerTransform;
        [SerializeField] private MiniGameSpinner spinner;

        [Header("Settings")]
        [Range(1, 50)]
        [SerializeField] private float smallRadialPrc;
        [SerializeField] private float maxSpeedMultiplier;
        [SerializeField] private float revealTime;
        [SerializeField] private float holdTime;
        [SerializeField] private KeyCode hitKey;
        [SerializeField] private float missScreenShakeStrength;

        // private
        private Rarity currentRarity;
        private FishType currentFish;
        private int hitAmntRemain;
        private int misses;
        private float speed;
        public Coroutine missCoroutine;
        private bool spinning = true;

        private void Awake()
        {
            instance = this;
        }
        private void OnEnable()
        {
            EventManager.FishMiniGameStart += Initialize;
            EventManager.FishMiniGameEnd += Miss;
        }
        private void OnDisable()
        {
            EventManager.FishMiniGameStart -= Initialize;
            EventManager.FishMiniGameEnd -= Miss;
        }
        public void Initialize(FishType fish)
        {
            // get currentfish and rarity
            currentFish = fish;
            Rarity? rarity = null;
            foreach (Rarity item in FishingRod.instance.rarities)
            {
                if (item.rarity == fish.type) rarity = item;
            }
            currentRarity = rarity.Value;

            // set hitAMnt
            hitAmntRemain = rarity.Value.hitAmnt;

            // radials
            mainRadial.Initialize(rarity.Value.skillCheckHitDegrees);
            smallRadial.Initialize((float)rarity.Value.skillCheckHitDegrees / 100f * rarity.Value.skillCheckSmallPrc);

            // sprite
            SetSprite(fish);

            // setup
            SetupAreas(rarity.Value);
            SetupPetals(rarity.Value);
        }
        private void SetupAreas(Rarity rarity)
        {
            // random rotation
            float amnt = Random.Range(90, 360 - rarity.skillCheckHitDegrees);

            // apply rotation
            smallRadial.transform.Rotate(transform.forward * amnt);
            mainRadial.transform.rotation = Quaternion.Euler(smallRadial.transform.localEulerAngles + new Vector3(0, 0, (float)rarity.skillCheckHitDegrees / 100f * rarity.skillCheckSmallPrc));

            // random direction and speed for spinner
            int dir = Random.value < 0.5 ? -1 : 1;
            speed = rarity.skillCheckSpeed * Random.Range(1, maxSpeedMultiplier) * dir;
        }
        private void SetupPetals(Rarity rarity)
        {
            for (int i = 0; i < rarity.hitAmnt; i++)
            {
                petals[i].SetState(petals[i].white);
            }
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
            if (Input.GetKeyDown(hitKey) && missCoroutine == null)
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
            petals[currentRarity.hitAmnt - hitAmntRemain].SetState(petals[0].green);
            hitAmntRemain--;
            EventManager.OnSpinnerHit(hitAmntRemain);

            if (hitAmntRemain == 0 || type == ESkillCheckType.HitSmall)
            {
                spinning = false;
                StartCoroutine(EndCoroutine());
            }
            else
            {
                SetupAreas(currentRarity);
            }
        }
        private void Miss(int amnt = 0)
        {
            if (amnt != 0) misses = 1;
            if (missCoroutine == null)missCoroutine = StartCoroutine(MissCoroutine());
        }
        private void FixedUpdate()
        {
            if (spinning) spinnerTransform.transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
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
        private IEnumerator MissCoroutine()
        {
            EventManager.OnScreenShake(missScreenShakeStrength, false);

            petals[currentRarity.hitAmnt - hitAmntRemain].SetState(petals[0].red);
            yield return new WaitForSeconds(holdTime);
            for (int i = currentRarity.hitAmnt - 1; i > -1; i--)
            {
                petals[i].SetState(petals[i].gray);
                yield return new WaitForSeconds(holdTime);
            }
            yield return new WaitForSeconds(holdTime);

            if (misses == 0)
            {
                misses++;
                Initialize(currentFish);
                missCoroutine = null;
                EventManager.OnMinigameMiss(1);
                yield break;
            }
            FishingMiniGameManager.instance.ContinueFishing(false);
            missCoroutine = null;
        }
    }
}

