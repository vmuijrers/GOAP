using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AttackOtherBeastTask : Task {


    public override float EvaluateStressLevel(BaseBeast caller)
    {
        if (base.EvaluateStressLevel(caller) >= 0)
        {
            
            //stressLevel = 1- caller.stats.health;

            return stressLevel;
        }
        return -1;

    }

    public override void Run(BaseBeast caller)
    {
        print("Running Task: " + this);
        BaseBeast closestBeast = (BaseBeast)Executioner.GetClosestGameObjectOfType<BaseBeast>(gameObject, true);


        List<IEnumerator> actionList = new List<IEnumerator>();
        actionList.Add(Actions.MoveAndLookToObjectRigidBody(caller, closestBeast.gameObject, caller.stats.moveSpeed, caller.stats.moveSpeed, 1f));
        //actionList.Add(Executioner.Parallel(caller, ParallelFinishType.FirstDone,
        //            Actions.MoveToObject(caller, closestBeast.gameObject, caller.stats.moveSpeed, 1f),
        //            Actions.LookAtObject(caller, closestBeast.gameObject, caller.stats.rotationSpeed)));
        actionList.Add(Actions.AttackBeast(caller, closestBeast));
        actionList.Add(Actions.FinishedTask(caller, this));


        caller.StartCoroutine(Executioner.Sequence(actionList.ToArray()));


    }
}
