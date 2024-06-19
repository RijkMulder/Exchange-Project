using Fishing;
using FishingLine;
using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;

public class FishBaitVisual : MonoBehaviour
{
    [SerializeField] private Vector2 minOffset;
    [SerializeField] private Vector2 maxOffset;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float hitTime;
    [SerializeField] private float minDistance;
    [SerializeField] private float rotationOffset;
    [Range(1, 100)]
    [SerializeField] private float fakeChance;

    private Vector3 target;
    private Coroutine isNearCoroutine;
    public void Initialize()
    {
        target = FishHook.instance.transform.position;
        transform.position = target + new Vector3() 
        { x = Random.Range (minOffset.x, maxOffset.x),
          y = Random.Range(minOffset.y, maxOffset.y), 
          z = -1
        };
    }
    private void Update()
    {
        if (target == Vector3.zero) return;
        if (FishingRod.instance.state == FishingState.Idle) Destroy(gameObject);
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        Vector2 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - rotationOffset, Vector3.forward);

        if (Vector3.Distance(transform.position, target) <= minDistance)
        {
            if (isNearCoroutine == null) isNearCoroutine = StartCoroutine(IsNearHook());
        }
    }
    private IEnumerator IsNearHook()
    {
        float rng = Random.value;
        if (rng * 100 <= fakeChance) target = new Vector2(0, 10);
        else FishingRod.instance.caught = true;

        yield return new WaitForSeconds(hitTime);
        FishingRod.instance.caught = false;
        Destroy(gameObject);

        isNearCoroutine = null;
    }
}
