using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fishing;
using Unity.VisualScripting;
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

        [SerializeField] private Vector2 bounds;
        [SerializeField] private Transform fishingRodTop;
        [SerializeField] private float moveBackTime;

        private Vector3 startPos;
        private Coroutine moveCoroutine;
        private void Awake()
        {
            instance = this;
            startPos = transform.position;
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
            moveCoroutine = StartCoroutine(MoveLineAsync(startPos));
            FishingRod.instance.state = FishingState.Idle;
        }
        public void CastLine()
        {
            FishingRod.instance.state = FishingState.Fishing;

            Vector2 mousePos = Input.mousePosition;
            Vector3 mouseWolrdPos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 origin = transform.position;
            Vector3 targetPos = new Vector3
            {
                x = Mathf.Clamp(mouseWolrdPos.x, origin.x - bounds.x, origin.x + bounds.x),
                y = Mathf.Clamp(mouseWolrdPos.y, origin.y, origin.y + bounds.y),
                z = transform.position.z
            };

            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveLineAsync(targetPos));
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

