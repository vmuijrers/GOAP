using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VaalsGOAP
{
    public class FindCoverGoal : Goal
    {
        public override void Initialize(GOAPAgent agent)
        {
            priority = 3;
            this.agent = agent;
            conditions = new List<EffectState> { new EffectState(Effect.IsInCover, false), new EffectState(Effect.Alerted, true) };
            effects = new List<EffectState> { new EffectState(Effect.IsInCover, true) };
        }

        public override bool IsViable(State state)
        {
            return state.CheckIfEffectsArePresent(conditions);
        }
    }
}

