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

    private GameObject waterInstance;
    private void Start()
    {
        Vector3 camPos = Camera.main.transform.position;
        Instantiate(bubbles, new Vector3(camPos.x , camPos.y + bubbleOffset.y, camPos.z + bubbleOffset.x), Quaternion.Euler(-90, 0, 0), transform);
        waterInstance = Instantiate(water, new Vector3(camPos.x + waterOffset.x, camPos.y + waterOffset.y, camPos.z + waterOffset.z), Quaternion.identity, transform);
        StartCoroutine(DoTransition());
    }

    public IEnumerator DoTransition()
    {
        float t = 0;
        Vector3 startPos = waterInstance.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 0 + waterTravelDistance, 0);

        bool half = false;
        while (t < transitionDuration)
        {
            t += Time.deltaTime;
            float prc = t / transitionDuration;
            if (prc > 0.7 && !half)
            {
                WindowManager.Instance.ChangeWindow(false);
                half = true;
            }
            waterInstance.transform.position = Vector3.Lerp(startPos, endPos, waterCurve.Evaluate(prc));
            yield return null;
        }
        Destroy(gameObject, 3f);
    }
}
