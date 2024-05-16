using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class Radial : MonoBehaviour
{
    public Image img;
    private PolygonCollider2D col;
    private RectTransform rectTransform;
    [SerializeField] private float segments;
    public void Initialize(float degrees)
    {
        img = GetComponent<Image>();
        col = GetComponent<PolygonCollider2D>();
        rectTransform = GetComponent<RectTransform>();

        // img
        img.fillAmount = 1f / 360f * degrees;

        // collider
        float fillAmount = img.fillAmount;
        float angleStep = (img.fillAmount * 360f) / segments;
        List<Vector2> points = new List<Vector2>();

        points.Add(Vector2.zero); // Center of the circle

        float radius = rectTransform.rect.width / 2;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            points.Add(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius);
        }

        col.SetPath(0, points.ToArray());
    }
}
