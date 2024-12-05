using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{

    public abstract class Action : ScriptableObject {

        public abstract IEnumerator DoAction(Agent agent);
        public static IEnumerator OnActionsCompleted(Agent agent)
        {
            yield return null;
            agent.EvaluateStates();
        }
	}


    






}
