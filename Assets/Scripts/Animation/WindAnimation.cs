using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindAnimation : MonoBehaviour
{
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private GameObject[] wind = new GameObject[2];
    private Coroutine currentCoroutine;
    private void OnEnable()
    {
        if (currentCoroutine != null)StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(Animation());
    }
    private IEnumerator Animation()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            int rng = Random.Range(1, 3);
            GameObject go = Instantiate(wind[rng - 1], transform.position, Quaternion.identity, transform);
            Destroy(go, 3f);
        }
    }
}
