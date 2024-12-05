using UnityEngine;
using System.Collections;

public class TestBitScript : MonoBehaviour {

    public Effects conditionEffect;
    public BeastStats stats;
	// Use this for initialization
	void Start () {
        EffectState s = new EffectState(conditionEffect,false);
        //print(stats.IsEffectActive(conditionEffect));
        print(stats.GetConditionEffect(s));
        stats.FlipFlag(Effects.EnemyNear);
        print(Effects.EnemyNear);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
