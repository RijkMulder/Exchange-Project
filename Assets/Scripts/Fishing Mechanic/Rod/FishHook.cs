using System.Collections;
using UnityEngine;
using Fishing;
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
            if (gameObject.activeInHierarchy)moveCoroutine = StartCoroutine(MoveLineAsync(startPos));
            if (FishingRod.instance)FishingRod.instance.ChangeState(FishingState.Idle);
            animator.SetTrigger("Catch");
        }
        public void CastLine()
        {
            // check if hitting water
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if ( hit.transform && !hit.transform.GetComponent<WaterTag>()) return;

            // go fishing state
            FishingRod.instance.ChangeState(FishingState.Fishing);

            // move to position
            Vector3 targetPos = hit.point;
            Vector3 direction = targetPos - FishingRod.instance.transform.position;
            float distance = direction.magnitude;

            if (distance > maxHookDistance)
            {
                direction.Normalize();
                targetPos = FishingRod.instance.transform.position + direction * maxHookDistance;
            }
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveLineAsync(targetPos));
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

