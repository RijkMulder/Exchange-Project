using Fishing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoveryPopup : MonoBehaviour
{
    CanvasGroup group;
    [SerializeField] private Image img;
    [SerializeField] private float fadeTime;
    [SerializeField] private float holdTime;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        Initialize(FishingRod.instance.currentFish);
        StartCoroutine(Fade());
    }
    private void Initialize(FishType type)
    {
        img.sprite = type.fishSprite;
    }
    private IEnumerator Fade()
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float prc = t / fadeTime;
            group.alpha = Mathf.Lerp(0, 1, prc);
            yield return null;
        }
        yield return new WaitForSeconds(holdTime);

        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float prc = t / fadeTime;
            group.alpha = Mathf.Lerp(1, 0, prc);
            yield return null;
        }
        Destroy(gameObject);
    }
}
