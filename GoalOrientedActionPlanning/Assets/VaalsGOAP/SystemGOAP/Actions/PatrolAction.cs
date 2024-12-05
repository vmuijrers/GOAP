using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VaalsGOAP
{
    public class PatrolAction : Action
    {
        public float offSetFromObstacle = 5f;
        private float timeInThisAction = 0;
        private Vector3 endPos;
        public override void Initialize(GOAPAgent actor)
        {
            this.actor = actor;
            cost = 1;
            conditions = new List<EffectState>() { new EffectState(Effect.Alerted, false) , new EffectState(Effect.SeePlayer, false) };
            effects = new List<EffectState>() { new EffectState(Effect.Alerted, true) };
        }

        public override bool IsViable()
        {
            return true;
        }

        public override void OnEnterAction()
        {
            actor.agent.speed = 1f;
            Vector3 rndDir = Random.insideUnitCircle;
            endPos = actor.gameObject.transform.position +new Vector3(rndDir.x,0, rndDir.y).normalized * Random.Range(1,5f);
            timeInThisAction = 0;
            actor.agent.stoppingDistance = 0.1f;
        
            actor.agent.SetDestination(endPos);
            actor.transform.LookAt(endPos);
            actor.animController.FadeToState("Walk", 0.1f);
        }

        public override void OnUpdateAction()
        {
            timeInThisAction += Time.deltaTime;
            if (actor.agent.remainingDistance < actor.agent.stoppingDistance)
            {
                OnActionCompleted();
            }
        }
        public override void OnExitAction()
        {
            
            actor.animController.FadeToState("Idle", 0.1f); //this is an ugly hack because of the weird diveRoll animation not updating the position
        }
        public override void OnActionCompleted()
        {
            actor.state.RemoveEffect(Effect.Alerted);
            actor.transform.position = (endPos);
            Debug.Log("Patrol Action Completed!");
            actor.OnActionCompleted(this);
            
        }

        public override void OnActionFailed()
        {
            Debug.Log("Patrol Action Failed!");
            actor.OnActionFailed(this);
        }

    }
}

