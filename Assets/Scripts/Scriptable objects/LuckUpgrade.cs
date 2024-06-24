using Fishing;
using UnityEngine;

[CreateAssetMenu]
public class LuckUpgrade : ScriptableObject
{
    public int coins;
    public Rarity[] rarities = new Rarity[6];
}
