using Events;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeUIElement : MonoBehaviour
{
    private protected Image img;
    private protected TMP_Text text;
    [SerializeField] private float fadeTime;
    private void Awake()
    {
        img = GetComponent<Image>();
        text = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        EventManager.DayEnd += DayEnd;
    }
    private void OnDisable()
    {
        EventManager.DayEnd -= DayEnd;
    }
    private void DayEnd(int day)
    {
        StartCoroutine(FadeImg());
    }
    private protected IEnumerator FadeImg()
    {
        float t = 0f;
        Color color = img ? img.color : text.color;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float prc = t / fadeTime;
            Color targetColor = new Color(color.r, color.g, color.b, prc);
            if (img) img.color = targetColor;
            else text.color = targetColor;
            yield return null;
        }

    }
}
