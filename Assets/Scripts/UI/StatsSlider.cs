using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsSlider : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text value;
    public Image slider;

    public void Setup(int amnt, int max)
    {
        value.text = amnt.ToString();

        slider.fillAmount = Mathf.Clamp((float)amnt / (float)max, 0, 1);
    }
}
