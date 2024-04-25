using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radial : MonoBehaviour
{
    public Image img;
    public void Initialize(float degrees, Color color)
    {
        img = GetComponent<Image>();
        img.fillAmount = 1f / 360f * degrees;
        img.color = color;
    }
}
