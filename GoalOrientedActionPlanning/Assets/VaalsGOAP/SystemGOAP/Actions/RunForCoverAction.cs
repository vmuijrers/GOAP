using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VaalsGOAP
{
    public class RunForCoverAction : Action
    {
        public Obstacle target;
        public override void Initialize(GOAPAgent actor)
        {
            this.actor = actor;
            cost = 2;
            conditions = new List<EffectState>() {new EffectState(Effect.NearCover, false) };
            effects = new List<EffectState>() { new EffectState(Effect.NearCover, true) };
        }

        public override bool IsViable()
        {
            if (target != null)
            {
                return true;
            }

            target = ObjectFinder.FindNearestObjectByType<Obstacle>(actor.gameObject, actor.sightRange);

            return target != null;
        }

        public override void OnActionCompleted()
        {
            actor.OnActionCompleted(this);
        }

        public override void OnActionFailed()
        {
            actor.OnActionFailed(this);
        }

        public override void OnEnterAction()
        {
            actor.agent.speed = 3.5f;
            actor.agent.stoppingDistance = 5f;
            actor.animController.FadeToState("Run", 0.1f);
        }

        public override void OnExitAction()
        {
            
        }

        public override void OnUpdateAction()
        {
            if (target != null)
            {
                if (actor.agent.pathEndPosition != target.transform.position)
                {
                    actor.agent.SetDestination(target.transform.position);
                }

                if (Vector3.Distance(actor.transform.position, target.transform.position) <= actor.agent.stoppingDistance)
                {
                    OnActionCompleted();

                }
            }
            else
            {
                OnActionFailed();
            }
        }
    }
}

