using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ChargeAttackTask : Task
{
    public float chargeSpeed;
    public float chargeDistance;
    public float chargeCooldown = 5;
    public override void Init()
    {

    }
    public override float EvaluateStressLevel(BaseBeast b)
    {
        if (base.EvaluateStressLevel(b) >= 0)
        {
            stressLevel = 1;
            return stressLevel;// Global.Remap(Global.DistanceToObject(b.gameObject,b.sensor.closeEnemy), 0,b.sensor.senseRange,0,1);
            
        }
        return -1;
    }
    public override void Run(BaseBeast caller)
    {
        List<IEnumerator> actionList = new List<IEnumerator>();
        actionList.Add(Actions.LookAtObject(caller, caller.sensor.closeEnemy.gameObject, caller.stats.rotationSpeed));
        actionList.Add(Actions.Wait(1));
        actionList.Add(Actions.ChargeAttack(caller, chargeSpeed, chargeDistance));
        actionList.Add(Actions.StartCooldownEffect(caller, this, Effects.ChargeAbilityCooldown, chargeCooldown));
        actionList.Add(Actions.FinishedTask(caller, this));
        //actionList.Add(Executioner.Parallel(caller,ParallelFinishType.AllDone,Actions.StartCooldownEffect(caller,this,Effects.ChargeAbilityCooldown, chargeCooldown),Actions.FinishedTask(caller, this)));
        //TODO fix cooldownAction is stopped because of next task (all coroutines stopped in beast)

        caller.StartCoroutine(Executioner.Sequence(actionList.ToArray()));
    }

}

