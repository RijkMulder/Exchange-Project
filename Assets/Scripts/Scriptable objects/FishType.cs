using System;
using System.Net.Http.Headers;
using UnityEngine;
[CreateAssetMenu(menuName = "Fish/New fish type", fileName = "FishType")]
[Serializable]
public class FishType : ScriptableObject
{
    [Header("Name")]
    public string fishName;

    [Header("Stats")]
    [Tooltip("How rare is this fish?")]public EFishType type;
    public int chipCount;

    [Header("Visual")]
    public Sprite fishSprite;
    public Sprite fishUnknownSprite;

    [Header("Story")]
    public string description;
}
