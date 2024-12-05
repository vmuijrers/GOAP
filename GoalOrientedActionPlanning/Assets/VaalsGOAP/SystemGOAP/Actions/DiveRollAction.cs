using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VaalsGOAP
{
    public class DiveRollAction : Action
    {
        public Obstacle target;
        public float offSetFromObstacle = 5f;
        private float timeInThisAction = 0;
        private Vector3 endPos;
        public override void Initialize(GOAPAgent actor)
        {
            this.actor = actor;
            cost = 1;
            conditions = new List<EffectState>() { new EffectState(Effect.IsInCover, false) , new EffectState(Effect.NearCover, true) };
            effects = new List<EffectState>() { new EffectState(Effect.IsInCover, true) };
        }

        public override bool IsViable()
        {
            if(target != null)
            {
                return true;
            }

            target = ObjectFinder.FindNearestObjectByType<Obstacle>(actor.gameObject, actor.sightRange);

            return target != null;
        }

        public override void OnEnterAction()
        {
            target = ObjectFinder.FindNearestObjectByType<Obstacle>(actor.gameObject, actor.sightRange);
            timeInThisAction = 0;
            actor.agent.stoppingDistance = 5.5f;
            
            if (target != null)
            {
                endPos = target.transform.position - (target.transform.position - actor.transform.position).normalized * 1f;
                GameObject Visual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Visual.transform.position = endPos;
                actor.agent.SetDestination(endPos);
            }
            actor.transform.LookAt(endPos);
            actor.animController.FadeToState("DiveRoll", 0.1f);
        }

        public override void OnUpdateAction()
        {
            timeInThisAction += Time.deltaTime;
            if (target != null)
            {

                if (timeInThisAction >= actor.animController.anim.GetCurrentAnimatorStateInfo(0).length)
                {
                    OnActionCompleted();
                }

            }
            else
            {
                OnActionFailed();
            }
        }
        public override void OnExitAction()
        {
            actor.animController.FadeToState("Idle", 0f); //this is an ugly hack because of the weird diveRoll animation not updating the position
            actor.transform.position = (endPos);
        }
        public override void OnActionCompleted()
        {
            Debug.Log("DiveRoll Action Completed!");
            actor.OnActionCompleted(this);
        }

        public override void OnActionFailed()
        {
            Debug.Log("DiveRoll Action Failed!");
            actor.OnActionFailed(this);
        }

    }
}

