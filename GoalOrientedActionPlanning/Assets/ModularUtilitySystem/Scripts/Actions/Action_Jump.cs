using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Action_Jump", menuName = "Action/Jump")]
    public class Action_Jump : Action
    {

        public Variable_Float jumpHeight;
        public override IEnumerator DoAction(Agent agent)
        {
            //Debug.Log("Jump Action Started");
            yield return new WaitForSeconds(1f);
            // Debug.Log("Jump Action Stopped");
        }

    }
}
