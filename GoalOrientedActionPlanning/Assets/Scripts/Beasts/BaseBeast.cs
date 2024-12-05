using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BaseBeast : MonoBehaviour,IDamageable {

    public Task[] tasks;
    public Task currentTask;
    public BeastStats stats;
    public Dictionary<int, List<bool>> parallelListener;
    public int batchNumber = 0; // is used for the parallel listener
    internal Rigidbody rb;
    public Sensor sensor;

    private float senseCooldown = 0;
    // Use this for initialization
    void Start () {
        sensor.owner = this;
        rb = GetComponent<Rigidbody>();
        //stats.currentEffects = new Dictionary<Effects, bool>();
        parallelListener = new Dictionary<int, List<bool>>();
        stats.ResetStats();
        tasks = GetComponents<Task>();
        InitializeTasks();
        CheckForNewTask();

    }
	
	// Update is called once per frame
	void Update () {
        if (stats.dead) return;
        RunPassiveEffects();
        Sense();


	}

    void InitializeTasks()
    {
        foreach(Task t in tasks)
        {
            t.Init();
        }
    }

    public void RunPassiveEffects()
    {
        if (!stats.dead)
        {
            stats.RunEffects();
        }
    }
    public void Sense()
    {
        senseCooldown -= Time.deltaTime;
        if (senseCooldown <= 0)
        {
            senseCooldown = sensor.senseUpdateTime;
            if (sensor.SenseForEnemies(gameObject))
            {
                stats.AddEffect(Effects.EnemyNear);
                CheckForNewTask();

            }
            else
            {
                stats.RemoveEffect(Effects.EnemyNear);
            }
            
        }
    }
    public void OnDead()
    {
        if (currentTask != null)
        {
            ClearParallelListener();
            currentTask.StopAllCoroutines();

        }
        StopAllCoroutines();
    }
    /// <summary>
    /// Get the task with highest stress level
    /// </summary>
    public void CheckForNewTask()
    {

        Task newTask = currentTask;
        float maxVal;
        if (currentTask != null)
        {
            maxVal = currentTask.EvaluateStressLevel(this);
        }
        else
        {

            maxVal = -1000f;

        }
        foreach (Task t in tasks)
        {
            
            if (!t.Equals(newTask))
            {
                float val = t.EvaluateStressLevel(this);
                if (val > maxVal)
                {
                    
                    newTask = t;
                    maxVal = val;
                }
            }
        }



        if (currentTask != null) 
        {
            if (!currentTask.Equals(newTask))
            {
                ClearParallelListener();
                //currentTask.StopAllCoroutines(); //this is where the cooldown Run
                StopAllCoroutines();
                if (maxVal >= 0)
                {
                    currentTask = newTask;
                    currentTask.Run(this);
                }
                else
                {
                    CheckForNewTask();
                }
            }
            

        }
        else
        {
            if (maxVal >= 0)
            {
                currentTask = newTask;
                currentTask.Run(this);
            }
            else
            {
                CheckForNewTask();
            }
        }

        
    }

    public void TakeDamage(BaseBeast attacker, float damage)
    {
        if (!stats.dead) {
            stats.health -= damage;
            if (stats.health <= 0)
            {
                stats.dead = true;
                OnDead();
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, sensor.listenDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, sensor.senseRange);
    }

    public void ClearParallelListener()
    {

        parallelListener.Clear();
        batchNumber = 0;
    }



}
