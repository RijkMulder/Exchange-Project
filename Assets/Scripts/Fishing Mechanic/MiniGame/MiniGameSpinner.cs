using Fishing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fishing.Minigame
{
    public class MiniGameSpinner : MonoBehaviour
    {
        public ESkillCheckType type;
        [SerializeField] private GameObject orienter;
        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(orienter.transform.position, -orienter.transform.up * 100000, 100f);
            if (hit.transform && hit.transform.TryGetComponent(out HitArea area)) type = area.skillCheckType;
            else type = ESkillCheckType.Miss;
        }
    }
}

