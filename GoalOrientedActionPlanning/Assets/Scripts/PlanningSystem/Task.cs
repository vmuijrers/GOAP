using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum TaskState { Failed,Success,Running}
public abstract class Task : MonoBehaviour {

    public float stressLevel = 0;
    public AnimationCurve stressLevelCurve;
    public List<EffectState> conditions;

    // Use this for initialization
    void Start () {
        
    }
	public virtual void Init()
    {

    }

    public virtual float EvaluateStressLevel(BaseBeast b)
    {
        if (!enabled) return -1;
        //base function checks if the conditions are there to perform this task
        if (conditions.Count ==0 )
        {
            return 1;
        }
        else
        {
            foreach(EffectState s in conditions)
            {
                if (!b.stats.GetConditionEffect(s))
                {
                    return -1;
                }
            }
            return 1;
            
        }
    }

    public abstract void Run(BaseBeast b);

}
