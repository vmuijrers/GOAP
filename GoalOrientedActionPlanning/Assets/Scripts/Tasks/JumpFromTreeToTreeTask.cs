using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JumpFromTreeToTreeTask : Task {


    public override float EvaluateStressLevel(BaseBeast caller)
    {
        if (base.EvaluateStressLevel(caller) >= 0)
        {
            stressLevel = 1;
            return stressLevel;
        }
        return -1f;
    }

    public override void Run(BaseBeast caller)
    {
        print("Running Task: " + this);
        TreeComponent closestTree = (TreeComponent)Executioner.GetClosestGameObjectOfType<TreeComponent>(gameObject, true);
        TreeComponent secondClosestTree = (TreeComponent)Executioner.GetClosestGameObjectOfType<TreeComponent>(closestTree.gameObject, true);
        List<IEnumerator> actionList = new List<IEnumerator>();

        actionList.Add(Actions.JumpFromTreeToTree(caller, closestTree, secondClosestTree));
        actionList.Add(Actions.FinishedTask(caller, this));


        caller.StartCoroutine(Executioner.Sequence(actionList.ToArray()));


    }
}
