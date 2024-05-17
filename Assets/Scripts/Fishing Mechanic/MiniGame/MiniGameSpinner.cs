using Fishing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fishing.Minigame
{
    public class MiniGameSpinner : MonoBehaviour
    {
        public ESkillCheckType type;

        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up * 100000, 100f);
            if (hit.transform && hit.transform.TryGetComponent(out HitArea area)) type = area.skillCheckType;
            else type = ESkillCheckType.Miss;
        }
    }
}

