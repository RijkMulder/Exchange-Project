using System;
using System.Net.Http.Headers;
using UnityEngine;
[CreateAssetMenu(menuName = "Rod/New rod type", fileName = "RodType")]
[Serializable]
public class RodType : ScriptableObject
{
    [Header("Name")]
    [Tooltip("What is the name of this rod?")] public string rodName;

    [Header("Price")]
    [Tooltip("How expensive is this rod?")] public int coins;

    [Header("Stats")]
    [Tooltip("How fast is this rod?")] public float speed;
    [Tooltip("How durable is this rod?")] public float durability;
    [Tooltip("How lucky is this rod?")] public float luck;

    [Header("Visual")]
    [Tooltip("What is the sprite of this rod?")] public Sprite rodSprite;
}
