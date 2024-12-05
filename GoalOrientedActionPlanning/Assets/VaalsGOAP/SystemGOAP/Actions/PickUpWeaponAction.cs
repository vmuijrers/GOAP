using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace VaalsGOAP
{
    public class PickupWeaponAction : Action
    {
        public Weapon targetWeapon;
        private float timeInThisAction = 0;
        public override void Initialize(GOAPAgent actor)
        {
            this.actor = actor;
            cost = 2;
            conditions = new List<EffectState>() { new EffectState(Effect.NearWeapon, true) ,new EffectState(Effect.HasWeapon, false) };
            effects = new List<EffectState>() { new EffectState(Effect.HasWeapon, true) };
        }

        public override bool IsViable()
        {

            if (targetWeapon != null && !targetWeapon.isPickedUp)
            {
                return true;
            }
            
            targetWeapon = ObjectFinder.FindNearestObjectByType<Weapon>(actor.gameObject, actor.sightRange,
                new OptionalParameter("isPickedUp", false));

            return targetWeapon != null;
        }

        public override void OnEnterAction()
        {
            //base.OnEnterAction();
            timeInThisAction = 0;
            actor.animController.FadeToState("PickUp", 0.4f);
        }

        public override void OnUpdateAction()
        {
            timeInThisAction += Time.deltaTime;
            if (targetWeapon != null)
            {
                              
                if(timeInThisAction >= actor.animController.anim.GetCurrentAnimatorStateInfo(0).length )
                {
                    OnActionCompleted();
                }
            }
            else
            {
                OnActionFailed();
            }

        }

        public override void OnActionCompleted()
        {
            //base.OnActionCompleted();
            targetWeapon.isPickedUp = true;



            actor.OnActionCompleted(this);
        }

        public override void OnActionFailed()
        {
            //base.OnActionFailed();
            actor.OnActionFailed(this);
        }

        public override void OnExitAction()
        {
            //base.OnExitAction();
            actor.animController.FadeToState("Idle", 0.2f); 
        }
    }
}

