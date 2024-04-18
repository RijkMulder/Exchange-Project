using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FishingLine
{
    [RequireComponent(typeof(LineRenderer), typeof(Animator))]
    [Tooltip("FishLine only controlls the LineRenderer")]
    public class FishLine : MonoBehaviour
    {
        public static FishLine instance;
        public LineRenderer line;
        public Animator animator;

        [SerializeField] private List<AnimationClip> clips = new List<AnimationClip>();
        [SerializeField] private Transform fishingRodTop;
        [SerializeField] private Transform hook;
        [SerializeField] private float moveBackTime;

        private Coroutine moveCoroutine;
        private void Awake()
        {
            instance = this;
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
            line.SetPosition(1, hook.position);
        }
        public void ResetPos()
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveLineAsync(hook.position));
        }
        public void CastLine()
        {
            int rng = Random.Range(1, 6);
            animator.SetInteger("Random", rng);
            //animator.SetInteger("Random", 0);
        }
        private IEnumerator MoveLineAsync(Vector3 targetPos)
        {
            float t = 0;
            Vector3 startPos = line.GetPosition(1);
            while(t < moveBackTime)
            {
                t += Time.deltaTime;
                float prc = t / moveBackTime;
                line.SetPosition(1, Vector3.Slerp(startPos, targetPos, prc));
                yield return null;
            }
        }

    }
}

