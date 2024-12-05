using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HideInTreeTask : Task {

    public override void Init()
    {
        //conditions |= Effects.EnemyNear;
        //conditions.Add(new EffectState(Effects.EnemyNear, true));
    }
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
        print("Running Task: "+this);
        TreeComponent closestTree = (TreeComponent)Executioner.GetClosestGameObjectOfType<TreeComponent>(gameObject, true);


        List<IEnumerator> actionList = new List<IEnumerator>();


        actionList.Add(Actions.MoveToPosition(caller, closestTree.transform.position, caller.stats.moveSpeed, 1));
        actionList.Add(Actions.GoUpTree(caller, closestTree));
        actionList.Add(Actions.SetEffectValue(caller,Effects.InTree,true));
        actionList.Add(Actions.FinishedTask(caller, this));


        StartCoroutine(Executioner.Sequence(actionList.ToArray()));


    }
}
