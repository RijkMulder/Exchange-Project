using UnityEngine;
using UnityEngine.UI;
using Events;
using Fishing;
using FishingLine;

public class FishReelVisual : MonoBehaviour
{
    public SpriteRenderer img;
    private FishHook hook;
    private void Awake()
    {
        hook = FishHook.instance;
    }
    private void Update()
    {
        if (hook.transform.position == hook.startPos)
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        EventManager.FishCaught += Initialize;
    }

    private void OnDisable()
    {
        EventManager.FishCaught -= Initialize;
    }
    private void Initialize(FishType fish)
    {
        transform.position = hook.transform.position;
        img.sprite = fish.fishSprite;
    }
}
