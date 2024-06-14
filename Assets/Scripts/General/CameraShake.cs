using Events;
using Fishing.Minigame;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private bool start;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float repeatShakeMultiplier;
    [SerializeField] private float shakeWaitTime;
    Vector3 startPos;
    private void Awake()
    {
        startPos = transform.position;
    }
    private void OnEnable()
    {
        transform.position = startPos;
        EventManager.ScreenShake += Initiate;
    }
    private void Initiate(float s, bool r) 
    {
        StartCoroutine(Shake(s, r));
    }
    private IEnumerator Shake(float strength, bool repeat)
    {
        float elapsedTime = 0;
        if (repeat) yield return StartCoroutine(RepeatShake(strength));

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float force = curve.Evaluate(elapsedTime / duration) * strength;
            transform.position = startPos + Random.insideUnitSphere * force;
            yield return null;
        }

        transform.position = startPos;

        yield return new WaitForSeconds(shakeWaitTime);
        StartCoroutine(RepeatShake(strength));
    }
    private IEnumerator RepeatShake(float strength)
    {
        Vector3 startPos = transform.position;
        while (true)
        {
            transform.position = startPos + Random.insideUnitSphere * (strength / repeatShakeMultiplier);
            if (FishingMiniGameManager.instance.transform.childCount == 0)
            {
                transform.position = startPos;
                yield break;
            }
            yield return null;
        }
    }
}