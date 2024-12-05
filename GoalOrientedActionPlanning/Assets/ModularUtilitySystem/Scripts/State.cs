using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySystem
{
    [CreateAssetMenu(fileName = "State_default",menuName = "States/DefaultState")]
	public class State : ScriptableObject{

        public int priority;
        public Condition[] allConditions;
        public Action[] allActions;
	
        public bool EvaluateState(Stats stats)
        {
            foreach(Condition c in allConditions)
            {
                if (!c.EvaluateCondition(stats)) return false;
            }
            return true;
        }

        public void ExecuteActions(Agent agent)
        {
            Debug.Log("Executing: " + name + "!");
            List<IEnumerator> allRoutines = new List<IEnumerator>();
            foreach(Action ac in allActions)
            {
                allRoutines.Add(ac.DoAction(agent));
            }
            allRoutines.Add(Action.OnActionsCompleted(agent));
            agent.StartCoroutine(Util.Sequence(allRoutines.ToArray()));
        }


    }

    public class StateComparer : IComparer<State>
    {
        public int Compare(State x, State y)
        {
            return x.priority < y.priority ? 1 : x.priority == y.priority ? 0 : -1;
        }
    }
}
