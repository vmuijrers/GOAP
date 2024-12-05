using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace UtilitySystem
{
    public class Agent : MonoBehaviour
    {
        public Stats stats;
        public List<State> allStates;
        private State currentState;

        internal Rigidbody rigidBody;
        internal NavMeshAgent navmeshAgent;
        internal Animator animator;
        // Use this for initialization
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            navmeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            stats.InitState();
            allStates.Sort(new StateComparer());
            EvaluateStates();
            foreach(State s in allStates)
            {
                Debug.Log(s.name);
            }
        }

        // Update is called once per frame
        void Update()
        {
            //if(currentState == null || (currentState != null && !currentState.EvaluateState(stats)))
            //{
            //    EvaluateStates();
            //}
        }

        public void EvaluateStates()
        {
            for(int i=0;i < allStates.Count;i++)
            {
                List<State> currentPriorityStates = new List<State>();

                State curState = allStates[i];
                if (curState.EvaluateState(stats))
                {
                    currentPriorityStates.Add(curState);
                    while (i+1 < allStates.Count && curState.priority == allStates[i + 1].priority )
                    {
                        if(allStates[i + 1].EvaluateState(stats))
                        {
                            currentPriorityStates.Add(allStates[i + 1]);
                        }
                        i++;
                    }

                    if(currentPriorityStates.Count> 0)
                    {
                        currentState = currentPriorityStates[Random.Range(0, currentPriorityStates.Count)];
                        currentState.ExecuteActions(this);
                        break;
                    }
                }
            }

        }


        public void TakeDamage(Variable_Float damageStat, Variable_Float hitPointStat)
        {
            Variable_Float hitPoints = stats.GetStat<Variable_Float>(hitPointStat.name);
            hitPoints.ApplyChange(-damageStat.Value);
            Debug.Log("HitPoints: " + hitPoints.Value);
           
        }
    }
}

