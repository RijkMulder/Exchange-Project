using UnityEngine;
using Events;
using System.Collections;
using UI;
using System.Diagnostics.CodeAnalysis;

public class DayEndManager : MonoBehaviour
{
    [SerializeField] private float fadeTime;

    private CanvasGroup canvasGroup;
    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        EventManager.DayEnd += Fade;
        EventManager.EndOverview += TurnOff;
    }
    private void OnDisable()
    {
        EventManager.DayEnd -= Fade;
        EventManager.EndOverview -= TurnOff;
    }
    private void Fade(int day)
    {
        InventoryUI.instance.Initialize();
        StartCoroutine(StartFade());
    }
    private void TurnOff()
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
    }
    private IEnumerator StartFade()
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float prc = t / fadeTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, prc);
            yield return null;
        }
        canvasGroup.interactable = true;
    }
}
