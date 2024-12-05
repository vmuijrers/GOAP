using UnityEngine;
using System.Collections;

[System.Flags]
public enum Effects
{
    InTree =     (1 << 0),
    GetsHungry = (1 << 1),
    EnemyNear =  (1 << 2),
    ChargeAbilityCooldown = (1 << 3),
    ChargeAbilityActive = ( 1 << 4),

}

[System.Serializable]
public struct EffectState
{
    public EffectState(Effects effect,bool value)
    {
        this.effect = effect;
        this.isActive = value;
    }
    public Effects effect;
    public bool isActive;

}