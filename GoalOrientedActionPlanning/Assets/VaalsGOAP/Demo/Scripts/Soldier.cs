using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VaalsGOAP
{
    public class Soldier : GOAPAgent, IRagDoll
    {
        public GameObject RagDollPrefab;
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            allActions = new List<Action>() {
            ScriptableObject.CreateInstance<MoveToWeaponAction>(),
            ScriptableObject.CreateInstance<PickupWeaponAction>(),
            ScriptableObject.CreateInstance<DiveRollAction>(),
            ScriptableObject.CreateInstance<RunForCoverAction>(),
            ScriptableObject.CreateInstance<PatrolAction>()
            };
            allGoals = new List<Goal> {
                ScriptableObject.CreateInstance<FindWeaponGoal>(),
                ScriptableObject.CreateInstance<FindCoverGoal>(),
                ScriptableObject.CreateInstance<RoamGoal>()
            };
            InitializeActionsAndGoals();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        public GameObject SpawnRagDoll()
        {
            GameObject go = Instantiate(RagDollPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
            return go;
        }
    }
}

