using System.Net.Http.Headers;
using UnityEngine;
[CreateAssetMenu(menuName = "Fish/New fish type", fileName = "FishType")]
public class FishType : ScriptableObject
{
    [Header("Name")]
    public string fishName;

    [Header("Stats")]
    [Tooltip("How rare is this fish?")]public EFishType type;
    public int chipCount;

    [Header("Difficulty")]
    [Tooltip("How big is the area you can land in?")][Range(0, 360)] public float chance;
    [Tooltip("How many spins to catch?")][Range(1, 5)]public int hitAmnt;

    [Header("Visual")]
    public Sprite fishSprite;
    public Sprite fishUnknownSprite;
}
