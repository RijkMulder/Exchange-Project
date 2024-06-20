using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;

    private Coroutine currentCoroutine;
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentCoroutine = StartCoroutine(Animation());
    }
    private void OnEnable()
    {
        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(Animation());
    }
    private IEnumerator Animation()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            int rng = Random.Range(1, 3);
            animator.SetInteger("WindType", rng);
            yield return new WaitForSeconds(0.1f);
            animator.SetInteger("WindType", 0);
        }
    }
}
