using System.Collections;
using UnityEngine;
public class Bubble : MonoBehaviour
{
    public Animator animator;

    public void Pop(int time)
    {
        StartCoroutine(PopCoroutine(time));
    }
    private IEnumerator PopCoroutine(int time)
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("Pop");

        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}