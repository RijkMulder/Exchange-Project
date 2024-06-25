using System.Collections;
using UnityEngine;

public class WindowTransition : MonoBehaviour 
{
    [Header("gameobjs")]
    [SerializeField] private GameObject bubbles;
    [SerializeField] private GameObject water;

    [Header("settings")]
    [SerializeField] private float transitionDuration;
    [SerializeField] private Vector2 bubbleOffset;
    [SerializeField] private Vector3 waterOffset;
    [SerializeField] private float waterTravelDistance;
    [SerializeField] private AnimationCurve waterCurve;
    [Range(0, 1)][SerializeField] private float windowTransitionPoint = 0.7f;

    private GameObject waterInstance;
    public void StartTransition(Window window)
    {
        Vector3 camPos = Camera.main.transform.position;
        Instantiate(bubbles, new Vector3(camPos.x, camPos.y + bubbleOffset.y, camPos.z + bubbleOffset.x), Quaternion.Euler(-90, 0, 0), transform);
        waterInstance = Instantiate(water, new Vector3(camPos.x + waterOffset.x, camPos.y + waterOffset.y, camPos.z + waterOffset.z), Quaternion.identity, transform);
        StartCoroutine(DoTransition(window));
    }
    public IEnumerator DoTransition(Window window)
    {
        // get current window
        Window currentWindow = WindowManager.Instance.currentWindow;

        // audio
        AudioManager.Instance.Play("Bubble2");
        AudioManager.Instance.Play("WaterWave");

        // turn current window UI off 
        currentWindow.windowUI.alpha = 0f;

        // set start values
        float t = 0;
        Vector3 startPos = waterInstance.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 0 + waterTravelDistance, 0);

        bool half = false;
        while (t < transitionDuration)
        {
            t += Time.deltaTime;
            float prc = t / transitionDuration;

            // when to transition window
            if (prc > windowTransitionPoint && !half)
            {
                WindowManager.Instance.ChangeWindow(false, window);
                half = true;
            }

            // lerp water
            waterInstance.transform.position = Vector3.Lerp(startPos, endPos, waterCurve.Evaluate(prc));

            // lerp UI of new window
            float max = windowTransitionPoint * transitionDuration;
            float total = transitionDuration - max;
            float prc2 = (t - max) / total;
            window.windowUI.alpha = Mathf.Lerp(0, 1, prc2);
            yield return null;
        }
        Destroy(gameObject);
    }
}
