
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

        [Space]
        [Header("Minigame")]
        public int hitAmnt;
        public int skillCheckSpeed;
        public int skillCheckHitDegrees;
        public int skillCheckSmallPrc;
    }
}
