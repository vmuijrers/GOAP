using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VaalsGOAP
{
    public class AttackTargetWithWeaponGoal : Goal
    {

        public override void Initialize(GOAPAgent agent)
        {
            this.agent = agent;
            conditions = new List<EffectState> {new EffectState(Effect.HasBulletsLeft, true), new EffectState(Effect.HasWeapon, true), new EffectState(Effect.SeePlayer, true) };
            effects = new List<EffectState> { new EffectState(Effect.PlayerDead, true) };
        }
        public override bool IsViable(State state)
        {
            return state.CheckIfEffectsArePresent(conditions);
        }
    
    }
}

