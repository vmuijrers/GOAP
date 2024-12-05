using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VaalsGOAP
{
    public enum Effect
    {
        SeePlayer        = (1 << 0),
        NearPlayer       = (1 << 1),
        NearRangedWeapon = (1 << 2),
        NearMeleeWeapon  = (1 << 3),
        HasMeleeWeapon   = (1 << 4),
        HasRangedWeapon  = (1 << 5),
        SeesWeapon       = (1 << 6),
        IsDamaged        = (1 << 7),
        HasWeapon        = (1 << 8),
        NearWeapon       = (1 << 9),
        PlayerDead       = (1 << 10),
        HasLowHealth     = (1 << 11),
        IsInCover        = (1 << 12),
        HasBulletsLeft   = (1 << 13),
        NearCover        = (1 << 14),
        Alerted          = (1 << 15),
        IsDead           = (1 << 16)

    }

    [System.Serializable]
    public struct EffectState
    {
        public EffectState(Effect effect, bool value)
        {
            this.effect = effect;
            this.isActive = value;
        }
        public Effect effect;
        public bool isActive;

    }
}

