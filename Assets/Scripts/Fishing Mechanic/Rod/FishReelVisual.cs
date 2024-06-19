using UnityEngine;
using UnityEngine.UI;
using Events;
using Fishing;
using FishingLine;

public class FishReelVisual : MonoBehaviour
{
    public SpriteRenderer img;
    private FishHook hook;
    private void Update()
    {
        transform.position = hook.transform.position;
        float distance = Vector3.Distance(hook.transform.position, hook.startPos);
        if (distance < 0.1f || FishingRod.instance.state == FishingState.Fishing)
        {
            Destroy(gameObject);
        }
    }
    public void Initialize(FishType fish)
    {
        hook = FishHook.instance;
        transform.position = hook.transform.position;
        img.sprite = fish.fishSprite;
    }
}
