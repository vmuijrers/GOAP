using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Action_Charge", menuName = "Action/Charge")]
    public class Action_Charge : Action
    {
        public Variable_Float chargeDuration;
        public override IEnumerator DoAction(Agent agent)
        {
            //Debug.Log("Charging Action Started!");
            yield return new WaitForSeconds(chargeDuration.Value);
            // Debug.Log("Charging Action Stopped!");
        }
    }
}
