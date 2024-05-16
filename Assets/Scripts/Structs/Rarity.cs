
// my name spaces
using System;
using UnityEngine;

namespace Fishing
{
    [Serializable]
    public struct Rarity
    {
        public EFishType rarity;
        public int probability;
        public Color color;
    }
}
