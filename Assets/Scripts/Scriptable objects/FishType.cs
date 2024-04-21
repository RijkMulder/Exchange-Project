using System.Net.Http.Headers;
using UnityEngine;
[CreateAssetMenu(menuName = "Fish/New fish type", fileName = "FishType")]
public class FishType : ScriptableObject
{
    [Header("Name")]
    public string fishName;

    [Header("Stats")]
    public EFishType type;
    public int chipCount;

    [Header("Visual")]
    public Sprite fishSprite;
}
