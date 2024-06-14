using System;
using System.Net.Http.Headers;
using UnityEngine;
[CreateAssetMenu(menuName = "Rod/New rod upgrade", fileName = "RodUpgrades")]
[Serializable]
public class RodType : ScriptableObject
{
    [Header("Price")]
    [Tooltip("How expensive is this rod?")] public int coins;
    [Header("Stats")]
    [Tooltip("How fast is this rod?")] public float value;
}
