using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "Action_Attack", menuName = "Action/Attack")]
    public class Action_Attack : Action
    {
        public Variable_Float damageStat;
        public Variable_Float hitPointStatToDealDamageTo;
        public override IEnumerator DoAction(Agent agent)
        {
            //Debug.Log("Attacking Action Started!");
            yield return new WaitForSeconds(2f);
            agent.TakeDamage(damageStat, hitPointStatToDealDamageTo);
            //Debug.Log("Attacking Action Stopped!");
        }

    }
}
