using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSplash : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DestroyAfterAnim());
    }
    private IEnumerator DestroyAfterAnim()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        Destroy(gameObject);
    }
}
