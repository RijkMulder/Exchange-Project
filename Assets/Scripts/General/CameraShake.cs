using Events;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private bool start;
    [SerializeField] private AnimationCurve curve;
    private void OnEnable()
    {
        Debug.Log("enabled");
        EventManager.ScreenShake += Initiate;
    }
    private void Initiate(float s, bool r) 
    {
        Debug.Log("Initiating");
        StartCoroutine(Shake(s, r));
    }
    private IEnumerator Shake(float strength, bool repeat)
    {
        Vector3 startPos = transform.position;
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
    }
    private IEnumerator RepeatShake(float strength)
    {
        Vector3 startPos = transform.position;
        while (true)
        {
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
    }
}