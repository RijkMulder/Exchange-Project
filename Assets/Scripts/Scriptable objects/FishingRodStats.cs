using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishingRodStats : ScriptableObject
{
    [Header("Base levels")]
    public float baseLuck;
    public int baseFishLevel;

    [Header("Max levels")]
    public int maxLuckLevel;
    public int maxFishLevel;

    [Header("setttings")]
    public float minFishTime;
    public float maxFishTime;
}
