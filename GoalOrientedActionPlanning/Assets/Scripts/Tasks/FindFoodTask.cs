using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FindFoodTask : Task {

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public override float EvaluateStressLevel(BaseBeast caller)
    {
        if (base.EvaluateStressLevel(caller) >=0)
        {
            
            //stressLevel = stressLevelCurve.Evaluate(caller.stats.health); //1 - caller.stats.health;
            return stressLevel;
        }
        return -1;

    }


    public override void Run(BaseBeast caller)
    {
        print("Running Task: " + this);
        //FoodComponent closestFood = (FoodComponent)Executioner.GetClosestGameObjectOfType<FoodComponent>(gameObject, true);
        //TreeComponent closestTree = (TreeComponent)Executioner.GetClosestGameObjectOfType<TreeComponent>(gameObject, true);

        List<IEnumerator> actionsList = new List<IEnumerator>();

        caller.sensor.GetClosestBestFood(caller);
        actionsList.Add(Actions.MoveAndLookToObjectRigidBody(caller, caller.sensor.closestFood.gameObject, caller.stats.moveSpeed, caller.stats.rotationSpeed, 1));
        actionsList.Add(Actions.EatFood(caller, caller.sensor.closestFood));
        actionsList.Add(Actions.FinishedTask(caller, this));

        StartCoroutine(Executioner.Sequence(actionsList.ToArray()));
    }
}
