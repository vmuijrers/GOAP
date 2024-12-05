using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace VaalsGOAP
{
    public class MoveToWeaponAction : Action
    {
        public Weapon targetWeapon;
        public override void Initialize(GOAPAgent actor)
        {
            //Debug.Log(actor);
            this.actor = actor;
            cost = 2;
            conditions = new List<EffectState>() { new EffectState(Effect.NearWeapon, false) };
            effects = new List<EffectState>() { new EffectState(Effect.NearWeapon, true) };
        }

        public override bool IsViable()
        {

            if (targetWeapon != null)
            {
                if (!targetWeapon.isPickedUp)
                { return true; }
                else
                {
                    targetWeapon = null;
                }
            }

            targetWeapon = ObjectFinder.FindNearestObjectByType<Weapon>(actor.gameObject, actor.sightRange,
                new OptionalParameter("isPickedUp", false),
                new OptionalParameter("isMarkedForPickup", false));
            Debug.Log("Weapon Found: " + targetWeapon);
            if(targetWeapon != null)
            {
                targetWeapon.isMarkedForPickup = true;
            }

            return targetWeapon != null;
        }

        public override void OnEnterAction()
        {
            //base.OnEnterAction();
            actor.agent.speed = 3.5f;
            actor.agent.stoppingDistance = 1.5f;
            actor.animController.FadeToState("Run", 0.1f);
        }

        public override void OnUpdateAction()
        {
            if(targetWeapon != null)
            {
                if(actor.agent.pathEndPosition != targetWeapon.transform.position)
                {
                    actor.agent.SetDestination(targetWeapon.transform.position);
                }
                
                if(Vector3.Distance(actor.transform.position, targetWeapon.transform.position) <= actor.agent.stoppingDistance)
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
            //base.OnExitAction();
            //actor.anim.CrossFade("Idle", 0.1f);
        }
        public override void OnActionCompleted()
        {
            //base.OnActionCompleted();
            targetWeapon.isMarkedForPickup = false;
            actor.OnActionCompleted(this);
        }

        public override void OnActionFailed()
        {
            //base.OnActionFailed();
            actor.OnActionFailed(this);
        }


    }
}

