using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fishing;
using Unity.VisualScripting;
namespace FishingLine
{
    [RequireComponent(typeof(LineRenderer), typeof(Animator))]
    [Tooltip("FishLine only controlls the LineRenderer")]
    public class FishLine : MonoBehaviour
    {
        public static FishLine instance;
        public LineRenderer line;
        public Animator animator;

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
            animator = GetComponent<Animator>();
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
        }
        public void CastLine(int type)
        {
            FishingRod.instance.state = FishingState.Fishing;
            animator.SetInteger("Random", type);
        }
        private IEnumerator MoveLineAsync(Vector3 targetPos)
        {
            animator.enabled = false;
            float t = 0;
            Vector3 startPos = line.GetPosition(1);
            while(t < moveBackTime)
            {
                t += Time.deltaTime;
                float prc = t / moveBackTime;
                Vector3 newPos = Vector3.Slerp(startPos, targetPos, prc);
                transform.position = newPos;
                yield return null;
            }
            moveCoroutine = null;
            FishingRod.instance.state = FishingState.Idle;
            animator.enabled = true;
        }
    }
}

