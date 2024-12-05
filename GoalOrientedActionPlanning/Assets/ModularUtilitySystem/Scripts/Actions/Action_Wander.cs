using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Action_Wander", menuName = "Action/Wander")]
    public class Action_Wander : Action {

        public override IEnumerator DoAction(Agent agent)
        {
            //Debug.Log("Started Action Wandering");
            yield return new WaitForSeconds(3f);
            //Debug.Log("Stopped Action Wandering");
        }

    }
}
