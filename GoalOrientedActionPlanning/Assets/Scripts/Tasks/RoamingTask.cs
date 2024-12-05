using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RoamingTask : Task
{

    public float roamingDistance = 20;
    public bool stayNearStartPosition = false;
    Vector3 startPos;
    public void Start()
    {
        startPos = transform.position;
    }
    public override void Init()
    {

    }
    public override float EvaluateStressLevel(BaseBeast b)
    {
        if (base.EvaluateStressLevel(b) >= 0)
        {
            return stressLevel;
        }
        return -1;
    }
    public override void Run(BaseBeast caller)
    {
        List<IEnumerator> actionList = new List<IEnumerator>();

        Vector3 roamingVec = new Vector3(Random.Range(-roamingDistance, roamingDistance), 0, Random.Range(-roamingDistance, roamingDistance));
        Vector3 targetPos;
        if (stayNearStartPosition)
        {
            targetPos = startPos + roamingVec;
        }
        else
        {
            targetPos = transform.position + roamingVec;
        }

        actionList.Add(Actions.MoveAndLookToPositionRigidBody(caller, targetPos, caller.stats.moveSpeed, caller.stats.rotationSpeed, 1));
        actionList.Add(Actions.Wait(Random.Range(5f, 10f)));
        actionList.Add(Actions.FinishedTask(caller, this));


        caller.StartCoroutine(Executioner.Sequence(actionList.ToArray()));
    }

}

