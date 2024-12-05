using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace VaalsGOAP {
    public abstract class GOAPAgent : MonoBehaviour
    {
        public State state;

        internal List<Goal> allGoals;
        internal List<Action> allActions;

        internal Goal currentGoal;
        internal Action currentAction;


        public float sightRange = 10;
        internal NavMeshAgent agent;
        internal AnimationController animController;

        internal Weapon weapon;


        // Use this for initialization
        protected virtual void Start()
        {
            animController = GetComponent<AnimationController>();
            agent = GetComponent<NavMeshAgent>();
            var rbs = GetComponentsInChildren<Rigidbody>();
            foreach(Rigidbody rb in rbs)
            {
                rb.gameObject.AddComponent<HitHandler>();
            }
 
        }

        protected void InitializeActionsAndGoals() { 

            foreach (Action ac in allActions)
            {
                //Debug.Log("Action Initialized: " + ac);
                ac.Initialize(this);
            }
            foreach (Goal g in allGoals)
            {
                //Debug.Log("Action Initialized: " + g);
                g.Initialize(this);
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (state.CheckIfEffectIsPresent(Effect.IsDead)) { return; }
            ExecuteGoal();

        }

        public void ExecuteGoal()
        {
            if (currentGoal == null)
            {
                ReplanGoal();

            }
            else if (currentAction == null)
            {
                GetNextAction();
            } else if (currentAction.IsViable() && currentGoal.IsViable(state))
            {
                currentAction.OnUpdateAction();
            }
            else
            {
                //Current Goal or Action no longer viable! Replan!
                ReplanGoal();
            }
                
            
        }

        public void OnActionCompleted(Action action)
        {
            //Debug.Log("Action Completed!");
            state.AddEffectsToState(action.effects);
            action.OnExitAction();

            animController.OnStateChanged(state);

            GetNextAction();

        }

        public void OnActionFailed(Action action)
        {
            action.OnExitAction();
            currentAction = null;

        }

        public void GetNextAction()
        {
            if(currentGoal != null && currentGoal.actionStack.Count > 0)
            {
                currentAction = currentGoal.actionStack.Pop();
                currentAction.OnEnterAction();
            }
            else
            {
                //CurrentGoal Completed Succesfully! Replan!
                //Debug.Log("Goal Completed!");
                ReplanGoal();
            }
           

            
        }

        public void ReplanGoal()
        {
            if(currentAction != null)
            {
                currentAction.OnExitAction();
            }
            currentAction = null;
            currentGoal = null;
            Planner.PlanActions(state, allActions, allGoals, out currentGoal);
            
            //Debug
            //if (currentGoal != null)
            //{
            //    //Debug.Log("Goal Found!");
            //    foreach (Action ac in currentGoal.actionStack)
            //    {
            //        //Debug.Log("Actions For Current Goal: " + ac);

            //    }

            //}
            //else
            //{
            //    //Debug.Log("Goal not Found!");
            //}

            
            
        }
    }
}

