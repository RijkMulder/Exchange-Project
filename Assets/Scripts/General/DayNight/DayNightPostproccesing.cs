using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightPostproccesing : MonoBehaviour
{
    Volume volume;
    [SerializeField] private Vector2 exposure;
    [SerializeField] private AnimationCurve xposureCurve;
    [SerializeField] private AnimationCurve colorCurve;
    [ColorUsage(true, true)]
    public Color colour;

    private Color startColor;
    private void Start()
    {
        volume = GetComponent<Volume>();
        if(volume.profile.TryGet(out ColorAdjustments col))
        {
            startColor = col.colorFilter.value;
        }
    }
    private void Update()
    {
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
}
