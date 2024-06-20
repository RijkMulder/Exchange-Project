using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightPostproccesing : MonoBehaviour
{
    Volume volume;
    ColorAdjustments startAdjustments;
    [SerializeField] private Vector2 exposure;
    [SerializeField] private AnimationCurve xposureCurve;
    [SerializeField] private AnimationCurve colorCurve;
    [ColorUsage(true, true)]
    public Color colour;

    private Color startColor;
    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out ColorAdjustments color);
        startAdjustments = color;
        if(volume.profile.TryGet(out ColorAdjustments col))
        {
            startColor = col.colorFilter.value;
        }
    }
    private void Update()
    {
        if (TimeManager.instance.span.Hours >= TimeManager.instance.dayEndTime)
        {
            return;
        }
        UpdateVolume();
    }
    public void UpdateVolume()
    {
        if (volume.profile.TryGet(out ColorAdjustments color))
        {
            TimeManager time = TimeManager.instance;
            float totalMins = (time.span.Hours * 60f + time.span.Minutes) - time.dayStartTime * 60f;
            float prc = totalMins / ((time.dayEndTime - time.dayStartTime) * 60);
            color.postExposure.value = Mathf.Lerp(exposure.x, exposure.y, xposureCurve.Evaluate(prc));
            color.colorFilter.value = Color.Lerp(startColor, colour, colorCurve.Evaluate(prc));
        }
    }
    public void ResetVolume()
    {
        volume.profile.TryGet(out ColorAdjustments color);

        color.postExposure.value = 0;
        color.colorFilter.value = startColor;
    }
}
