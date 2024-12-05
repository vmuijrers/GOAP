using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Action_Idle", menuName = "Action/Idle")]
    public class Action_Idle : Action
    {

        public float idleDuration = 5f;
        public override IEnumerator DoAction(Agent agent)
        {
            //Debug.Log("Idle Action Started!");
            yield return new WaitForSeconds(idleDuration);

            //Debug.Log("Idle Action Ended!");
        }
        

    }
}
