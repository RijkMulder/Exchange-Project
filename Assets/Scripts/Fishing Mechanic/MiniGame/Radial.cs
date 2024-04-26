using UnityEngine;
using UnityEngine.UI;

public class Radial : MonoBehaviour
{
    public Image img;
    public void Initialize(float degrees)
    {
        img = GetComponent<Image>();
        img.fillAmount = 1f / 360f * degrees;
    }
}
