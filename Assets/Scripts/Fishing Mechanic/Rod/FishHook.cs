using System.Collections;
using UnityEngine;
using Fishing;
using System.Collections.Generic;

namespace FishingLine
{
    /// <summary>
    /// FishHook only controlls the line renderer of the fishing rod.
    /// </summary>
    [RequireComponent(typeof(LineRenderer), typeof(Animator))]
    public class FishHook : MonoBehaviour
    {
        public static FishHook instance;
        public LineRenderer line;

        [SerializeField] private float maxHookDistance;
        [SerializeField] private Transform fishingRodTop;
        [SerializeField] private float moveBackTime;
        [SerializeField] private Animator animator;

        public Vector3 startPos;
        private Coroutine moveCoroutine;
        private void Awake()
        {
            instance = this;
            startPos = transform.position;
            Debug.Log(startPos);
        }
        private void OnEnable()
        {
            ResetPos();
        }
        private void OnValidate()
        {
            line = GetComponent<LineRenderer>();
        }
        private void Update()
        {
            UpdatePos();
        }
        public void UpdatePos()
        {
            line.SetPosition(0, fishingRodTop.position);
            line.SetPosition(1, transform.position);
        }
        public void ResetPos()
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            if (gameObject.activeInHierarchy) moveCoroutine = StartCoroutine(MoveLineAsync(startPos));
            if (FishingRod.instance) FishingRod.instance.ChangeState(FishingState.Idle);
            animator.ResetTrigger("Cast");
            animator.SetTrigger("Catch");
        }
        public void CastLineAnim()
        {
            FishingRod.instance.ChangeState(FishingState.Fishing);
            animator.ResetTrigger("Catch");
            animator.SetTrigger("Cast");
        }
        private IEnumerator MoveLineAsync(Vector3 targetPos)
        {
            float t = 0;
            Vector3 startPos = line.GetPosition(1);
            while (t < moveBackTime)
            {
                t += Time.deltaTime;
                float prc = t / moveBackTime;
                Vector3 newPos = Vector3.Lerp(startPos, targetPos, prc);
                transform.position = newPos;
                yield return null;
            }
            moveCoroutine = null;
        }
    }
}

