using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Sensor  {

    public BaseBeast owner;
    public bool enemyNear = false;
    public BaseBeast closeEnemy;

    public FoodComponent closestFood;
    public float listenDistance = 3;
    public float senseRange = 5;
    public LayerMask enemyLayer;
    public float senseUpdateTime = 1;

    public List<FoodComponent> foods;
    public List<BaseBeast> enemies;
    public List<TreeComponent> trees;

    public void Init()
    {
        foods = new List<FoodComponent>();
        enemies = new List<BaseBeast>();
        trees = new List<TreeComponent>();
    }


    public bool SenseForEnemies(GameObject caller)
    {
        Collider[] enemies = Physics.OverlapSphere(caller.transform.position, senseRange, enemyLayer);
        closeEnemy = null;
        enemyNear = false;
        foreach (Collider c in enemies)
        {
            if (caller.gameObject != c.gameObject)
            {
                if (Vector3.Dot(caller.transform.forward, c.gameObject.transform.position - caller.transform.position) > 0f || Vector3.Distance(caller.transform.position, c.gameObject.transform.position) < listenDistance)
                {
                    if (!c.gameObject.GetComponent<BaseBeast>().stats.dead)
                    {
                        //in Sight
                        closeEnemy = c.gameObject.GetComponent<BaseBeast>();
                        enemyNear = true;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public FoodComponent GetClosestBestFood(BaseBeast food)
    {
        float n = 0;
        FoodComponent ff = null;
        foreach(FoodComponent f in foods)
        {
            if(f.nutrition > n)
            {
                ff = f;
                n = f.nutrition;
            }
        }
        closestFood = ff;
        return ff;

    }

    public BaseBeast GetClosestEnemy(BaseBeast caller)
    {
        if(enemies.Count == 0)
        {
            return null;
        }
        float maxDist = 10000;
        closeEnemy = null;
        foreach (BaseBeast c in enemies)
        {

            
            if (!c.gameObject.GetComponent<BaseBeast>().stats.dead)
            {
                float dist = Vector3.Distance(caller.transform.position, c.transform.position);
                if (dist < maxDist)
                {
                    maxDist = dist;
                    //in Sight
                    closeEnemy = c;
                    enemyNear = true;
                    
                }
            }

            
        }
        return closeEnemy;
    }

    //public void OnTriggerEnter(Collider col)
    //{

    //    if (col.GetComponent<TreeComponent>())
    //    {
    //        trees.Add(col.GetComponent<TreeComponent>());
            
    //    }

    //    if (col.GetComponent<FoodComponent>())
    //    {
    //        foods.Add(col.GetComponent<FoodComponent>());
            
    //    }

    //    if (col.GetComponent<BaseBeast>())
    //    {
    //        if (col.gameObject != owner.gameObject)
    //        {
    //            enemies.Add(col.GetComponent<BaseBeast>());
                
    //            owner.CheckForNewTask();
    //        }
    //    }
    //}
    



    //public void OnTriggerExit(Collider col)
    //{
    //    if (col.GetComponent<TreeComponent>())
    //    {
    //        trees.Remove(col.GetComponent<TreeComponent>());
    //    }

    //    if (col.GetComponent<FoodComponent>())
    //    {
    //        foods.Remove(col.GetComponent<FoodComponent>());
    //    }

    //    if (col.GetComponent<BaseBeast>())
    //    {
    //        if (col.gameObject != owner.gameObject)
    //        {
    //            enemies.Remove(col.GetComponent<BaseBeast>());
    //        }
    //    }

    //}




}
