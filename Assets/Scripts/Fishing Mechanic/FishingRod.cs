using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private Vector2 downPos;
    [SerializeField] private Vector2 upPos;
    private Vector2 originPos;
    private void Start()
    {
        originPos = transform.GetChild(0).transform.position;

        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.SetPosition(0, originPos);
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(0) + new Vector3(0, downPos.y, 0));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            lineRenderer.SetPosition(1, Vector2.Lerp(originPos + downPos, originPos + upPos, 0.00001f * Time.deltaTime));
        }
        if (Input.GetMouseButton(1))
        {
            lineRenderer.SetPosition(1, Vector2.Lerp(originPos + upPos, originPos + downPos, 1 * Time.deltaTime));
        }
    }
}
