using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VaalsGOAP;
namespace VaalsGOAP
{
    public class RoamGoal : Goal
    {
        public override void Initialize(GOAPAgent agent)
        {
            this.agent = agent;
            priority = 0;
            conditions = new List<EffectState>() { new EffectState(Effect.Alerted, false), new EffectState(Effect.SeePlayer, false) };
            effects = new List<EffectState>() { new EffectState(Effect.Alerted, true) };
        }

        public override bool IsViable(State state)
        {
            return state.CheckIfEffectsArePresent(conditions);
        }
    }
}