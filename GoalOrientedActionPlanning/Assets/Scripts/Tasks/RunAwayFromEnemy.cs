using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class RunAwayFromEnemy : Task {

    public float runAwayMinDistance = 10;
    public float runAwayMaxDistance = 20;
    public override void Init()
    {
        
    }
    public override float EvaluateStressLevel(BaseBeast b)
    {
        if(base.EvaluateStressLevel(b) >= 0)
        {
            //stressLevel = 1 - b.stats.health;
            return stressLevel;
        }
        return -1;
    }
    public override void Run(BaseBeast caller)
    {
        List<IEnumerator> actionList = new List<IEnumerator>();

        caller.sensor.GetClosestEnemy(caller);
        actionList.Add(Actions.MoveAndLookToPositionRigidBody(caller, caller.transform.position+(caller.transform.position - caller.sensor.closeEnemy.transform.position).normalized * UnityEngine.Random.Range(runAwayMinDistance, runAwayMaxDistance),caller.stats.moveSpeed,caller.stats.rotationSpeed,1));
        actionList.Add(Actions.FinishedTask(caller, this));


        caller.StartCoroutine(Executioner.Sequence(actionList.ToArray()));
    }

}
